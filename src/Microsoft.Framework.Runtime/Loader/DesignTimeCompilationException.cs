// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Framework.Runtime
{
    internal class DesignTimeCompilationException : Exception, ICompilationException
    {
        public DesignTimeCompilationException(IList<string> errors)
            : base(string.Join(Environment.NewLine, errors))
        {
            var failure = new CompilationFailure
            {
                Messages = errors.Select(e => new CompilationMessage { Message = e })
            };
            CompilationFailures = new[] { failure };
            Errors = errors;
        }

        public IList<string> Errors { get; }

        public IEnumerable<ICompilationFailure> CompilationFailures { get; }

        private sealed class CompilationFailure : ICompilationFailure
        {
            public IEnumerable<ICompilationMessage> Messages { get; set; }

            public string CompiledContent { get; } = null;

            public string SourceFileContent { get; } = null;

            public string SourceFilePath { get; } = null;
        }

        private sealed class CompilationMessage : ICompilationMessage
        {
            public string Message { get; set; }

            public int EndColumn { get; } = 0;

            public int EndLine { get; } = 0;

            public int StartColumn { get; } = 0;

            public int StartLine { get; } = 0;
        }
    }
}