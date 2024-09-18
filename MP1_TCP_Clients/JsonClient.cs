using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MP1_TCP_Clients
{
    public class JsonClient
    {
        public string ip = "127.0.0.1";
        public int port = 7;

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
                    string command = Console.ReadLine();

                    Console.WriteLine("Enter Value1:");
                    string inputValue1 = Console.ReadLine();
                    int value1 = int.Parse(inputValue1);

                    Console.WriteLine("Enter Value2::");
                    string inputValue2 = Console.ReadLine();
                    int value2 = int.Parse(inputValue2);

                    // Send Values to Client
                    JsonCommand commandObj = new JsonCommand()
                    {
                        Method = command,
                        Value1 = value1,
                        Value2 = value2
                    };

                    string commandAsJson = JsonSerializer.Serialize<JsonCommand>(commandObj);

                    Console.WriteLine(commandAsJson); // Use while dev

                    writer.WriteLine(commandAsJson);
                    writer.Flush();
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
