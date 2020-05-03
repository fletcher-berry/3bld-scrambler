using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BldScramblerLib;
using System.IO;

namespace BldScramblerUi
{
    /// <summary>
    /// Generates scrambles based on a file
    /// </summary>
    public partial class FileScrambler : ScramblerControl
    {
        private List<AbstractScramblerService> scramblers;

        private List<Node> edgeNodes;
        private List<Node> cornerNodes;
        private List<CombinationNode> combinationNodes;
        private Random rand;

        public FileScrambler(List<CombinationNode> combinationNodes, List<Node> edgeNodes, List<Node> cornerNodes, Random rand)
        {
            InitializeComponent();

            this.edgeNodes = edgeNodes;
            this.cornerNodes = cornerNodes;
            this.combinationNodes = combinationNodes;
            this.rand = rand;
        }

        public override string Scramble()
        {
            if(scramblers == null)
            {
                MessageBox.Show("No file selected.", "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            var total = scramblers.Sum(x => x.Weight);
            var scrambler = scramblers.SelectRandom(x => new Fraction(x.Weight, total), rand);
            return scrambler.Scramble(rand);
        }

        private void SelectFileButton_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "txt files (*.txt)|*.txt"
            };
            var result = dialog.ShowDialog();
            if(result == DialogResult.OK)
            {
                var fileName = dialog.FileName;
                try
                {
                    using (var reader = new StreamReader(fileName))
                    {
                        try
                        {
                            List<AbstractScramblerService> currScramblers = ScrambleFileParser.GetScramblers(reader, combinationNodes, edgeNodes, cornerNodes);
                            scramblers = currScramblers;
                            FileNameLabel.Text = Path.GetFileName(fileName);
                        }
                        catch(ScrambleFileException ex)
                        {
                            MessageBox.Show(ex.Message, "parsing error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("unable to read the file.");
                }
            }
        }
    }
}
