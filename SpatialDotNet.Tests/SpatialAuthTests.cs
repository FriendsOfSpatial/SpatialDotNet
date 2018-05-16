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

            await spatial.Auth.Login();
        }

        [Test]
        public async Task CanLoginForced()
        {
            var spatial = new Spatial();

            await spatial.Auth.Login(force: true);
        }

        [Test]
        public async Task CanLogout()
        {
            var spatial = new Spatial();

            await spatial.Auth.Logout();
        }
    }
}