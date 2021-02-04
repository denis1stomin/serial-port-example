using System;
using System.Collections.Generic;
using CommandLine;

namespace SerialPortDuplex
{
    class Program
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
            Console.WriteLine($"Started serial port terminal with parameters:");
            Console.WriteLine($"    Port name = {param.Port}");
            Console.WriteLine($"    Chat mode = {param.Mode}");
        }

        static void NotParsed(IEnumerable<Error> err)
        {
            Console.WriteLine("spduplex -p <port-name> [--hex]");
            Console.WriteLine("Type --help to get full help message.");

            Environment.Exit(1);
        }
    }
}
