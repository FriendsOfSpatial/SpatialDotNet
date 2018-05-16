using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SpatialDotNet.Tests
{
    [TestFixture]
    public class SpatialAuthTests
    {
        [Test]
        public async Task CanLogin()
        {
            var spatial = new Spatial();

            var result = await spatial.Auth.Login();
            
            Assert.IsNotEmpty(result);
        }

        [Test]
        public void TimeoutFormat()
        {
            var timeout = new TimeSpan(0, 0, 25, 10);

            var result = timeout.ToString(@"mm\mss\s");

            Assert.AreEqual("25m10s", result);
        }
    }
}