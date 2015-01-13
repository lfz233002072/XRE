// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Microsoft.Framework.Runtime.Roslyn
{
    /// <summary>
    /// An implementation of <see cref="ICompilationFailure"/> for Roslyn compilation.
    /// </summary>
    public class RoslynCompilationFailure : ICompilationFailure
    {
        /// <summary>
        /// Initializes a new instance of <see cref="RoslynCompilationFailure"/>.
        /// </summary>
        /// <param name="diagnostics">A sequence of <see cref="Diagnostic"/>s from Roslyn compilation.</param>
        public RoslynCompilationFailure(IEnumerable<Diagnostic> diagnostics)
        {
            Messages = diagnostics.Select(diagnostic => new RoslynCompilationMessage(diagnostic));
        }

        /// <inheritdoc />
        public IEnumerable<ICompilationMessage> Messages { get; }

        /// <inheritdoc />
        public string SourceFilePath { get; }

        /// <inheritdoc />
        public string SourceFileContent
        {
            get { return TryReadFile(SourceFilePath); }
        }

        /// <inheritdoc />
        public string CompiledContent { get; } = null;

        private static string TryReadFile(string sourceFilePath)
        {
            try
            {
                return File.ReadAllText(sourceFilePath);
            }
            catch (Exception)
            {
                // Don't throw if we encounter an exception.
                return string.Empty;
            }
        }
    }
}