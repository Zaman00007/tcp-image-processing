using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing; 

class TcpImageServer
{
    static void Main(string[] args)
    {
        TcpListener listener = new TcpListener(IPAddress.Any, 5000);
        listener.Start();
        Console.WriteLine("Server is running...");

        while (true)
        {
            using (TcpClient client = listener.AcceptTcpClient())
            using (NetworkStream stream = client.GetStream())
            {
                Console.WriteLine("Client connected.");

                
                byte[] lengthBuffer = new byte[4];
                stream.Read(lengthBuffer, 0, lengthBuffer.Length);
                int imageLength = BitConverter.ToInt32(lengthBuffer, 0);

                
                byte[] imageBytes = new byte[imageLength];
                stream.Read(imageBytes, 0, imageLength);
                Console.WriteLine("Image received. Processing...");

                
                using (Image<Rgba32> image = Image.Load<Rgba32>(new MemoryStream(imageBytes)))
                {
                    
                    image.Mutate(x => x.Invert());

                    
                    image.Save("output_image.jpg");
                    Console.WriteLine("Image processed and saved as output_image.jpg.");
                }
            }
        }
    }
}
