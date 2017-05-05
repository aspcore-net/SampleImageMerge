using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageMergeDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Overlay();
            return;
            // Create a new image
            var img = new Bitmap(230, 300);
            Graphics g = Graphics.FromImage(img);
            g.CompositingMode = CompositingMode.SourceOver;

            // Place a.g
            var imgMain = Image.FromFile("Images\\img1.jpg");
            var imgOverlay = Image.FromFile("Images\\overlay.png");
            var finalImage = new Bitmap(imgOverlay.Width, imgOverlay.Height, PixelFormat.Format32bppArgb);
            var graphics = Graphics.FromImage(finalImage);

            g.DrawImage(imgMain, new Point(0, 0));

            // Place b.jpg
            g.DrawImage(imgOverlay, new Point(0, 0));
            // Save changes as output.jpg
            img.Save("Images\\output.png", ImageFormat.Png);

            //string firstText = "Hello";
            //string secondText = "World";

            //PointF firstLocation = new PointF(10f, 10f);
            //PointF secondLocation = new PointF(10f, 50f);

            //string imageFilePath = @"Images\img1.jpg";
            //Bitmap bitmap = (Bitmap)Image.FromFile(imageFilePath);//load the image file

            //using (Graphics graphics = Graphics.FromImage(bitmap))
            //{
            //    using (Font arialFont = new Font("Arial", 10))
            //    {
            //        graphics.DrawString(firstText, arialFont, Brushes.Blue, firstLocation);
            //        graphics.DrawString(secondText, arialFont, Brushes.Red, secondLocation);
            //    }
            //}

            //bitmap.Save("Images\\output.jpg");//save the image file
        }


        public static void DrawString()
        {
            string text2 = "Draw text in a rectangle by passing a RectF to the DrawString method.";
            using (Font font2 = new Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Point))
            {
                Rectangle rect2 = new Rectangle(30, 10, 100, 122);

                // Specify the text is wrapped.
                //TextFormatFlags flags = TextFormatFlags.WordBreak;
                //TextRenderer.DrawText(e.Graphics, text2, font2, rect2, Color.Blue, flags);
                //e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(rect2));

            }
        }

        public static void Overlay()
        {
            // set the light and dark overlay colors
            Color c1 = Color.FromArgb(140, Color.Black);
            Color c2 = Color.FromArgb(140, Color.Black);

            // set up the tile size - this will be 8x8 pixels, with each light/dark square being 4x4 pixels
            int length = 8;
            int halfLength = length / 2;

            using (Bitmap overlay = new Bitmap(length, length, PixelFormat.Format32bppArgb))
            {
                // draw the overlay - this will be a 2 x 2 grid of squares,
                // alternating between colors c1 and c2
                for (int x = 0; x < length; x++)
                {
                    for (int y = 0; y < length; y++)
                    {
                        if ((x < halfLength && y < halfLength) || (x >= halfLength && y >= halfLength))
                            overlay.SetPixel(x, y, c1);
                        else
                            overlay.SetPixel(x, y, c2);
                    }
                }

                // open the source image
                using (Image image = Image.FromFile(@"Images\img1.jpg"))
                using (Graphics graphics = Graphics.FromImage(image))
                {
                    // create a brush from the overlay image, draw over the source image and save to a new image
                    using (Brush overlayBrush = new TextureBrush(overlay))
                    {
                        graphics.FillRectangle(overlayBrush, new Rectangle(new Point(0, 0), image.Size));
                        image.Save(@"Images\img1_temp.jpg");
                    }
                }
            }
        }
    }
}
