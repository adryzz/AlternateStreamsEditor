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
using Trinet.Core.IO.Ntfs;

namespace AlternateStreamsEditor
{
    public partial class StreamSelectorDialog : Form
    {
        FileInfo File;
        List<AlternateDataStreamInfo> Streams = new List<AlternateDataStreamInfo>();
        public AlternateDataStreamInfo SelectedStream = null;

        public StreamSelectorDialog(FileInfo file)
        {
            InitializeComponent();
            File = file;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            foreach(AlternateDataStreamInfo a in File.ListAlternateDataStreams())
            {
                Streams.Add(a);
                comboBox1.Items.Add(a.Name);
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button3.Enabled = true;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Streams.Clear();
            comboBox1.Items.Clear();
            foreach (AlternateDataStreamInfo a in File.ListAlternateDataStreams())
            {
                Streams.Add(a);
                comboBox1.Items.Add(a.Name);
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            SelectedStream = Streams[comboBox1.SelectedIndex];
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
