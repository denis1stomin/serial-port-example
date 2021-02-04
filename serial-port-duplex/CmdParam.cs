using System.IO.Ports;
using CommandLine;

namespace SerialPortDuplex
{
    public class CmdParam
    {
        [Option('n', "port-name", Required = false, HelpText = "Target serial port name.")]
        public string PortName { get; set; }

        [Option("baud-rate", Required = false, HelpText = "Serial port baud rate.")]
        public string BaudRate { get; set; }

        [Option("parity", Required = false, HelpText = "Serial port parity.")]
        public string Parity { get; set; }

        [Option("data-bits", Required = false, HelpText = "Serial port data bits.")]
        public string DataBits { get; set; }

        [Option("stop-bits", Required = false, HelpText = "Serial port stop bits.")]
        public string StopBits { get; set; }

        [Option("handshake", Required = false, HelpText = "Serial port handshake.")]
        public string Handshake { get; set; }

        [Option('m', "chat-mode", Default = CustomSerialProtocol.Mode.Dec, Required = false, HelpText = "Chat mode. Can be HEX or DEC.")]
        public CustomSerialProtocol.Mode Mode { get; set; }

        [Option("print-sp-options", Default = false, Required = false, HelpText = "Prints detailed serial port options.")]
        public bool PrintSerialPortDetails { get; set; }
    }
}
