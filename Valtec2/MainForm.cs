using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO.Ports;

namespace Valtec2
{
    public partial class MainForm : Form
    {
        public struct TCounterParam
        {
            public byte BMBusAddress;
            public int iSerialNumber;
            public short siFlowTemp;
            public int iVolume;
            public int iOnTime;
            public int iKCalPerHour;
            public int iMCal;
        }
        TCounterParam tcntparam;
        static SerialPort rs232Port;
        byte[] cmdFindCounter = new byte[5] { 0x10, 0x40, 0x00, 0x40, 0x16 }; // Запрос (инит) счетчика с адресом 0
        public DataTable dtTCounters = new DataTable("TCounters");
        public DataRow[] foundRows = null;
        public MainForm()
        {
            InitializeComponent(); // Рисуем форму
            CreateTable(ref dtTCounters);
            InitializeGridViewFromFile(); // Заполняем таблицу из файла
            InitialiseRs232Port();
        }

         private void btnAddCounter_Click(object sender, EventArgs e)
        {
            int bytecount = 0;
            byte checksumm = 0;
            int checksummPlace = 0;
            //TCounterParam tcntparam = new TCounterParam();
            byte[] rxmessage = new byte[200]; //{ 0x50, 0x54, 0x41, 0x66, 0x58, 0x38 };
            byte[] cmdString = new byte[5] { 0x10, 0x5b, 0xFE, 0x59, 0x16 };
            rs232Port.PortName = Properties.Settings.Default.rsPort;
            rs232Port.Open();
            rs232Port.Write(cmdString, 0, 5);
            System.Threading.Thread.Sleep(1000);
            bytecount = rs232Port.Read(rxmessage, 0, 180);
            checksumm = countCheckSumm(ref rxmessage, 4, rxmessage[1]);
            rs232Port.Close();
            checksummPlace = rxmessage[1] + 4; // Байт контрольной суммы находится на предпоследнем месте в принятой телеграмме. Соответственно длина (второй байт) плюс смещение не участвующее в подсчете
            if (checksumm != rxmessage[checksummPlace])
            {
                MessageBox.Show("Ошибка общения с прибором. Несовпадение контрольной суммы в принятом пакете. Проверьте подключение и повторите.");
            }
            else
            {
                readTCounterParam(ref rxmessage);
                AddTCounterDialog addFormTCounter = new AddTCounterDialog();
                //addFormTCounter.set_lblResultInfo(string.Join(string.Empty, rxmessage)); //rxmessage.ToString()
                addFormTCounter.tbSerialNumber.Text = tcntparam.iSerialNumber.ToString("X8");//bytecount.ToString();
                addFormTCounter.tbMBusAddr.Text = tcntparam.BMBusAddress.ToString();

                string expression;
                expression = tcntparam.iSerialNumber.ToString("X8");

                // Use the Select method to find all rows matching the filter.
                foundRows = dtTCounters.Select("SerialNumber LIKE '%" + expression + "%'");
                if (foundRows.Length == 0)
                {
                    addFormTCounter.lblResultInfo.ForeColor = Color.Blue;
                    addFormTCounter.lblResultInfo.Text = "Обнаружен новый. Добавляем?";
                    addFormTCounter.bIsTCounterNew = true;
                }
                else
                {
                    addFormTCounter.lblResultInfo.ForeColor = Color.Red;
                    addFormTCounter.lblResultInfo.Text = "Такой S/N уже есть в списке!";
                    addFormTCounter.tbLine.Text = foundRows[0].ItemArray[1].ToString();
                    addFormTCounter.tbRoomNumber.Text = foundRows[0].ItemArray[3].ToString();
                    addFormTCounter.foundRows = foundRows; // Объект не новый, передаем указатель на найденную строку для удаления
                    addFormTCounter.bIsTCounterNew = false;
                }
                addFormTCounter.dt = dtTCounters;
                addFormTCounter.rs232Port = rs232Port;
                addFormTCounter.Show();
                
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //dgvTCounters.DataSource = null;
            //dgvTCounters.Refresh();
        }

        //private void btnFindCounter_Click(object sender, EventArgs e)
        //{
        //    dtTCounters.WriteXml(@"../../Список счетчиков.xml");
        //}
        private void InitializeGridViewFromFile()
        {
            fillDataTableFromXMLFile(ref dtTCounters);
            dgvTCounters.DataSource = dtTCounters;
            // Оформим все красиво
            DataGridViewCellStyle headerStyle = dgvTCounters.ColumnHeadersDefaultCellStyle;
            headerStyle.BackColor = Color.Chocolate;
            headerStyle.ForeColor = Color.White;
            headerStyle.Font = new Font("Arial", 9.75F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)));
            dgvTCounters.Columns[0].HeaderText = "№ п/п";
            dgvTCounters.Columns[1].HeaderText = "№ Линии";
            dgvTCounters.Columns[2].HeaderText = "Зав. Номер";
            dgvTCounters.Columns[3].HeaderText = "Квартира №";
            dgvTCounters.Columns[4].HeaderText = "Адр.на шине";
        }
        private void InitialiseRs232Port()
        {
            cbRs232Port.Text = Properties.Settings.Default.rsPort;
            rs232Port = new SerialPort();
            rs232Port.BaudRate = Properties.Settings.Default.rsSpeed;
            rs232Port.Parity = Parity.None;
            rs232Port.StopBits = StopBits.One;
            rs232Port.DataBits = 8;
            rs232Port.Handshake = Handshake.None;
            rs232Port.RtsEnable = false;
            rs232Port.ReadTimeout = 500;
            rs232Port.WriteTimeout = 500;
        }
        private void fillDataTableFromXMLFile(ref DataTable dt)
        {
            int iLine = Properties.Settings.Default.mbusLine;
            cbSelectedLine.Text = Properties.Settings.Default.mbusLine.ToString();
            XDocument xDoc = null;
            
            try
            {
                switch (iLine)
                {
                    case 0:
                        unionAllFile(ref dt); // Собрать информацию со всех файлов
                        break;
                    case 1:
                        xDoc = XDocument.Load(@"Files/Список счетчиков Линии 1.xml"); //загружаем xml файл
                        break;
                    case 2:
                        xDoc = XDocument.Load(@"Files/Список счетчиков Линии 2.xml"); //загружаем xml файл
                        break;
                    case 3:
                        xDoc = XDocument.Load(@"Files/Список счетчиков Линии 3.xml"); //загружаем xml файл
                        break;
                    case 4:
                        xDoc = XDocument.Load(@"Files/Список счетчиков Линии 4.xml"); //загружаем xml файл
                        break;
                    case 5:
                        xDoc = XDocument.Load(@"Files/Список счетчиков Линии 5.xml"); //загружаем xml файл
                        break;
                    default:
                        MessageBox.Show("Такой линии нет. Линия - " + Properties.Settings.Default.mbusLine.ToString());
                        Properties.Settings.Default.mbusLine = 0;
                        Properties.Settings.Default.Save();
                        Close();
                        break;
                }
                
                if(iLine != 0)
                {  // Формируем таблицу только из счётчиков выбранной линии
                    btnSaveCommonTable.Visible = false;
                    DataRow newRow = null;
                    foreach (XElement element in xDoc.Descendants("TCounters"))
                    {
                        if (int.Parse(element.Element("Line").Value) == iLine)
                        {
                            newRow = dt.NewRow();
                            newRow["Line"] = int.Parse(element.Element("Line").Value);
                            newRow["SerialNumber"] = element.Element("SerialNumber").Value;
                            newRow["RoomNumber"] = int.Parse(element.Element("RoomNumber").Value);
                            newRow["MBusAddress"] = int.Parse(element.Element("MBusAddress").Value);
                            dt.Rows.Add(newRow);
                        }
                    }
                }else
                {
                    btnSaveCommonTable.Visible = true;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //return dt;

        }

        private void CreateTable(ref DataTable dt)
        { // Создаём таблицу с 5 столбцами. Устанавливаем свойства столбцов
            //DataTable dt = new DataTable("TCounters");

            DataColumn colID = new DataColumn("id", typeof(Int32));
            colID.ReadOnly = true;
            colID.Unique = true;
            colID.AllowDBNull = false;
            colID.AutoIncrement = true;
            colID.AutoIncrementSeed = 0;
            colID.AutoIncrementStep = 1;
            DataColumn colLine = new DataColumn("Line", typeof(Int32));
            DataColumn colSerialNumber = new DataColumn("SerialNumber", typeof(string));
            DataColumn colRoomNumber = new DataColumn("RoomNumber", typeof(Int32));
            DataColumn colMBusAddress = new DataColumn("MBusAddress", typeof(Int32));
            //добавляем колонки в таблицу
            dt.Columns.AddRange(new DataColumn[] { colID, colLine, colSerialNumber, colRoomNumber, colMBusAddress });
        }
       
          private void cbRs232Port_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.rsPort = cbRs232Port.SelectedItem.ToString();
            Properties.Settings.Default.Save();
        }

        public void readTCounterParam(ref byte[] rxmsg)
        {
            //Заполнить структуру полученную по broacast адресу FE
            tcntparam.BMBusAddress = rxmsg[5];
            tcntparam.iSerialNumber = rxmsg[7] + (rxmsg[8] * 256) + (rxmsg[9] * 256 * 256) + (rxmsg[10] * 256 * 256 * 256);


        }

        public byte countCheckSumm( ref byte[] rxmsg, int start, int length)
        {
            byte checksum = 0;
            for(int i = start; i < (start+length); i++)
            {
                checksum += rxmsg[i];
            }
            return checksum;
        }

        public void insertCurrentTCounterIntoDataTable()
        {
            MessageBox.Show("Вызов принят");

        }

        private void cbSelectedLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.mbusLine = int.Parse(cbSelectedLine.SelectedItem.ToString());
            Properties.Settings.Default.Save();
            dtTCounters.Clear();
            InitializeGridViewFromFile();
        }

        private void unionAllFile(ref DataTable dt)
        {
            XDocument xDoc;
            DataRow newRow = null;
            for (int i = 1; i < 6; i++)
            {
                xDoc = XDocument.Load(@"Files/Список счетчиков Линии " + i + ".xml"); //загружаем xml файл
                foreach (XElement element in xDoc.Descendants("TCounters"))
                {
                    newRow = dt.NewRow();
                    newRow["Line"] = int.Parse(element.Element("Line").Value);
                    newRow["SerialNumber"] = element.Element("SerialNumber").Value;
                    newRow["RoomNumber"] = int.Parse(element.Element("RoomNumber").Value);
                    newRow["MBusAddress"] = int.Parse(element.Element("MBusAddress").Value);
                    dt.Rows.Add(newRow);
                }
            }
        }

        private void btnSaveCommonTable_Click(object sender, EventArgs e)
        {
            dtTCounters.WriteXml(@"../../Список счетчиков.xml");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
