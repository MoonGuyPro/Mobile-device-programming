using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientData;

namespace ClientDataTest
{
    internal class ConnectionServiceMock : IConnectionService
    {
        public event Action<string>? Logger;
        public event Action? OnConnectionStateChanged;
        public event Action<string>? OnMessage;
        public event Action? OnError;
        public event Action? OnDisconnect;

        public Task Connect(Uri peerUri)
        {
            throw new NotImplementedException();
        }

        public Task Disconnect()
        {
            throw new NotImplementedException();
        }

        public bool IsConnected()
        {
            return true;
        }

        public async Task SendAsync(string message)
        {

            await Task.Delay(0);
        }

        // Fields and methods for test purposes

        private Serializer serializer = Serializer.Create();

       
    }
}
