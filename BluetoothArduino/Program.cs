using System;
using InTheHand.Net.Sockets;
using System.IO;
using InTheHand.Net.Bluetooth;
using System.Runtime.Intrinsics.Arm;

namespace BluetoothArduino
{
    class Program
    {
        static void Main(string[] args)
        {
            string arduinoBluetoothAddress = "98D351FD7034";//Device Address

            BluetoothClient client = new BluetoothClient();
            BluetoothClient newClient = new BluetoothClient();
            var devices = client.DiscoverDevices();
            foreach (BluetoothDeviceInfo device in devices)
            {
                Console.WriteLine(device.DeviceAddress.ToString());
                Console.WriteLine(device.DeviceAddress);
                Console.WriteLine(device.DeviceName);
                Console.WriteLine("");
                if (device.DeviceAddress.ToString() == arduinoBluetoothAddress)
                {
                    try
                    {
                        if (newClient.Connected)
                        {
                            newClient.Close();
                        }
                        newClient.Connect(device.DeviceAddress, BluetoothService.SerialPort);
                    }
                    catch(Exception e) 
                    {
                        throw new Exception(e.Message);
                    }

                    break;
                }
            }

            if (newClient.Connected)
            {
                Console.WriteLine("Połączono");
                Stream stream = newClient.GetStream();
                while(true)
                {
                    Console.Write("Przekaż:\n\t");
                    string message = Console.ReadLine();
                    byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                    stream.Write(data, 0, data.Length);
                    Console.WriteLine("Przekazano");
                }
            }
            else
            {
                Console.WriteLine("Nie można połączyć!");
            }
        }
    }
}