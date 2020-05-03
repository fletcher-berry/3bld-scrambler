using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BldScramblerLib
{
    /// <summary>
    /// Used to find all possibilities of permutations of given numbers of pieces
    /// </summary>
    public class Decomposer
    {
        private Dictionary<int, List<PermLeaf>> Cache;

        public Decomposer()
        {
            Cache = new Dictionary<int, List<PermLeaf>>();
            Cache.Add(1, new List<PermLeaf> { new PermLeaf(new List<int> { 1 }, new Fraction(1, 1)) });
            Cache.Add(2, new List<PermLeaf> { new PermLeaf(new List<int> { 2 }, new Fraction(1, 2)),
                                              new PermLeaf(new List<int> { 1, 1 }, new Fraction(1, 2))});
        }

        /// <summary>
        /// Finds all permutations of the given number of pieces, and their relative probabilities
        /// </summary>
        /// <param name="permNum"></param>
        /// <returns></returns>
        public List<PermLeaf> Decompose(int permNum)
        {
            if(Cache.ContainsKey(permNum))
            {
                return Cache[permNum];
            }
            int max = Cache.Keys.Max();
            for(int k = max + 1; k <= permNum; k++)
            {
                Cache.Add(k, ActuallyDecompose(k));
            }
            return Cache[permNum];

        }

        private List<PermLeaf> ActuallyDecompose(int permNum)
        {
            var leaves = new List<PermLeaf>();
            leaves.Add(new PermLeaf(new List<int> { permNum }, new Fraction(1, permNum)));
            leaves.Add(new PermLeaf(new List<int> { permNum - 1, 1 }, new Fraction(1, permNum)));

            var perms = new List<Perm>();
            for(int k = 1; k <= permNum - 2; k++)
            {
                perms.Add(new Perm(new List<int> { k, permNum - k }, new Fraction(1, permNum)));
            }
            var newLeaves = perms.SelectMany(x => Decompose(x));
            leaves.AddRange(newLeaves);
            return leaves;

        }

        private List<PermLeaf> Decompose(Perm perm)
        {
            var baseList = perm.Cycles.Take(perm.Cycles.Count - 1).ToList();
            var permNum = perm.Cycles[perm.Cycles.Count - 1];
            var permLeaves = Decompose(permNum);
            var newLeaves = permLeaves.Select(x => new PermLeaf(baseList.Concat(x.Cycles).ToList(), (x.Probability * perm.Probability).Simplify())).ToList();
            return newLeaves;
        }

        /// <summary>
        /// Given a permutation of pieces, create a Final Leaf by finding all possibilities of the number of pieces twisted in place in the permutation, and their relative probabilities
        /// </summary>
        /// <param name="permLeaf"></param>
        /// <param name="numOrientations"></param>
        /// <returns></returns>
        public List<FinalLeaf> ApplyTwists(PermLeaf permLeaf, int numOrientations)
        {
            int numSolved = permLeaf.Cycles.Count(x => x == 1) - (permLeaf.Cycles[0] == 1 ? 1 : 0);
            List<(int, Fraction)> values = new List<(int, Fraction)>();
            for(int k = 0; k <= numSolved; k++)
            {
                Fraction prob = MyMath.Prob(numSolved, k, new Fraction(numOrientations - 1, numOrientations));
                values.Add((k, prob));
            }
            return values.Select((x) => new FinalLeaf(permLeaf.Cycles, permLeaf.Probability * x.Item2, x.Item1)).ToList();
        }


    }
}
