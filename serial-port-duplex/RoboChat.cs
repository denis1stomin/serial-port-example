using System;
using System.IO.Ports;
using System.Threading;

namespace SerialPortDuplex
{
    public class RoboChat : IDisposable
    {
        public const string ExitWord = "QUIT";

        public RoboChat(ISerialProtocol protocolStream)
        {
            _protocolStream = protocolStream ?? throw new ArgumentNullException(nameof(protocolStream));
        }

        public void Start()
        {
            _continue = true;

            var readThread = new Thread(ReadThreadFunc);
            readThread.Start();

            while (_continue)
            {
                var message = Console.ReadLine();

                if (_stringComparer.Equals(ExitWord, message))
                    _continue = false;
                else
                {
                    _protocolStream.Send(message);
                }
            }

            readThread.Join();
        }

        // TODO : implement full MS Dispose pattern
        public void Dispose()
        {
            _protocolStream?.Dispose();
        }

        private void ReadThreadFunc()
        {
            while (_continue)
            {
                try
                {
                    string message = _protocolStream.Receive();
                    Console.WriteLine(message);
                }
                catch (TimeoutException) { }
            }
        }

        private bool _continue;
        private ISerialProtocol _protocolStream;
        private readonly StringComparer _stringComparer = StringComparer.OrdinalIgnoreCase;
    }
}
