namespace CoverZeroScheduling
{
    partial class AthleteForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AthleteForm));
            this.gbCustomer = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.gbCustomerInfo = new System.Windows.Forms.GroupBox();
            this.btnNewAddress = new System.Windows.Forms.Button();
            this.btnEditAddress = new System.Windows.Forms.Button();
            this.tbCountry = new System.Windows.Forms.TextBox();
            this.tbCity = new System.Windows.Forms.TextBox();
            this.lblCustID = new System.Windows.Forms.Label();
            this.cbZip = new System.Windows.Forms.ComboBox();
            this.lblLastUpdated = new System.Windows.Forms.Label();
            this.lblLastupdate = new System.Windows.Forms.Label();
            this.tbPhone = new System.Windows.Forms.TextBox();
            this.lb6 = new System.Windows.Forms.Label();
            this.lbl5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl7 = new System.Windows.Forms.Label();
            this.lbl1 = new System.Windows.Forms.Label();
            this.lbl2 = new System.Windows.Forms.Label();
            this.lbl3 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.tbAdd = new System.Windows.Forms.TextBox();
            this.btnSaveCustomer = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbPosition = new System.Windows.Forms.ComboBox();
            this.cbDiscipline = new System.Windows.Forms.ComboBox();
            this.gbCustomer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.gbCustomerInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // gbCustomer
            // 
            this.gbCustomer.BackColor = System.Drawing.Color.LightSteelBlue;
            this.gbCustomer.Controls.Add(this.btnClose);
            this.gbCustomer.Controls.Add(this.pictureBox1);
            this.gbCustomer.Controls.Add(this.gbCustomerInfo);
            this.gbCustomer.Controls.Add(this.btnSaveCustomer);
            this.gbCustomer.Location = new System.Drawing.Point(12, 23);
            this.gbCustomer.Name = "gbCustomer";
            this.gbCustomer.Size = new System.Drawing.Size(543, 356);
            this.gbCustomer.TabIndex = 12;
            this.gbCustomer.TabStop = false;
            this.gbCustomer.Text = "Athlete";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(464, 303);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(283, 27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(254, 230);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 30;
            this.pictureBox1.TabStop = false;
            // 
            // gbCustomerInfo
            // 
            this.gbCustomerInfo.Controls.Add(this.cbDiscipline);
            this.gbCustomerInfo.Controls.Add(this.cbPosition);
            this.gbCustomerInfo.Controls.Add(this.label3);
            this.gbCustomerInfo.Controls.Add(this.label2);
            this.gbCustomerInfo.Controls.Add(this.btnNewAddress);
            this.gbCustomerInfo.Controls.Add(this.btnEditAddress);
            this.gbCustomerInfo.Controls.Add(this.tbCountry);
            this.gbCustomerInfo.Controls.Add(this.tbCity);
            this.gbCustomerInfo.Controls.Add(this.lblCustID);
            this.gbCustomerInfo.Controls.Add(this.cbZip);
            this.gbCustomerInfo.Controls.Add(this.lblLastUpdated);
            this.gbCustomerInfo.Controls.Add(this.lblLastupdate);
            this.gbCustomerInfo.Controls.Add(this.tbPhone);
            this.gbCustomerInfo.Controls.Add(this.lb6);
            this.gbCustomerInfo.Controls.Add(this.lbl5);
            this.gbCustomerInfo.Controls.Add(this.label1);
            this.gbCustomerInfo.Controls.Add(this.lbl7);
            this.gbCustomerInfo.Controls.Add(this.lbl1);
            this.gbCustomerInfo.Controls.Add(this.lbl2);
            this.gbCustomerInfo.Controls.Add(this.lbl3);
            this.gbCustomerInfo.Controls.Add(this.tbName);
            this.gbCustomerInfo.Controls.Add(this.tbAdd);
            this.gbCustomerInfo.Location = new System.Drawing.Point(6, 19);
            this.gbCustomerInfo.Name = "gbCustomerInfo";
            this.gbCustomerInfo.Size = new System.Drawing.Size(271, 331);
            this.gbCustomerInfo.TabIndex = 29;
            this.gbCustomerInfo.TabStop = false;
            // 
            // btnNewAddress
            // 
            this.btnNewAddress.Location = new System.Drawing.Point(181, 283);
            this.btnNewAddress.Name = "btnNewAddress";
            this.btnNewAddress.Size = new System.Drawing.Size(79, 23);
            this.btnNewAddress.TabIndex = 39;
            this.btnNewAddress.Text = "New Address";
            this.btnNewAddress.UseVisualStyleBackColor = true;
            this.btnNewAddress.Click += new System.EventHandler(this.btnAddAddress_Click);
            // 
            // btnEditAddress
            // 
            this.btnEditAddress.Location = new System.Drawing.Point(90, 283);
            this.btnEditAddress.Name = "btnEditAddress";
            this.btnEditAddress.Size = new System.Drawing.Size(85, 23);
            this.btnEditAddress.TabIndex = 38;
            this.btnEditAddress.Text = "Edit Address";
            this.btnEditAddress.UseVisualStyleBackColor = true;
            this.btnEditAddress.Click += new System.EventHandler(this.btnEditAddress_Click);
            // 
            // tbCountry
            // 
            this.tbCountry.Enabled = false;
            this.tbCountry.Location = new System.Drawing.Point(82, 229);
            this.tbCountry.Name = "tbCountry";
            this.tbCountry.Size = new System.Drawing.Size(126, 20);
            this.tbCountry.TabIndex = 37;
            // 
            // tbCity
            // 
            this.tbCity.Enabled = false;
            this.tbCity.Location = new System.Drawing.Point(81, 176);
            this.tbCity.Name = "tbCity";
            this.tbCity.Size = new System.Drawing.Size(126, 20);
            this.tbCity.TabIndex = 36;
            // 
            // lblCustID
            // 
            this.lblCustID.AutoSize = true;
            this.lblCustID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCustID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustID.Location = new System.Drawing.Point(77, 22);
            this.lblCustID.Name = "lblCustID";
            this.lblCustID.Size = new System.Drawing.Size(98, 15);
            this.lblCustID.TabIndex = 35;
            this.lblCustID.Text = "Auto Generated";
            // 
            // cbZip
            // 
            this.cbZip.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbZip.Enabled = false;
            this.cbZip.FormattingEnabled = true;
            this.cbZip.ItemHeight = 13;
            this.cbZip.Location = new System.Drawing.Point(80, 202);
            this.cbZip.Name = "cbZip";
            this.cbZip.Size = new System.Drawing.Size(127, 21);
            this.cbZip.TabIndex = 33;
            this.cbZip.SelectedIndexChanged += new System.EventHandler(this.cbAthleteZip_SelectedIndexChanged);
            this.cbZip.TextChanged += new System.EventHandler(this.cbCustZip_TextChanged);
            // 
            // lblLastUpdated
            // 
            this.lblLastUpdated.AutoSize = true;
            this.lblLastUpdated.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastUpdated.Location = new System.Drawing.Point(86, 261);
            this.lblLastUpdated.Name = "lblLastUpdated";
            this.lblLastUpdated.Size = new System.Drawing.Size(31, 13);
            this.lblLastUpdated.TabIndex = 31;
            this.lblLastUpdated.Text = "Last";
            // 
            // lblLastupdate
            // 
            this.lblLastupdate.AutoSize = true;
            this.lblLastupdate.Location = new System.Drawing.Point(6, 261);
            this.lblLastupdate.Name = "lblLastupdate";
            this.lblLastupdate.Size = new System.Drawing.Size(74, 13);
            this.lblLastupdate.TabIndex = 30;
            this.lblLastupdate.Text = "Last Updated:";
            // 
            // tbPhone
            // 
            this.tbPhone.Enabled = false;
            this.tbPhone.Location = new System.Drawing.Point(81, 124);
            this.tbPhone.Name = "tbPhone";
            this.tbPhone.Size = new System.Drawing.Size(126, 20);
            this.tbPhone.TabIndex = 2;
            this.tbPhone.TextChanged += new System.EventHandler(this.tbAthletePhone_TextChanged);
            this.tbPhone.Leave += new System.EventHandler(this.tbAthletePhone_Leave);
            // 
            // lb6
            // 
            this.lb6.AutoSize = true;
            this.lb6.Location = new System.Drawing.Point(22, 206);
            this.lb6.Name = "lb6";
            this.lb6.Size = new System.Drawing.Size(50, 13);
            this.lb6.TabIndex = 7;
            this.lb6.Text = "Zip Code";
            // 
            // lbl5
            // 
            this.lbl5.AutoSize = true;
            this.lbl5.Location = new System.Drawing.Point(48, 180);
            this.lbl5.Name = "lbl5";
            this.lbl5.Size = new System.Drawing.Size(24, 13);
            this.lbl5.TabIndex = 6;
            this.lbl5.Text = "City";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 232);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Country";
            // 
            // lbl7
            // 
            this.lbl7.AutoSize = true;
            this.lbl7.Location = new System.Drawing.Point(24, 127);
            this.lbl7.Name = "lbl7";
            this.lbl7.Size = new System.Drawing.Size(48, 13);
            this.lbl7.TabIndex = 8;
            this.lbl7.Text = "Phone #";
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Location = new System.Drawing.Point(6, 22);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(54, 13);
            this.lbl1.TabIndex = 21;
            this.lbl1.Text = "Athlete ID";
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.Location = new System.Drawing.Point(36, 48);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(35, 13);
            this.lbl2.TabIndex = 4;
            this.lbl2.Text = "Name";
            // 
            // lbl3
            // 
            this.lbl3.AutoSize = true;
            this.lbl3.Location = new System.Drawing.Point(26, 154);
            this.lbl3.Name = "lbl3";
            this.lbl3.Size = new System.Drawing.Size(45, 13);
            this.lbl3.TabIndex = 5;
            this.lbl3.Text = "Address";
            // 
            // tbName
            // 
            this.tbName.BackColor = System.Drawing.SystemColors.Window;
            this.tbName.Location = new System.Drawing.Point(82, 45);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(125, 20);
            this.tbName.TabIndex = 1;
            this.tbName.TextChanged += new System.EventHandler(this.tbAthleteName_TextChanged);
            this.tbName.Leave += new System.EventHandler(this.tbAthleteName_Leave);
            // 
            // tbAdd
            // 
            this.tbAdd.Enabled = false;
            this.tbAdd.Location = new System.Drawing.Point(81, 150);
            this.tbAdd.Name = "tbAdd";
            this.tbAdd.Size = new System.Drawing.Size(126, 20);
            this.tbAdd.TabIndex = 3;
            this.tbAdd.TextChanged += new System.EventHandler(this.tbAthleteAddress_TextChanged);
            this.tbAdd.Leave += new System.EventHandler(this.tbAthleteAddress_Leave);
            // 
            // btnSaveCustomer
            // 
            this.btnSaveCustomer.Location = new System.Drawing.Point(378, 303);
            this.btnSaveCustomer.Name = "btnSaveCustomer";
            this.btnSaveCustomer.Size = new System.Drawing.Size(75, 23);
            this.btnSaveCustomer.TabIndex = 5;
            this.btnSaveCustomer.Text = "Save";
            this.btnSaveCustomer.UseVisualStyleBackColor = true;
            this.btnSaveCustomer.Click += new System.EventHandler(this.btnSaveAthlete_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 42;
            this.label2.Text = "Position";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 43;
            this.label3.Text = "Discipline";
            // 
            // cbPosition
            // 
            this.cbPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPosition.FormattingEnabled = true;
            this.cbPosition.ItemHeight = 13;
            this.cbPosition.Location = new System.Drawing.Point(82, 71);
            this.cbPosition.Name = "cbPosition";
            this.cbPosition.Size = new System.Drawing.Size(127, 21);
            this.cbPosition.TabIndex = 44;
            this.cbPosition.SelectedIndexChanged += new System.EventHandler(this.cbPosition_SelectedIndexChanged);
            // 
            // cbDiscipline
            // 
            this.cbDiscipline.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDiscipline.FormattingEnabled = true;
            this.cbDiscipline.ItemHeight = 13;
            this.cbDiscipline.Location = new System.Drawing.Point(82, 97);
            this.cbDiscipline.Name = "cbDiscipline";
            this.cbDiscipline.Size = new System.Drawing.Size(127, 21);
            this.cbDiscipline.TabIndex = 45;
            // 
            // AthleteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(567, 391);
            this.Controls.Add(this.gbCustomer);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AthleteForm";
            this.Text = "Athlete";
            this.gbCustomer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.gbCustomerInfo.ResumeLayout(false);
            this.gbCustomerInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnSaveCustomer;
        private System.Windows.Forms.Label lbl3;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.Label lbl7;
        private System.Windows.Forms.Label lbl5;
        private System.Windows.Forms.Label lb6;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.GroupBox gbCustomerInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnClose;
        internal System.Windows.Forms.GroupBox gbCustomer;
        internal System.Windows.Forms.TextBox tbName;
        internal System.Windows.Forms.TextBox tbPhone;
        internal System.Windows.Forms.TextBox tbAdd;
        internal System.Windows.Forms.Label lblLastUpdated;
        internal System.Windows.Forms.Label lblCustID;
        internal System.Windows.Forms.ComboBox cbZip;
        internal System.Windows.Forms.Label lblLastupdate;
        internal System.Windows.Forms.TextBox tbCountry;
        internal System.Windows.Forms.TextBox tbCity;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        internal System.Windows.Forms.Button btnEditAddress;
        internal System.Windows.Forms.Button btnNewAddress;
        internal System.Windows.Forms.ComboBox cbDiscipline;
        internal System.Windows.Forms.ComboBox cbPosition;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}