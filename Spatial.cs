using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NLog;

namespace SpatialDotNet
{
    public class SpatialResponse
    {
        [JsonProperty("cli-version")]
        public string CliVersion { get; set; }

        [JsonProperty("level")]
        public SpatialLogLevel Level { get; set; }

        [JsonProperty("msg")]
        public string Message { get; set; }

        [JsonProperty("time")]
        public DateTime Time { get; set; }

        public void ToLog(Logger log)
        {
            log.Log(Level, Time, Message);
        }
    }

    public enum SpatialCliStructure
    {
        V1,
        V2
    }

    public enum SpatialSnapshotFormat
    {
        Text,
        Binary
    }

    public enum SpatialLogLevel
    {
        Debug = 0,
        Info = 1,
        Warning = 2,
        Error = 3,
        Fatal = 4,
        Panic = 5
    }
    
    public class Spatial
    {
        private Logger Log => LogManager.GetCurrentClassLogger();

        protected Func<SpatialCommandRunner> CommandFactory;

        public string SpatialPath { get; private set; }
        public string ProjectPath { get; private set; }

        public SpatialAuth Auth { get; private set; }
        public SpatialCloud Cloud { get; private set; }
        public SpatialConfig Config { get; private set; }
        public SpatialLocal Local { get; private set; }
        public SpatialProject Project { get; private set; }
        public SpatialSetup Setup { get; private set; }

        public Spatial(string projectPath = "")
        {
            ProjectPath = projectPath;
            if(!string.IsNullOrEmpty(ProjectPath))
                if(!Directory.Exists(ProjectPath))
                    throw new DirectoryNotFoundException($"Invalid projectPath: {ProjectPath}");

            SpatialPath = FindSpatial();
            if(!File.Exists(SpatialPath))
                throw new FileNotFoundException($"spatial.exe not found at {SpatialPath}");

            if(!HasPowerShell())
                throw new Exception("Powershell not found");

            CommandFactory = () => new SpatialCommandRunner(SpatialPath, ProjectPath);

            Auth = new SpatialAuth(CommandFactory);
            Cloud = new SpatialCloud(CommandFactory);
            Config = new SpatialConfig(CommandFactory);
            Local = new SpatialLocal(CommandFactory);
            Project = new SpatialProject(CommandFactory);
            Setup = new SpatialSetup(CommandFactory);
        }

        public bool IsValid()
        {
            return File.Exists(SpatialPath);
        }

        private bool HasPowerShell()
        {
            // TODO
            return true;
        }

        public async Task Update(string version = "", bool rollback = false)
        {
            await CommandFactory().SetCommand(string.IsNullOrEmpty(version) 
                ? $"update {(rollback ? "--rollback" : string.Empty)}" 
                : $"update {version}").Execute();
        }

        public async Task<IEnumerable<SpatialResponse>> Version()
        {
            var commandResult = await CommandFactory().SetCommand("version").Execute();
            return commandResult.Select(JsonConvert.DeserializeObject<SpatialResponse>).ToList();
        }

        public async Task<string> Help()
        {
            var result = await CommandFactory().SetCommand("-h").Execute(true);
            return result.First();
        }

        private string FindSpatial()
        {
            const string fileName = @"spatial.exe";
            var fullPath = Environment.ExpandEnvironmentVariables(fileName);
            if (!File.Exists(fullPath))
            {
                if (string.IsNullOrEmpty(Path.GetDirectoryName(fullPath)))
                {
                    foreach (var path in (Environment.GetEnvironmentVariable(@"PATH") ?? string.Empty).Split(';'))
                    {
                        var path2 = path.Trim();
                        if (!string.IsNullOrEmpty(path2) && File.Exists(path2 = Path.Combine(path2, fullPath)))
                            return Path.GetFullPath(path2);
                    }
                }

                return string.Empty;
            }

            return Path.GetFullPath(fullPath);
        }
    }
}