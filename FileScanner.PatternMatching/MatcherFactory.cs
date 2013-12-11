using System;
using System.Collections.Generic;

namespace FileScanner.PatternMatching
{
    /// <summary>
    /// The factory creating concrete objects implementing IMatcher interface.
    /// </summary>
    public class MatcherFactory
    {
        /// <summary>
        /// Enumerates possible algorithms used in classes implementing
        /// IMatcher.
        /// </summary>
        public enum MatchAlgorithm
        {
            Regex,
            AhoCorasick
        }

        /// <summary>
        /// Initializes a new object implementing the IMatcher interface for
        /// finding the specified patterns.
        /// </summary>
        /// <param name="patterns">The patterns to search for in text.</param>
        /// <param name="algorithm">
        /// The algorithm used for underlying implementation.
        /// </param>
        public IMatcher Create(List<string> patterns, MatchAlgorithm algorithm = MatchAlgorithm.AhoCorasick)
        {
            if (patterns.Count == 0)
                throw new ArgumentException("No patterns were given.");

            switch (algorithm)
            {
                case MatchAlgorithm.AhoCorasick:
                    return new AhoMatcher(patterns);

                case MatchAlgorithm.Regex:
                    return new RegexMatcher(patterns);

                default:
                    throw new ArgumentException("Invalid algorithm given");
            }
        }
    }
}
