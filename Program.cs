using System;
using System.IO;
using System.Drawing;

namespace ImpulseViewConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            Bitmap input, result;
            int k = 1,
                max = 0,
                summ = 0;
            byte a, r, g, b;

            string path = @"..\..\..\inputs";

            string[] inputs = Directory.GetFiles(path);

            foreach (string s in inputs)
            {
                input = new Bitmap(s, true);

                int width = input.Width;
                int height = input.Height;

                result = new Bitmap(width, height);

                int[,] resultPixels = new int[width, height],
                       avarageColor = new int[width, height];

                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        Color pixelColor = input.GetPixel(x, y);

                        r = pixelColor.R;
                        g = pixelColor.G;
                        b = pixelColor.B;

                        avarageColor[x, y] = (r + g + b) / 3;
                    }
                }

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        if (avarageColor[i, j] > max)
                        {
                            max = avarageColor[i, j];
                        }
                    }
                    for (int j = 0; j < height; j++)
                    {
                        summ += avarageColor[i, j];
                        if (summ >= k * max)
                        {
                            resultPixels[i, j] = 255;
                            summ -= k * max;
                        }
                        else
                        {
                            resultPixels[i, j] = 0;
                        }
                    }
                    summ = 0;
                    max = 0;
                }

                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        Color pixelColor = input.GetPixel(x, y);

                        a = pixelColor.A;
                        int res = resultPixels[x, y];

                        Color newColor = Color.FromArgb(a, res, res, res);

                        result.SetPixel(x, y, newColor);
                    }
                }

                string fileName = Path.GetFileName(s);

                result.Save(@"..\..\..\results\" + fileName, System.Drawing.Imaging.ImageFormat.Bmp);

            }
            Console.WriteLine("DONE");
            Console.ReadLine();
        }
    }
}
