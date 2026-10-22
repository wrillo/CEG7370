namespace ParallelSwarmIntelligence
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
            this.btnParallelSwarms = new System.Windows.Forms.Button();
            this.btnSequentialSwarm = new System.Windows.Forms.Button();
            this.dg1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.lblResult = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dg1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnParallelSwarms
            // 
            this.btnParallelSwarms.Location = new System.Drawing.Point(16, 21);
            this.btnParallelSwarms.Margin = new System.Windows.Forms.Padding(2);
            this.btnParallelSwarms.Name = "btnParallelSwarms";
            this.btnParallelSwarms.Size = new System.Drawing.Size(92, 28);
            this.btnParallelSwarms.TabIndex = 0;
            this.btnParallelSwarms.Text = "Parallel Swarm";
            this.btnParallelSwarms.UseVisualStyleBackColor = true;
            this.btnParallelSwarms.Click += new System.EventHandler(this.btnParallelSwarms_Click);
            // 
            // btnSequentialSwarm
            // 
            this.btnSequentialSwarm.Location = new System.Drawing.Point(6, 52);
            this.btnSequentialSwarm.Margin = new System.Windows.Forms.Padding(2);
            this.btnSequentialSwarm.Name = "btnSequentialSwarm";
            this.btnSequentialSwarm.Size = new System.Drawing.Size(109, 28);
            this.btnSequentialSwarm.TabIndex = 1;
            this.btnSequentialSwarm.Text = "Sequential Swarm";
            this.btnSequentialSwarm.UseVisualStyleBackColor = true;
            // 
            // dg1
            // 
            this.dg1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dg1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg1.Location = new System.Drawing.Point(122, 21);
            this.dg1.Margin = new System.Windows.Forms.Padding(2);
            this.dg1.Name = "dg1";
            this.dg1.RowHeadersWidth = 82;
            this.dg1.RowTemplate.Height = 33;
            this.dg1.Size = new System.Drawing.Size(565, 387);
            this.dg1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 437);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Best Result";
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(118, 437);
            this.lblResult.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(35, 13);
            this.lblResult.TabIndex = 4;
            this.lblResult.Text = "label2";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 472);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dg1);
            this.Controls.Add(this.btnSequentialSwarm);
            this.Controls.Add(this.btnParallelSwarms);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dg1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnParallelSwarms;
        private System.Windows.Forms.Button btnSequentialSwarm;
        private System.Windows.Forms.DataGridView dg1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblResult;
    }
}

