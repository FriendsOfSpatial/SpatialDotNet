using System;
using System.Linq;
using System.Threading.Tasks;

namespace SpatialDotNet
{
    public class SpatialSetup : SpatialCommandBase
    {
        public SpatialSetup(Func<SpatialCommandRunner> commandFactory) : base(commandFactory) { }

        public async Task InstallDependencies(bool dryRun = false, bool force = false, bool withJdk = false, bool withMsvc = true)
        {
            throw new NotImplementedException();
        }

        public async Task<string> Help()
        {
            var result = await CommandFactory().SetCommand($"project setup -h").Execute(true);
            return result.First();
        }
    }
}