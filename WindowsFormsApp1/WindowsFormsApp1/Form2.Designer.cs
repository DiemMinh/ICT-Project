namespace WindowsFormsApp1
{
    partial class Form2
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
            this.comboBox = new System.Windows.Forms.ComboBox();
            this.test = new System.Windows.Forms.Label();
            this.StartDateComboBox = new System.Windows.Forms.ComboBox();
            this.EndDateComboBox = new System.Windows.Forms.ComboBox();
            this.genderLabel = new System.Windows.Forms.Label();
            this.ageLabel = new System.Windows.Forms.Label();
            this.test1 = new System.Windows.Forms.Label();
            this.calculateBtn = new System.Windows.Forms.Button();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.test2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBox
            // 
            this.comboBox.FormattingEnabled = true;
            this.comboBox.Location = new System.Drawing.Point(134, 151);
            this.comboBox.Name = "comboBox";
            this.comboBox.Size = new System.Drawing.Size(121, 24);
            this.comboBox.TabIndex = 0;
            this.comboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // test
            // 
            this.test.AutoSize = true;
            this.test.Location = new System.Drawing.Point(65, 403);
            this.test.Name = "test";
            this.test.Size = new System.Drawing.Size(0, 17);
            this.test.TabIndex = 1;
            // 
            // StartDateComboBox
            // 
            this.StartDateComboBox.FormattingEnabled = true;
            this.StartDateComboBox.Location = new System.Drawing.Point(418, 151);
            this.StartDateComboBox.Name = "StartDateComboBox";
            this.StartDateComboBox.Size = new System.Drawing.Size(121, 24);
            this.StartDateComboBox.TabIndex = 2;
            this.StartDateComboBox.SelectedIndexChanged += new System.EventHandler(this.StartDate_SelectedIndexChanged);
            // 
            // EndDateComboBox
            // 
            this.EndDateComboBox.FormattingEnabled = true;
            this.EndDateComboBox.Location = new System.Drawing.Point(573, 151);
            this.EndDateComboBox.Name = "EndDateComboBox";
            this.EndDateComboBox.Size = new System.Drawing.Size(121, 24);
            this.EndDateComboBox.TabIndex = 3;
            this.EndDateComboBox.SelectedIndexChanged += new System.EventHandler(this.EndDateComboBox_SelectedIndexChanged);
            // 
            // genderLabel
            // 
            this.genderLabel.AutoSize = true;
            this.genderLabel.Location = new System.Drawing.Point(134, 235);
            this.genderLabel.Name = "genderLabel";
            this.genderLabel.Size = new System.Drawing.Size(56, 17);
            this.genderLabel.TabIndex = 4;
            this.genderLabel.Text = "Gender";
            // 
            // ageLabel
            // 
            this.ageLabel.AutoSize = true;
            this.ageLabel.Location = new System.Drawing.Point(137, 295);
            this.ageLabel.Name = "ageLabel";
            this.ageLabel.Size = new System.Drawing.Size(33, 17);
            this.ageLabel.TabIndex = 5;
            this.ageLabel.Text = "Age";
            // 
            // test1
            // 
            this.test1.AutoSize = true;
            this.test1.Location = new System.Drawing.Point(508, 310);
            this.test1.Name = "test1";
            this.test1.Size = new System.Drawing.Size(46, 17);
            this.test1.TabIndex = 6;
            this.test1.Text = "label1";
            this.test1.Click += new System.EventHandler(this.test1_Click);
            // 
            // calculateBtn
            // 
            this.calculateBtn.Location = new System.Drawing.Point(378, 360);
            this.calculateBtn.Name = "calculateBtn";
            this.calculateBtn.Size = new System.Drawing.Size(75, 23);
            this.calculateBtn.TabIndex = 7;
            this.calculateBtn.Text = "Calculate";
            this.calculateBtn.UseVisualStyleBackColor = true;
            this.calculateBtn.Click += new System.EventHandler(this.calculateBtn_Click);
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(394, 204);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(120, 89);
            this.checkedListBox1.TabIndex = 8;
            this.checkedListBox1.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged_1);
            // 
            // test2
            // 
            this.test2.AutoSize = true;
            this.test2.Location = new System.Drawing.Point(195, 70);
            this.test2.Name = "test2";
            this.test2.Size = new System.Drawing.Size(46, 17);
            this.test2.TabIndex = 9;
            this.test2.Text = "label1";
            this.test2.Click += new System.EventHandler(this.test2_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.test2);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.calculateBtn);
            this.Controls.Add(this.test1);
            this.Controls.Add(this.ageLabel);
            this.Controls.Add(this.genderLabel);
            this.Controls.Add(this.EndDateComboBox);
            this.Controls.Add(this.StartDateComboBox);
            this.Controls.Add(this.test);
            this.Controls.Add(this.comboBox);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox;
        private System.Windows.Forms.Label test;
        private System.Windows.Forms.ComboBox StartDateComboBox;
        private System.Windows.Forms.ComboBox EndDateComboBox;
        private System.Windows.Forms.Label genderLabel;
        private System.Windows.Forms.Label ageLabel;
        private System.Windows.Forms.Label test1;
        private System.Windows.Forms.Button calculateBtn;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Label test2;
    }
}