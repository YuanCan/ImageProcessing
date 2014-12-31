using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ImageProcessing
{
    public class ImageContainer
    {
        public GrayColorMix[] imageData;
        public Bitmap processedBitmap;
        public bool grayBitmapInit = false;

        private Bitmap grayBitmap;
        private Bitmap originalBitmap;


        private int width;
        private int height;

        public Bitmap GrayBitmap
        {
            get { return grayBitmap; }
        }
        public Bitmap OriginalBitmap
        {
            get { return originalBitmap; }
        }
        public int Width
        {
            get { return width; }
        }
        public int Height
        {
            get { return height; }
        }


        private static ImageContainer instance;

        static public ImageContainer GetInstance()
        {
            if (instance == null)
            {
                instance = new ImageContainer();
            }

            return instance;
        }
        // private constructor for singleton instance
        private ImageContainer()
        {

        }

        public GrayColorMix[] GetBitmapData(string path)
        {
            int testIndex = 0;
            try
            {
                Bitmap bitmap = new Bitmap(path);
                // save the original bitmap
                originalBitmap = bitmap;
                // create the gray bitmap
                grayBitmap = new Bitmap(bitmap.Width, bitmap.Height, originalBitmap.PixelFormat);
                // create process bitmap
                processedBitmap = new Bitmap(bitmap.Width, bitmap.Height, originalBitmap.PixelFormat);
                GrayColorMix[] grayData = new GrayColorMix[bitmap.Width * bitmap.Height];

                width = bitmap.Width;
                height = bitmap.Height;
                Rectangle rect = new Rectangle(0, 0, width, height);
                // lock bitmap 
                BitmapData bitmapData = bitmap.LockBits(rect, ImageLockMode.ReadWrite, bitmap.PixelFormat);
                BitmapData grayBitmapData = grayBitmap.LockBits(rect, ImageLockMode.ReadWrite, bitmap.PixelFormat);
                int length = bitmapData.Stride * height;
                int stride = bitmapData.Stride;
                // create the data container
                byte[] data = new byte[length];
                byte[] grayByteData = new byte[length];
                // get the bitmap data pointer
                IntPtr originalDataPtr = bitmapData.Scan0;
                IntPtr grayDataPtr = grayBitmapData.Scan0;
                if (bitmap.PixelFormat != PixelFormat.Format32bppArgb && bitmap.PixelFormat != PixelFormat.Format24bppRgb)
                {
                    Console.WriteLine("Invalid Pixel format " + bitmap.PixelFormat);
                    imageData = null;
                    grayBitmap = null;
                    originalBitmap = null;
                    return null;
                }
                int offset = bitmap.PixelFormat == PixelFormat.Format32bppArgb ? 4 : 3;
                // copy the bitmap data
                Marshal.Copy(originalDataPtr, data, 0, length);
                int grayDataIndex = 0;
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0, xMax = bitmapData.Width * offset; x < xMax; x += offset)
                    {
                        int index = (y * stride) + x;
                        grayData[grayDataIndex] = new GrayColorMix();
                        if (bitmap.PixelFormat == PixelFormat.Format32bppArgb)
                        {
                            grayData[grayDataIndex].color = ((UInt32)data[index + 3] << 24) + ((UInt32)data[index + 2] << 16) +
                                ((UInt32)data[index + 1] << 8) + (UInt32)data[index];
                            grayData[grayDataIndex].gray = CalculateGray(grayData[grayDataIndex].color, ref grayByteData[index + 3]);
                            UInt32 grayValue = grayData[grayDataIndex].gray;

                            grayByteData[index] = (byte)grayValue;
                            grayByteData[index + 1] = (byte)grayValue;
                            grayByteData[index + 2] = (byte)grayValue;

                        }
                        else if (bitmap.PixelFormat == PixelFormat.Format24bppRgb)
                        {
                            grayData[grayDataIndex].color = ((UInt32)data[index + 2] << 16) + ((UInt32)data[index + 1] << 8) +
                                ((UInt32)data[index]);
                            byte dataTemp = new byte();
                            grayData[grayDataIndex].gray = CalculateGray(grayData[grayDataIndex].color, ref dataTemp);
                            UInt32 grayValue = grayData[grayDataIndex].gray;
                            grayByteData[index] = (byte)grayValue;
                            grayByteData[index + 1] = (byte)grayValue;
                            grayByteData[index + 2] = (byte)grayValue;
                        }

                        grayData[grayDataIndex].x = x;
                        grayData[grayDataIndex].y = y;

                        grayDataIndex++;
                    }
                }
                // copy data
                Marshal.Copy(data, 0, originalDataPtr, length);
                Marshal.Copy(grayByteData, 0, grayDataPtr, length);
                bitmap.UnlockBits(bitmapData);
                grayBitmap.UnlockBits(grayBitmapData);
                imageData = grayData;
                return grayData;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public static UInt32 CalculateGray(UInt32 color, ref byte setAlpha)
        {
            UInt32 gray = (((color & 0x00ff0000) >> 16) * 299 + ((color & 0x0000ff00) >> 8) * 587 +
                (color & 0x000000ff) * 114 + 500) / 1000;

            if (((color & 0xff000000) >> 24) == 0)
            {
                setAlpha = 0;
            }
            else
            {
                setAlpha = (byte)gray;
            }
            return gray;
        }

        public void CreateBitmap(ClusterPoint[] points)
        {
            if (points.Length != width * height)
            {
                Console.WriteLine("Invalid length " + points.Length);
                return;
            }

            if (processedBitmap == null)
            {
                processedBitmap = new Bitmap(width, height, originalBitmap.PixelFormat);
            }
            Rectangle rect = new Rectangle(0, 0, width, height);
            BitmapData data = processedBitmap.LockBits(rect, ImageLockMode.ReadWrite, originalBitmap.PixelFormat);
            int length = data.Stride * height;
            byte[] bytesData = new byte[length];
            // get the byte offset
            int offset = originalBitmap.PixelFormat == PixelFormat.Format32bppArgb ? 4 : 3;
            // copy the bitmap data
            Marshal.Copy(data.Scan0, bytesData, 0, length);
            int dataIndex = 0;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0, xMax = originalBitmap.Width * offset; x < xMax; x += offset)
                {
                    int index = (y * data.Stride) + x;
                    if (originalBitmap.PixelFormat == PixelFormat.Format32bppArgb)
                    {
                        bytesData[index] = (byte)(points[dataIndex].ClusterIndex * 255.0 / (ViewController.GetInstance().categoryNumber - 1));
                        bytesData[index + 1] = (byte)(points[dataIndex].ClusterIndex * 255.0 / (ViewController.GetInstance().categoryNumber - 1));
                        bytesData[index + 2] = (byte)(points[dataIndex].ClusterIndex * 255.0 / (ViewController.GetInstance().categoryNumber - 1));
                        bytesData[index + 3] = 0xff;
                    }
                    else
                    {
                        bytesData[index] = (byte)(points[dataIndex].ClusterIndex * 255.0 / (ViewController.GetInstance().categoryNumber - 1));
                        bytesData[index + 1] = (byte)(points[dataIndex].ClusterIndex * 255.0 / (ViewController.GetInstance().categoryNumber - 1));
                        bytesData[index + 2] = (byte)(points[dataIndex].ClusterIndex * 255.0 / (ViewController.GetInstance().categoryNumber - 1));
                    }
                    dataIndex++;
                }
            }
            Marshal.Copy(bytesData, 0, data.Scan0, length);
            processedBitmap.UnlockBits(data);
        }
    }

    public class GrayColorMix
    {
        public UInt32 color;
        public UInt32 gray;
        public UInt32 grayColor;
        public int x;
        public int y;
    }
}
