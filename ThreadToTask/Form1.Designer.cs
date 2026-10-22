namespace ThreadToTask
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
            this.btnMyCompute = new System.Windows.Forms.Button();
            this.btnMyComputeTask = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnMyCompute
            // 
            this.btnMyCompute.Location = new System.Drawing.Point(64, 75);
            this.btnMyCompute.Name = "btnMyCompute";
            this.btnMyCompute.Size = new System.Drawing.Size(123, 68);
            this.btnMyCompute.TabIndex = 0;
            this.btnMyCompute.Text = "MyCompute Thread";
            this.btnMyCompute.UseVisualStyleBackColor = true;
            this.btnMyCompute.Click += new System.EventHandler(this.btnMyCompute_Click);
            // 
            // btnMyComputeTask
            // 
            this.btnMyComputeTask.Location = new System.Drawing.Point(205, 75);
            this.btnMyComputeTask.Name = "btnMyComputeTask";
            this.btnMyComputeTask.Size = new System.Drawing.Size(123, 68);
            this.btnMyComputeTask.TabIndex = 1;
            this.btnMyComputeTask.Text = "MyCompute Task";
            this.btnMyComputeTask.UseVisualStyleBackColor = true;
            this.btnMyComputeTask.Click += new System.EventHandler(this.btnMyComputeTask_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 210);
            this.Controls.Add(this.btnMyComputeTask);
            this.Controls.Add(this.btnMyCompute);
            this.Name = "Form1";
            this.Text = "Thread To Task";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnMyCompute;
        private System.Windows.Forms.Button btnMyComputeTask;
    }
}

