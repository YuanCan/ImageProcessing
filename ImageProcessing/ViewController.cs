﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageProcessing
{
    public class ViewController
    {
        public int categoryNumber;

        private static ViewController instance;

        static public ViewController GetInstance()
        {
            if (instance == null)
            {
                instance = new ViewController();
            }

            return instance;
        }

        public Image OnClickOpenFile(string path)
        {
            if (ImageContainer.GetInstance() == null)
            {
                return null;
            }

            ImageContainer container = ImageContainer.GetInstance();
            container.GetBitmapData(path);
            return container.OriginalBitmap;
        }

        public Image OnClickOriginal()
        {
            ImageContainer container = ImageContainer.GetInstance();
            return container.OriginalBitmap;
        }

        public Image OnClickGrayImage()
        {
            ImageContainer container = ImageContainer.GetInstance();
            return container.GrayBitmap;
        }
    }
}
