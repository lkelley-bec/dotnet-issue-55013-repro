using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace cancel_connect_test
{
    public class Tests
    {
        [Test]
        public async Task TestCancelWebSocketConnection()
        {
            var cts = new CancellationTokenSource();
            var uri = new Uri("ws://localhost");
            var ws = new ClientWebSocket();
            var connectTask = ws.ConnectAsync(uri, cts.Token);
            cts.Cancel();
            try
            {
                await connectTask;
                Assert.AreNotEqual(WebSocketState.Open, ws.State);
            }
            catch (TaskCanceledException) { }
            catch (WebSocketException) { }
        }
    }
}
