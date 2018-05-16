using System.Collections.Generic;

namespace SpatialDotNet.Tests
{
    public class SpatialTestUtilities
    {
        public static string ProjectPath { get; } = @"G:\Frontier\spatialos";
        public static string DeploymentName { get; } = @"";

        public static bool AllResponsesAreOk(IEnumerable<SpatialResponse> responses)
        {
            foreach(var response in responses)
                if (response.Level >= SpatialLogLevel.Warning)
                    return false;

            return true;
        }
    }
}