using FileScanner.PatternMatching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileScanner
{

    /// <summary>
    /// Results of one single search. Provide information needed by SearchSummary and PersistanceManager modules.
    /// Represents all results of single search of multiple phrases in multiple searchees
    /// </summary>
    public interface ISearchResult
    {
        /// <summary>
        /// All searched phrases
        /// </summary>
        IEnumerable<string> Phrases { get; }

        /// <summary>
        /// All searchees that had at least one match in search
        /// </summary>
        IEnumerable<SearcheeResult> Searchees { get; }

        /// <summary>
        /// Total number of searched searchees in search (:P). Includes searchees with no match.
        /// </summary>
        int ProcessedSearcheesCount { get; }

        /// <summary>
        /// Date when search started
        /// </summary>
        DateTime StartDate { get; }

        /// <summary>
        /// Date when search ended
        /// </summary>
        DateTime EndDate { get; }
    }



    internal class SearchResult : ISearchResult
    {
        public IEnumerable<SearcheeResult> Searchees { get; private set; }
        public int ProcessedSearcheesCount { get; private set; }
        public IEnumerable<string> Phrases { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }


        internal SearchResult(IEnumerable<string> phrases, IEnumerable<SearcheeResult> results,
            int processedPathsCount, DateTime startDate, DateTime endDate)
        {
            Phrases = phrases;
            Searchees = results;
            ProcessedSearcheesCount = processedPathsCount;
            StartDate = startDate;
            EndDate = endDate;
        }
    }


    internal class EmptySearchResult : SearchResult
    {
        internal EmptySearchResult() : base(new string[] { }, new SearcheeResult[] { }, 0, DateTime.MinValue, DateTime.MinValue) { }
    }


    /// <summary>
    /// Translates data from PersistanceManager's ISearch to Core's ISearchResult
    /// </summary>
    internal class SearchResultAdapter : ISearchResult
    {
        public IEnumerable<SearcheeResult> Searchees { get; private set; }
        public int ProcessedSearcheesCount { get; private set; }
        public IEnumerable<string> Phrases { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        internal SearchResultAdapter(PersistanceManager.Interfaces.ISearch search)
        {
            StartDate = search.StartTime;
            EndDate = search.EndTime;
            ProcessedSearcheesCount = (int)search.ProcessedFilesCount;
            Phrases = search.Phrases;

            var searchees = new List<SearcheeResult>(search.Count());
            foreach (var matchingFile in search)
            {
                var searchee = new FileSearchee(matchingFile.FullPath);
                searchees.Add(new SearcheeResult(searchee, matchingFile.Matches));
            }

            Searchees = searchees;
        }
    }


    /// <summary>
    /// Represents all results of single search of multiple phrases in one Searchee (usually Searchee == file)
    /// </summary>
    public class SearcheeResult
    {
        public ISearchee Searchee { get; private set; }
        public IEnumerable<Match> Matches { get; private set; }

        internal SearcheeResult(ISearchee searchee, IEnumerable<Match> matches)
        {
            this.Searchee = searchee;
            this.Matches = matches;
        }
    }
}
