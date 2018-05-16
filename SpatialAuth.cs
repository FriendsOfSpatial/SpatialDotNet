using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SpatialDotNet
{
    public class SpatialAuth : SpatialCommandBase
    {
        public SpatialAuth(Func<SpatialCommandRunner> commandFactory) : base(commandFactory) { }

        public async Task<IEnumerable<SpatialResponse>> Login()
        {
            var commandResult = await CommandFactory().SetCommand("auth login").Execute();
            return commandResult.Select(JsonConvert.DeserializeObject<SpatialResponse>).ToList();
        }

        public async Task<IEnumerable<SpatialResponse>> Logout()
        {
            var commandResult = await CommandFactory().SetCommand("auth logout").Execute();
            return commandResult.Select(JsonConvert.DeserializeObject<SpatialResponse>).ToList();
        }

        public async Task<string> Help()
        {
            var result = await CommandFactory().SetCommand("auth -h").Execute(true);
            return result.First();
        }
    }
}