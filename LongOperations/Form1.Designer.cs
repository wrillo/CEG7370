namespace LongOperations
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
            this.btnCompute = new System.Windows.Forms.Button();
            this.btnTemp = new System.Windows.Forms.Button();
            this.btnStock = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCompute
            // 
            this.btnCompute.Location = new System.Drawing.Point(23, 17);
            this.btnCompute.Name = "btnCompute";
            this.btnCompute.Size = new System.Drawing.Size(124, 23);
            this.btnCompute.TabIndex = 2;
            this.btnCompute.Text = "Compute";
            this.btnCompute.Click += new System.EventHandler(this.btnCompute_Click);
            // 
            // btnTemp
            // 
            this.btnTemp.Location = new System.Drawing.Point(23, 46);
            this.btnTemp.Name = "btnTemp";
            this.btnTemp.Size = new System.Drawing.Size(124, 23);
            this.btnTemp.TabIndex = 1;
            this.btnTemp.Text = "Get Temperature";
            this.btnTemp.Click += new System.EventHandler(this.btnTemp_Click);
            // 
            // btnStock
            // 
            this.btnStock.Location = new System.Drawing.Point(23, 75);
            this.btnStock.Name = "btnStock";
            this.btnStock.Size = new System.Drawing.Size(124, 23);
            this.btnStock.TabIndex = 0;
            this.btnStock.Text = "Get Stock Price";
            this.btnStock.Click += new System.EventHandler(this.btnStock_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(183, 172);
            this.Controls.Add(this.btnStock);
            this.Controls.Add(this.btnTemp);
            this.Controls.Add(this.btnCompute);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCompute;
        private System.Windows.Forms.Button btnTemp;
        private System.Windows.Forms.Button btnStock;
    }
}

