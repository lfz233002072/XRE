// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Microsoft.Framework.Runtime.Roslyn
{
    /// <summary>
    /// An implementation of <see cref="ICompilationMessage"/> that wraps 
    /// <see cref="Diagnostic"/> instances from Roslyn compilation.
    /// </summary>
    public class RoslynCompilationMessage : ICompilationMessage
    {
        /// <summary>
        /// Initializes a new instance of <see cref="RoslynCompilationMessage"/>.
        /// </summary>
        /// <param name="diagnostic">The <see cref="Diagnostic"/> instance to read
        /// diagnostic details from.</param>
        public RoslynCompilationMessage(Diagnostic diagnostic)
        {
            Message = CSharpDiagnosticFormatter.Instance.Format(diagnostic);

            var lineSpan = diagnostic.Location.GetMappedLineSpan();
            StartColumn = lineSpan.StartLinePosition.Character;
            StartLine = lineSpan.StartLinePosition.Line;

            EndColumn = lineSpan.EndLinePosition.Character;
            EndLine = lineSpan.EndLinePosition.Line;
        }

        /// <inheritdoc />
        public int EndColumn { get; }

        /// <inheritdoc />
        public int EndLine { get; }

        /// <inheritdoc />
        public string Message { get; }

        /// <inheritdoc />
        public int StartColumn { get; }

        /// <inheritdoc />
        public int StartLine { get; }
    }
}