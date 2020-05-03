using BldScramblerLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BldScramblerUi
{
    public partial class Form1 : Form
    {
        private ScramblerControl scramblerControl;
        private Dictionary<string, ScramblerControl> scramblerControlMap;
        private Random random;

        public Form1()
        {
            InitializeComponent();

            // make sure kociemba is installed
            var startInfo = new ProcessStartInfo
            {
                UseShellExecute = false,
                FileName = "kociemba.exe",
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };
            try
            {
                var process = new Process { StartInfo = startInfo };
                process.Start();
                process.WaitForExit();
            }
            catch
            {
                MessageBox.Show("Kociemba not found.\nPlease install Kociemba in order to use this program", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }

            var nodeTuple = Initialize.GetNodes();
            List<Node> edgeNodes = nodeTuple.Item1;
            List<Node> cornerNodes = nodeTuple.Item2;
            List<CombinationNode> combinationNodes = Initialize.GetCombinationNodes(edgeNodes, cornerNodes);
            random = new Random();
            scramblerControlMap = new Dictionary<string, ScramblerControl>
            {
                ["Number of algs"] = new AlgScrambler(edgeNodes, cornerNodes, combinationNodes, random),
                ["Flipped Edges"] = new FlippedEdgeScrambler(edgeNodes, cornerNodes, random),
                ["Twisted Corners"] = new TwistedCornerScrambler(edgeNodes, cornerNodes, random),
                ["Custom"] = new FileScrambler(combinationNodes, edgeNodes, cornerNodes, random)
            };
            foreach (var pair in scramblerControlMap)
            {
                var control = pair.Value;
                control.Visible = true;
                control.Location = new Point(135, 39);
                this.TypeSelector.Items.Add(pair.Key);
            }
            this.TypeSelector.SelectedItem = "Number of algs";
        }

        private void TypeSelector_SelectedValueChanged(object sender, EventArgs e)
        {
            var selectedControl = scramblerControlMap[(string)TypeSelector.SelectedItem];
            this.Controls.Remove(scramblerControl);
            scramblerControl = selectedControl;
            this.Controls.Add(scramblerControl);
        }

        private void ScrambleButton_Click(object sender, EventArgs e)
        {
            var lastScramble = ScrambleLabel.Text;
            ScrambleLabel.Text = "Scrambling";
            this.Refresh();
            var scramble = scramblerControl.Scramble();
            if (scramble != null)
                ScrambleLabel.Text = scramble;
            else
                ScrambleLabel.Text = lastScramble;
        }
    }
}
