using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Frozenskys.Helpers;
using System.Net;

namespace Frozenskys.Helpers.UnitTests
{
    [TestClass]
    public class PingerTest
    {
        Pinger _pinger = new Pinger();

        [TestMethod]
        public void ConnectToLocalHostFails()
        {
            Assert.AreEqual("No Connection", _pinger.Send(IPAddress.Parse("127.0.0.0").MapToIPv4()));    
        }

        [TestMethod]
        public void ConnectToGooglePasses()
        {
            Assert.AreEqual("Ok", _pinger.Send(IPAddress.Parse("216.58.198.110").MapToIPv4()));
        }
    }
}
