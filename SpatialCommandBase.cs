using System;

namespace SpatialDotNet
{
    public abstract class SpatialCommandBase
    {
        protected Func<SpatialCommandRunner> CommandFactory;
        
        protected SpatialCommandBase(Func<SpatialCommandRunner> commandFactory)
        {
            CommandFactory = commandFactory;
        }
    }
}