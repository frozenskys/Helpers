namespace Frozenskys.Helpers
{
    using System.Net;
    using System.Net.Sockets;

    public class Pinger
    {
        public string Send(IPAddress address)
        {
           return this.Send(address, 80);
        }

        public string Send(IPAddress address, int Port)
        {
            var status = "";
            using (var client = new TcpClient())
            {
                if (!client.ConnectAsync(address, Port).Wait(1000))
                {
                    status = "No Connection";
                }
                else
                {
                    status = "Ok";
                }
            }
            return status;
        }
    }
}
