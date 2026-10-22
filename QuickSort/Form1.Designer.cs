namespace QuickSort
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
            this.btnSortSequential = new System.Windows.Forms.Button();
            this.btnSortSequentialLarge = new System.Windows.Forms.Button();
            this.btnSortParallelLarge = new System.Windows.Forms.Button();
            this.btnLibrarySort = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSortSequential
            // 
            this.btnSortSequential.AutoSize = true;
            this.btnSortSequential.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSortSequential.Location = new System.Drawing.Point(61, 22);
            this.btnSortSequential.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSortSequential.Name = "btnSortSequential";
            this.btnSortSequential.Size = new System.Drawing.Size(89, 23);
            this.btnSortSequential.TabIndex = 0;
            this.btnSortSequential.Text = "Sort Sequential";
            this.btnSortSequential.UseVisualStyleBackColor = true;
            this.btnSortSequential.Click += new System.EventHandler(this.btnSortSequential_Click);
            // 
            // btnSortSequentialLarge
            // 
            this.btnSortSequentialLarge.AutoSize = true;
            this.btnSortSequentialLarge.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSortSequentialLarge.Location = new System.Drawing.Point(46, 56);
            this.btnSortSequentialLarge.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSortSequentialLarge.Name = "btnSortSequentialLarge";
            this.btnSortSequentialLarge.Size = new System.Drawing.Size(119, 23);
            this.btnSortSequentialLarge.TabIndex = 1;
            this.btnSortSequentialLarge.Text = "Sort Sequential Large";
            this.btnSortSequentialLarge.UseVisualStyleBackColor = true;
            this.btnSortSequentialLarge.Click += new System.EventHandler(this.btnSortSequentialLarge_Click);
            // 
            // btnSortParallelLarge
            // 
            this.btnSortParallelLarge.AutoSize = true;
            this.btnSortParallelLarge.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSortParallelLarge.Location = new System.Drawing.Point(52, 93);
            this.btnSortParallelLarge.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSortParallelLarge.Name = "btnSortParallelLarge";
            this.btnSortParallelLarge.Size = new System.Drawing.Size(103, 23);
            this.btnSortParallelLarge.TabIndex = 2;
            this.btnSortParallelLarge.Text = "Sort Parallel Large";
            this.btnSortParallelLarge.UseVisualStyleBackColor = true;
            this.btnSortParallelLarge.Click += new System.EventHandler(this.btnSortParallelLarge_Click);
            // 
            // btnLibrarySort
            // 
            this.btnLibrarySort.Location = new System.Drawing.Point(55, 131);
            this.btnLibrarySort.Name = "btnLibrarySort";
            this.btnLibrarySort.Size = new System.Drawing.Size(99, 23);
            this.btnLibrarySort.TabIndex = 3;
            this.btnLibrarySort.Text = "Library Sort";
            this.btnLibrarySort.UseVisualStyleBackColor = true;
            this.btnLibrarySort.Click += new System.EventHandler(this.btnLibrarySort_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(206, 185);
            this.Controls.Add(this.btnLibrarySort);
            this.Controls.Add(this.btnSortParallelLarge);
            this.Controls.Add(this.btnSortSequentialLarge);
            this.Controls.Add(this.btnSortSequential);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Quick Sort Parallelization";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSortSequential;
        private System.Windows.Forms.Button btnSortSequentialLarge;
        private System.Windows.Forms.Button btnSortParallelLarge;
        private System.Windows.Forms.Button btnLibrarySort;
    }
}

