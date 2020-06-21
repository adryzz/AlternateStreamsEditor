using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trinet.Core.IO.Ntfs;

namespace NTFSStreamsEditor
{
    public partial class StreamInfoDialog : Form
    {
        AlternateDataStreamInfo Info;
        public StreamInfoDialog(AlternateDataStreamInfo info)
        {
            InitializeComponent();
            Info = info;
        }

        private void StreamInfoDialog_Load(object sender, EventArgs e)
        {
            label1.Text = string.Format("Stream Name: {0}\nFile path: {1}\nFull Stream path: {2}\nSize: {3} bytes\nStream Type: {4}\nAttributes: {5}", Info.Name, Info.FilePath, Info.FullPath, Info.Size, Info.StreamType, Info.Attributes.ToString());
        }
    }
}
