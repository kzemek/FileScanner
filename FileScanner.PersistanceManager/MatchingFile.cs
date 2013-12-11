using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using FileScanner.PatternMatching;

namespace FileScanner.PersistanceManager
{
    [Serializable()]
    public class MatchingFile
    {
        public MatchingFile(string fileName, string fullPath, long sizeInBytes, IEnumerable<Match> matches)
        {
            FileName = fileName;
            FullPath = fullPath;
            SizeInBytes = sizeInBytes;
            Matches = matches;
        }

        internal MatchingFile(DataRow row, IEnumerable<Match> matches): this(row["fileName"].ToString(), row["fullPath"].ToString(), long.Parse(row["sizeInBytes"].ToString()), matches)
        {
        }

        public string FileName { get; private set; }

        public string FullPath { get; private set; }

        public long SizeInBytes { get; private set; }

        public IEnumerable<Match> Matches { get; private set; }

        protected bool Equals(MatchingFile other)
        {
            return string.Equals(FileName, other.FileName) && string.Equals(FullPath, other.FullPath) && SizeInBytes == other.SizeInBytes && Matches.SequenceEqual(other.Matches);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((MatchingFile) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (FileName != null ? FileName.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (FullPath != null ? FullPath.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ SizeInBytes.GetHashCode();
                hashCode = (hashCode*397) ^ (Matches != null ? Matches.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}