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
    }
}
