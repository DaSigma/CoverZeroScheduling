namespace DataLibrary
{
    partial class AppointmentForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AppointmentForm));
            this.gbAppointment = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnDone = new System.Windows.Forms.Button();
            this.btnSaveApt = new System.Windows.Forms.Button();
            this.gbAppointmentInfo = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbUsr = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dTPStartTime = new System.Windows.Forms.DateTimePicker();
            this.dTPEndTime = new System.Windows.Forms.DateTimePicker();
            this.lblUpdated = new System.Windows.Forms.Label();
            this.dTPStart = new System.Windows.Forms.DateTimePicker();
            this.cbCustName = new System.Windows.Forms.ComboBox();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.lblLastUpdate = new System.Windows.Forms.Label();
            this.lblApptID = new System.Windows.Forms.Label();
            this.lb6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl1 = new System.Windows.Forms.Label();
            this.lbl2 = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.gbAppointment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.gbAppointmentInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // gbAppointment
            // 
            this.gbAppointment.BackColor = System.Drawing.Color.LightSteelBlue;
            this.gbAppointment.Controls.Add(this.pictureBox1);
            this.gbAppointment.Controls.Add(this.btnDone);
            this.gbAppointment.Controls.Add(this.btnSaveApt);
            this.gbAppointment.Controls.Add(this.gbAppointmentInfo);
            this.gbAppointment.Location = new System.Drawing.Point(12, 23);
            this.gbAppointment.Name = "gbAppointment";
            this.gbAppointment.Size = new System.Drawing.Size(561, 261);
            this.gbAppointment.TabIndex = 12;
            this.gbAppointment.TabStop = false;
            this.gbAppointment.Text = "Appointment";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(306, 19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(249, 207);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 30;
            this.pictureBox1.TabStop = false;
            // 
            // btnDone
            // 
            this.btnDone.Location = new System.Drawing.Point(455, 232);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(72, 23);
            this.btnDone.TabIndex = 8;
            this.btnDone.Text = "Close";
            this.btnDone.UseVisualStyleBackColor = true;
            // 
            // btnSaveApt
            // 
            this.btnSaveApt.Enabled = false;
            this.btnSaveApt.Location = new System.Drawing.Point(377, 232);
            this.btnSaveApt.Name = "btnSaveApt";
            this.btnSaveApt.Size = new System.Drawing.Size(72, 23);
            this.btnSaveApt.TabIndex = 7;
            this.btnSaveApt.Text = "Save";
            this.btnSaveApt.UseVisualStyleBackColor = true;
            this.btnSaveApt.Click += new System.EventHandler(this.btnSaveApt_Click);
            // 
            // gbAppointmentInfo
            // 
            this.gbAppointmentInfo.Controls.Add(this.label5);
            this.gbAppointmentInfo.Controls.Add(this.cbUsr);
            this.gbAppointmentInfo.Controls.Add(this.label4);
            this.gbAppointmentInfo.Controls.Add(this.label3);
            this.gbAppointmentInfo.Controls.Add(this.label2);
            this.gbAppointmentInfo.Controls.Add(this.dTPStartTime);
            this.gbAppointmentInfo.Controls.Add(this.dTPEndTime);
            this.gbAppointmentInfo.Controls.Add(this.lblUpdated);
            this.gbAppointmentInfo.Controls.Add(this.dTPStart);
            this.gbAppointmentInfo.Controls.Add(this.cbCustName);
            this.gbAppointmentInfo.Controls.Add(this.cbType);
            this.gbAppointmentInfo.Controls.Add(this.lblLastUpdate);
            this.gbAppointmentInfo.Controls.Add(this.lblApptID);
            this.gbAppointmentInfo.Controls.Add(this.lb6);
            this.gbAppointmentInfo.Controls.Add(this.label1);
            this.gbAppointmentInfo.Controls.Add(this.lbl1);
            this.gbAppointmentInfo.Controls.Add(this.lbl2);
            this.gbAppointmentInfo.Controls.Add(this.dtpEnd);
            this.gbAppointmentInfo.Location = new System.Drawing.Point(6, 16);
            this.gbAppointmentInfo.Name = "gbAppointmentInfo";
            this.gbAppointmentInfo.Size = new System.Drawing.Size(294, 239);
            this.gbAppointmentInfo.TabIndex = 29;
            this.gbAppointmentInfo.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(68, 104);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 42;
            this.label5.Text = "Coach";
            // 
            // cbUsr
            // 
            this.cbUsr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUsr.FormattingEnabled = true;
            this.cbUsr.Location = new System.Drawing.Point(114, 96);
            this.cbUsr.Margin = new System.Windows.Forms.Padding(2);
            this.cbUsr.Name = "cbUsr";
            this.cbUsr.Size = new System.Drawing.Size(151, 21);
            this.cbUsr.TabIndex = 41;
            this.cbUsr.SelectedIndexChanged += new System.EventHandler(this.cbUsr_SelectedIndexChanged);
            this.cbUsr.Validating += new System.ComponentModel.CancelEventHandler(this.cbUsr_Validating);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(194, 171);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 13);
            this.label4.TabIndex = 40;
            this.label4.Text = "To";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(114, 171);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 39;
            this.label3.Text = "From";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 155);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 38;
            this.label2.Text = "Appointment Time";
            // 
            // dTPStartTime
            // 
            this.dTPStartTime.CustomFormat = "hh:mm tt";
            this.dTPStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dTPStartTime.Location = new System.Drawing.Point(116, 148);
            this.dTPStartTime.Name = "dTPStartTime";
            this.dTPStartTime.ShowUpDown = true;
            this.dTPStartTime.Size = new System.Drawing.Size(73, 20);
            this.dTPStartTime.TabIndex = 4;
            this.dTPStartTime.Value = new System.DateTime(2020, 11, 16, 9, 0, 0, 0);
            this.dTPStartTime.ValueChanged += new System.EventHandler(this.dTPStartTime_ValueChanged);
            // 
            // dTPEndTime
            // 
            this.dTPEndTime.CustomFormat = "hh:mm tt";
            this.dTPEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dTPEndTime.Location = new System.Drawing.Point(196, 148);
            this.dTPEndTime.Name = "dTPEndTime";
            this.dTPEndTime.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dTPEndTime.ShowUpDown = true;
            this.dTPEndTime.Size = new System.Drawing.Size(70, 20);
            this.dTPEndTime.TabIndex = 6;
            this.dTPEndTime.Value = new System.DateTime(2020, 11, 16, 10, 21, 0, 0);
            // 
            // lblUpdated
            // 
            this.lblUpdated.AutoSize = true;
            this.lblUpdated.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpdated.Location = new System.Drawing.Point(113, 191);
            this.lblUpdated.Name = "lblUpdated";
            this.lblUpdated.Size = new System.Drawing.Size(75, 13);
            this.lblUpdated.TabIndex = 33;
            this.lblUpdated.Text = "01/01/2002";
            // 
            // dTPStart
            // 
            this.dTPStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dTPStart.Location = new System.Drawing.Point(115, 122);
            this.dTPStart.Name = "dTPStart";
            this.dTPStart.Size = new System.Drawing.Size(150, 20);
            this.dTPStart.TabIndex = 3;
            this.dTPStart.Value = new System.DateTime(2020, 12, 25, 0, 0, 0, 0);
            this.dTPStart.ValueChanged += new System.EventHandler(this.dTPStart_ValueChanged);
            // 
            // cbCustName
            // 
            this.cbCustName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCustName.FormattingEnabled = true;
            this.cbCustName.Location = new System.Drawing.Point(115, 41);
            this.cbCustName.Name = "cbCustName";
            this.cbCustName.Size = new System.Drawing.Size(150, 21);
            this.cbCustName.TabIndex = 37;
            this.cbCustName.SelectedIndexChanged += new System.EventHandler(this.cbCustName_SelectedIndexChanged);
            // 
            // cbType
            // 
            this.cbType.FormattingEnabled = true;
            this.cbType.Location = new System.Drawing.Point(115, 68);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(150, 21);
            this.cbType.TabIndex = 2;
            this.cbType.TextChanged += new System.EventHandler(this.cbType_TextChanged);
            // 
            // lblLastUpdate
            // 
            this.lblLastUpdate.AutoSize = true;
            this.lblLastUpdate.Location = new System.Drawing.Point(32, 191);
            this.lblLastUpdate.Name = "lblLastUpdate";
            this.lblLastUpdate.Size = new System.Drawing.Size(74, 13);
            this.lblLastUpdate.TabIndex = 30;
            this.lblLastUpdate.Text = "Last Updated:";
            // 
            // lblApptID
            // 
            this.lblApptID.AutoSize = true;
            this.lblApptID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblApptID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApptID.Location = new System.Drawing.Point(116, 20);
            this.lblApptID.Name = "lblApptID";
            this.lblApptID.Size = new System.Drawing.Size(16, 15);
            this.lblApptID.TabIndex = 35;
            this.lblApptID.Text = "0";
            // 
            // lb6
            // 
            this.lb6.AutoSize = true;
            this.lb6.Location = new System.Drawing.Point(75, 76);
            this.lb6.Name = "lb6";
            this.lb6.Size = new System.Drawing.Size(31, 13);
            this.lb6.TabIndex = 7;
            this.lb6.Text = "Type";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 129);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Appointment Date";
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Location = new System.Drawing.Point(63, 20);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(43, 13);
            this.lbl1.TabIndex = 21;
            this.lbl1.Text = "Appt ID";
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.Location = new System.Drawing.Point(35, 49);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(71, 13);
            this.lbl2.TabIndex = 4;
            this.lbl2.Text = "Athlete Name";
            // 
            // dtpEnd
            // 
            this.dtpEnd.Enabled = false;
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEnd.Location = new System.Drawing.Point(116, 216);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(128, 20);
            this.dtpEnd.TabIndex = 5;
            this.dtpEnd.Visible = false;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // AppointmentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(585, 305);
            this.Controls.Add(this.gbAppointment);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AppointmentForm";
            this.Text = "Appointment";
            this.gbAppointment.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.gbAppointmentInfo.ResumeLayout(false);
            this.gbAppointmentInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnSaveApt;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.Label lb6;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.GroupBox gbAppointmentInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        internal System.Windows.Forms.Label lblUpdated;
        internal System.Windows.Forms.Label lblApptID;
        internal System.Windows.Forms.DateTimePicker dtpEnd;
        internal System.Windows.Forms.DateTimePicker dTPStart;
        internal System.Windows.Forms.ComboBox cbCustName;
        internal System.Windows.Forms.ComboBox cbType;
        private System.Windows.Forms.Button btnDone;
        internal System.Windows.Forms.GroupBox gbAppointment;
        internal System.Windows.Forms.Label lblLastUpdate;
        internal System.Windows.Forms.DateTimePicker dTPStartTime;
        internal System.Windows.Forms.DateTimePicker dTPEndTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        internal System.Windows.Forms.ComboBox cbUsr;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}