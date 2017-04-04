using System.IO;
using Jobbr.ComponentModel.Execution;
using Jobbr.ComponentModel.Execution.Model;
using Jobbr.Server.ForkedExecution.Execution;

namespace Jobbr.Server.ForkedExecution.Tests.Infrastructure
{
    public class TestBase
    {
        protected FakeGeneratedJobRunsStore jobRunFakeTuples;
        protected JobRunProgressUpdateStore storedProgressUpdates;
        protected JobRunInfoServiceMock jobRunInformationService;
        protected PeriodicTimerMock periodicTimerMock;
        protected ManualTimeProvider manualTimeProvider;

        public TestBase()
        {
            this.jobRunFakeTuples = new FakeGeneratedJobRunsStore();
            this.storedProgressUpdates = new JobRunProgressUpdateStore();
            this.jobRunInformationService = new JobRunInfoServiceMock(this.jobRunFakeTuples);
        }

        protected static ForkedExecutionConfiguration GivenAMinimalConfiguration()
        {
            var forkedExecutionConfiguration = new ForkedExecutionConfiguration()
            {
                BackendAddress = "notNeeded",
                JobRunDirectory = Path.GetTempPath(),
                JobRunnerExecutable = "Jobbr.Server.ForkedExecution.TestEcho.exe",
                MaxConcurrentProcesses = 4
            };

            return forkedExecutionConfiguration;
        }

        protected ForkedJobExecutor GivenAStartedExecutor(ForkedExecutionConfiguration forkedExecutionConfiguration)
        {
            this.periodicTimerMock = new PeriodicTimerMock();
            this.manualTimeProvider = new ManualTimeProvider();

            var jobRunContextFactory = new JobRunContextFactory(forkedExecutionConfiguration, this.storedProgressUpdates);

            var executor = new ForkedJobExecutor(jobRunContextFactory,  this.jobRunInformationService, this.storedProgressUpdates, this.periodicTimerMock, this.manualTimeProvider, forkedExecutionConfiguration);

            executor.Start();

            return executor;
        }
    }
}