using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SpatialDotNet.Tests
{
    [TestFixture]
    public class SpatialProjectTests
    {
        [Test]
        public async Task CanGetHelp()
        {
            var spatial = new Spatial(SpatialTestUtilities.ProjectPath);

            var helpText = await spatial.Project.Help();

            Assert.IsNotEmpty(helpText);
        }
    }

    [TestFixture]
    public class SpatialProjectAssemblyTests
    {

        [Test]
        public async Task CantDeleteAssembly()
        {
            var spatial = new Spatial(SpatialTestUtilities.ProjectPath);

            var result = await spatial.Project.Assembly.Delete("abc123");

            Assert.IsFalse(result);
        }

        // BUG!
        [Test]
        public async Task CantDownloadAssembly()
        {
            var spatial = new Spatial(SpatialTestUtilities.ProjectPath);

            var result = await spatial.Project.Assembly.Download("abc123", @"C:\Temp\");

            Assert.IsFalse(result);
        }

        // BUG: Doesn't return JSON
        [Test]
        public async Task CanListAssemblies()
        {
            var spatial = new Spatial(SpatialTestUtilities.ProjectPath);

            var result = await spatial.Project.Assembly.List();

        }
    }

    [TestFixture]
    public class SpatialProjectAssemblyArtifactTests
    {
        // BUG: This throws a command line error
        [Test]
        public async Task CantDownloadAssemblyArtifact()
        {
            var spatial = new Spatial(SpatialTestUtilities.ProjectPath);

            var result = await spatial.Project.Assembly.Artifact.Download("abc123", "hat", @"C:\Temp");

            Assert.IsTrue(result);
        }

        [Test]
        public async Task CanListAssemblyArtifacts()
        {
            var spatial = new Spatial(SpatialTestUtilities.ProjectPath);

            var result = await spatial.Project.Assembly.Artifact.List("abc123");

            Assert.NotNull(result);
        }
    }

    [TestFixture]
    public class SpatialProjectAssemblyWorkerTests
    {
        // BUG: Doesn't return anything
        [Test]
        public async Task CantDeleteAssemblyWorker()
        {
            var spatial = new Spatial(SpatialTestUtilities.ProjectPath);

            var result = await spatial.Project.Assembly.Worker.Delete();

            Assert.IsTrue(result);
        }

        // TODO: CantGetAssemblyWorker

    }

    [TestFixture]
    public class SpatialProjectDeploymentTests
    {
        [Test]
        public async Task CantDeleteDeployment()
        {
            var spatial = new Spatial(SpatialTestUtilities.ProjectPath);

            var result = await spatial.Project.Deployment.Delete("abc123");

            Assert.IsFalse(result);
        }

        [Test]
        public async Task CanListLiveDeployments()
        {
            var spatial = new Spatial(SpatialTestUtilities.ProjectPath);

            var result = await spatial.Project.Deployment.ListLive();

        }

        [Test]
        public async Task CanListTerminatedDeployments()
        {
            var spatial = new Spatial(SpatialTestUtilities.ProjectPath);

            var result = await spatial.Project.Deployment.ListTerminated();

        }
    }

    [TestFixture]
    public class SpatialProjectDeploymentTagsTests
    {
        // CantAddTag
        // CantDeleteTag
        // CanListTags
        // CanGetHelp
    }

    [TestFixture]
    public class SpatialProjectDeploymentWorkerFlagTests
    {
        // CantDeleteFlag
        // CantGetFlag
        // CanListFlags
        // CanSetFlag
        // CanGetHelp
    }

    [TestFixture]
    public class SpatialProjectHistoryTests
    {
        // CantClone
        // CantCreate
        // CantDelete
        // CanList
        // CanGetHelp
    }

    [TestFixture]
    public class SpatialProjectHistorySnapshotTests
    {
        // CantConvert
        // CantCreate
        // CantDownload
        // CanList
        // CantUpload
        // CanGetHelp
    }
}