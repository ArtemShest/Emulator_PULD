using Avalonia.Collections;
using DynamicData;
using DynamicData.Kernel;
using Emulator_PULD.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator_PULD.ViewModels
{
    public class RecieveMessage : ReactiveObject
    {
        private byte[]? _message;
        private string? _dateTime;

        public byte[]? message
        {
            get => _message;
            set => this.RaiseAndSetIfChanged(ref _message, value);
        }

        public string? dateTime
        {
            get => _dateTime;
            set => this.RaiseAndSetIfChanged(ref _dateTime, value);
        }

        public RecieveMessage(byte[] data)
        {
            message = data;

            dateTime = DateTime.Now.ToLongTimeString();
        }
    }


    public class ViewModelBase : ReactiveObject
    {
        private PULD _puld = new();
        public PULD puld
        {
            get => _puld;
            set => this.RaiseAndSetIfChanged(ref _puld, value);
        }

        private bool _continue;
        public SerialPort _serialPort = new SerialPort();

        private string _selectedCom;
        public string selectedCom
        {
            get => _selectedCom;
            set => this.RaiseAndSetIfChanged(ref _selectedCom, value);
        }
        private AvaloniaList<string> _coms = new();
        public AvaloniaList<string> coms
        {
            get => _coms;
            set => this.RaiseAndSetIfChanged(ref _coms, value);
        }

        private AvaloniaList<RecieveMessage> _recieveMsg = new();
        public AvaloniaList<RecieveMessage> recieveMsg
        {
            get => _recieveMsg;
            set => this.RaiseAndSetIfChanged(ref _recieveMsg, value);
        }

        private AvaloniaList<RecieveMessage> _transmitMsg = new();
        public AvaloniaList<RecieveMessage> transmitMsg
        {
            get => _transmitMsg;
            set => this.RaiseAndSetIfChanged(ref _transmitMsg, value);
        }
        public ViewModelBase()
        {

            
            string[] str_coms = SerialPort.GetPortNames();
            foreach(string str in str_coms) { coms.Add(str); }


            

            selectedCom = str_coms[0];

        }

        public void uart_connect()
        {
            _serialPort.PortName = selectedCom;
            _serialPort.BaudRate = 38400;
            _serialPort.Parity = Parity.None;
            _serialPort.DataBits = 8;
            _serialPort.StopBits = StopBits.One;
            _serialPort.Handshake = Handshake.None;
            _serialPort.NewLine = 0x0D.ToString();

            _serialPort.ReadTimeout = 2000;
            _serialPort.WriteTimeout = 500;

            _serialPort.Open();
            _continue = true;


            PULD_tick();

        }

        private void takeAns()
        {
            List<byte> data = new() {0xA5, 0x03, 0x05, 0x10, puld.state};
            byte cs = 0;
            byte[] sendBytes = new byte[6];
            for (int i = 0; i<5; i++)
            {
                sendBytes[i] = data[i]; 
                cs += data[i];
            }
            sendBytes[5] = ((byte)(0 - cs));

            _serialPort.Write(sendBytes, 0, 6);
            transmitMsg.Insert(0, new(sendBytes));
            if (transmitMsg.Count > 3600) transmitMsg.RemoveAt(transmitMsg.Count - 1);

            _serialPort.DiscardOutBuffer();
        }

        public async Task PULD_tick()
        {
            Task.Run(() =>
            {
                byte[] arr = new byte[5];

                while (true)
                {
                    if (_serialPort.IsOpen)
                    {
                        try
                        {
//                          _serialPort.Read(arr, 0, 6);
//                          arr = Encoding.ASCII.GetBytes(_serialPort.ReadLine());
                            if (_serialPort.BytesToRead >= 5)
                            {
                                _serialPort.Read(arr, 0, 5);
                                recieveMsg.Insert(0, new(arr));
                                if (recieveMsg.Count > 3600) recieveMsg.RemoveAt(recieveMsg.Count - 1);
                                _serialPort.DiscardInBuffer();

                                takeAns();
                            }
                        }
                        catch (TimeoutException) { }
                    }
                }
            });
        }   
    }
}