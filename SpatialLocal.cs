using System;
using System.Linq;
using System.Threading.Tasks;

namespace SpatialDotNet
{
    public class SpatialLocal : SpatialCommandBase
    {
        public SpatialLocal(Func<SpatialCommandRunner> commandFactory) : base(commandFactory) { }

        public async Task Launch()
        {
            throw new NotImplementedException();
        }

        public async Task WorkerLaunch()
        {
            throw new NotImplementedException();
        }

        public async Task<string> Help()
        {
            var result = await CommandFactory().SetCommand("local -h").Execute(true);
            return result.First();
        }
    }
}