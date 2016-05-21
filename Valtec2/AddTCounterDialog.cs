using System;
using System.Data;
using System.Windows.Forms;
using System.IO.Ports;

namespace Valtec2
{
    public partial class AddTCounterDialog : Form
    {
        // Указатели на объекты необходимые для помещения новой записи в таблицу, записи адреса в счетчик и записи измененной таблицы в файл
        public DataTable dt = null;
        public DataRow[] foundRows = null;
        public SerialPort rs232Port = null;
        public bool bIsTCounterNew = false;
        public byte[] cmdWriteNewAddress = new byte[12] { 0x68, 0x06, 0x06, 0x68, 0x53, 0xFE, 0x51, 0x01, 0x7A, 0x50, 0x6D, 0x16 };
        public byte[] cmdReadInit = new byte[5] { 0x10, 0x40, 0x21, 0x61, 0x16 };

        public AddTCounterDialog()
        {
            //public DataTable tmpdt = 
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAddCurrentTCounterToList_Click(object sender, EventArgs e)
        {
            bool isRsCommunicationSuccess;
            byte foundedMBusAddress;
            // Выясним новая запись или замена параметров существующей
            if(bIsTCounterNew == false)
            {
                // Удалить запись чтобы потом добавить её как новую в счётчик и файл
                foundRows[0].Delete();
                dt.AcceptChanges();
            }
/* !!! */  // Подобрать ближайший свободный MBus Адрес на выбранной линии
            foundedMBusAddress = findFirstFreeMBusAddressInTable();
            if(foundedMBusAddress == 254)
            {
                MessageBox.Show("Не удалось найти свободный MBus адрес в таблице", "!ВНИМАНИЕ!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
            tbMBusAddr.Text = foundedMBusAddress.ToString();
            // Добавить новую строку в таблицу
            DataRow newRow = dt.NewRow();
            newRow["Line"] = int.Parse(tbLine.Text);
            newRow["SerialNumber"] = tbSerialNumber.Text;
            newRow["RoomNumber"] = int.Parse(tbRoomNumber.Text);
            newRow["MBusAddress"] = int.Parse(tbMBusAddr.Text);
            dt.Rows.Add(newRow);
            //Пишем адрес в счетчик
            cmdWriteNewAddress[9] = byte.Parse(tbMBusAddr.Text); // Вписать новое значение адреса в команду записи адреса
            normalizeCheckSumm(ref cmdWriteNewAddress, 4, cmdWriteNewAddress[1]); // Вписать контрольную сумму
            isRsCommunicationSuccess = writeAddressToTCounter();       // Записать адрес в счётчик
            if(isRsCommunicationSuccess == true)
            {
                //Сохраняем DataTable в файл
                dt.WriteXml(@"../../Список счетчиков.xml");
            }
            else
            {
                MessageBox.Show("Адрес " + tbMBusAddr.Text + " не записался!. \n Линия " + tbLine.Text + " Квартира " + tbRoomNumber.Text, "!ВНИМАНИЕ!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Close();
        }
        public void normalizeCheckSumm(ref byte[] txmsg, int start, int length)
        {
            byte checksum = 0;
            for (int i = start; i < (start + length); i++)
            {
                checksum += txmsg[i];
            }
            txmsg[10] = checksum;
        }

        private bool writeAddressToTCounter()
        {
            int rxResult;
            rs232Port.Open();
            rs232Port.Write(cmdWriteNewAddress, 0, 12);
            System.Threading.Thread.Sleep(700);
            rxResult = rs232Port.ReadByte();
            if (rxResult != 0xE5)
            {
                rs232Port.Close();
                return false;
            }
            else
            {
                cmdReadInit[2] = cmdWriteNewAddress[9]; // Новый адрес
                cmdReadInit[3] = (byte)(cmdReadInit[1] + cmdReadInit[2]); // Контрольная сумма 
                rs232Port.Write(cmdReadInit, 0, 5); // Запрос запрограмированного счетчика по новому адресу
                System.Threading.Thread.Sleep(700);
                rxResult = rs232Port.ReadByte();
                if (rxResult != 0xE5)
                {
                    rs232Port.Close();
                    return false;
                }
                else
                {
                    rs232Port.Close();
                    return true;
                }
            }
        }

        private byte findFirstFreeMBusAddressInTable()
        {
            int freeMBusAddress;
            for (freeMBusAddress = 1; freeMBusAddress < 254; freeMBusAddress++)
            {
                foundRows = dt.Select("MBusAddress = " + freeMBusAddress + "AND Line = " + int.Parse(tbLine.Text));
                if (foundRows.Length == 0) // Означает что найден первый свободный адрес для указанной линии
                    break;
            }
             return (byte)freeMBusAddress;
        }
    }
}
