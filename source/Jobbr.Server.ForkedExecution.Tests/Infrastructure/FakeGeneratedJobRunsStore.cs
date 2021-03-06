using System;
using System.Collections.Generic;
using System.Linq;
using Jobbr.ComponentModel.Execution.Model;

namespace Jobbr.Server.ForkedExecution.Tests.Infrastructure
{
    public class FakeGeneratedJobRunsStore
    {
        private readonly List<FakeJobRunStoreTuple> store = new List<FakeJobRunStoreTuple>();

        private readonly object syncRoot = new object();

        internal FakeJobRunStoreTuple CreateFakeJobRun()
        {
            return this.CreateFakeJobRun(DateTime.UtcNow);
        }

        public FakeJobRunStoreTuple CreateFakeJobRun(DateTime plannedStartDateTimeUtc)
        {
            long id;
            lock (this.syncRoot)
            {
                id = this.store.Any() ? this.store.Max(e => e.Id) + 1 : 1;
            }

            var fakeJobRun = new FakeJobRunStoreTuple
            {
                Id = id,
        
                PlannedJobRun = new PlannedJobRun
                {
                    PlannedStartDateTimeUtc = plannedStartDateTimeUtc,
                    Id = id
                },
                JobRunInfo = new JobRunInfo
                {
                    Id = id,
                    JobId = new Random().Next(1, Int32.MaxValue),
                    TriggerId = new Random().Next(1, Int32.MaxValue),
                }
            };

            lock (this.syncRoot)
            {
                this.store.Add(fakeJobRun);
            }

            return fakeJobRun;
        }

        public FakeJobRunStoreTuple GetByJobRunId(long id)
        {
            lock (this.syncRoot)
            {
                return this.store.SingleOrDefault(e => e.Id == id);
            }
        }
    }
}