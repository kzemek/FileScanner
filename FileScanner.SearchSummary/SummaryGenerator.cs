using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

[assembly:InternalsVisibleTo("FileScanner.SearchSummary.Tests")]

namespace FileScanner.SearchSummary
{
    public struct SearchResult
    {
        public string fileName;
        public string fullFilePath;
        public long? fileSizeBytes;
        public DateTime? dateCreated;
        public DateTime? dateLastAccess;
        public DateTime? dateLastModified;

        public SearchResult(String fileName,
                            String fullFilePath,
                            long? fileSizeBytes,
                            DateTime? dateCreated,
                            DateTime? dateLastAccess,
                            DateTime? dateLastModified)
        {
            this.fileName = fileName;
            this.fullFilePath = fullFilePath;
            this.fileSizeBytes = fileSizeBytes;
            this.dateCreated = dateCreated;
            this.dateLastAccess = dateLastAccess;
            this.dateLastModified = dateLastModified;
        }

    }

    public class SummaryGenerator: ISummaryGenerator
    {
        internal void Generate(IDocumentBuilder builder,
                               ReportOptions options,
                               string searchQuery,
                               IEnumerable<string> inputPaths,
                               IEnumerable<MatchingFile> searchResults)
        {
            builder.AddReportHeader(options.headerHasGenerationDate ? DateTime.Now : (DateTime?)null,
                                    options.headerHasSearchQuery ? searchQuery : null,
                                    options.headerHasInputPaths ? inputPaths : null);

            builder.AddSectionHeader("Search results");
            foreach (MatchingFile match in searchResults.OrderByDescending(info => info.accuracy))
            {
                SearchResult result = new SearchResult();
                result.fileName = options.resultHasFileName ? match.fileInfo.Name : null;
                result.fullFilePath = options.resultHasFullFilePath ? match.fileInfo.ToString() : null;
                result.fileSizeBytes = options.resultHasFileSize ? match.fileInfo.Length : (long?)null;
                result.dateCreated = options.resultHasCreationTime ? match.fileInfo.CreationTime : (DateTime?)null;
                result.dateLastAccess = options.resultHasLastAccessTime ? match.fileInfo.LastAccessTime : (DateTime?)null;
                result.dateLastModified = options.resultHasLastModificationTime ? match.fileInfo.LastWriteTime : (DateTime?)null;

                builder.AddSearchResult(result);
            }

            builder.AddReportFooter();
            builder.Save(options.outputFilePath);
        }

        public void Generate(string searchQuery,
                             IEnumerable<string> inputPaths,
                             IEnumerable<MatchingFile> searchResults)
        {
            SummaryOptionsForm form = new SummaryOptionsForm();

            if (form.ShowDialog() == DialogResult.OK)
            {
                Generate(new TxtDocumentBuilder(), form.Options, searchQuery, inputPaths, searchResults);
            }
        }
    }
}
