using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileScanner.Preprocessing
{
    public interface IPreprocessorFactory
    {
        /// <summary>
        /// Creates and returns a new IPreprocessor instance.
        /// </summary>
        /// <returns>new IPreprocessor instance</returns>
        IPreprocessor GetIPreprocessor();
    }
}
