using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BldScramblerLib
{
    public static class Scrambler
    {
        /// <summary>
        /// Gets a scramble sequence based on the number of algorithms specified.
        /// </summary>
        /// <param name="edgeNodes">All possible edge permutations and their probabilites</param>
        /// <param name="cornerNodes">All possible corner permutations and their probabilites</param>
        /// <param name="combinationNode">A set of possibilities of numbers of egde and corner algorithms.</param>
        /// <param name="rand"></param>
        /// <returns></returns>
        public static string GetScramble(List<Node> edgeNodes, List<Node> cornerNodes, CombinationNode combinationNode, Random rand)
        {
            var leaves = combinationNode.SelectPermutation(edgeNodes, cornerNodes, rand);
            return GetScrambleFromLeaves(leaves.Item1, leaves.Item2, rand);
        }

        /// <summary>
        /// Gets a scramble sequence based on the permutations in the nodes and their probabilities
        /// </summary>
        /// <param name="edgeNode">A set of edge permutations and their relative probabilities</param>
        /// <param name="cornerNode">A set of corner permutations and their relative probabilities</param>
        /// <param name="rand"></param>
        /// <param name="isFloatingTwist">If true, any corner twist will be floating, if false, it won't be floating, if null, twisted corners will be twisted randomly</param>
        /// <returns></returns>
        public static string GetScramble(Node edgeNode, Node cornerNode, Random rand, bool? isFloatingTwist)
        {
            var edgeLeaf = edgeNode.Leaves.SelectRandom(x => x.Probability, rand);
            var cornerLeaf = cornerNode.Leaves.SelectRandom(x => x.Probability, rand);
            if(isFloatingTwist == null)
                return GetScrambleFromLeaves(edgeLeaf, cornerLeaf, rand);

            List<Tuple<int, int>> twistWeights = new List<Tuple<int, int>>();
            for (int k = 0; k <= cornerLeaf.NumTwisted; k++)
            {
                if(isFloatingTwist.Value == ((cornerLeaf.NumTwisted + k) % 3 == 0))
                {
                    twistWeights.Add(Tuple.Create(k, MyMath.Choose(cornerLeaf.NumTwisted, k)));
                }
            }
            var sum = twistWeights.Sum(x => x.Item2);
            var randInt = rand.Next(sum);
            var curr = 0;
            var numTwisted = 0;
            foreach (var tuple in twistWeights)
            {
                curr += tuple.Item2;
                if(curr > randInt)
                {
                    numTwisted = tuple.Item1;
                    break;
                }
            }
            return GetScrambleFromLeaves(edgeLeaf, cornerLeaf, rand, numTwisted);
        }

        private static string GetScrambleFromLeaves(FinalLeaf edgeLeaf, FinalLeaf cornerLeaf, Random rand, int twistedRight = -1)
        {
            var cube = new SetupCube(rand);
            cube.ApplyCornerLeaf(cornerLeaf, twistedRight);
            cube.ApplyEdgeLeaf(edgeLeaf);
            var cubeStr = cube.ToKociembaString();

            var startInfo = new ProcessStartInfo
            {
                UseShellExecute = false,
                FileName = "kociemba.exe",
                Arguments = cubeStr,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };

            try
            {
                var process = new Process { StartInfo = startInfo };
                process.Start();
                process.WaitForExit();
                var output = process.StandardOutput.ReadToEnd();
                return Inverser.Inverse(output);
            }
            catch
            {
                return null;
            }
        }
    }
}
