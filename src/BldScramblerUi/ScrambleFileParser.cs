using BldScramblerLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BldScramblerUi
{
    /// <summary>
    /// Parses scramble files used to determine criteria for generating scrambles
    /// </summary>
    public class ScrambleFileParser
    {
        public static List<AbstractScramblerService> GetScramblers(StreamReader reader, List<CombinationNode> combinationNodes, List<Node> edgeNodes, List<Node> cornerNodes)
        {
            var scramblers = new List<AbstractScramblerService>();
            var lineNum = 1;
            while (reader.Peek() > 0)
            {
                var line = reader.ReadLine().Trim();
                var tokens = line.Split(' ').Select(x => x.Trim()).ToList();
                if (tokens.Count == 0)
                    continue;
                if (tokens[0].Equals("algs", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (tokens.Count == 1)
                        throw CreateError(lineNum, "Number of algs not specified");
                    if (!int.TryParse(tokens[1], out int numAlgs))
                        throw CreateError(lineNum, "Number of algs is not an integer");
                    if (numAlgs < 1 || numAlgs > 14)
                        throw CreateError(lineNum, "Number of algs must be between 1 and 14");
                    int weight = 1;
                    if (tokens.Count > 2)
                        weight = ParseWeight(tokens[2], lineNum);
                    if (tokens.Count > 3)
                        throw CreateError(lineNum, $"unexpected token '{tokens[3]}'");
                    scramblers.Add(new AlgScramblerService(combinationNodes.Single(x => x.NumAlgs == numAlgs), edgeNodes, cornerNodes, weight));

                }
                else if (tokens[0].Equals("edges", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (tokens.Count == 1)
                        throw CreateError(lineNum, "Number of flipped edges not specified");
                    if (!int.TryParse(tokens[1], out int flippedEdges))
                        throw CreateError(lineNum, "Number of flipped edges is not an integer");
                    if (flippedEdges < 0 || flippedEdges > 11)
                        throw CreateError(lineNum, "Number of flipped edges must be between 0 and 11");
                    var canBeGreater = false;
                    var weight = 1;
                    if(tokens.Count > 2)
                    {
                        if (int.TryParse(tokens[2], out int tempWeight))
                        {
                            weight = tempWeight;
                            if(weight <= 0)
                                throw CreateError(lineNum, "Weight must be greater than 0");
                            if (tokens.Count > 3)
                                throw CreateError(lineNum, $"unexpected token '{tokens[3]}'");
                        }
                        else
                        {
                            if (tokens[2].Equals("equal", StringComparison.CurrentCultureIgnoreCase))
                                canBeGreater = false;
                            else if (tokens[2].Equals("atleast", StringComparison.CurrentCultureIgnoreCase))
                                canBeGreater = true;
                            else
                                throw CreateError(lineNum, "expected 'equal' or 'atleast' to specify the amount of flipped edges");
                            if(tokens.Count > 3)
                            {
                                weight = ParseWeight(tokens[3], lineNum);
                                if (tokens.Count > 4)
                                    throw CreateError(lineNum, $"Unexpected token '{tokens[4]}'");
                            }
                        }
                    }
                    scramblers.Add(new EdgeScramblerService(edgeNodes, cornerNodes, flippedEdges, canBeGreater, weight));
                }
                else if(tokens[0].Equals("corners", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (tokens.Count == 1)
                        throw CreateError(lineNum, "Number of twisted corners not specified");
                    if (!int.TryParse(tokens[1], out int twistedCorners))
                        throw CreateError(lineNum, "Number of twisted corners is not an integer");
                    if (twistedCorners < 0 || twistedCorners > 7)
                        throw CreateError(lineNum, "Number of twisted corners must be between 0 and 7");
                    var canBeGreater = false;
                    bool? isFloating = null;
                    var weight = 1;
                    for (int k = 0; k < 2; k++)
                    {
                        if (tokens.Count > 2 + k)
                        {
                            if (int.TryParse(tokens[2 + k], out int tempWeight))
                            {
                                weight = tempWeight;
                                if (weight <= 0)
                                    throw CreateError(lineNum, "Weight must be greater than 0");
                                if (tokens.Count > 2 + k + 1)
                                    throw CreateError(lineNum, $"unexpected token '{tokens[2 + k + 1]}'");
                                break;
                            }
                            else
                            {
                                if (tokens[2 + k].Equals("equal", StringComparison.CurrentCultureIgnoreCase))
                                    canBeGreater = false;
                                else if (tokens[2 + k].Equals("atleast", StringComparison.CurrentCultureIgnoreCase))
                                    canBeGreater = true;
                                else if (tokens[2 + k].Equals("floating", StringComparison.CurrentCultureIgnoreCase))
                                    isFloating = true;
                                else if (tokens[2 + k].Equals("nonfloating", StringComparison.CurrentCultureIgnoreCase))
                                    isFloating = false;
                                else
                                    throw CreateError(lineNum, "expected 'equal' or 'atleast' to specify the amount of twisted corners: or 'floating' or 'nonfloating' to specify whether the twist is floating");
                            }
                        }
                    }
                    if (tokens.Count > 4)
                        weight = ParseWeight(tokens[4], lineNum);
                    if (tokens.Count > 5)
                        throw CreateError(lineNum, $"Unexpected token '{tokens[5]}'");
                    if (canBeGreater && isFloating != null)
                        throw CreateError(lineNum, "If specifying atleast, cannot specfy floating");
                    if (twistedCorners == 0 && isFloating == false)
                        throw CreateError(lineNum, "0-twists must be floating.");
                    if (twistedCorners == 1 && isFloating == true)
                        throw CreateError(lineNum, "1-twists must be nonfloating");
                    scramblers.Add(new CornerScramblerService(edgeNodes, cornerNodes, twistedCorners, canBeGreater, isFloating, weight));
                }
                lineNum++;
            }
            return scramblers;
        }

        private static ScrambleFileException CreateError(int lineNum, string text)
        {
            return new ScrambleFileException($"Line {lineNum}: {text}");
        }

        private static int ParseWeight(string token, int lineNum)
        {
            if (!int.TryParse(token, out int weight))
                throw CreateError(lineNum, "weight is not an integer");
            if (weight <= 0)
                throw CreateError(lineNum, "Weight must be greater than 0");
            return weight;
        }
    }
}
