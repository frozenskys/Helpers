namespace Frozenskys.Helpers
{
    using System.Net;
    using System.Net.Sockets;

    public class Pinger
    {
        public string Send(IPAddress address)
        {
            using (var sckt = new TcpClient())
            {
                try
                {
                    sckt.Connect(address, 443);
                    if (sckt.Connected)
                    {
                        sckt.Close();
                        return "Ok";
                    }
                }
                catch
                {
                    return "No Connection";
                }
            }
            return "Errm";
        }
    }
}
