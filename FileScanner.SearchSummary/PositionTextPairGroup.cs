using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileScanner.SearchSummary
{
    class PositionTextPairGroup
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
}
