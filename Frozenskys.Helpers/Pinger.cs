namespace Frozenskys.Helpers
{
    using System.Net;
    using System.Net.Sockets;

    public class Pinger
    {
        public string Send(IPAddress address)
        {
            var status = "";
            using (var client = new TcpClient())
            {
                if (!client.ConnectAsync(address, 443).Wait(1000))
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
