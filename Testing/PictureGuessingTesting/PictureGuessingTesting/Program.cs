using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace PictureGuessingTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Step to convert the image to the Byte Array
            string imagePath = @"C:\Users\chris\Desktop\hauli.bmp";
            byte[] result = ConvertImageToByteArray(imagePath);
            using (StreamWriter sw = new StreamWriter(@"C:\Users\chris\Desktop\hauli.txt"))
            {
                foreach (byte b in result)
                {
                    sw.WriteLine(b);
                }
            }
           
        } 

        public static byte[] ConvertImageToByteArray(string imagePath)
        {
            byte[] imageByteArray = null;
            FileStream fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
            using (BinaryReader reader = new BinaryReader(fileStream))
            {
                imageByteArray = new byte[reader.BaseStream.Length];
                for (int i = 0; i < reader.BaseStream.Length; i++)
                    imageByteArray[i] = reader.ReadByte();
            }
            return imageByteArray;
        }

        public static byte[] test()
        {
            Image img = Image.FromFile(@"C:\Lenna.jpg");
            byte[] arr;
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                arr =  ms.ToArray();
            });
        }
    }
}
