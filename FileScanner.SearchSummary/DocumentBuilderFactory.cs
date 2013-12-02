using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace FileScanner.SearchSummary
{
    class DocumentBuilderFactory
    {
        private static IDictionary<string, Type> SUPPORTED_FORMATS =
                new ReadOnlyDictionary<string, Type>(
                    new Dictionary<string, Type> {
                        { ".pdf", typeof(PDFDocumentBuilder) },
                        { ".txt", typeof(TxtDocumentBuilder) }
                    });

        public static IEnumerable<string> GetSupportedFormats()
        {
            return SUPPORTED_FORMATS.Select(keyVal => keyVal.Key);
        }

        public static string GetDefaultFormat()
        {
            return SUPPORTED_FORMATS.First().Key;
        }

        public static IDocumentBuilder Create(string type)
        {
            type = type.ToLower();

            if (!SUPPORTED_FORMATS.ContainsKey(type))
                throw new ArgumentException("Unsupported file format: " + type);

            return (IDocumentBuilder)Activator.CreateInstance(SUPPORTED_FORMATS[type]);
        }
    }
}
