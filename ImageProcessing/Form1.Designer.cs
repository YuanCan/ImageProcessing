namespace ImageProcessing
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
            this.OpenImage = new System.Windows.Forms.Button();
            this.ImageArea = new System.Windows.Forms.PictureBox();
            this.GrayImage = new System.Windows.Forms.Button();
            this.OriginalImage = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ImageArea)).BeginInit();
            this.SuspendLayout();
            // 
            // OpenImage
            // 
            this.OpenImage.Location = new System.Drawing.Point(-2, 1);
            this.OpenImage.Name = "OpenImage";
            this.OpenImage.Size = new System.Drawing.Size(80, 23);
            this.OpenImage.TabIndex = 0;
            this.OpenImage.Text = "OpenImage";
            this.OpenImage.UseVisualStyleBackColor = true;
            this.OpenImage.Click += new System.EventHandler(this.OpenImage_Click);
            // 
            // ImageArea
            // 
            this.ImageArea.InitialImage = null;
            this.ImageArea.Location = new System.Drawing.Point(12, 30);
            this.ImageArea.Name = "ImageArea";
            this.ImageArea.Size = new System.Drawing.Size(844, 461);
            this.ImageArea.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ImageArea.TabIndex = 1;
            this.ImageArea.TabStop = false;
            this.ImageArea.Click += new System.EventHandler(this.ImageArea_Click);
            // 
            // GrayImage
            // 
            this.GrayImage.Location = new System.Drawing.Point(85, 1);
            this.GrayImage.Name = "GrayImage";
            this.GrayImage.Size = new System.Drawing.Size(75, 23);
            this.GrayImage.TabIndex = 2;
            this.GrayImage.Text = "GrayImage";
            this.GrayImage.UseVisualStyleBackColor = true;
            this.GrayImage.Click += new System.EventHandler(this.GrayImage_Click);
            // 
            // OriginalImage
            // 
            this.OriginalImage.Location = new System.Drawing.Point(167, 0);
            this.OriginalImage.Name = "OriginalImage";
            this.OriginalImage.Size = new System.Drawing.Size(100, 24);
            this.OriginalImage.TabIndex = 3;
            this.OriginalImage.Text = "OriginalImage";
            this.OriginalImage.UseVisualStyleBackColor = true;
            this.OriginalImage.Click += new System.EventHandler(this.OriginalImage_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 511);
            this.Controls.Add(this.OriginalImage);
            this.Controls.Add(this.GrayImage);
            this.Controls.Add(this.ImageArea);
            this.Controls.Add(this.OpenImage);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.ImageArea)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button OpenImage;
        private System.Windows.Forms.PictureBox ImageArea;
        private System.Windows.Forms.Button GrayImage;
        private System.Windows.Forms.Button OriginalImage;
    }
}

