using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MP1_TCP_Clients
{
    public class StringClient
    {
        public string ip = "127.0.0.1";
        public int port = 21;

        public void run()
        {
            Console.WriteLine("TCP Client");
            Console.WriteLine($"Connecting to {ip}:{port}");

            bool keepSending = true;

            try
            {
                TcpClient socket = new TcpClient(ip, port);

                Console.WriteLine($"Connected to {ip}:{port}");

                NetworkStream ns = socket.GetStream();
                StreamReader reader = new StreamReader(ns);
                StreamWriter writer = new StreamWriter(ns);

                while (keepSending)
                {
                    Console.WriteLine("Enter Command:");

                    string message = Console.ReadLine();

                    writer.WriteLine(message);
                    writer.Flush();

                    string response = reader.ReadLine();

                    // If response is as expected, continue
                    if (response == "Input numbers")
                    {
                        Console.WriteLine("Enter Value1:");
                        string value1 = Console.ReadLine();

                        Console.WriteLine("Enter Value2::");
                        string value2 = Console.ReadLine();

                        // Send Values to Client
                        writer.WriteLine($"{value1} {value2}");
                        writer.Flush();

                        response = reader.ReadLine();
                    }

                    Console.WriteLine(response);

                    if (message.ToLower() == "stop")
                    {
                        keepSending = false;
                    }
                }

                socket.Close();

            }
            catch (SocketException ex)
            {
                Console.WriteLine($"No server found on {ip}:{port}");
            }
        }
    }
}
