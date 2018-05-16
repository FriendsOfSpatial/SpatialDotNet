using System.Threading.Tasks;
using NUnit.Framework;

namespace SpatialDotNet.Tests
{
    // TODO: None of these work without a valid deployment
    [TestFixture]
    public class SpatialCloudTests
    {
        [Test]
        public async Task CantConnectOutsideProject()
        {
            var spatial = new Spatial();

            var result = await spatial.Cloud.Connect(SpatialTestUtilities.DeploymentName);

            Assert.NotNull(result);
            Assert.IsEmpty(result.Error);
        }

        [Test]
        public async Task CanConnect()
        {
            var spatial = new Spatial(SpatialTestUtilities.ProjectPath);

            var result = await spatial.Cloud.Connect(SpatialTestUtilities.DeploymentName);

            Assert.NotNull(result);
            Assert.IsEmpty(result.Error);
        }

        [Test]
        public async Task CanDelete()
        {
            var spatial = new Spatial(SpatialTestUtilities.ProjectPath);

            var result = await spatial.Cloud.Delete(SpatialTestUtilities.DeploymentName);

            Assert.NotNull(result);
            Assert.IsEmpty(result.Error);
        }

        [Test]
        public async Task CanGetHelp()
        {
            var spatial = new Spatial(SpatialTestUtilities.ProjectPath);

            var result = await spatial.Cloud.Help();

            Assert.NotNull(result);
            Assert.IsNotEmpty(result);
        }
    }
}