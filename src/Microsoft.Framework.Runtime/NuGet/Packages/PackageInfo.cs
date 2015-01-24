using Microsoft.Framework.Runtime.DependencyManagement;
using System;
using System.IO;

namespace NuGet
{
    public class PackageInfo
    {
        private readonly IFileSystem _repositoryRoot;
        private readonly string _versionDir;
        private readonly LockFileLibrary _lockFileLibrary;
        private IPackage _package;

        public PackageInfo(
            IFileSystem repositoryRoot, 
            string packageId, 
            SemanticVersion version, 
            string versionDir,
            LockFileLibrary lockFileLibrary = null)
        {
            _repositoryRoot = repositoryRoot;
            Id = packageId;
            Version = version;
            _versionDir = versionDir;
            _lockFileLibrary = lockFileLibrary;
        }

        public string Id { get; private set; }

        public SemanticVersion Version { get; private set; }

        public IPackage Package
        {
            get
            {
                if (_package == null)
                {
                    var nuspecPath = Path.Combine(_versionDir, string.Format("{0}.nuspec", Id));
                    if (_lockFileLibrary == null)
                    {
                        _package = new UnzippedPackage(_repositoryRoot, nuspecPath);
                    }
                    else
                    {
                        _package = new LockFilePackage(_repositoryRoot, nuspecPath, _lockFileLibrary);
                    }
                }

                return _package;
            }
        }
    }
}