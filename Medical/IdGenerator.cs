using System.Drawing;
using System.IO;

using System.Windows;
using System.Windows.Media.Imaging;
using ZXing;
using ZXing.Windows.Compatibility;

namespace Medical
{
    class IdGenerator
    {
        public int generateid(string filepath)
        {
            int id = 0;
            //testing the file reading
            try
            {
                string file = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "genIds/"+filepath);
                using (StreamReader reader = new StreamReader(file))
                {
                    string line;
                    line = reader.ReadLine();
                    id = int.Parse(line);
                    reader.Close();

                }
                using (StreamWriter writer = new StreamWriter(file)) {
                    writer.WriteLine(id+1);
                    writer.Close();
                }
            }
            catch (Exception ex)
            {
                id = 0;
                MessageBox.Show(ex.Message, "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
           
            return id;

        }

        public BitmapImage GenerateQRCode(string data)
        {
            try
            {
                BarcodeWriter barcodeWriter = new BarcodeWriter
                {
                    Format = BarcodeFormat.QR_CODE,  // Choose barcode format
                    Options = new ZXing.Common.EncodingOptions
                    {
                        Width = 120,
                        Height = 72
                    }
                };
                //using (Bitmap barcodeBitmap = barcodeWriter.Write(data))
                //{
                //    barcodeBitmap.Save(@"C:\Users\PC\source\repos\Clinic_Eye\Medical\bin\Debug\net8.0-windows\barcode.png", System.Drawing.Imaging.ImageFormat.Png);
                //}

                Bitmap barcodeBitmap = barcodeWriter.Write(data);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    // Save the Bitmap to the MemoryStream in a format like PNG
                    barcodeBitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);

                    // Reset the position of the stream to the beginning
                    memoryStream.Seek(0, SeekOrigin.Begin);

                    // Create a BitmapImage from the MemoryStream
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = memoryStream;
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.EndInit();

                    return bitmapImage;
                }
                //BitmapImage barcodeImage = new BitmapImage();
                //barcodeImage.BeginInit();
                //barcodeImage.UriSource = new Uri(@"C:\Users\PC\source\repos\Clinic_Eye\Medical\bin\Debug\net8.0-windows\barcode.png", UriKind.Absolute);
                //barcodeImage.EndInit();
                //return barcodeImage;

            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "naruto", MessageBoxButton.OK, MessageBoxImage.Information);
              
                return null;
            }

        }

    }
}
