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

    class SummaryGenerator: ISummaryGenerator
    {
        internal static List<PositionTextPairGroup> GroupMatchPositions(
                IDictionary<string, IEnumerable<int>> matchPositions,
                int contextSizeChars)
        {
            IEnumerable<PositionTextPair> positionToText =
                    matchPositions.Select(pair => new List<PositionTextPair>(pair.Value.Select(pos => new PositionTextPair(pos, pair.Key))))
                                  .Aggregate(new List<PositionTextPair>(), (list, all) => new List<PositionTextPair>(all.Concat(list)))
                                  .OrderBy(pair => pair.position);

            List<PositionTextPairGroup> groups = new List<PositionTextPairGroup>();
            foreach (PositionTextPair pair in positionToText)
            {
                bool hasMatchingGroup = false;

                foreach (PositionTextPairGroup group in groups)
                {
                    if (group.IsWithinRange(pair, contextSizeChars))
                    {
                        group.Extend(pair, contextSizeChars);
                        hasMatchingGroup = true;
                        break;
                    }
                }

                if (!hasMatchingGroup)
                    groups.Add(new PositionTextPairGroup(pair, contextSizeChars));
            }

            return groups;
        }

        internal static void GenerateContext(IDocumentBuilder builder,
                                             List<PositionTextPairGroup> groups,
                                             StreamReader _reader)
        {
            if (groups.Count == 0)
                return;

            builder.BeginContextBlock();

            PositionAwareStreamReader reader = new PositionAwareStreamReader(_reader.BaseStream, _reader.CurrentEncoding);
            PositionTextPairGroup prevGroup = null;

            foreach (PositionTextPairGroup group in groups)
            {
                char[] textBuf = new char[group.MaxChunkSize];
                long lastEnd = group.startPosition;
                int charsRead;

                reader.Seek(group.startPosition);

                if (prevGroup == null)
                {
                    if (groups.First().startPosition > 0)
                        builder.AddContextText("... ");
                }
                else if (group.startPosition != prevGroup.endPosition)
                    builder.AddContextText(" ... ");


                foreach (PositionTextPair pair in group.pairs.OrderBy(p => p.position))
                {
                    if (pair.position > lastEnd)
                    {
                        charsRead = reader.Read(textBuf, 0, (int)(pair.position - lastEnd));
                        lastEnd += charsRead;
                        builder.AddContextText(new string(textBuf, 0, charsRead));
                    }
                    charsRead = (int)reader.Read(textBuf, 0, pair.text.Length);
                    lastEnd += charsRead;
                    builder.AddContextText(new string(textBuf, 0, charsRead), TextStyle.Bold);
                }

                charsRead = reader.Read(textBuf, 0, (int)Math.Max(0, group.endPosition - lastEnd));
                builder.AddContextText(new string(textBuf, 0, charsRead));

                prevGroup = group;
            }

            if (!reader.EndOfStream)
                builder.AddContextText(" ...");

            builder.EndContextBlock();
        }

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

            IEnumerable<MatchingFile> sortedResults = searchResults.OrderByDescending(info => info.accuracy);
            if (options.maxEntries > 0)
                sortedResults = sortedResults.Take(options.maxEntries);

            foreach (MatchingFile match in sortedResults)
            {
                SearchResult result = new SearchResult();
                result.fileName = match.fileInfo.Name;
                result.fullFilePath = options.resultHasFullFilePath ? match.fileInfo.ToString() : null;
                result.fileSizeBytes = options.resultHasFileSize ? match.fileInfo.Length : (long?)null;
                result.dateCreated = options.resultHasCreationTime ? match.fileInfo.CreationTime : (DateTime?)null;
                result.dateLastAccess = options.resultHasLastAccessTime ? match.fileInfo.LastAccessTime : (DateTime?)null;
                result.dateLastModified = options.resultHasLastModificationTime ? match.fileInfo.LastWriteTime : (DateTime?)null;

                builder.AddSearchResult(result);

                if (options.resultHasContext)
                {
                    List<PositionTextPairGroup> groups = GroupMatchPositions(match.searchResults, options.contextSizeChars);
                    GenerateContext(builder, groups, match.fileReader);
                }
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
                ReportOptions options = form.Options;
                string fileExtension = Path.GetExtension(options.outputFilePath);
                IDocumentBuilder builder = DocumentBuilderFactory.Create(fileExtension);

                Generate(builder, options, searchQuery, inputPaths, searchResults);
            }
        }
    }
}
