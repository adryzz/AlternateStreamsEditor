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
using System.IO;

namespace AlternateStreamsEditor
{
    public partial class StreamCreatorDialog : Form
    {
        public AlternateDataStreamInfo SelectedStream = null;
        FileInfo Info;

        public StreamCreatorDialog(FileInfo info)
        {
            InitializeComponent();
            Info = info;
        }

        private void StreamCreatorDialog_Load(object sender, EventArgs e)
        {

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            button2.Enabled = textBox1.Text != "";
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (Info.AlternateDataStreamExists(textBox1.Text))
            {
                MessageBox.Show("That Stream already exists!", "AlternateStreamsEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SelectedStream = FileSystem.GetAlternateDataStream(Info, textBox1.Text, FileMode.Create);
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
