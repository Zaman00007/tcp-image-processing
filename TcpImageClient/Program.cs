using System;
using System.IO;
using System.Net.Sockets;

class TcpImageClient
{
    static void Main(string[] args)
    {
        string imagePath = "input_image.jpg"; 

        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Image file not found!");
            return;
        }

        byte[] imageBytes = File.ReadAllBytes(imagePath); 

        using (TcpClient client = new TcpClient("127.0.0.1", 5000)) 
        using (NetworkStream stream = client.GetStream())
        {
            Console.WriteLine("Connected to the server. Sending image...");

            
            byte[] lengthBuffer = BitConverter.GetBytes(imageBytes.Length);
            stream.Write(lengthBuffer, 0, lengthBuffer.Length);

            
            stream.Write(imageBytes, 0, imageBytes.Length);
            Console.WriteLine("Image sent successfully.");
        }
    }
}
