using System;
using FluentScheduler;

namespace Lottery.DataUpdater.Jobs
{
    public class StructureMapJobFactory : IJobFactory
    {
        public IJob GetJobInstance<T>() where T : IJob
        {
            var job = Activator.CreateInstance<T>();
            return job;
        }
    }
}