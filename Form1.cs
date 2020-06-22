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
    public partial class Form1 : Form
    {
        AlternateDataStreamInfo Info;
        bool Saved = true;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            Saved = false;
            if (Info != null)
            {
                Text = string.Format("AlternateStreamsEditor - {0}*", Info.FullPath);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var res = openFileDialog1.ShowDialog(this);
            if (res == DialogResult.OK)
            {
                StreamCreatorDialog dialog = new StreamCreatorDialog(new FileInfo(openFileDialog1.FileName));
                var res1 = dialog.ShowDialog(this);
                if (res1 == DialogResult.OK)
                {
                    Info = dialog.SelectedStream;
                    textBox2.Text = Info.FullPath;
                    Saved = true;
                    Text = string.Format("AlternateStreamsEditor - {0}", Info.FullPath);
                }
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            var res = openFileDialog1.ShowDialog(this);
            if (res == DialogResult.OK)
            {
                StreamSelectorDialog dialog = new StreamSelectorDialog(new FileInfo(openFileDialog1.FileName));
                var res1 = dialog.ShowDialog(this);
                if (res1 == DialogResult.OK)
                {
                    AlternateDataStreamInfo info = dialog.SelectedStream;
                    var res2 = MessageBox.Show(this, "Do you really wanna delete the Stream " + info.Name + "?", "AlternateStreamsEditor", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (res2 == DialogResult.Yes)
                    {
                        if (info.FullPath.Equals(Info.FullPath))
                        {
                            MessageBox.Show(this, "Close the Stream then try again!", "AlternateStreamsEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        info.Delete();
                    }
                }
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (!Saved)
            {
                var result = MessageBox.Show(this, "Save?", "AlternateStreamsEditor", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Cancel)
                {
                    return;
                }
                else if (result == DialogResult.Yes)
                {
                    if (Info == null)
                    {
                        Button1_Click(null, null);
                        Button4_Click(null, null);
                    }
                    else
                    {
                        Button4_Click(null, null);
                    }
                    Saved = true;
                }
            }
            var res = openFileDialog1.ShowDialog(this);
            if (res == DialogResult.OK)
            {
                StreamSelectorDialog dialog = new StreamSelectorDialog(new FileInfo(openFileDialog1.FileName));
                var res1 = dialog.ShowDialog(this);
                if (res1 == DialogResult.OK)
                {
                    Info = dialog.SelectedStream;
                    StreamReader reader = Info.OpenText();
                    textBox1.Text = reader.ReadToEnd();
                    reader.Dispose();
                    textBox2.Text = Info.FullPath;
                    Saved = true;
                    Text = string.Format("AlternateStreamsEditor - {0}", Info.FullPath);
                }
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (Info == null)
            {
                MessageBox.Show(this, "Open an existing Stream or create one!", "AlternateStreamsEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                FileStream stream = Info.OpenWrite();
                StreamWriter writer = new StreamWriter(stream);
                writer.Write(textBox1.Text);
                writer.Flush();
                writer.Dispose();
                stream.Dispose();
                Saved = true;
                Text = string.Format("AlternateStreamsEditor - {0}", Info.FullPath);
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (!Saved)
            {
                var result = MessageBox.Show(this, "Save?", "AlternateStreamsEditor", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Cancel)
                {
                    return;
                }
                else if (result == DialogResult.Yes)
                {
                    Button4_Click(null, null);
                }
            }
            Info = null;
            textBox1.Text = "";
            textBox2.Text = "";
            Text = "AlternateStreamsEditor";
            Saved = true;
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            if (Info == null)
            {
                MessageBox.Show(this, "Open an existing Stream or create one!", "AlternateStreamsEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                StreamInfoDialog dialog = new StreamInfoDialog(Info);
                dialog.Show(this);
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Saved)
            {
                var res = MessageBox.Show(this, "You have unsaved changes. Save?", "AlternateStreamsEditor", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (res == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else if (res == DialogResult.Yes)
                {
                    if (Info == null)
                    {
                        Button1_Click(null, null);
                        Button4_Click(null, null);
                    }
                    else
                    {
                        Button4_Click(null, null);
                    }
                }
            }
        }
    }
}
