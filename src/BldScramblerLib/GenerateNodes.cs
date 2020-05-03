using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BldScramblerLib
{
    public class GenerateNodes
    {
        /// <summary>
        /// Generates nodes for the edges of the Rubik's cube.
        /// Note that only even permutations are allowed, because parity is solved when solving corners.
        /// </summary>
        /// <returns></returns>
        public static List<Node> GenerateEdgeNodes()
        {
            var decomposer = new Decomposer();
            var perms = decomposer.Decompose(12);
            var finalPerms = perms
                .SelectMany(x => decomposer.ApplyTwists(x, 2))
                .Where(x => x.Cycles.Count % 2 == 0)
                .ToList();

            var tempNode = new Node(finalPerms, 0);
            var leaves = tempNode.Leaves;
            var nodes = Composer.GetNodes(leaves);
            return nodes;
        }

        /// <summary>
        /// Generates nodes for the corners of the Rubik's cube
        /// </summary>
        /// <returns></returns>
        public static List<Node> GenerateCornerNodes()
        {
            var decomposer = new Decomposer();
            var perms = decomposer.Decompose(8);
            var finalPerms = perms
                .SelectMany(x => decomposer.ApplyTwists(x, 3))
                .ToList();
            var nodes = Composer.GetNodes(finalPerms);
            return nodes;
        }
    }
}
