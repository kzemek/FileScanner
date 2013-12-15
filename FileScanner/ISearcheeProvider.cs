using FileScanner.FileParsing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileScanner
{
    public interface ISearcheeProvider : IEnumerable<ISearchee>, IEnumerable
    {
    }



    internal class ParsedFileSearcheeProvider : ISearcheeProvider
    {
        private static readonly Func<string, IEnumerable<string>> DefaultLister =
            x =>
            {
                return File.Exists(x) ? new[] { x } :
                    Directory.GetFiles(x, "*.*", SearchOption.AllDirectories);
            };


        private IParseStrategy _parseStrategy;
        private string _rootPath;

        /// <summary>
        /// Returns list of file paths in file tree for given root
        /// Getter exposed for testability purposes
        /// </summary>
        private Func<string, IEnumerable<string>> _lister = DefaultLister;
        internal Func<string, IEnumerable<string>> FileLister
        {
            private get { return _lister; }
            set { _lister = value; }
        }


        internal ParsedFileSearcheeProvider(string rootPath, IParseStrategy parseStrategy)
        {
            this._rootPath = rootPath;
            this._parseStrategy = parseStrategy;
        }


        public IEnumerator<ISearchee> GetEnumerator()
        {
            var files = FileLister(_rootPath);

            foreach (var filePath in files)
            {
                var fileParserFactory = new FileParserFactory(filePath, _parseStrategy);

                yield return new ParsedFileSearchee(filePath, fileParserFactory);
            }
        }


        System.Collections.IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
