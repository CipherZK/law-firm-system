
//namespace Diploma.Pages
//{
//    partial class sv
//    {
//        /// <summary>
//        /// Required designer variable.
//        /// </summary>
//        private System.ComponentModel.IContainer components = null;

//        /// <summary>
//        /// Clean up any resources being used.
//        /// </summary>
//        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && (components != null))
//            {
//                components.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        #region Windows Form Designer generated code

//        /// <summary>
//        /// Required method for Designer support - do not modify
//        /// the contents of this method with the code editor.
//        /// </summary>
//        private void InitializeComponent()
//        {
//            this.button1 = new System.Windows.Forms.Button();
//            this.button2 = new System.Windows.Forms.Button();
//            this.sendTextBox = new Guna.UI2.WinForms.Guna2TextBox();
//            this.listBox1 = new System.Windows.Forms.ListBox();
//            this.textBox1 = new System.Windows.Forms.TextBox();
//            this.label1 = new System.Windows.Forms.Label();
//            this.button3 = new System.Windows.Forms.Button();
//            this.SuspendLayout();
//            // 
//            // button1
//            // 
//            this.button1.Location = new System.Drawing.Point(561, 367);
//            this.button1.Name = "button1";
//            this.button1.Size = new System.Drawing.Size(68, 55);
//            this.button1.TabIndex = 0;
//            this.button1.Text = "send";
//            this.button1.UseVisualStyleBackColor = true;
//            this.button1.Click += new System.EventHandler(this.button1_Click);
//            // 
//            // button2
//            // 
//            this.button2.Location = new System.Drawing.Point(12, 338);
//            this.button2.Name = "button2";
//            this.button2.Size = new System.Drawing.Size(62, 32);
//            this.button2.TabIndex = 1;
//            this.button2.Text = "attech";
//            this.button2.UseVisualStyleBackColor = true;
//            this.button2.Click += new System.EventHandler(this.button2_Click);
//            // 
//            // sendTextBox
//            // 
//            this.sendTextBox.AutoRoundedCorners = true;
//            this.sendTextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
//            this.sendTextBox.BorderRadius = 26;
//            this.sendTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
//            this.sendTextBox.DefaultText = "";
//            this.sendTextBox.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
//            this.sendTextBox.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
//            this.sendTextBox.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
//            this.sendTextBox.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
//            this.sendTextBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
//            this.sendTextBox.Font = new System.Drawing.Font("Lucida Fax", 10.8F);
//            this.sendTextBox.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
//            this.sendTextBox.Location = new System.Drawing.Point(91, 367);
//            this.sendTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
//            this.sendTextBox.Name = "sendTextBox";
//            this.sendTextBox.PasswordChar = '\0';
//            this.sendTextBox.PlaceholderText = "Type something to send";
//            this.sendTextBox.SelectedText = "";
//            this.sendTextBox.Size = new System.Drawing.Size(438, 55);
//            this.sendTextBox.TabIndex = 49;
//            this.sendTextBox.TextOffset = new System.Drawing.Point(50, 0);
//            this.sendTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.sendTextBox_KeyDown);
//            // 
//            // listBox1
//            // 
//            this.listBox1.FormattingEnabled = true;
//            this.listBox1.ItemHeight = 16;
//            this.listBox1.Location = new System.Drawing.Point(91, 168);
//            this.listBox1.Name = "listBox1";
//            this.listBox1.Size = new System.Drawing.Size(458, 148);
//            this.listBox1.TabIndex = 50;
//            // 
//            // textBox1
//            // 
//            this.textBox1.Location = new System.Drawing.Point(186, 63);
//            this.textBox1.Name = "textBox1";
//            this.textBox1.Size = new System.Drawing.Size(321, 22);
//            this.textBox1.TabIndex = 51;
//            // 
//            // label1
//            // 
//            this.label1.AutoSize = true;
//            this.label1.Location = new System.Drawing.Point(103, 348);
//            this.label1.Name = "label1";
//            this.label1.Size = new System.Drawing.Size(44, 16);
//            this.label1.TabIndex = 52;
//            this.label1.Text = "label1";
//            // 
//            // button3
//            // 
//            this.button3.Location = new System.Drawing.Point(61, 47);
//            this.button3.Name = "button3";
//            this.button3.Size = new System.Drawing.Size(75, 23);
//            this.button3.TabIndex = 53;
//            this.button3.Text = "button3";
//            this.button3.UseVisualStyleBackColor = true;
//            this.button3.Click += new System.EventHandler(this.button3_Click);
//            // 
//            // sv
//            // 
//            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
//            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
//            this.ClientSize = new System.Drawing.Size(800, 450);
//            this.Controls.Add(this.button3);
//            this.Controls.Add(this.label1);
//            this.Controls.Add(this.textBox1);
//            this.Controls.Add(this.listBox1);
//            this.Controls.Add(this.sendTextBox);
//            this.Controls.Add(this.button2);
//            this.Controls.Add(this.button1);
//            this.Name = "sv";
//            this.Text = "sv";
//            this.ResumeLayout(false);
//            this.PerformLayout();

//        }

//        #endregion

//        private System.Windows.Forms.Button button1;
//        private System.Windows.Forms.Button button2;
//        private Guna.UI2.WinForms.Guna2TextBox sendTextBox;
//        private System.Windows.Forms.ListBox listBox1;
//        private System.Windows.Forms.TextBox textBox1;
//        private System.Windows.Forms.Label label1;
//        private System.Windows.Forms.Button button3;
//    }
//}