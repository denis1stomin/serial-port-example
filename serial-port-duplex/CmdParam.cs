using System.IO.Ports;
using CommandLine;

namespace SerialPortDuplex
{
    class CmdParam
    {
        [Option('n', "port-name", Required = true, HelpText = "Target serial port name.")]
        public string Port { get; set; }

        [Option('b', "baud-rate", Required = false, HelpText = "Serial port baud rate.")]
        public int BaudRate { get; set; }

        [Option('p', "parity", Required = false, HelpText = "Serial port parity.")]
        public Parity Parity { get; set; }

        [Option('d', "data-bits", Required = false, HelpText = "Serial port data bits.")]
        public int DataBits { get; set; }

        [Option('s', "stop-bits", Required = false, HelpText = "Serial port stop bits.")]
        public StopBits StopBits { get; set; }

        [Option('h', "handshake", Required = false, HelpText = "Serial port handshake.")]
        public Handshake Handshake { get; set; }

        [Option('m', "chat-mode", Default = ChatMode.Hex, Required = false, HelpText = "Chat mode. Can be HEX or DEC.")]
        public ChatMode Mode { get; set; }

        public enum ChatMode
        {
            Hex,
            Dec
        }
    }
}
