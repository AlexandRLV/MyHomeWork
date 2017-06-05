using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace MyBMP
{
    class MyBMP
    {
        public ushort bfType;
        public uint bfSize;
        public ushort bfReserved1;
        public ushort bfReserved2;
        public uint bfOffBits;
        public uint biSize;
        public int biWidth;
        public int biHeight;
        public ushort biPlanes;
        public ushort biBitCount;
        public uint biCompression;
        public uint biSizeImage;
        public int biXPelsPerMeter;
        public int biYPelsPerMeter;
        public uint biClrUsed;
        public uint biClrImportant;
        public byte[] pixelData;
        public byte[] data;
        public MyColor[,] picture;

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("bfType: ");
            builder.AppendLine(bfType.ToString());
            builder.Append("bfSize: ");
            builder.AppendLine(bfSize.ToString());
            builder.Append("bfReserved1: ");
            builder.AppendLine(bfReserved1.ToString());
            builder.Append("bfReserved2: ");
            builder.AppendLine(bfReserved2.ToString());
            builder.Append("bfOffBits: ");
            builder.AppendLine(bfOffBits.ToString());
            builder.Append("biSize: ");
            builder.AppendLine(biSize.ToString());
            builder.Append("biWidth: ");
            builder.AppendLine(biWidth.ToString());
            builder.Append("biHeight: ");
            builder.AppendLine(biHeight.ToString());
            builder.Append("biPlanes: ");
            builder.AppendLine(biPlanes.ToString());
            builder.Append("biBitCount: ");
            builder.AppendLine(biBitCount.ToString());
            builder.Append("biCompression: ");
            builder.AppendLine(biCompression.ToString());
            builder.Append("biSizeImage: ");
            builder.AppendLine(biSizeImage.ToString());
            builder.Append("biXPelsPerMeter: ");
            builder.AppendLine(biXPelsPerMeter.ToString());
            builder.Append("biYPelsPerMeter: ");
            builder.AppendLine(biYPelsPerMeter.ToString());
            builder.Append("biClrUsed: ");
            builder.AppendLine(biClrUsed.ToString());
            builder.Append("biClrImportant: ");
            builder.AppendLine(biClrImportant.ToString());
            return builder.ToString();
        }

        public void ReadData(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                byte[] arr;
                arr = new byte[fs.Length];
                fs.Read(arr, 0, arr.Length);
                this.bfType = BitConverter.ToUInt16(arr, 0); //Encoding.ASCII.GetString(arr, 0, 2); //
                this.bfSize = BitConverter.ToUInt32(arr, 2);
                this.bfReserved1 = BitConverter.ToUInt16(arr, 6);
                this.bfReserved2 = BitConverter.ToUInt16(arr, 8);
                this.bfOffBits = BitConverter.ToUInt32(arr, 10);
                this.biSize = BitConverter.ToUInt32(arr, 14);
                this.biWidth = BitConverter.ToInt32(arr, 18);
                this.biHeight = BitConverter.ToInt32(arr, 22);
                this.biPlanes = BitConverter.ToUInt16(arr, 26);
                this.biBitCount = BitConverter.ToUInt16(arr, 28);
                this.biCompression = BitConverter.ToUInt32(arr, 30);
                this.biSizeImage = BitConverter.ToUInt32(arr, 34);
                this.biXPelsPerMeter = BitConverter.ToInt32(arr, 38);
                this.biYPelsPerMeter = BitConverter.ToInt32(arr, 42);
                this.biClrUsed = BitConverter.ToUInt32(arr, 46);
                this.biClrImportant = BitConverter.ToUInt32(arr, 50);
                this.pixelData = arr.Skip(int.Parse((this.bfOffBits).ToString())).ToArray();
                this.data = new byte[arr.Length];
                arr.CopyTo(this.data, 0);
                DataToArray();
            }
        }

        public void WriteInFile(string path)
        {
            PixelsToArray();
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                this.pixelData.CopyTo(this.data, this.bfOffBits);
                fs.Write(this.data, 0, this.data.Length);
            }
        }

        public void PixelsToArray()
        {
            List<byte> pixels = new List<byte>();
            byte[] b = BitConverter.GetBytes(this.bfType);
            var pixel = b.Concat(BitConverter.GetBytes(bfSize));
            b = BitConverter.GetBytes(bfReserved1);
            pixel = pixel.Concat(b);
            b = BitConverter.GetBytes(bfReserved2);
            pixel = pixel.Concat(b);
            b = BitConverter.GetBytes(bfOffBits);
            pixel = pixel.Concat(b);
            b = BitConverter.GetBytes(biSize);
            pixel = pixel.Concat(b);
            b = BitConverter.GetBytes(biWidth);
            pixel = pixel.Concat(b);
            b = BitConverter.GetBytes(biHeight);
            pixel = pixel.Concat(b);
            b = BitConverter.GetBytes(biPlanes);
            pixel = pixel.Concat(b);
            b = BitConverter.GetBytes(biBitCount);
            pixel = pixel.Concat(b);
            b = BitConverter.GetBytes(biCompression);
            pixel = pixel.Concat(b);
            b = BitConverter.GetBytes(biSizeImage);
            pixel = pixel.Concat(b);
            b = BitConverter.GetBytes(biXPelsPerMeter);
            pixel = pixel.Concat(b);
            b = BitConverter.GetBytes(biYPelsPerMeter);
            pixel = pixel.Concat(b);
            b = BitConverter.GetBytes(biClrUsed);
            pixel = pixel.Concat(b);
            b = BitConverter.GetBytes(biClrImportant);
            pixel = pixel.Concat(b);
            int n = this.picture.GetLength(1);
            int m = this.picture.GetLength(0);
            for (int i = 0; i <n; i++)
            {                
                for (int j = 0; j < m; j++)
                {
                    MyColor c = this.picture[j, i];
                    pixels.Add(c.Blue);
                    pixels.Add(c.Green);
                    pixels.Add(c.Red);
                }
                while (pixels.Count % 4 != 0)
                {
                    pixels.Add(0);
                }
            }
            this.pixelData = pixels.ToArray();
            pixel.ToArray().CopyTo(this.data, 0);
        }

        public void Transpose()
        {
            MyColor[,] pixels = new MyColor[this.biHeight, this.biWidth];

            for (int i = 0; i < this.biHeight; i++)
            {
                for (int j = 0; j < this.biWidth; j++)
                {
                    pixels[i, j] = this.picture[this.biWidth-j-1, this.biHeight-i-1];
                }
            }
            this.picture = pixels;
            int x = this.biWidth;
            this.biWidth = this.biHeight;
            this.biHeight = x;
        }

        public void DataToArray()
        {
            this.picture = new MyColor[this.biWidth, this.biHeight];
            int lineLen = this.pixelData.Length / this.biHeight;
            for (int i = 0; i < this.biHeight; i++)
            {
                int j = 0;
                int pos = 0;
                while (j < this.biWidth)
                {
                    MyColor c = new MyColor();
                    c.Blue = this.pixelData[pos+lineLen*i];
                    pos++;
                    c.Green = this.pixelData[pos + lineLen * i];
                    pos++;
                    c.Red = this.pixelData[pos + lineLen * i];
                    pos++;
                    this.picture[j, i] = c;
                    j++;
                }
            }
        }

        public void InverseColors()
        {
            foreach (MyColor c in picture)
            {
                c.Inverse();
            }
        }

        public void WriteColors()
        {
            for (int i = this.picture.GetLength(1) - 1; i >= 0; i--)
            {
                for (int j = 0; j < this.picture.GetLength(0); j++)
                {
                    Console.Write(this.picture[j, i] + " | ");
                }
                Console.WriteLine();
            }
        }

        public void Mirror()
        {
            MyColor[,] pixels = new MyColor[this.biWidth, this.biHeight];
            for (int i = 0; i < this.biWidth; i++)
            {
                for (int j = 0; j < this.biHeight; j++)
                {
                    pixels[i, j] = this.picture[this.biWidth - i - 1, j];
                }
            }
            this.picture = pixels;
        }

        public void FillSpace(int x1, int y1, int x2, int y2, byte red, byte green, byte blue)
        {
            if ((x1 < x2) && (x2 < picture.GetLength(0)) && (y1 < y2) && (y2 < picture.GetLength(1)))
            {
                for (int i = x1; i < x2; i++)
                {
                    for (int j = y1; j < y2; j++)
                    {
                        picture[i, j].Red = red;
                        picture[i, j].Green = green;
                        picture[i, j].Blue = blue;
                    }
                }
            }
        }

        public void XOR(MyBMP b)
        {
            for (int i = 0; i < this.pixelData.Length; i++)
            {
                this.pixelData[i] = Convert.ToByte(this.pixelData[i] ^ b.pixelData[i]);
            }
            DataToArray();
        }
    }

    class MyColor
    {
        public byte Red;
        public byte Green;
        public byte Blue;

        public void Inverse()
        {
            Red = (byte)(255 - Red);
            Green = (byte)(255 - Green);
            Blue = (byte)(255 - Blue);
        }

        public override string ToString()
        {
            return String.Format($"{Red} {Green} {Blue}");
        }
    }

    class Program
    {
        public static void Inverse(string path)
        {
            MyBMP bmp = new MyBMP();
            bmp.ReadData(path);
            Console.WriteLine(bmp);
            bmp.InverseColors();
            bmp.WriteInFile("Inversed.bmp");
            Console.WriteLine();
        }

        public static void Transpose(string path)
        {
            MyBMP bmp = new MyBMP();
            bmp.ReadData(path);
            bmp.Transpose();
            bmp.WriteInFile("Transposed.bmp");
        }

        public static void Mirror(string path)
        {
            MyBMP bmp = new MyBMP();
            bmp.ReadData(path);
            bmp.Mirror();
            bmp.WriteInFile("Mirrored.bmp");
        }

        public static void XOR(string path1, string path2)
        {
            MyBMP bmp1 = new MyBMP();
            bmp1.ReadData(path1);
            MyBMP bmp2 = new MyBMP();
            bmp2.ReadData(path2);
            MyBMP bmp = new MyBMP();
            bmp.ReadData(path1);
            bmp.XOR(bmp2);
            bmp.WriteInFile("XORed.bmp");
            bmp.XOR(bmp1);
            bmp.WriteInFile("XORed1.bmp");
            bmp = new MyBMP();
            bmp.ReadData(path1);
            bmp.XOR(bmp2);
            bmp.XOR(bmp2);
            bmp.WriteInFile("XORed2.bmp");
        }

        public static void FillSpace(string path)
        {
            MyBMP bmp = new MyBMP();
            bmp.ReadData(path);
            bmp.FillSpace(10, 10, 300, 200, 0, 0, 0);
            bmp.WriteInFile("Filled1.bmp");
        }

        static void Main(string[] args)
        {
            Inverse("parrots.bmp");
            Transpose("parrots.bmp");
            Mirror("parrots.bmp");
            XOR("parrots.bmp", "metallica.bmp");
            FillSpace("parrots.bmp");
        }
    }
}
