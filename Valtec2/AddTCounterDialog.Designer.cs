namespace Valtec2
{
    partial class AddTCounterDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbSerialNumber = new System.Windows.Forms.TextBox();
            this.tbLine = new System.Windows.Forms.TextBox();
            this.tbRoomNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbMBusAddr = new System.Windows.Forms.TextBox();
            this.lblResultInfo = new System.Windows.Forms.Label();
            this.btnAddCurrentTCounterToList = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbSerialNumber
            // 
            this.tbSerialNumber.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbSerialNumber.Location = new System.Drawing.Point(144, 15);
            this.tbSerialNumber.Name = "tbSerialNumber";
            this.tbSerialNumber.Size = new System.Drawing.Size(135, 22);
            this.tbSerialNumber.TabIndex = 4;
            // 
            // tbLine
            // 
            this.tbLine.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbLine.Location = new System.Drawing.Point(97, 86);
            this.tbLine.Name = "tbLine";
            this.tbLine.Size = new System.Drawing.Size(100, 22);
            this.tbLine.TabIndex = 0;
            this.tbLine.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbRoomNumber
            // 
            this.tbRoomNumber.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbRoomNumber.Location = new System.Drawing.Point(97, 112);
            this.tbRoomNumber.Name = "tbRoomNumber";
            this.tbRoomNumber.Size = new System.Drawing.Size(100, 22);
            this.tbRoomNumber.TabIndex = 1;
            this.tbRoomNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(11, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Найден прибор №";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(20, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "Линия";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(20, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Квартира";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(12, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "Его MBus Адрес";
            // 
            // tbMBusAddr
            // 
            this.tbMBusAddr.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbMBusAddr.Location = new System.Drawing.Point(144, 42);
            this.tbMBusAddr.Name = "tbMBusAddr";
            this.tbMBusAddr.Size = new System.Drawing.Size(53, 22);
            this.tbMBusAddr.TabIndex = 5;
            this.tbMBusAddr.TabStop = false;
            // 
            // lblResultInfo
            // 
            this.lblResultInfo.AutoSize = true;
            this.lblResultInfo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblResultInfo.ForeColor = System.Drawing.Color.Red;
            this.lblResultInfo.Location = new System.Drawing.Point(20, 146);
            this.lblResultInfo.Name = "lblResultInfo";
            this.lblResultInfo.Size = new System.Drawing.Size(156, 16);
            this.lblResultInfo.TabIndex = 10;
            this.lblResultInfo.Text = "Найден новый прибор";
            this.lblResultInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnAddCurrentTCounterToList
            // 
            this.btnAddCurrentTCounterToList.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAddCurrentTCounterToList.Location = new System.Drawing.Point(23, 177);
            this.btnAddCurrentTCounterToList.Name = "btnAddCurrentTCounterToList";
            this.btnAddCurrentTCounterToList.Size = new System.Drawing.Size(105, 54);
            this.btnAddCurrentTCounterToList.TabIndex = 2;
            this.btnAddCurrentTCounterToList.Text = "Добавить";
            this.btnAddCurrentTCounterToList.UseVisualStyleBackColor = true;
            this.btnAddCurrentTCounterToList.Click += new System.EventHandler(this.btnAddCurrentTCounterToList_Click);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnExit.Location = new System.Drawing.Point(174, 177);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(105, 54);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Выйти";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // AddTCounterDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 246);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnAddCurrentTCounterToList);
            this.Controls.Add(this.lblResultInfo);
            this.Controls.Add(this.tbMBusAddr);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbRoomNumber);
            this.Controls.Add(this.tbLine);
            this.Controls.Add(this.tbSerialNumber);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddTCounterDialog";
            this.Text = "Поиск приборов на шине";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnAddCurrentTCounterToList;
        private System.Windows.Forms.Button btnExit;
        public System.Windows.Forms.TextBox tbSerialNumber;
        public System.Windows.Forms.TextBox tbMBusAddr;
        public System.Windows.Forms.TextBox tbLine;
        public System.Windows.Forms.TextBox tbRoomNumber;
        public System.Windows.Forms.Label lblResultInfo;
    }
}