using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SpatialDotNet
{
    public class SpatialProject : SpatialCommandBase
    {
        public SpatialProjectAssembly Assembly { get; }
        public SpatialProjectDeployment Deployment { get; }
        public SpatialProjectHistory History { get; }

        public SpatialProject(Func<SpatialCommandRunner> commandFactory) : base(commandFactory)
        {
            Assembly = new SpatialProjectAssembly(CommandFactory);
            Deployment = new SpatialProjectDeployment(CommandFactory);
            History = new SpatialProjectHistory(CommandFactory);
        }
        
        public async Task<string> Help(string projectName = "")
        {
            var result = await CommandFactory().SetCommand($"project {(string.IsNullOrEmpty(projectName) ? string.Empty : $"--project_name {projectName}")} -h").Execute(true);
            return result.First();
        }
    }
    
    public class SpatialProjectAssembly : SpatialCommandBase
    {
        public SpatialProjectAssemblyArtifact Artifact { get; }
        public SpatialProjectAssemblyWorker Worker { get; }

        public SpatialProjectAssembly(Func<SpatialCommandRunner> commandFactory) : base(commandFactory)
        {
            Artifact = new SpatialProjectAssemblyArtifact(CommandFactory);
            Worker = new SpatialProjectAssemblyWorker(CommandFactory);
        }

        public async Task Delete(string assemblyName, string projectName = "")
        {
            await CommandFactory().SetCommand($"project assembly delete {assemblyName} {(string.IsNullOrEmpty(projectName) ? string.Empty : $"--project_name {projectName}")}").Execute();
        }

        public async Task Download(string assemblyName, string outputPath, string projectName = "")
        {
            await CommandFactory().SetCommand($"project assembly download {assemblyName} {outputPath} {(string.IsNullOrEmpty(projectName) ? string.Empty : $"--project_name {projectName}")}").Execute();
        }

        public async Task<IEnumerable<SpatialResponse>> List(string projectName = "")
        {
            var commandResult = await CommandFactory().SetCommand($"project assembly list {(string.IsNullOrEmpty(projectName) ? string.Empty : $"--project_name {projectName}")}").Execute();
            return commandResult.Select(JsonConvert.DeserializeObject<SpatialResponse>).ToList();
        }

        public async Task<string> Help(string projectName = "")
        {
            var result = await CommandFactory().SetCommand($"project assembly {(string.IsNullOrEmpty(projectName) ? string.Empty : $"--project_name {projectName}")} -h").Execute(true);
            return result.First();
        }
    }

    public class SpatialProjectAssemblyArtifact : SpatialCommandBase
    {
        public SpatialProjectAssemblyArtifact(Func<SpatialCommandRunner> commandFactory) : base(commandFactory) { }

        public async Task Download(string assemblyName, string artifactName, string outputPath, string projectName = "")
        {
            await CommandFactory().SetCommand($"project assembly artifact download {assemblyName} {artifactName} {outputPath} {(string.IsNullOrEmpty(projectName) ? string.Empty : $"--project_name {projectName}")}").Execute();
        }

        public async Task<IEnumerable<SpatialResponse>> List(string projectName = "")
        {
            var commandResult = await CommandFactory().SetCommand($"project assembly artifact list {(string.IsNullOrEmpty(projectName) ? string.Empty : $"--project_name {projectName}")}").Execute();
            return commandResult.Select(JsonConvert.DeserializeObject<SpatialResponse>).ToList();
        }

        public async Task<string> Help(string projectName = "")
        {
            var result = await CommandFactory().SetCommand($"project assembly artifact {(string.IsNullOrEmpty(projectName) ? string.Empty : $"--project_name {projectName}")} -h").Execute(true);
            return result.First();
        }
    }

    public class SpatialProjectAssemblyWorker : SpatialCommandBase
    {
        public SpatialProjectAssemblyWorker(Func<SpatialCommandRunner> commandFactory) : base(commandFactory) { }

        public async Task Delete(string assemblyName = "", string name = "", string projectName = "")
        {
            await CommandFactory().SetCommand($"project assembly worker delete " +
                                              $"{(string.IsNullOrEmpty(assemblyName) ? string.Empty : $"--assembly {assemblyName}")} " +
                                              $"{(string.IsNullOrEmpty(name) ? string.Empty : $"--name {name}")} " +
                                              $"{(string.IsNullOrEmpty(projectName) ? string.Empty : $"--project_name {projectName}")}")
                .Execute();
        }

        public async Task<IEnumerable<SpatialResponse>> Get(string assemblyName = "", string name = "", string projectName = "")
        {
            var commandResult = await CommandFactory().SetCommand($"project assembly worker get " +
                                                                  $"{(string.IsNullOrEmpty(assemblyName) ? string.Empty : $"--assembly {assemblyName}")} " +
                                                                  $"{(string.IsNullOrEmpty(name) ? string.Empty : $"--name {name}")} " +
                                                                  $"{(string.IsNullOrEmpty(projectName) ? string.Empty : $"--project_name {projectName}")}")
                .Execute();

            return commandResult.Select(JsonConvert.DeserializeObject<SpatialResponse>).ToList();
        }

        public async Task<IEnumerable<SpatialResponse>> Get(string assemblyName = "", string projectName = "")
        {
            var commandResult = await CommandFactory().SetCommand($"project assembly worker list " +
                                                                  $"{(string.IsNullOrEmpty(assemblyName) ? string.Empty : $"--assembly {assemblyName}")} " +
                                                                  $"{(string.IsNullOrEmpty(projectName) ? string.Empty : $"--project_name {projectName}")}")
                .Execute();

            return commandResult.Select(JsonConvert.DeserializeObject<SpatialResponse>).ToList();
        }

        public async Task<string> Help(string projectName = "")
        {
            var result = await CommandFactory().SetCommand($"project assembly worker {(string.IsNullOrEmpty(projectName) ? string.Empty : $"--project_name {projectName}")} -h").Execute(true);
            return result.First();
        }
    }

    public class SpatialProjectDeployment : SpatialCommandBase
    {
        public SpatialProjectDeploymentTags Tags { get; }
        public SpatialProjectDeploymentWorkerFlag WorkerFlag { get; }

        public SpatialProjectDeployment(Func<SpatialCommandRunner> commandFactory) : base(commandFactory)
        {
            Tags = new SpatialProjectDeploymentTags(CommandFactory);
            WorkerFlag = new SpatialProjectDeploymentWorkerFlag(CommandFactory);
        }

        public async Task Delete(string deploymentName, string projectName = "")
        {
            await CommandFactory().SetCommand($"project deployment delete {deploymentName} {(string.IsNullOrEmpty(projectName) ? string.Empty : $"--project_name {projectName}")}").Execute();
        }

        public async Task Launch(
            string assemblyName, 
            string cluster = "", 
            string deploymentDescription = "",
            string experimentalRuntime = "", 
            bool hideOverview = false, 
            bool noHistory = false, 
            string snapshot = "",
            string tags = "", 
            TimeSpan? uploadTimeout = null, 
            string projectName = "")
        {
            await CommandFactory().SetCommand($"project deployment launch {assemblyName} " +
                                              $"{(string.IsNullOrEmpty(cluster) ? string.Empty : $"--cluster {cluster}")} " +
                                              $"{(string.IsNullOrEmpty(deploymentDescription) ? string.Empty : $"--deployment_description {deploymentDescription}")} " +
                                              $"{(string.IsNullOrEmpty(experimentalRuntime) ? string.Empty : $"--experimental_runtime {experimentalRuntime}")} " +
                                              $"{(hideOverview ? "--hide_overview_page" : string.Empty)} " +
                                              $"{(noHistory ? "--no_history" : string.Empty)} " +
                                              $"{(string.IsNullOrEmpty(snapshot) ? string.Empty : $"--snapshot {snapshot}")} " +
                                              $"{(string.IsNullOrEmpty(tags) ? string.Empty : $"--tags {tags}")} " +
                                              $"{uploadTimeout?.ToString(@"mm\mss\s") ?? string.Empty} " +
                                              $"{(string.IsNullOrEmpty(projectName) ? string.Empty : $"--project_name {projectName}")}")
                .Execute();
        }

        public async Task<IEnumerable<SpatialResponse>> ListLive(string projectName)
        {
            var commandResult = await CommandFactory().SetCommand($"project deployment list live {(string.IsNullOrEmpty(projectName) ? string.Empty : $"--project_name {projectName}")}").Execute();
            return commandResult.Select(JsonConvert.DeserializeObject<SpatialResponse>).ToList();
        }

        public async Task<IEnumerable<SpatialResponse>> ListTerminated(string projectName)
        {
            var commandResult = await CommandFactory().SetCommand($"project deployment list terminated {(string.IsNullOrEmpty(projectName) ? string.Empty : $"--project_name {projectName}")}").Execute();
            return commandResult.Select(JsonConvert.DeserializeObject<SpatialResponse>).ToList();
        }

        public async Task<string> Help(string projectName = "")
        {
            var result = await CommandFactory().SetCommand($"project deployment {(string.IsNullOrEmpty(projectName) ? string.Empty : $"--project_name {projectName}")} -h").Execute(true);
            return result.First();
        }
    }

    public class SpatialProjectDeploymentTags : SpatialCommandBase
    {
        public SpatialProjectDeploymentTags(Func<SpatialCommandRunner> commandFactory) : base(commandFactory) { }


        public async Task Add(string deploymentName, string tag, string projectName = "")
        {
            await CommandFactory().SetCommand($"project deployment tags add {deploymentName} {tag} {(string.IsNullOrEmpty(projectName) ? string.Empty : $"--project_name {projectName}")}").Execute();
        }

        public async Task Delete(string deploymentName, string tag, string projectName = "")
        {
            await CommandFactory().SetCommand($"project deployment tags delete {deploymentName} {tag} {(string.IsNullOrEmpty(projectName) ? string.Empty : $"--project_name {projectName}")}").Execute();
        }

        public async Task<IEnumerable<SpatialResponse>> List(string deploymentName, string projectName = "")
        {
            var commandResult = await CommandFactory().SetCommand($"project deployment tags list {deploymentName} {(string.IsNullOrEmpty(projectName) ? string.Empty : $"--project_name {projectName}")}").Execute();
            return commandResult.Select(JsonConvert.DeserializeObject<SpatialResponse>).ToList();
        }

        public async Task<string> Help(string projectName = "")
        {
            var result = await CommandFactory().SetCommand($"project deployment tags {(string.IsNullOrEmpty(projectName) ? string.Empty : $"--project_name {projectName}")} -h").Execute(true);
            return result.First();
        }
    }

    public class SpatialProjectDeploymentWorkerFlag : SpatialCommandBase
    {
        public SpatialProjectDeploymentWorkerFlag(Func<SpatialCommandRunner> commandFactory) : base(commandFactory) { }

        public async Task Delete(string projectName, string deploymentName, string workerType, string flagName)
        {
            await CommandFactory().SetCommand($"project deployment worker-flag delete {projectName} {deploymentName} {workerType} {flagName}").Execute();
        }

        public async Task<IEnumerable<SpatialResponse>> Get(string projectName, string deploymentName, string workerType, string flagName)
        {
            var commandResult = await CommandFactory().SetCommand($"project deployment worker-flag get {projectName} {deploymentName} {workerType} {flagName}").Execute();
            return commandResult.Select(JsonConvert.DeserializeObject<SpatialResponse>).ToList();
        }

        public async Task<IEnumerable<SpatialResponse>> List(string projectName, string deploymentName, string workerType)
        {
            var commandResult = await CommandFactory().SetCommand($"project deployment worker-flag list {projectName} {deploymentName} {workerType}").Execute();
            return commandResult.Select(JsonConvert.DeserializeObject<SpatialResponse>).ToList();
        }

        public async Task Set(string projectName, string deploymentName, string workerType, string flagName, string flagValue)
        {
            await CommandFactory().SetCommand($"project deployment worker-flag set {projectName} {deploymentName} {workerType} {flagName} {flagValue}").Execute();
        }

        public async Task<string> Help(string projectName = "")
        {
            var result = await CommandFactory().SetCommand($"project deployment worker-flag {(string.IsNullOrEmpty(projectName) ? string.Empty : $"--project_name {projectName}")} -h").Execute(true);
            return result.First();
        }
    }

    public class SpatialProjectHistory : SpatialCommandBase
    {
        public SpatialProjectHistorySnapshot Snapshot { get; }

        public SpatialProjectHistory(Func<SpatialCommandRunner> commandFactory) : base(commandFactory)
        {
            Snapshot = new SpatialProjectHistorySnapshot(CommandFactory);
        }

        public async Task Clone(
            string oldHistory, 
            string newHistory, 
            bool keepTags = true, 
            int? snapshotId = null,
            string tags = "", 
            string projectName = "")
        {
            await CommandFactory().SetCommand($"project history clone {oldHistory} {newHistory} " +
                                              $"{(keepTags ? "--keep_tags" : string.Empty)} " +
                                              $"{(snapshotId.HasValue ? $"--snapshot {snapshotId.Value}" : string.Empty)} " +
                                              $"{(string.IsNullOrEmpty(tags) ? string.Empty : $"--tags {tags}")} " +
                                              $"{(string.IsNullOrEmpty(projectName) ? string.Empty : $"--project_name {projectName}")}")
                .Execute();
        }

        public async Task Create(
            string historyName, 
            string pathToSnapshot, 
            string tags = "", 
            TimeSpan? uploadTimeout = null,
            string projectName = "")
        {
            await CommandFactory().SetCommand($"project history snapshot create {historyName} {pathToSnapshot} " +
                                              $"{(string.IsNullOrEmpty(tags) ? string.Empty : $"--tags {tags}")} " +
                                              $"{(string.IsNullOrEmpty(projectName) ? string.Empty : $"--project_name {projectName}")}")
                .Execute();
        }

        public async Task Delete(string history, bool force = false, string projectName = "")
        {
            await CommandFactory().SetCommand($"project history snapshot delete {history} " +
                                              $"{(force ? "--force" : string.Empty)} " +
                                              $"{(string.IsNullOrEmpty(projectName) ? string.Empty : $"--project_name {projectName}")}")
                .Execute();
        }

        public async Task<IEnumerable<SpatialResponse>> List(DateTime? from, DateTime? to, string projectName = "")
        {
            var commandResult = await CommandFactory().SetCommand($"project history snapshot list " +
                                                                  $"{@from?.ToString("yyyy-mm-dd") ?? string.Empty} " +
                                                                  $"{to?.ToString("yyyy-mm-dd") ?? string.Empty} " +
                                                                  $"{(string.IsNullOrEmpty(projectName) ? string.Empty : $"--project_name {projectName}")}")
                .Execute();
            return commandResult.Select(JsonConvert.DeserializeObject<SpatialResponse>).ToList();
        }

        public async Task<string> Help(string projectName = "")
        {
            var result = await CommandFactory().SetCommand($"project history {(string.IsNullOrEmpty(projectName) ? string.Empty : $"--project_name {projectName}")} -h").Execute(true);
            return result.First();
        }
    }
    
    public class SpatialProjectHistorySnapshot : SpatialCommandBase
    {
        public SpatialProjectHistorySnapshot(Func<SpatialCommandRunner> commandFactory) : base(commandFactory) { }

        public async Task Convert(
            string inputFile,
            string outputFile,
            SpatialSnapshotFormat inputFormat = SpatialSnapshotFormat.Binary,
            SpatialSnapshotFormat outputFormat = SpatialSnapshotFormat.Text,
            string logPath = "",
            SpatialLogLevel logCallstackLevel = SpatialLogLevel.Panic,
            SpatialLogLevel logLevel = SpatialLogLevel.Info)
        {
            if(!File.Exists(inputFile))
                throw new FileNotFoundException($"The input file specified was not found ({inputFile})");

            if(!Directory.Exists(Path.GetDirectoryName(outputFile)))
                throw new DirectoryNotFoundException($"The output files folder was not found ({Path.GetDirectoryName(inputFile)}");

            if(!string.IsNullOrEmpty(logPath))
                if (!Directory.Exists(Path.GetDirectoryName(logPath)))
                    throw new DirectoryNotFoundException($"The log path folder was not found ({Path.GetDirectoryName(logPath)})");

            if (inputFormat == outputFormat)
            {
                File.Copy(inputFile, outputFile);
                return;
            }

            await CommandFactory().SetCommand($"project history snapshot convert --input {inputFile} --input-format {(inputFormat == SpatialSnapshotFormat.Binary ? "binary" : "text")} " +
                                              $"--output {outputFile} --output-format {(outputFormat == SpatialSnapshotFormat.Binary ? "binary" : "text")} " +
                                              $"{(string.IsNullOrEmpty(logPath) ? string.Empty : $"--log-directory {logPath}")} " +
                                              $"--log_callstack_level {logCallstackLevel.ToString().ToLower()} " +
                                              $"--log_level {logLevel.ToString().ToLower()}")
                .Execute();
        }

        public async Task Create(string history, TimeSpan? timeout = null, string projectName = "")
        {
            await CommandFactory().SetCommand($"project history snapshot create {history} " +
                                              $"{(timeout.HasValue ? $"--timeout {timeout.Value:mm\\mss\\s}" : string.Empty)} " +
                                              $"{(string.IsNullOrEmpty(projectName) ? string.Empty : $"--project_name {projectName}")}")
                .Execute();
        }

        public async Task Download(string historyName, int snapshotId, string outputPath, string tags = "", string projectName = "")
        {
            throw new NotImplementedException();
        }

        public async Task List(string deploymentName, DateTime? from = null, DateTime? to = null, string tags = "", string projectName = "")
        {
            throw new NotImplementedException();
        }

        public async Task Upload(string history, int snapshotId, string tags = "", TimeSpan? uploadTimeout = null, string projectName = "")
        {
            throw new NotImplementedException();
        }

        public async Task<string> Help(string projectName = "")
        {
            var result = await CommandFactory().SetCommand($"project history snapshot {(string.IsNullOrEmpty(projectName) ? string.Empty : $"--project_name {projectName}")} -h").Execute(true);
            return result.First();
        }
    }
}