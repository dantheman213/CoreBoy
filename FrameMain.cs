using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoreBoy
{
    public partial class FrameMain : Form
    {
        public FrameMain()
        {
            InitializeComponent();
        }

        private void loadROMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var d = new OpenFileDialog();
            d.Filter = "Gameboy ROMs (*.gb)|*.gb|All files (*.*)|*.*";
            if (d.ShowDialog() == DialogResult.OK)
            {
                var sys = new Gameboy(File.ReadAllBytes(d.FileName));
                sys.Loop();
            }
        }
    }
}
