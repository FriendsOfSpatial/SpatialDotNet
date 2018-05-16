using System.Collections.Generic;
using System.Management.Automation;
using System.Threading.Tasks;
using NLog;

namespace SpatialDotNet
{
    public class SpatialCommandRunner
    {
        protected Logger Log => LogManager.GetCurrentClassLogger();

        public string SpatialPath { get; private set; }
        public string ProjectPath { get; private set; }
        public string CommandText { get; private set; }

        public SpatialCommandRunner(string spatialPath, string projectPath = "")
        {
            SpatialPath = spatialPath;
            ProjectPath = projectPath;
        }

        /* Set additional arguments, spatial.exe is already set as is --json_output */
        public SpatialCommandRunner SetCommand(string command)
        {
            CommandText = command;

            return this;
        }

        public async Task<IEnumerable<string>> Execute(bool omitJson = false)
        {
            CommandText = omitJson
                ? $"{SpatialPath} {CommandText}"
                : $"{SpatialPath} {CommandText} --json_output";

            var result = new List<string>();

            using (var powerShellInstance = PowerShell.Create())
            {
                if (!string.IsNullOrEmpty(ProjectPath))
                    powerShellInstance.AddScript($"cd {ProjectPath}");

                powerShellInstance.AddScript(CommandText);

                Log.Log(LogLevel.Info, $"Executing Command: {CommandText}");

                var psResult = await new TaskFactory().FromAsync(powerShellInstance.BeginInvoke(), powerShellInstance.EndInvoke);
                
                foreach (var obj in psResult)
                    result.Add(obj.BaseObject.ToString());
            }

            return result;
        }
    }
}