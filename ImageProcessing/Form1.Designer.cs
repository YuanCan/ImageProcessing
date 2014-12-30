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
            this.Category = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ImplementFCM = new System.Windows.Forms.Button();
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
            this.ImageArea.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
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
            // Category
            // 
            this.Category.AutoSize = true;
            this.Category.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Category.Location = new System.Drawing.Point(451, 8);
            this.Category.Name = "Category";
            this.Category.Size = new System.Drawing.Size(120, 16);
            this.Category.TabIndex = 4;
            this.Category.Text = "CategoryNumber";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(577, 8);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 5;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // ImplementFCM
            // 
            this.ImplementFCM.Location = new System.Drawing.Point(273, 1);
            this.ImplementFCM.Name = "ImplementFCM";
            this.ImplementFCM.Size = new System.Drawing.Size(75, 23);
            this.ImplementFCM.TabIndex = 6;
            this.ImplementFCM.Text = "FCM";
            this.ImplementFCM.UseVisualStyleBackColor = true;
            this.ImplementFCM.Click += new System.EventHandler(this.ImplementFCM_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 511);
            this.Controls.Add(this.ImplementFCM);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Category);
            this.Controls.Add(this.OriginalImage);
            this.Controls.Add(this.GrayImage);
            this.Controls.Add(this.ImageArea);
            this.Controls.Add(this.OpenImage);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.ImageArea)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OpenImage;
        private System.Windows.Forms.PictureBox ImageArea;
        private System.Windows.Forms.Button GrayImage;
        private System.Windows.Forms.Button OriginalImage;
        private System.Windows.Forms.Label Category;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button ImplementFCM;
    }
}

