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

    public class PositionTextPair
    {
        public int position;
        public string text;

        public PositionTextPair(int position,
                                string text)
        {
            this.position = position;
            this.text = text;
        }

        public bool OverlapsOrIsAdjacentTo(PositionTextPair p)
        {
            return (position <= p.position && position + text.Length >= p.position)
                || (p.position <= position && p.position + p.text.Length >= position);
        }

        public void Merge(PositionTextPair p)
        {
            if (p.position >= position && p.position + p.text.Length <= position + text.Length)
                return;
            else if (position >= p.position && position + text.Length <= p.position + p.text.Length)
                text = p.text;
            else if (position <= p.position && position + text.Length >= p.position)
            {
                if (position + text.Length == p.position)
                    text += p.text;
                else
                    text += p.text.Substring(position + text.Length - p.position);
            }
            else if (p.position <= position && p.position + p.text.Length >= position)
            {
                if (p.position + p.text.Length == position)
                    text = p.text + text;
                else
                    text = p.text + text.Substring(p.position + p.text.Length - position);
            }

            position = Math.Min(position, p.position);
        }

        public override string ToString()
        {
            return String.Format("(position = {0}, text = <{1}>)", position, text);
        }
    }

    public class PositionTextPairGroup
    {
        public int startPosition;
        public int endPosition;
        public List<PositionTextPair> pairs;

        public int MaxChunkSize
        {
            get
            {
                int max = 0;
                int prevEnd = startPosition;

                foreach (PositionTextPair pair in pairs)
                {
                    max = Math.Max(max, pair.position - prevEnd);
                    max = Math.Max(max, pair.text.Length);
                    prevEnd = pair.position + pair.text.Length;
                }

                max = Math.Max(max, endPosition - pairs.Last().position + pairs.Last().text.Length);
                return max;
            }
        }

        public PositionTextPairGroup(PositionTextPair pair,
                                     int contextSizeChars)
        {
            startPosition = Math.Max(0, pair.position - contextSizeChars);
            endPosition = pair.position + pair.text.Length + contextSizeChars;
            pairs = new List<PositionTextPair>();
            pairs.Add(pair);
        }

        public PositionTextPairGroup Extend(PositionTextPair pair,
                                            int contextSizeChars)
        {
            if (startPosition > Math.Max(0, pair.position - contextSizeChars))
                startPosition = Math.Max(0, pair.position - contextSizeChars);
            else if (endPosition < pair.position + pair.text.Length + contextSizeChars)
                endPosition = pair.position + pair.text.Length + contextSizeChars;

            foreach (PositionTextPair p in pairs)
            {
                if (p.OverlapsOrIsAdjacentTo(pair))
                {
                    p.Merge(pair);
                    return this;
                }
            }
            pairs.Add(pair);
            return this;
        }

        public bool IsWithinRange(PositionTextPair pair,
                                  int contextSizeChars)
        {
            int distanceToStart = startPosition - (pair.position + pair.text.Length);
            int distanceFromEnd = pair.position - endPosition;

            Console.WriteLine("isWithinRange: group [{0} - {1}], pair {2}, {3}, context {4} => {5} {6}",
                              startPosition, endPosition, pair.position, pair.text.Length, contextSizeChars, distanceToStart, distanceFromEnd);
            return Math.Max(distanceToStart, distanceFromEnd) <= contextSizeChars;
        }

        public override string ToString()
        {
            return String.Format("startPosition {0}, endPosition {1}\npairs [{2}]", 
                                 startPosition, endPosition, pairs.Aggregate("", (p, all) => p.ToString() + "\n" + all));
        }
    }

    public class PositionAwareStreamReader: StreamReader
    {
        public long Position { get; private set; }

        public PositionAwareStreamReader(Stream stream):
            base(stream)
        {
            Position = 0;
        }

        public override int Read(char[] buffer, int offset, int count)
        {
            int bytesRead = base.Read(buffer, offset, count);
            Position += bytesRead;
            return bytesRead;
        }

        public void Seek(long position)
        {
            if (position == Position)
                return;

            if (position < Position)
                throw new NotImplementedException();
            else
            {
                char[] buffer = new char[1024];
                while (Position < position)
                    Position += Read(buffer, 0, Math.Min(1024, (int)(position - Position)));
            }
        }
    }

    public class SummaryGenerator: ISummaryGenerator
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
            PositionAwareStreamReader reader = new PositionAwareStreamReader(_reader.BaseStream);

            foreach (PositionTextPairGroup group in groups)
            {
                char[] textBuf = new char[group.MaxChunkSize];
                long lastEnd = group.startPosition;
                int charsRead;

                reader.Seek(group.startPosition);

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
            }
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
                result.fileName = options.resultHasFileName ? match.fileInfo.Name : null;
                result.fullFilePath = options.resultHasFullFilePath ? match.fileInfo.ToString() : null;
                result.fileSizeBytes = options.resultHasFileSize ? match.fileInfo.Length : (long?)null;
                result.dateCreated = options.resultHasCreationTime ? match.fileInfo.CreationTime : (DateTime?)null;
                result.dateLastAccess = options.resultHasLastAccessTime ? match.fileInfo.LastAccessTime : (DateTime?)null;
                result.dateLastModified = options.resultHasLastModificationTime ? match.fileInfo.LastWriteTime : (DateTime?)null;

                builder.AddSearchResult(result);

                List<PositionTextPairGroup> groups = GroupMatchPositions(match.searchResults, options.contextSizeChars);
                GenerateContext(builder, groups, match.fileReader);
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
