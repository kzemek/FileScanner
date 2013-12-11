using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FileScanner.PersistanceManager.Interfaces;

namespace FileScanner.PersistanceManager
{
    [Serializable()]
    public abstract class AbstractSearch : ISearch
    {
        public DateTime StartTime { get; protected set; }
        public DateTime EndTime { get; protected set; }
        public uint ProcessedFilesCount { get; protected set; }
        public abstract IEnumerable<string> Phrases { get; protected set; }
        public abstract IEnumerator<MatchingFile> GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        protected bool Equals(AbstractSearch other)
        {
            return StartTime.Equals(other.StartTime) && EndTime.Equals(other.EndTime) && ProcessedFilesCount == other.ProcessedFilesCount && Phrases.SequenceEqual(other.Phrases) && this.SequenceEqual(other);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals((AbstractSearch) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = StartTime.GetHashCode();
                hashCode = (hashCode*397) ^ EndTime.GetHashCode();
                hashCode = (hashCode*397) ^ (int) ProcessedFilesCount;
                return hashCode;
            }
        }
    }
}
