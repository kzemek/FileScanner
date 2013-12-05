using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileScanner.SearchSummary
{
    class PositionTextPair
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
}
