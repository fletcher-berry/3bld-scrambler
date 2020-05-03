using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BldScramblerLib
{
    /// <summary>
    /// Used to set up a cube to be solved with Kociemba.
    /// </summary>
    public class SetupCube
    {
        // Each list represents a position ('cubie') on the cube, and each element of a list represents the color in that position
        public CircularLinkedList<char> URFList;
        public CircularLinkedList<char> URBList;
        public CircularLinkedList<char> ULFList;
        public CircularLinkedList<char> ULBList;     
        public CircularLinkedList<char> DRFList;
        public CircularLinkedList<char> DRBList;
        public CircularLinkedList<char> DLFList;
        public CircularLinkedList<char> DLBList;

        public CircularLinkedList<char> URList;
        public CircularLinkedList<char> ULList;
        public CircularLinkedList<char> UFList;
        public CircularLinkedList<char> UBList;
        public CircularLinkedList<char> DRList;
        public CircularLinkedList<char> DLList;
        public CircularLinkedList<char> DFList;
        public CircularLinkedList<char> DBList;
        public CircularLinkedList<char> BRList;
        public CircularLinkedList<char> FRList;
        public CircularLinkedList<char> FLList;
        public CircularLinkedList<char> BLList;

        private Random rand;

        public SetupCube(Random rand)
        {
            URBList = new CircularLinkedList<char> ( 'U', 'B', 'R' );
            URFList = new CircularLinkedList<char> ( 'U', 'R', 'F' );
            ULFList = new CircularLinkedList<char> ( 'U', 'F', 'L' );
            ULBList = new CircularLinkedList<char> ( 'U', 'L', 'B' );
            DRBList = new CircularLinkedList<char> ( 'D', 'R', 'B' );
            DRFList = new CircularLinkedList<char> ( 'D', 'F', 'R' );
            DLFList = new CircularLinkedList<char> ( 'D', 'L', 'F' );
            DLBList = new CircularLinkedList<char> ( 'D', 'B', 'L' );

            URList = new CircularLinkedList<char> ( 'U', 'R' );
            UFList = new CircularLinkedList<char> ( 'U', 'F' );
            ULList = new CircularLinkedList<char> ( 'U', 'L' );
            UBList = new CircularLinkedList<char> ( 'U', 'B' );
            DRList = new CircularLinkedList<char>('D', 'R');
            DFList = new CircularLinkedList<char>('D', 'F');
            DLList = new CircularLinkedList<char>('D', 'L');
            DBList = new CircularLinkedList<char>('D', 'B');
            BLList = new CircularLinkedList<char>('B', 'L');
            FLList = new CircularLinkedList<char>('F', 'L');
            FRList = new CircularLinkedList<char>('F', 'R');
            BRList = new CircularLinkedList<char>('B', 'R');

            this.rand = rand;
        }

        /// <summary>
        /// Scrambles corners according to the given permutation.
        /// If the permutation has parity, the parity edges will be swapped.
        /// </summary>
        /// <param name="leaf">The permutation of the corners</param>
        /// <param name="twistedRight">The number of corners twisted clockwise in place.  If not specified, twisted corners will be twisted in random directions</param>
        public void ApplyCornerLeaf(FinalLeaf leaf, int twistedRight = -1)
        {
            var buffer = ULBList;
            // All pieces except the buffer
            var corners = new List<CircularLinkedList<char>> { URBList, ULFList, URFList, DLBList, DRBList, DLFList, DRFList };
            var twists = leaf.NumTwisted;
            var singles = leaf.Cycles.Skip(1).Count(x => x == 1);

            for(int cycleIdx = 0; cycleIdx < leaf.Cycles.Count; cycleIdx++)
            {
                var cycleLength = leaf.Cycles[cycleIdx];
                var cycleList = new List<CircularLinkedList<char>> { };
                if(cycleIdx == 0)
                    cycleList.Add(buffer);
                while(cycleList.Count < cycleLength)
                {
                    var corner = corners.SelectRandom(x => new Fraction(1, corners.Count), rand);
                    cycleList.Add(corner);
                    corners.Remove(corner);
                }
                var temp = cycleList[0].Head;
                for (int k = 0; k < cycleList.Count - 1; k++)
                {
                    cycleList[k].Head = cycleList[k + 1].Head;
                }
                cycleList[cycleList.Count - 1].Head = temp;

                if(cycleLength == 1 && cycleIdx != 0)   // twisted corner outside the buffer
                {
                    var twistedProb = (double)twists / singles;
                    if(rand.NextDouble() < twistedProb)
                    {
                        if (twistedRight == -1)
                        {
                            if (rand.NextDouble() < 0.5)
                            {
                                cycleList[0].Head = cycleList[0].Head.Right;
                                buffer.Head = buffer.Head.Left;
                            }
                            else
                            {
                                cycleList[0].Head = cycleList[0].Head.Left;
                                buffer.Head = buffer.Head.Right;
                            }
                        }
                        else
                        {
                            var twistedRightProb = (double)twistedRight / twists;
                            if(rand.NextDouble() < twistedRightProb)
                            {
                                cycleList[0].Head = cycleList[0].Head.Right;
                                buffer.Head = buffer.Head.Left;
                                twistedRight--;
                            }
                            else
                            {
                                cycleList[0].Head = cycleList[0].Head.Left;
                                buffer.Head = buffer.Head.Right;
                            }
                        }
                        singles--;
                        twists--;
                    }
                    else
                    {
                        singles--;
                    }
                }
                else
                {
                    foreach (var piece in cycleList)
                    {
                        var num = rand.Next(3);
                        if(num == 1)
                        {
                            piece.Head = piece.Head.Left;
                            buffer.Head = buffer.Head.Right;
                        }
                        if(num == 2)
                        {
                            piece.Head = piece.Head.Right;
                            buffer.Head = buffer.Head.Left;
                        }
                    }
                }
            }

            // swap UB and UL if parity
            if(leaf.Cycles.Count % 2 == 1)
            {
                var temp = UBList.Head;
                UBList.Head = ULList.Head;
                ULList.Head = temp;
            }
        }

        /// <summary>
        /// Scrambles edges according to the given permutation
        /// </summary>
        /// <param name="leaf"></param>
        public void ApplyEdgeLeaf(FinalLeaf leaf)
        {
            var buffer = UBList;
            // All pieces except the buffer
            var edges = new List<CircularLinkedList<char>> { ULList, UFList, URList, DLList, DRList, DFList, DBList, BRList, BLList, FRList, FLList };
            var twists = leaf.NumTwisted;
            var singles = leaf.Cycles.Skip(1).Count(x => x == 1);

            for (int cycleIdx = 0; cycleIdx < leaf.Cycles.Count; cycleIdx++)
            {
                var cycleLength = leaf.Cycles[cycleIdx];
                var cycleList = new List<CircularLinkedList<char>> { };
                if (cycleIdx == 0)
                    cycleList.Add(buffer);
                while (cycleList.Count < cycleLength)
                {
                    var edge = edges.SelectRandom(x => new Fraction(1, edges.Count), rand);
                    cycleList.Add(edge);
                    edges.Remove(edge);
                }
                var temp = cycleList[0].Head;
                for (int k = 0; k < cycleList.Count - 1; k++)
                {
                    cycleList[k].Head = cycleList[k + 1].Head;
                }
                cycleList[cycleList.Count - 1].Head = temp;

                if (cycleLength == 1 && cycleIdx != 0)
                {
                    var twistedProb = (double)twists / singles;
                    if (rand.NextDouble() < twistedProb)
                    {
                        cycleList[0].Head = cycleList[0].Head.Right;
                        buffer.Head = buffer.Head.Left;
                        singles--;
                        twists--;
                    }
                    else
                    {
                        singles--;
                    }
                }
                else
                {
                    foreach (var piece in cycleList)
                    {
                        var num = rand.Next(2);
                        if (num == 1)
                        {
                            piece.Head = piece.Head.Left;
                            buffer.Head = buffer.Head.Right;
                        }
                    }
                }
            }

        }

        /// <summary>
        /// Converts the cube into a cube string used as input to Kociemba
        /// </summary>
        /// <returns></returns>
        public string ToKociembaString()
        {
            var builder = new StringBuilder();
            builder.Append(ULBList.Head.Value);
            builder.Append(UBList.Head.Value);
            builder.Append(URBList.Head.Value);
            builder.Append(ULList.Head.Value);
            builder.Append('U');
            builder.Append(URList.Head.Value);
            builder.Append(ULFList.Head.Value);
            builder.Append(UFList.Head.Value);
            builder.Append(URFList.Head.Value);

            builder.Append(URFList.Head.Right.Value);
            builder.Append(URList.Head.Left.Value);
            builder.Append(URBList.Head.Left.Value);
            builder.Append(FRList.Head.Left.Value);
            builder.Append('R');
            builder.Append(BRList.Head.Left.Value);
            builder.Append(DRFList.Head.Left.Value);
            builder.Append(DRList.Head.Left.Value);
            builder.Append(DRBList.Head.Right.Value);

            builder.Append(ULFList.Head.Right.Value);
            builder.Append(UFList.Head.Left.Value);
            builder.Append(URFList.Head.Left.Value);
            builder.Append(FLList.Head.Value);
            builder.Append('F');
            builder.Append(FRList.Head.Value);
            builder.Append(DLFList.Head.Left.Value);
            builder.Append(DFList.Head.Left.Value);
            builder.Append(DRFList.Head.Right.Value);

            builder.Append(DLFList.Head.Value);
            builder.Append(DFList.Head.Value);
            builder.Append(DRFList.Head.Value);
            builder.Append(DLList.Head.Value);
            builder.Append('D');
            builder.Append(DRList.Head.Value);
            builder.Append(DLBList.Head.Value);
            builder.Append(DBList.Head.Value);
            builder.Append(DRBList.Head.Value);

            builder.Append(ULBList.Head.Right.Value);
            builder.Append(ULList.Head.Left.Value);
            builder.Append(ULFList.Head.Left.Value);
            builder.Append(BLList.Head.Left.Value);
            builder.Append('L');
            builder.Append(FLList.Head.Left.Value);
            builder.Append(DLBList.Head.Left.Value);
            builder.Append(DLList.Head.Left.Value);
            builder.Append(DLFList.Head.Right.Value);

            builder.Append(URBList.Head.Right.Value);
            builder.Append(UBList.Head.Left.Value);
            builder.Append(ULBList.Head.Left.Value);
            builder.Append(BRList.Head.Value);
            builder.Append('B');
            builder.Append(BLList.Head.Value);
            builder.Append(DRBList.Head.Left.Value);
            builder.Append(DBList.Head.Left.Value);
            builder.Append(DLBList.Head.Right.Value);

            return builder.ToString();
        }

    }
}
