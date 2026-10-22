namespace FirstWCFClient
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
            this.btnTestWCFMyMath = new System.Windows.Forms.Button();
            this.btnTestWCFViaProxy = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnTestWCFMyMath
            // 
            this.btnTestWCFMyMath.Location = new System.Drawing.Point(230, 42);
            this.btnTestWCFMyMath.Name = "btnTestWCFMyMath";
            this.btnTestWCFMyMath.Size = new System.Drawing.Size(214, 74);
            this.btnTestWCFMyMath.TabIndex = 0;
            this.btnTestWCFMyMath.Text = "Test WCF MyMath";
            this.btnTestWCFMyMath.UseVisualStyleBackColor = true;
            this.btnTestWCFMyMath.Click += new System.EventHandler(this.btnTestWCFMyMath_Click);
            // 
            // btnTestWCFViaProxy
            // 
            this.btnTestWCFViaProxy.Location = new System.Drawing.Point(230, 163);
            this.btnTestWCFViaProxy.Name = "btnTestWCFViaProxy";
            this.btnTestWCFViaProxy.Size = new System.Drawing.Size(214, 74);
            this.btnTestWCFViaProxy.TabIndex = 1;
            this.btnTestWCFViaProxy.Text = "Test WCF Via Proxy";
            this.btnTestWCFViaProxy.UseVisualStyleBackColor = true;
            this.btnTestWCFViaProxy.Click += new System.EventHandler(this.btnTestWCFViaProxy_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnTestWCFViaProxy);
            this.Controls.Add(this.btnTestWCFMyMath);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTestWCFMyMath;
        private System.Windows.Forms.Button btnTestWCFViaProxy;
    }
}

