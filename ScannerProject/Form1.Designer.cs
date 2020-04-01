namespace ScannerProject
{
    partial class Form1
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
            this.ScanBtn = new System.Windows.Forms.Button();
            this.InputTxt = new System.Windows.Forms.RichTextBox();
            this.OutputTxt = new System.Windows.Forms.RichTextBox();
            this.ClrBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ScanBtn
            // 
            this.ScanBtn.Location = new System.Drawing.Point(190, 399);
            this.ScanBtn.Name = "ScanBtn";
            this.ScanBtn.Size = new System.Drawing.Size(101, 25);
            this.ScanBtn.TabIndex = 0;
            this.ScanBtn.Text = "Scan";
            this.ScanBtn.UseVisualStyleBackColor = true;
            this.ScanBtn.Click += new System.EventHandler(this.Button1_Click);
            // 
            // InputTxt
            // 
            this.InputTxt.Location = new System.Drawing.Point(12, 12);
            this.InputTxt.Name = "InputTxt";
            this.InputTxt.Size = new System.Drawing.Size(449, 363);
            this.InputTxt.TabIndex = 1;
            this.InputTxt.Text = "";
            this.InputTxt.TextChanged += new System.EventHandler(this.RichTextBox1_TextChanged);
            // 
            // OutputTxt
            // 
            this.OutputTxt.Location = new System.Drawing.Point(468, 13);
            this.OutputTxt.Name = "OutputTxt";
            this.OutputTxt.Size = new System.Drawing.Size(320, 362);
            this.OutputTxt.TabIndex = 2;
            this.OutputTxt.Text = "";
            this.OutputTxt.TextChanged += new System.EventHandler(this.RichTextBox2_TextChanged);
            // 
            // ClrBtn
            // 
            this.ClrBtn.Location = new System.Drawing.Point(598, 400);
            this.ClrBtn.Name = "ClrBtn";
            this.ClrBtn.Size = new System.Drawing.Size(92, 23);
            this.ClrBtn.TabIndex = 3;
            this.ClrBtn.Text = "Clear\r\n";
            this.ClrBtn.UseVisualStyleBackColor = true;
            this.ClrBtn.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ClrBtn);
            this.Controls.Add(this.OutputTxt);
            this.Controls.Add(this.InputTxt);
            this.Controls.Add(this.ScanBtn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ScanBtn;
        private System.Windows.Forms.RichTextBox InputTxt;
        private System.Windows.Forms.RichTextBox OutputTxt;
        private System.Windows.Forms.Button ClrBtn;
    }
}

