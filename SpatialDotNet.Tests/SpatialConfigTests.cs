using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SpatialDotNet.Tests
{
    [TestFixture]
    public class SpatialConfigTests
    {
        // NOTE: This fails on spatials end
        [Test]
        public async Task CanGetSetCliStructure()
        {
            var spatial = new Spatial();

            var originalValue = await spatial.Config.CliStructure;
            Assert.AreEqual(SpatialCliStructure.V2, originalValue);

            spatial.Config.CliStructure = SpatialCliStructure.V1;

            var newValue = await spatial.Config.CliStructure;
            Assert.AreEqual(SpatialCliStructure.V1, newValue);

            spatial.Config.CliStructure = originalValue;
        }

        [Test]
        public async Task CanGetSetIgnoreUpdates()
        {
            var spatial = new Spatial();
            var originalValue = await spatial.Config.IgnoreUpdates;

            spatial.Config.IgnoreUpdates = !originalValue;

            var newValue = await spatial.Config.IgnoreUpdates;
            Assert.AreEqual(!originalValue, newValue);

            spatial.Config.IgnoreUpdates = originalValue;
        }

        [Test]
        public async Task CanGetSetHideOverviewPage()
        {
            var spatial = new Spatial();
            var originalValue = await spatial.Config.HideOverviewPage;

            spatial.Config.HideOverviewPage = !originalValue;

            var newValue = await spatial.Config.HideOverviewPage;
            Assert.AreEqual(!originalValue, newValue);

            spatial.Config.HideOverviewPage = originalValue;
        }

        [Test]
        public async Task CanGetSetSecureEnvironment()
        {
            var spatial = new Spatial();
            var originalValue = await spatial.Config.SecureEnvironment;

            spatial.Config.SecureEnvironment = !originalValue;

            var newValue = await spatial.Config.SecureEnvironment;
            Assert.AreEqual(!originalValue, newValue);

            spatial.Config.SecureEnvironment = originalValue;
        }
    }
}