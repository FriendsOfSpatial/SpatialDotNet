using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SpatialDotNet
{
    public class SpatialPackage : SpatialCommandBase
    {
        public SpatialPackage(Func<SpatialCommandRunner> commandFactory) : base(commandFactory) { }

        public async Task Retrieve(string type, string name, string version)
        {
            throw new NotImplementedException();
        }

        public async Task<string> Help()
        {
            var result = await CommandFactory().SetCommand("package -h").Execute(true);
            return result.First();
        }
    }
}