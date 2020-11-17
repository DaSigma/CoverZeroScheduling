namespace CoverZeroScheduling
{
    partial class Scheduling
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Scheduling));
            this.gbReports = new System.Windows.Forms.GroupBox();
            this.cbConsultant = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpMonth = new System.Windows.Forms.DateTimePicker();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.cbReports = new System.Windows.Forms.ComboBox();
            this.rtbReport = new System.Windows.Forms.RichTextBox();
            this.gbAppointments = new System.Windows.Forms.GroupBox();
            this.lblUpcoming = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnLoadAll = new System.Windows.Forms.Button();
            this.btnApptViewEdit = new System.Windows.Forms.Button();
            this.btnLoadData = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblApptDates = new System.Windows.Forms.Label();
            this.btnAptNew = new System.Windows.Forms.Button();
            this.lblCalender = new System.Windows.Forms.Label();
            this.dgvAppt = new System.Windows.Forms.DataGridView();
            this.dTPFrom = new System.Windows.Forms.DateTimePicker();
            this.rbWeek = new System.Windows.Forms.RadioButton();
            this.rbMonth = new System.Windows.Forms.RadioButton();
            this.btnAptDelete = new System.Windows.Forms.Button();
            this.gbScheduleCustomer = new System.Windows.Forms.GroupBox();
            this.dgvCustomer = new System.Windows.Forms.DataGridView();
            this.btnCustViewEdit = new System.Windows.Forms.Button();
            this.btnCustNew = new System.Windows.Forms.Button();
            this.btnCustDelete = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.gbReports.SuspendLayout();
            this.gbAppointments.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppt)).BeginInit();
            this.gbScheduleCustomer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // gbReports
            // 
            this.gbReports.BackColor = System.Drawing.Color.LightSteelBlue;
            this.gbReports.Controls.Add(this.cbConsultant);
            this.gbReports.Controls.Add(this.label2);
            this.gbReports.Controls.Add(this.dtpMonth);
            this.gbReports.Controls.Add(this.btnGenerate);
            this.gbReports.Controls.Add(this.cbReports);
            this.gbReports.Controls.Add(this.rtbReport);
            this.gbReports.Location = new System.Drawing.Point(613, 138);
            this.gbReports.Name = "gbReports";
            this.gbReports.Size = new System.Drawing.Size(616, 621);
            this.gbReports.TabIndex = 0;
            this.gbReports.TabStop = false;
            this.gbReports.Text = "Reports";
            // 
            // cbConsultant
            // 
            this.cbConsultant.FormattingEnabled = true;
            this.cbConsultant.Location = new System.Drawing.Point(127, 60);
            this.cbConsultant.Name = "cbConsultant";
            this.cbConsultant.Size = new System.Drawing.Size(121, 21);
            this.cbConsultant.TabIndex = 5;
            this.cbConsultant.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "label2";
            this.label2.Visible = false;
            // 
            // dtpMonth
            // 
            this.dtpMonth.CustomFormat = "MMMM";
            this.dtpMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpMonth.Location = new System.Drawing.Point(155, 60);
            this.dtpMonth.Name = "dtpMonth";
            this.dtpMonth.Size = new System.Drawing.Size(93, 20);
            this.dtpMonth.TabIndex = 3;
            this.dtpMonth.Visible = false;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(264, 33);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(173, 21);
            this.btnGenerate.TabIndex = 2;
            this.btnGenerate.Text = "Generate Report";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // cbReports
            // 
            this.cbReports.FormattingEnabled = true;
            this.cbReports.Items.AddRange(new object[] {
            "Appointment Types by Month",
            "Coach\'s Schedule",
            "All Coach\'s Schedule"});
            this.cbReports.Location = new System.Drawing.Point(19, 33);
            this.cbReports.Name = "cbReports";
            this.cbReports.Size = new System.Drawing.Size(229, 21);
            this.cbReports.TabIndex = 1;
            this.cbReports.SelectedIndexChanged += new System.EventHandler(this.cbReports_SelectedIndexChanged);
            // 
            // rtbReport
            // 
            this.rtbReport.Location = new System.Drawing.Point(22, 94);
            this.rtbReport.Name = "rtbReport";
            this.rtbReport.Size = new System.Drawing.Size(577, 497);
            this.rtbReport.TabIndex = 0;
            this.rtbReport.Text = "";
            // 
            // gbAppointments
            // 
            this.gbAppointments.BackColor = System.Drawing.Color.LightSteelBlue;
            this.gbAppointments.Controls.Add(this.lblUpcoming);
            this.gbAppointments.Controls.Add(this.pictureBox2);
            this.gbAppointments.Controls.Add(this.btnLoadAll);
            this.gbAppointments.Controls.Add(this.btnApptViewEdit);
            this.gbAppointments.Controls.Add(this.btnLoadData);
            this.gbAppointments.Controls.Add(this.label1);
            this.gbAppointments.Controls.Add(this.lblApptDates);
            this.gbAppointments.Controls.Add(this.btnAptNew);
            this.gbAppointments.Controls.Add(this.lblCalender);
            this.gbAppointments.Controls.Add(this.dgvAppt);
            this.gbAppointments.Controls.Add(this.dTPFrom);
            this.gbAppointments.Controls.Add(this.rbWeek);
            this.gbAppointments.Controls.Add(this.rbMonth);
            this.gbAppointments.Controls.Add(this.btnAptDelete);
            this.gbAppointments.Location = new System.Drawing.Point(6, 138);
            this.gbAppointments.Name = "gbAppointments";
            this.gbAppointments.Size = new System.Drawing.Size(601, 346);
            this.gbAppointments.TabIndex = 1;
            this.gbAppointments.TabStop = false;
            this.gbAppointments.Text = "Appointments";
            // 
            // lblUpcoming
            // 
            this.lblUpcoming.AutoSize = true;
            this.lblUpcoming.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpcoming.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblUpcoming.Location = new System.Drawing.Point(6, 312);
            this.lblUpcoming.Name = "lblUpcoming";
            this.lblUpcoming.Size = new System.Drawing.Size(184, 20);
            this.lblUpcoming.TabIndex = 16;
            this.lblUpcoming.Text = "Upcoming Appointments";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(507, 16);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(88, 52);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 15;
            this.pictureBox2.TabStop = false;
            // 
            // btnLoadAll
            // 
            this.btnLoadAll.Location = new System.Drawing.Point(373, 45);
            this.btnLoadAll.Name = "btnLoadAll";
            this.btnLoadAll.Size = new System.Drawing.Size(70, 23);
            this.btnLoadAll.TabIndex = 14;
            this.btnLoadAll.Text = "Load All";
            this.btnLoadAll.UseVisualStyleBackColor = true;
            this.btnLoadAll.Click += new System.EventHandler(this.btnLoadAll_Click);
            // 
            // btnApptViewEdit
            // 
            this.btnApptViewEdit.Location = new System.Drawing.Point(368, 309);
            this.btnApptViewEdit.Name = "btnApptViewEdit";
            this.btnApptViewEdit.Size = new System.Drawing.Size(75, 23);
            this.btnApptViewEdit.TabIndex = 13;
            this.btnApptViewEdit.Text = "View/Edit";
            this.btnApptViewEdit.UseVisualStyleBackColor = true;
            this.btnApptViewEdit.Click += new System.EventHandler(this.btnApptView_Click);
            // 
            // btnLoadData
            // 
            this.btnLoadData.Location = new System.Drawing.Point(343, 16);
            this.btnLoadData.Name = "btnLoadData";
            this.btnLoadData.Size = new System.Drawing.Size(100, 23);
            this.btnLoadData.TabIndex = 12;
            this.btnLoadData.Text = "Load Requested";
            this.btnLoadData.UseVisualStyleBackColor = true;
            this.btnLoadData.Click += new System.EventHandler(this.btnLoadData_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Appointments Displayed for: ";
            // 
            // lblApptDates
            // 
            this.lblApptDates.AutoSize = true;
            this.lblApptDates.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApptDates.Location = new System.Drawing.Point(145, 72);
            this.lblApptDates.Name = "lblApptDates";
            this.lblApptDates.Size = new System.Drawing.Size(279, 19);
            this.lblApptDates.TabIndex = 10;
            this.lblApptDates.Text = "All of your Appointments are Displayed";
            // 
            // btnAptNew
            // 
            this.btnAptNew.Location = new System.Drawing.Point(449, 309);
            this.btnAptNew.Name = "btnAptNew";
            this.btnAptNew.Size = new System.Drawing.Size(75, 23);
            this.btnAptNew.TabIndex = 7;
            this.btnAptNew.Text = "New";
            this.btnAptNew.UseVisualStyleBackColor = true;
            this.btnAptNew.Click += new System.EventHandler(this.btnAptNew_Click);
            // 
            // lblCalender
            // 
            this.lblCalender.AutoSize = true;
            this.lblCalender.Location = new System.Drawing.Point(6, 26);
            this.lblCalender.Name = "lblCalender";
            this.lblCalender.Size = new System.Drawing.Size(97, 13);
            this.lblCalender.TabIndex = 6;
            this.lblCalender.Text = "View Appointmens:";
            // 
            // dgvAppt
            // 
            this.dgvAppt.AllowUserToAddRows = false;
            this.dgvAppt.AllowUserToDeleteRows = false;
            this.dgvAppt.AllowUserToResizeRows = false;
            this.dgvAppt.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAppt.BackgroundColor = System.Drawing.Color.White;
            this.dgvAppt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvAppt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAppt.GridColor = System.Drawing.Color.LightSteelBlue;
            this.dgvAppt.Location = new System.Drawing.Point(0, 94);
            this.dgvAppt.Name = "dgvAppt";
            this.dgvAppt.ReadOnly = true;
            this.dgvAppt.RowHeadersVisible = false;
            this.dgvAppt.Size = new System.Drawing.Size(600, 207);
            this.dgvAppt.TabIndex = 5;
            this.dgvAppt.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dgvAppt.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DgvAppt_CellFormatting);
            // 
            // dTPFrom
            // 
            this.dTPFrom.CustomFormat = "yyyy-MM-dd hh:mm:ss";
            this.dTPFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dTPFrom.Location = new System.Drawing.Point(112, 19);
            this.dTPFrom.Name = "dTPFrom";
            this.dTPFrom.Size = new System.Drawing.Size(102, 20);
            this.dTPFrom.TabIndex = 4;
            this.dTPFrom.Value = new System.DateTime(2019, 1, 1, 0, 0, 0, 0);
            // 
            // rbWeek
            // 
            this.rbWeek.AutoSize = true;
            this.rbWeek.Location = new System.Drawing.Point(219, 22);
            this.rbWeek.Name = "rbWeek";
            this.rbWeek.Size = new System.Drawing.Size(54, 17);
            this.rbWeek.TabIndex = 3;
            this.rbWeek.TabStop = true;
            this.rbWeek.Text = "Week";
            this.rbWeek.UseVisualStyleBackColor = true;
            this.rbWeek.CheckedChanged += new System.EventHandler(this.rbWeek_CheckedChanged);
            // 
            // rbMonth
            // 
            this.rbMonth.AutoSize = true;
            this.rbMonth.Location = new System.Drawing.Point(279, 22);
            this.rbMonth.Name = "rbMonth";
            this.rbMonth.Size = new System.Drawing.Size(55, 17);
            this.rbMonth.TabIndex = 2;
            this.rbMonth.TabStop = true;
            this.rbMonth.Text = "Month";
            this.rbMonth.UseVisualStyleBackColor = true;
            this.rbMonth.CheckedChanged += new System.EventHandler(this.rbMonth_CheckedChanged);
            // 
            // btnAptDelete
            // 
            this.btnAptDelete.Location = new System.Drawing.Point(530, 307);
            this.btnAptDelete.Name = "btnAptDelete";
            this.btnAptDelete.Size = new System.Drawing.Size(70, 25);
            this.btnAptDelete.TabIndex = 2;
            this.btnAptDelete.Text = "Delete";
            this.btnAptDelete.UseVisualStyleBackColor = true;
            this.btnAptDelete.Click += new System.EventHandler(this.btnAptDelete_Click);
            // 
            // gbScheduleCustomer
            // 
            this.gbScheduleCustomer.BackColor = System.Drawing.Color.LightSteelBlue;
            this.gbScheduleCustomer.Controls.Add(this.dgvCustomer);
            this.gbScheduleCustomer.Controls.Add(this.btnCustViewEdit);
            this.gbScheduleCustomer.Controls.Add(this.btnCustNew);
            this.gbScheduleCustomer.Controls.Add(this.btnCustDelete);
            this.gbScheduleCustomer.Location = new System.Drawing.Point(6, 490);
            this.gbScheduleCustomer.Name = "gbScheduleCustomer";
            this.gbScheduleCustomer.Size = new System.Drawing.Size(601, 239);
            this.gbScheduleCustomer.TabIndex = 11;
            this.gbScheduleCustomer.TabStop = false;
            this.gbScheduleCustomer.Text = "Athletes";
            // 
            // dgvCustomer
            // 
            this.dgvCustomer.AllowUserToAddRows = false;
            this.dgvCustomer.AllowUserToDeleteRows = false;
            this.dgvCustomer.AllowUserToResizeRows = false;
            this.dgvCustomer.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCustomer.BackgroundColor = System.Drawing.Color.White;
            this.dgvCustomer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCustomer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCustomer.GridColor = System.Drawing.Color.LightSteelBlue;
            this.dgvCustomer.Location = new System.Drawing.Point(9, 32);
            this.dgvCustomer.Name = "dgvCustomer";
            this.dgvCustomer.ReadOnly = true;
            this.dgvCustomer.RowHeadersVisible = false;
            this.dgvCustomer.Size = new System.Drawing.Size(592, 169);
            this.dgvCustomer.TabIndex = 43;
            this.dgvCustomer.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCustomer_CellClick);
            // 
            // btnCustViewEdit
            // 
            this.btnCustViewEdit.Location = new System.Drawing.Point(363, 210);
            this.btnCustViewEdit.Name = "btnCustViewEdit";
            this.btnCustViewEdit.Size = new System.Drawing.Size(75, 23);
            this.btnCustViewEdit.TabIndex = 13;
            this.btnCustViewEdit.Text = "View/Edit";
            this.btnCustViewEdit.UseVisualStyleBackColor = true;
            this.btnCustViewEdit.Click += new System.EventHandler(this.btnCustViewEdit_Click);
            // 
            // btnCustNew
            // 
            this.btnCustNew.Location = new System.Drawing.Point(444, 210);
            this.btnCustNew.Name = "btnCustNew";
            this.btnCustNew.Size = new System.Drawing.Size(75, 23);
            this.btnCustNew.TabIndex = 12;
            this.btnCustNew.Text = "New";
            this.btnCustNew.UseVisualStyleBackColor = true;
            this.btnCustNew.Click += new System.EventHandler(this.btnCustNew_Click);
            // 
            // btnCustDelete
            // 
            this.btnCustDelete.Location = new System.Drawing.Point(525, 207);
            this.btnCustDelete.Name = "btnCustDelete";
            this.btnCustDelete.Size = new System.Drawing.Size(70, 25);
            this.btnCustDelete.TabIndex = 11;
            this.btnCustDelete.Text = "Delete";
            this.btnCustDelete.UseVisualStyleBackColor = true;
            this.btnCustDelete.Click += new System.EventHandler(this.btnCustDelete_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(531, 735);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(70, 24);
            this.btnExit.TabIndex = 12;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.MediumBlue;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(6, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1212, 119);
            this.panel1.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.MediumBlue;
            this.label3.Font = new System.Drawing.Font("Planet Benson 2", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(23, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(813, 98);
            this.label3.TabIndex = 0;
            this.label3.Text = "Cover Zero Training";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(881, 21);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(73, 66);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // Scheduling
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(1230, 768);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.gbScheduleCustomer);
            this.Controls.Add(this.gbAppointments);
            this.Controls.Add(this.gbReports);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Scheduling";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Scheduling";
            this.Load += new System.EventHandler(this.Scheduling_Load);
            this.gbReports.ResumeLayout(false);
            this.gbReports.PerformLayout();
            this.gbAppointments.ResumeLayout(false);
            this.gbAppointments.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppt)).EndInit();
            this.gbScheduleCustomer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbReports;
        private System.Windows.Forms.ComboBox cbReports;
        private System.Windows.Forms.RichTextBox rtbReport;
        private System.Windows.Forms.GroupBox gbAppointments;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnAptDelete;
        private System.Windows.Forms.DataGridView dgvAppt;
        private System.Windows.Forms.DateTimePicker dTPFrom;
        private System.Windows.Forms.RadioButton rbWeek;
        private System.Windows.Forms.RadioButton rbMonth;
        private System.Windows.Forms.Label lblCalender;
        private System.Windows.Forms.GroupBox gbScheduleCustomer;
        private System.Windows.Forms.Button btnAptNew;
        private System.Windows.Forms.Label lblApptDates;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnLoadData;
        private System.Windows.Forms.Button btnApptViewEdit;
        private System.Windows.Forms.Button btnLoadAll;
        private System.Windows.Forms.Button btnCustViewEdit;
        private System.Windows.Forms.Button btnCustNew;
        private System.Windows.Forms.Button btnCustDelete;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.DataGridView dgvCustomer;
        private System.Windows.Forms.Label lblUpcoming;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.DateTimePicker dtpMonth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbConsultant;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}