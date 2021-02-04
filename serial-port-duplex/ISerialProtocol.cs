using System;

namespace SerialPortDuplex
{
    public interface ISerialProtocol : IDisposable
    {
        void Send(string msg);
        string Receive();
    }
}
