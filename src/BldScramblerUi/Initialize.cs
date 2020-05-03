using BldScramblerLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BldScramblerUi
{
    /// <summary>
    /// Gets nodes from files, generating the files if necessary
    /// </summary>
    public static class Initialize
    {
        public static Tuple<List<Node>, List<Node>> GetNodes()
        {
            List<Node> edgeNodes;
            List<Node> cornerNodes;
            if(!File.Exists("edges.json"))
            {
                edgeNodes = GenerateNodes.GenerateEdgeNodes();
                using (var writer = new StreamWriter("edges.json"))
                {
                    writer.Write(JsonConvert.SerializeObject(edgeNodes));
                }
            }
            else
            {
                using (var reader = new StreamReader("edges.json"))
                {
                    var str = reader.ReadToEnd();
                    edgeNodes = JsonConvert.DeserializeObject<List<Node>>(str);
                }
            }
            if (!File.Exists("corners.json"))
            {
                cornerNodes = GenerateNodes.GenerateCornerNodes();
                using (var writer = new StreamWriter("corners.json"))
                {
                    writer.Write(JsonConvert.SerializeObject(cornerNodes));
                }
            }
            else
            {
                using (var reader = new StreamReader("corners.json"))
                {
                    var str = reader.ReadToEnd();
                    cornerNodes = JsonConvert.DeserializeObject<List<Node>>(str);
                }
            }
            return Tuple.Create(edgeNodes, cornerNodes);
        }

        public static List<CombinationNode> GetCombinationNodes(List<Node> edgeNodes, List<Node> cornerNodes)
        {
            List<CombinationNode> combinationNodes;
            if (!File.Exists("algNums.json"))
            {
                edgeNodes = GenerateNodes.GenerateEdgeNodes();
                var combinationLeaves = Composer.GetCombinationLeaves(edgeNodes, cornerNodes);
                combinationNodes = Composer.GetCombinationNodes(combinationLeaves);
                using (var writer = new StreamWriter("algNums.json"))
                {
                    writer.Write(JsonConvert.SerializeObject(combinationNodes));
                }
            }
            else
            {
                using (var reader = new StreamReader("algNums.json"))
                {
                    var str = reader.ReadToEnd();
                    combinationNodes = JsonConvert.DeserializeObject<List<CombinationNode>>(str);
                }
            }
            return combinationNodes;
        }
    }
}
