using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BldScramblerUi
{
    public class ScrambleFileException : Exception
    {
        public ScrambleFileException(string message) : base(message) { }
    }
}
