using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SpatialDotNet
{
    public class SpatialCloudResponse
    {
        [JsonProperty("cli-version")]
        public string CliVersion { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("level")]
        public string Level { get; set; }

        [JsonProperty("original_log_level")]
        public int OriginalLogLevel { get; set; }

        [JsonProperty("stack")]
        public string Stack { get; set; }
    }
    
    /* Unimplemented */
    public class SpatialCloud : SpatialCommandBase
    {
        public SpatialCloud(Func<SpatialCommandRunner> commandFactory) : base(commandFactory) { }

        public async Task<SpatialCloudResponse> Connect(string projectName = "")
        {
            var commandResult = await CommandFactory().SetCommand(string.IsNullOrEmpty(projectName)
                    ? "cloud connect external"
                    : $"cloud connect external --project_name {projectName}")
                .Execute();

            return JsonConvert.DeserializeObject<SpatialCloudResponse>(commandResult.ElementAt(1));
        }

        public async Task<SpatialCloudResponse> Delete(string projectName = "")
        {
            var commandResult = await CommandFactory().SetCommand(string.IsNullOrEmpty(projectName)
                    ? "cloud delete"
                    : $"cloud delete --project_name {projectName}")
                .Execute();

            return JsonConvert.DeserializeObject<SpatialCloudResponse>(commandResult.ElementAt(1));
        }

        public async Task<IEnumerable<SpatialResponse>> HistoryList()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<SpatialResponse>> HistorySnapshot()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<SpatialResponse>> Launch()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<SpatialResponse>> QueueSize()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<SpatialResponse>> RemainingCapacity()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<SpatialResponse>> Runtime()
        {
            throw new NotImplementedException();
        }

        public async Task<string> Help()
        {
            var result = await CommandFactory().SetCommand("cloud -h").Execute(true);
            return result.First();
        }
    }
}