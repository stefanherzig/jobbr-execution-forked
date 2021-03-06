﻿using System;
using System.Collections.Generic;
using Jobbr.ComponentModel.Registration;

namespace Jobbr.Server.ForkedExecution
{
    public class ForkedExecutionConfiguration : IFeatureConfiguration
    {
        public string BackendAddress { get; set; }

        public int MaxConcurrentProcesses { get; set; } = 4;

        public string JobRunnerExecutable { get; set; }

        public string JobRunDirectory { get; set; }

        public bool IsRuntimeWaitingForDebugger { get; set; }

        /// <summary>
        /// Callback to add custom parameters to jobrunner
        /// </summary>
        public Func<JobRunStartInfo, IEnumerable<KeyValuePair<string, string>>> AddJobRunnerArguments { get; set; }
    }
}
