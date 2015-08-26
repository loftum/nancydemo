using System;
using System.IO;
using Nancy;

namespace NancyDemo.Areas.Bootstrap
{
    public class AreaRootPathProvider : IRootPathProvider
    {
        private readonly string _rootPath;

        public AreaRootPathProvider(string area)
        {
            _rootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Areas", area);
        }

        public string GetRootPath()
        {
            return _rootPath;
        }
    }
}