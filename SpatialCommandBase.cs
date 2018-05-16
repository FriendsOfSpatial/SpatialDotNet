using System;
using NLog;

namespace SpatialDotNet
{
    public abstract class SpatialCommandBase
    {
        protected Logger Log => LogManager.GetCurrentClassLogger();

        protected Func<SpatialCommandRunner> CommandFactory;
        
        protected SpatialCommandBase(Func<SpatialCommandRunner> commandFactory)
        {
            CommandFactory = commandFactory;
        }
    }
}