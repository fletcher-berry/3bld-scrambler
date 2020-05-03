using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BldScramblerLib
{
    public static class SelectorExtensions
    {
        /// <summary>
        /// From a list of values and thier probabilities, select one randomly.  It is expected that the probabilities of all elements sum to 1.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="candidates">The values to choose from</param>
        /// <param name="probabilitySelector">A function used to get an element's probability</param>
        /// <param name="random"></param>
        /// <returns></returns>
        public static TValue SelectRandom<TValue>(this List<TValue> candidates, Func<TValue, Fraction> probabilitySelector, Random random)
        {
            var probability = Fraction.Zero;
            var target = random.NextDouble();
            foreach (var candidate in candidates)
            {
                var prob = probabilitySelector(candidate);
                probability += prob;
                if (probability.ToDouble() > target)
                    return candidate;
            }
            throw new Exception("Unable to select a random value");
        }
    }
}
