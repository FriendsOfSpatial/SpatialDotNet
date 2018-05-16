using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SpatialDotNet
{
    public class SpatialConfig : SpatialCommandBase
    {
        public SpatialConfig(Func<SpatialCommandRunner> commandFactory) : base(commandFactory) { }

        public async Task SetCliStructure(SpatialCliStructure structure)
        {
            await CommandFactory().SetCommand($"config set cli-structure: {(structure == SpatialCliStructure.V1 ? "v1" : "v2")}").Execute();
        }

        public async Task<IEnumerable<SpatialResponse>> GetCliStructure()
        {
            var commandResult = await CommandFactory().SetCommand("config get cli-structure").Execute();
            return commandResult.Select(JsonConvert.DeserializeObject<SpatialResponse>).ToList();
        }

        public async Task SetIgnoreUpdates(bool ignore)
        {
            await CommandFactory().SetCommand($"config set ignore-updates: {(ignore ? "true" : "false")}").Execute();
        }

        public async Task<IEnumerable<SpatialResponse>> GetIgnoreUpdates()
        {
            var commandResult = await CommandFactory().SetCommand("config get ignore-updates").Execute();
            return commandResult.Select(JsonConvert.DeserializeObject<SpatialResponse>).ToList();
        }

        public async Task SetHideOverviewPage(bool hide)
        {
            await CommandFactory().SetCommand($"config set hide-overview-page: {(hide ? "true" : "false")}").Execute();
        }

        public async Task<IEnumerable<SpatialResponse>> GetHideOverviewPage()
        {
            var commandResult = await CommandFactory().SetCommand("config get hide-overview-page").Execute();
            return commandResult.Select(JsonConvert.DeserializeObject<SpatialResponse>).ToList();
        }

        public async Task SetSecureEnvironment(bool secure)
        {
            await CommandFactory().SetCommand($"config set secure-environment: {(secure ? "true" : "false")}").Execute();
        }

        public async Task<IEnumerable<SpatialResponse>> GetSecureEnvironment()
        {
            var commandResult = await CommandFactory().SetCommand("config get secure-environment").Execute();
            return commandResult.Select(JsonConvert.DeserializeObject<SpatialResponse>).ToList();
        }

        public async Task<string> Help()
        {
            var result = await CommandFactory().SetCommand("config -h").Execute(true);
            return result.First();
        }
    }
}