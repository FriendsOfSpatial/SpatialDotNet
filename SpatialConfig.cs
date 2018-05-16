using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SpatialDotNet
{
    public class SpatialConfig : SpatialCommandBase
    {
        private readonly AsyncProperty<SpatialCliStructure> _cliStructure;
        public AsyncProperty<SpatialCliStructure> CliStructure
        {
            get => _cliStructure;
            set => CommandFactory().SetCommand($"config set cli-structure {(value == SpatialCliStructure.V1 ? "v1" : "v2")}").Execute().Wait();
        }

        private readonly AsyncProperty<bool> _ignoreUpdates;
        public AsyncProperty<bool> IgnoreUpdates
        {
            get => _ignoreUpdates;
            set => CommandFactory().SetCommand($"config set ignore-updates {(value ? "true" : "false")}").Execute().Wait();
        }

        private readonly AsyncProperty<bool> _hideOverviewPage;
        public AsyncProperty<bool> HideOverviewPage
        {
            get => _hideOverviewPage;
            set => CommandFactory().SetCommand($"config set hide-overview-page {(value ? "true" : "false")}").Execute().Wait();
        }

        private readonly AsyncProperty<bool> _secureEnvironment;
        public AsyncProperty<bool> SecureEnvironment
        {
            get => _secureEnvironment;
            set => CommandFactory().SetCommand($"config set secure-environment {(value ? "true" : "false")}").Execute().Wait();
        }
        
        public SpatialConfig(Func<SpatialCommandRunner> commandFactory) : base(commandFactory)
        {
            _cliStructure = new AsyncProperty<SpatialCliStructure>(() =>
            {
                return CommandFactory().SetCommand("config get cli-structure").Execute()
                    .ContinueWith(task => JsonConvert.DeserializeObject<SpatialResponse>(task.Result.Last()).Message.Contains("v1") ? SpatialCliStructure.V1 : SpatialCliStructure.V2);
            });

            _ignoreUpdates = new AsyncProperty<bool>(() =>
            {
                return CommandFactory().SetCommand("config get ignore-updates").Execute()
                    .ContinueWith(task => JsonConvert.DeserializeObject<SpatialResponse>(task.Result.Last()).Message.Contains("true"));
            });

            _hideOverviewPage = new AsyncProperty<bool>(() =>
            {
                return CommandFactory().SetCommand("config get hide-overview-page").Execute()
                    .ContinueWith(task => JsonConvert.DeserializeObject<SpatialResponse>(task.Result.Last()).Message.Contains("true"));
            });

            _secureEnvironment = new AsyncProperty<bool>(() =>
            {
                return CommandFactory().SetCommand("config get secure-environment").Execute()
                    .ContinueWith(task => JsonConvert.DeserializeObject<SpatialResponse>(task.Result.Last()).Message.Contains("true"));
            });
        }

        /* spatial config -h */
        public async Task<string> Help()
        {
            var result = await CommandFactory().SetCommand("config -h").Execute(true);
            return result.First();
        }
    }
}