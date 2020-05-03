using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BldScramblerUi
{
    /// <summary>
    /// Creates scrambles based on certain criteria defined in a file
    /// </summary>
    public abstract class AbstractScramblerService
    {
        public int Weight { get; set; }

        public abstract string Scramble(Random rand);
    }
}
