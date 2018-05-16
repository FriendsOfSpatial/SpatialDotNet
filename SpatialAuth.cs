using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SpatialDotNet
{
    public class SpatialAuth : SpatialCommandBase
    {
        public SpatialAuth(Func<SpatialCommandRunner> commandFactory) : base(commandFactory) { }

        /* spatial auth login [flags] */
        public async Task Login(bool force = false, string ivyCredentialDirectory = "", string m2CredentialDirectory = "")
        {
            if(!string.IsNullOrEmpty(ivyCredentialDirectory))
                if(!Directory.Exists(ivyCredentialDirectory))
                    throw new DirectoryNotFoundException($"Ivy credential directory was specified but not found ({ivyCredentialDirectory})");

            if(!string.IsNullOrEmpty(m2CredentialDirectory))
                if(!Directory.Exists(m2CredentialDirectory))
                    throw new DirectoryNotFoundException($"m2 credential directory was specified but not found ({m2CredentialDirectory})");

            var commandResult = await CommandFactory().SetCommand($"auth login " +
                                                                  $"{(force ? "--force " : string.Empty)}" +
                                                                  $"{(string.IsNullOrEmpty(ivyCredentialDirectory) ? string.Empty : $"--ivy_credential_directory {ivyCredentialDirectory} ")}" +
                                                                  $"{(string.IsNullOrEmpty(m2CredentialDirectory) ? string.Empty : $"--m2_credential_directory {m2CredentialDirectory} ")}")
                .Execute();


            commandResult.Select(JsonConvert.DeserializeObject<SpatialResponse>).ForEach(o => o.ToLog(Log));
        }

        /* spatial auth logout [flags] */
        public async Task Logout()
        {
            var commandResult = await CommandFactory().SetCommand("auth logout").Execute();
            commandResult.Select(JsonConvert.DeserializeObject<SpatialResponse>).ForEach(o => o.ToLog(Log));
        }

        /* spatial auth -h */
        public async Task<string> Help()
        {
            var result = await CommandFactory().SetCommand("auth -h").Execute(true);
            return result.First();
        }
    }
}