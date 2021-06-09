using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginFramework;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MakeProperties
{
    //image color convert to grayScale
    public class MakeGrayScale : IFilter
    {
        public string Name => "Make GrayScale";

        public Image RunPlugin(Image src)
        {
            var bitmap = new Bitmap(src);

            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    Color c = bitmap.GetPixel(i, j);

                    //Apply conversion equation
                    byte gray = (byte)(.21 * c.R + .71 * c.G + .071 * c.B);

                    //Set the color of this pixel
                    bitmap.SetPixel(i, j, Color.FromArgb(gray, gray, gray));
                }
            }


            return bitmap;

        }
    }

    
    public class Resize : IFilter
    {

        public string Name => "Resize Image";
        public Image RunPlugin(Image src)
        {
            int resizedW = 100;
            int resizedH = 100;

            Bitmap bmp = new Bitmap(resizedW, resizedH);
            Graphics graphic = Graphics.FromImage((Image)bmp);
            graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphic.DrawImage(src, 0, 0, resizedW, resizedH);
            graphic.Dispose();
            return (Image)bmp;
        }


    }


    public class MakeBlur : IFilter
    {
        public string Name => "Make Blur";

        public Image RunPlugin(Image src)
        {
            var bitmap = new Bitmap(src);

            for (int x = 1; x < bitmap.Width; x++)
            {
                for (int y = 1; y < bitmap.Height; y++)
                {
                    try
                    {
                        Color prevX = bitmap.GetPixel(x - 1, y);
                        Color nextX = bitmap.GetPixel(x + 1, y);
                        Color prevY = bitmap.GetPixel(x, y - 1);
                        Color nextY = bitmap.GetPixel(x, y + 1);

                        int avgR = (int)((prevX.R + nextX.R + prevY.R + nextY.R) / 4);
                        int avgG = (int)((prevX.G + nextX.G + prevY.G + nextY.G) / 4);
                        int avgB = (int)((prevX.B + nextX.B + prevY.B + nextY.B) / 4);

                        bitmap.SetPixel(x, y, Color.FromArgb(avgR, avgG, avgB));

                    }
                    catch (Exception) { }
                }
            }


            return bitmap;

        }

       
    }

    public class MakeBlue : IFilter
    {
        public string Name => "Make Blue";



        public Image RunPlugin(Image src)
        {
            var bitmap = new Bitmap(src);
            for (int row = 0; row < bitmap.Height; ++row)
            {
                for (int col = 0; col < bitmap.Width; ++col)
                {
                    Color color = bitmap.GetPixel(col, row);
                    if (color.B > 0)
                    {
                        color = Color.FromArgb(color.A, color.R, color.G, 255);
                    }
                    bitmap.SetPixel(col, row, color);
                }
            }

            return bitmap;

        }
    }

    public class MakeGreen : IFilter
    {
        public string Name => "Make Green";
        public Image RunPlugin(Image src)
        {
            var bitmap = new Bitmap(src);
            for (int row = 0; row < bitmap.Height; ++row)
            {
                for (int col = 0; col < bitmap.Width; ++col)
                {
                    Color color = bitmap.GetPixel(col, row);
                    if (color.G > 0)
                    {
                        color = Color.FromArgb(color.A, color.R, 255, color.G);
                    }
                    bitmap.SetPixel(col, row, color);
                }
            }

            return bitmap;

        }
    }

}
