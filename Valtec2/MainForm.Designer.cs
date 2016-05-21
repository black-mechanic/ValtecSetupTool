using System.Windows.Forms;
using System.Drawing;


namespace Valtec2
{
    partial class MainForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnAddCounter = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSaveCommonTable = new System.Windows.Forms.Button();
            this.dgvTCounters = new System.Windows.Forms.DataGridView();
            this.cbRs232Port = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbSelectedLine = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTCounters)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAddCounter
            // 
            this.btnAddCounter.Location = new System.Drawing.Point(12, 17);
            this.btnAddCounter.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAddCounter.Name = "btnAddCounter";
            this.btnAddCounter.Size = new System.Drawing.Size(183, 82);
            this.btnAddCounter.TabIndex = 1;
            this.btnAddCounter.Text = "Добавить";
            this.btnAddCounter.UseVisualStyleBackColor = true;
            this.btnAddCounter.Click += new System.EventHandler(this.btnAddCounter_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(12, 558);
            this.btnExit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(183, 82);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "Выход";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSaveCommonTable
            // 
            this.btnSaveCommonTable.Location = new System.Drawing.Point(12, 130);
            this.btnSaveCommonTable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSaveCommonTable.Name = "btnSaveCommonTable";
            this.btnSaveCommonTable.Size = new System.Drawing.Size(183, 82);
            this.btnSaveCommonTable.TabIndex = 3;
            this.btnSaveCommonTable.Text = "Сохранить";
            this.btnSaveCommonTable.UseVisualStyleBackColor = true;
            this.btnSaveCommonTable.Visible = false;
            this.btnSaveCommonTable.Click += new System.EventHandler(this.btnSaveCommonTable_Click);
            // 
            // dgvTCounters
            // 
            this.dgvTCounters.AllowUserToAddRows = false;
            this.dgvTCounters.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Moccasin;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dgvTCounters.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvTCounters.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dgvTCounters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTCounters.EnableHeadersVisualStyles = false;
            this.dgvTCounters.Location = new System.Drawing.Point(254, 15);
            this.dgvTCounters.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvTCounters.Name = "dgvTCounters";
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dgvTCounters.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvTCounters.Size = new System.Drawing.Size(556, 733);
            this.dgvTCounters.TabIndex = 4;
            // 
            // cbRs232Port
            // 
            this.cbRs232Port.FormattingEnabled = true;
            this.cbRs232Port.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9",
            "COM10",
            "COM11",
            "COM12"});
            this.cbRs232Port.Location = new System.Drawing.Point(76, 724);
            this.cbRs232Port.Name = "cbRs232Port";
            this.cbRs232Port.Size = new System.Drawing.Size(121, 24);
            this.cbRs232Port.TabIndex = 5;
            this.cbRs232Port.SelectedIndexChanged += new System.EventHandler(this.cbRs232Port_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 727);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "RS Порт";
            // 
            // cbSelectedLine
            // 
            this.cbSelectedLine.FormattingEnabled = true;
            this.cbSelectedLine.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.cbSelectedLine.Location = new System.Drawing.Point(76, 694);
            this.cbSelectedLine.Name = "cbSelectedLine";
            this.cbSelectedLine.Size = new System.Drawing.Size(52, 24);
            this.cbSelectedLine.TabIndex = 7;
            this.cbSelectedLine.SelectedIndexChanged += new System.EventHandler(this.cbSelectedLine_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 697);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "Линия";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(827, 761);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbSelectedLine);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbRs232Port);
            this.Controls.Add(this.dgvTCounters);
            this.Controls.Add(this.btnSaveCommonTable);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnAddCounter);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.Text = "Конфигуратор Valtec";
            ((System.ComponentModel.ISupportInitialize)(this.dgvTCounters)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Button btnAddCounter;
        private Button btnExit;
        private Button btnSaveCommonTable;
        private DataGridView dgvTCounters;
        private ComboBox cbRs232Port;
        private Label label1;
        private ComboBox cbSelectedLine;
        private Label label2;
    }
}

