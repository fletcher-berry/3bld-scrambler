using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BldScramblerLib
{

    public static class Inverser
    {
        /// <summary>
        /// Finds the inverse of an algorithm.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Inverse(string input)
        {
            return string.Join(" ", input.Split(' ').Select(x => InverseMove(x.Trim()).Trim()).Reverse());
        }

        private static string InverseMove(string move)
        {
            if (move.Length == 1)
                return $"{move}'";
            else if (move.EndsWith("2"))
                return move;
            else
                return move[0].ToString();
        }
    }
}
