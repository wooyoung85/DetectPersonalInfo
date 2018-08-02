namespace DetectPesonalInfo
{
    partial class Detect
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
            this.label7 = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnDetect = new System.Windows.Forms.Button();
            this.gb_Info = new System.Windows.Forms.GroupBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtPassWord = new System.Windows.Forms.TextBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDbmsName = new System.Windows.Forms.TextBox();
            this.txtServerIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gv_TableList = new System.Windows.Forms.DataGridView();
            this.gv_TagetTableList = new System.Windows.Forms.DataGridView();
            this.btnTargetInsert = new System.Windows.Forms.Button();
            this.btnConnectChange = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.lblStartTime = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblEndTime = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblRunningTime = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gv_TableList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_TagetTableList)).BeginInit();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1043, 64);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 12);
            this.label7.TabIndex = 33;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(25, 83);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(661, 30);
            this.progressBar.TabIndex = 31;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(553, 152);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(121, 12);
            this.label6.TabIndex = 30;
            this.label6.Text = "개인정보 검출 리스트";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(170, 152);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 12);
            this.label5.TabIndex = 29;
            this.label5.Text = "테이블 리스트";
            // 
            // btnDetect
            // 
            this.btnDetect.Location = new System.Drawing.Point(697, 83);
            this.btnDetect.Name = "btnDetect";
            this.btnDetect.Size = new System.Drawing.Size(103, 30);
            this.btnDetect.TabIndex = 28;
            this.btnDetect.Text = "개인정보 검출";
            this.btnDetect.UseVisualStyleBackColor = true;
            this.btnDetect.Visible = false;
            this.btnDetect.Click += new System.EventHandler(this.btnDetect_Click);
            // 
            // gb_Info
            // 
            this.gb_Info.Location = new System.Drawing.Point(25, 39);
            this.gb_Info.Name = "gb_Info";
            this.gb_Info.Size = new System.Drawing.Size(775, 38);
            this.gb_Info.TabIndex = 27;
            this.gb_Info.TabStop = false;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(657, 11);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 26;
            this.btnConnect.Text = "DB 접속";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtPassWord
            // 
            this.txtPassWord.Location = new System.Drawing.Point(570, 12);
            this.txtPassWord.Name = "txtPassWord";
            this.txtPassWord.Size = new System.Drawing.Size(81, 21);
            this.txtPassWord.TabIndex = 25;
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(390, 12);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(90, 21);
            this.txtID.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(493, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 12);
            this.label4.TabIndex = 23;
            this.label4.Text = "PassWord";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(359, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 12);
            this.label3.TabIndex = 22;
            this.label3.Text = "ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(199, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 12);
            this.label2.TabIndex = 21;
            this.label2.Text = "DBMS 명";
            // 
            // txtDbmsName
            // 
            this.txtDbmsName.Location = new System.Drawing.Point(269, 12);
            this.txtDbmsName.Name = "txtDbmsName";
            this.txtDbmsName.Size = new System.Drawing.Size(76, 21);
            this.txtDbmsName.TabIndex = 20;
            // 
            // txtServerIP
            // 
            this.txtServerIP.Location = new System.Drawing.Point(78, 12);
            this.txtServerIP.Name = "txtServerIP";
            this.txtServerIP.Size = new System.Drawing.Size(109, 21);
            this.txtServerIP.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 12);
            this.label1.TabIndex = 18;
            this.label1.Text = "서버 IP";
            // 
            // gv_TableList
            // 
            this.gv_TableList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gv_TableList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gv_TableList.Location = new System.Drawing.Point(23, 183);
            this.gv_TableList.Name = "gv_TableList";
            this.gv_TableList.RowTemplate.Height = 23;
            this.gv_TableList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gv_TableList.Size = new System.Drawing.Size(368, 395);
            this.gv_TableList.TabIndex = 34;
            // 
            // gv_TagetTableList
            // 
            this.gv_TagetTableList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gv_TagetTableList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gv_TagetTableList.Location = new System.Drawing.Point(435, 183);
            this.gv_TagetTableList.Name = "gv_TagetTableList";
            this.gv_TagetTableList.RowTemplate.Height = 23;
            this.gv_TagetTableList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gv_TagetTableList.Size = new System.Drawing.Size(365, 395);
            this.gv_TagetTableList.TabIndex = 35;
            this.gv_TagetTableList.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gv_TagetTableList_CellContentDoubleClick);
            // 
            // btnTargetInsert
            // 
            this.btnTargetInsert.Location = new System.Drawing.Point(376, 144);
            this.btnTargetInsert.Name = "btnTargetInsert";
            this.btnTargetInsert.Size = new System.Drawing.Size(75, 23);
            this.btnTargetInsert.TabIndex = 36;
            this.btnTargetInsert.Text = "대상 입력";
            this.btnTargetInsert.UseVisualStyleBackColor = true;
            this.btnTargetInsert.Click += new System.EventHandler(this.btnTargetInsert_Click);
            // 
            // btnConnectChange
            // 
            this.btnConnectChange.Location = new System.Drawing.Point(657, 11);
            this.btnConnectChange.Name = "btnConnectChange";
            this.btnConnectChange.Size = new System.Drawing.Size(118, 23);
            this.btnConnectChange.TabIndex = 37;
            this.btnConnectChange.Text = "DB접속정보 초기화";
            this.btnConnectChange.UseVisualStyleBackColor = true;
            this.btnConnectChange.Visible = false;
            this.btnConnectChange.Click += new System.EventHandler(this.btnConnectChange_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(23, 120);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 38;
            this.label8.Text = "시작";
            // 
            // lblStartTime
            // 
            this.lblStartTime.AutoSize = true;
            this.lblStartTime.Location = new System.Drawing.Point(58, 119);
            this.lblStartTime.Name = "lblStartTime";
            this.lblStartTime.Size = new System.Drawing.Size(17, 12);
            this.lblStartTime.TabIndex = 39;
            this.lblStartTime.Text = "...";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(172, 119);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 12);
            this.label10.TabIndex = 40;
            this.label10.Text = "종료";
            // 
            // lblEndTime
            // 
            this.lblEndTime.AutoSize = true;
            this.lblEndTime.Location = new System.Drawing.Point(208, 119);
            this.lblEndTime.Name = "lblEndTime";
            this.lblEndTime.Size = new System.Drawing.Size(17, 12);
            this.lblEndTime.TabIndex = 41;
            this.lblEndTime.Text = "...";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(338, 119);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 42;
            this.label9.Text = "진행시간";
            // 
            // lblRunningTime
            // 
            this.lblRunningTime.AutoSize = true;
            this.lblRunningTime.Location = new System.Drawing.Point(398, 119);
            this.lblRunningTime.Name = "lblRunningTime";
            this.lblRunningTime.Size = new System.Drawing.Size(17, 12);
            this.lblRunningTime.TabIndex = 43;
            this.lblRunningTime.Text = "...";
            // 
            // Detect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 590);
            this.Controls.Add(this.lblRunningTime);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblEndTime);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lblStartTime);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnTargetInsert);
            this.Controls.Add(this.gv_TagetTableList);
            this.Controls.Add(this.gv_TableList);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnDetect);
            this.Controls.Add(this.gb_Info);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.txtPassWord);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDbmsName);
            this.Controls.Add(this.txtServerIP);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnConnectChange);
            this.Name = "Detect";
            this.Text = "Detect";
            ((System.ComponentModel.ISupportInitialize)(this.gv_TableList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_TagetTableList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnDetect;
        private System.Windows.Forms.GroupBox gb_Info;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtPassWord;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDbmsName;
        private System.Windows.Forms.TextBox txtServerIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView gv_TableList;
        private System.Windows.Forms.DataGridView gv_TagetTableList;
        private System.Windows.Forms.Button btnTargetInsert;
        private System.Windows.Forms.Button btnConnectChange;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblStartTime;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblEndTime;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblRunningTime;
    }
}