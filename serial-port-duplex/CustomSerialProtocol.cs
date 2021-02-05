using System;
using System.Threading;
using System.Globalization;
using System.Collections.Generic;

namespace SerialPortDuplex
{
    public class CustomSerialProtocol : TextSerialProtocol
    {
        public enum Mode
        {
            Hex,
            Dec
        }

        public byte[] Preamb { get; } = { 0xDD };

        public CustomSerialProtocol(CmdParam param)
            : base(param)
        {
            if (!(_param.Mode == Mode.Hex || _param.Mode == Mode.Dec))
                throw new ArgumentException($"{nameof(param)}. Got unsupported chat mode = '{_param.Mode}'");
        }

        public override string Receive()
        {
            // TODO : is it correct from msb/lsb perspective?

            var bytes = new List<byte>(4);
            for (int nextByte = _serialPort.ReadByte(); !IsPreamb((byte)nextByte); nextByte = _serialPort.ReadByte())
            {
                if (nextByte == -1)
                    throw new Exception("Unexpected end of the stream.");
                
                bytes.Add((byte)nextByte);

                Thread.Sleep(0);
            }

            // TODO : what if we have more bytes than we need? Looks like we will lose some data..

            int code = BitConverter.ToInt32(bytes.ToArray(), 0);

            string msg = null;
            if (_param.Mode == Mode.Dec)
                msg = code.ToString();
            else
                msg = Convert.ToString(code, 16);
            
            return msg;
        }

        public override void Send(string msg)
        {
            if (string.IsNullOrEmpty(msg))
            {
                SendPreamb();
                return;
            }

            int parsedMsg = 0;
            if (_param.Mode == Mode.Dec)
                parsedMsg = int.Parse(msg);
            else
            {
                if (!int.TryParse(msg, NumberStyles.HexNumber, null, out parsedMsg))
                    parsedMsg = Convert.ToInt32(msg , 16);
            }

            Send(parsedMsg);
        }

        private void Send(int msg)
        {
            SendPreamb();

            var bytes = BitConverter.GetBytes(msg);
            _serialPort.Write(bytes, 0, bytes.Length);
        }

        private void SendPreamb()
        {
            _serialPort.Write(Preamb, 0, 1);
        }

        private bool IsPreamb(byte b)
        {
            return b == Preamb[0];
        }
    }
}
