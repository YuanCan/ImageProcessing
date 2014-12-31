using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ImageProcessing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void OpenImage_Click(object sender, EventArgs e)
        {
            string myname;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            // set the default path
            openFileDialog.InitialDirectory = Application.ExecutablePath;
            // set the dialog filter type
            openFileDialog.Filter = "*.jpg,*.jpeg,*.bmp,*.jif,*.ico,*.png,*.tif,*.wmf|*.jpg;*.jpeg;*.bmp;*.gif;*.ico;*.png;*.tif;*.wmf";
            // display dialog
            openFileDialog.ShowDialog();
            myname = openFileDialog.FileName;
            if (ViewController.GetInstance() == null)
            {
                return;
            }

            Image data = ViewController.GetInstance().OnClickOpenFile(myname);
            this.ImageArea.Image = data;

            this.ClientSize = new System.Drawing.Size(Math.Max(ClientSize.Width, data.Width + 20), Math.Max(ClientSize.Height, data.Height + 50));
        }

        private void ImageArea_Click(object sender, EventArgs e)
        {

        }

        private void GrayImage_Click(object sender, EventArgs e)
        {
            Image data = ViewController.GetInstance().OnClickGrayImage();
            this.ImageArea.Image = data;
        }

        private void OriginalImage_Click(object sender, EventArgs e)
        {
            Image data = ViewController.GetInstance().OnClickOriginal();
            this.ImageArea.Image = data;
        }

        private void ImplementFCM_Click(object sender, EventArgs e)
        {
            CMeansAlgorithm processing = new CMeansAlgorithm();
            processing.PrepareForRun(ViewController.GetInstance().categoryNumber);
            processing.Run(Math.Pow(10, -5),ViewController.GetInstance().iterateNumber);
            ImageContainer.GetInstance().CreateBitmap(processing.Points);
            this.ImageArea.Image = ImageContainer.GetInstance().processedBitmap;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(textBox1.Text, out ViewController.GetInstance().categoryNumber);
        }

        private void IterateValue_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(IterateValue.Text, out ViewController.GetInstance().iterateNumber);
        }
    }
}
