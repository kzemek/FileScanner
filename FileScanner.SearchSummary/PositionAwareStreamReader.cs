using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileScanner.SearchSummary
{
    class PositionAwareStreamReader: StreamReader
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
}
