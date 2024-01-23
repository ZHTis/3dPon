using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

namespace Components
{
    public class SocketComCenter : Singleton<SocketComCenter>
    {
        private bool _dataAvailable;

        public bool DataAvailable
        {
            get
            {
                var value = _dataAvailable;
                _dataAvailable = false;
                return value;
            }
        }

        public int DataSize { get; private set; }
        
        private TcpListener _server;
        private NetworkStream _stream;

        public async void SetServer(string hostname, int port, byte[] buf)
        {
            _server = new TcpListener(IPAddress.Parse(hostname), port);
            _server.Start();
            Debug.Log("Connecting...");

            var client = await _server.AcceptTcpClientAsync();
            Debug.Log("Connected");
            
            _stream = client.GetStream();
            Read(buf);
        }
        
        
        public void SendStringAsync(string msg)
        {
            SendAsync(Encoding.UTF8.GetBytes(msg));
        }

        public async void SendAsync(byte[] msg)
        {
            if (_stream.CanWrite)
            {
                await _stream.WriteAsync(msg, 0, msg.Length);
            }
            else
            {
                Debug.Log("client is not available");
            }
        }

        public void Read(byte[] buffer)
        {
            if (_stream != null)
            {
                var data = _stream.ReadAsync(buffer, 0,buffer.Length);
                Debug.Log("U got: " + Encoding.UTF8.GetString(buffer));
            }
        }

        public void CloseServer()
        {
            _server.Stop();
        }
    }
}
