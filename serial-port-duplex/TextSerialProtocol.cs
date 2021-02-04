using System;
using System.IO.Ports;

namespace SerialPortDuplex
{
    public class TextSerialProtocol : ISerialProtocol
    {
        public TextSerialProtocol(CmdParam param)
        {
            _param = param ?? throw new ArgumentNullException(nameof(param));

            InitSpInstance();
        }

        public virtual string Receive()
        {
            return _serialPort.ReadLine();
        }

        public virtual void Send(string msg)
        {
            _serialPort.WriteLine(msg);
        }

        // TODO : implement full MS Dispose pattern
        public void Dispose()
        {
            _serialPort?.Dispose();
        }

        public static void PrintAvailableSerialPortOptions()
        {
            Console.WriteLine("Available Ports:");
            foreach (string s in SerialPort.GetPortNames())
            {
                Console.WriteLine("   {0}", s);
            }

            Console.WriteLine("Available Parity options:");
            foreach (string s in Enum.GetNames(typeof(Parity)))
            {
                Console.WriteLine("   {0}", s);
            }

            Console.WriteLine("Available StopBits options:");
            foreach (string s in Enum.GetNames(typeof(StopBits)))
            {
                Console.WriteLine("   {0}", s);
            }

            Console.WriteLine("Available Handshake options:");
            foreach (string s in Enum.GetNames(typeof(Handshake)))
            {
                Console.WriteLine("   {0}", s);
            }
        }

        private void InitSpInstance()
        {
            _serialPort = new SerialPort()
            {
                PortName = _param.PortName,
                ReadTimeout = 500,
                WriteTimeout = 500
            };

            _serialPort.BaudRate = _param.BaudRate == null ?_serialPort.BaudRate : int.Parse(_param.BaudRate);
            _serialPort.Parity = _param.Parity == null ? _serialPort.Parity : Enum.Parse<Parity>(_param.Parity, true);
            _serialPort.DataBits = _param.DataBits == null ?_serialPort.DataBits : int.Parse(_param.DataBits);
            _serialPort.StopBits = _param.StopBits == null ? _serialPort.StopBits : Enum.Parse<StopBits>(_param.StopBits, true);
            _serialPort.Handshake = _param.Handshake == null ? _serialPort.Handshake : Enum.Parse<Handshake>(_param.Handshake, true);

            _serialPort.Open();
        }

        protected SerialPort _serialPort;
        protected readonly CmdParam _param;
    }
}
