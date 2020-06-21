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

namespace NTFSStreamsEditor
{
    public partial class Form1 : Form
    {
        AlternateDataStreamInfo Info;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var res = openFileDialog1.ShowDialog();
            if (res == DialogResult.OK)
            {
                StreamCreatorDialog dialog = new StreamCreatorDialog(new FileInfo(openFileDialog1.FileName));
                var res1 = dialog.ShowDialog();
                if (res1 == DialogResult.OK)
                {
                    Info = dialog.SelectedStream;
                    textBox2.Text = Info.FullPath;
                }
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            var res = openFileDialog1.ShowDialog();
            if (res == DialogResult.OK)
            {
                StreamSelectorDialog dialog = new StreamSelectorDialog(new FileInfo(openFileDialog1.FileName));
                var res1 = dialog.ShowDialog();
                if (res1 == DialogResult.OK)
                {
                    AlternateDataStreamInfo info = dialog.SelectedStream;
                    var res2 = MessageBox.Show("Do you really wanna delete the stream " + info.Name + "?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (res2 == DialogResult.Yes)
                    {
                        if (info.FullPath.Equals(Info.FullPath))
                        {
                            MessageBox.Show("Close the Stream then try again!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        info.Delete();
                    }
                }
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            var res = openFileDialog1.ShowDialog();
            if (res == DialogResult.OK)
            {
                StreamSelectorDialog dialog = new StreamSelectorDialog(new FileInfo(openFileDialog1.FileName));
                var res1 = dialog.ShowDialog();
                if (res1 == DialogResult.OK)
                {
                    Info = dialog.SelectedStream;
                    StreamReader reader = Info.OpenText();
                    textBox1.Text = reader.ReadToEnd();
                    reader.Dispose();
                    textBox2.Text = Info.FullPath;
                }
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (Info == null)
            {
                MessageBox.Show("Open an existing stream or create one!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                FileStream stream = Info.OpenWrite();
                StreamWriter writer = new StreamWriter(stream);
                writer.Write(textBox1.Text);
                writer.Flush();
                writer.Dispose();
                stream.Dispose();
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Info = null;
            textBox2.Text = "";
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            if (Info == null)
            {
                MessageBox.Show("Open an existing stream or create one!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                StreamInfoDialog dialog = new StreamInfoDialog(Info);
                dialog.Show();
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }
    }
}
