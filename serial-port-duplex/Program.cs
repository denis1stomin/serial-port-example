using System;
using System.Collections.Generic;
using CommandLine;

namespace SerialPortDuplex
{
    public class Program
    {
        static void Main(string[] args)
        {
            var parser = new Parser(x => {
                x.CaseSensitive = false;
                x.IgnoreUnknownArguments = true;
                x.AutoHelp = true;
            });

            parser.ParseArguments<CmdParam>(args)
                .WithParsed<CmdParam>(MainInner)
                .WithNotParsed(NotParsed);
        }

        static void MainInner(CmdParam param)
        {
            if (param.PrintSerialPortDetails)
            {
                TextSerialProtocol.PrintAvailableSerialPortOptions();
                Environment.Exit(0);
            }

            Console.WriteLine($"Starting serial port chat with parameters:");
            Console.WriteLine($"    Port name = {param.PortName}");
            Console.WriteLine($"    Chat mode = {param.Mode}");
            Console.WriteLine($"Type {RoboChat.ExitWord} to exit..");
            Console.WriteLine("");

            //ISerialProtocol protocol = new TextSerialProtocol(param);
            ISerialProtocol protocol = new CustomSerialProtocol(param);
            using (var chat = new RoboChat(protocol))
            {
                chat.Start();
            }
        }

        static void NotParsed(IEnumerable<Error> err)
        {
            Console.WriteLine("sp-duplex -n <port-name>");
            Console.WriteLine("Type --help to get full help message.");

            Environment.Exit(1);
        }
    }
}
