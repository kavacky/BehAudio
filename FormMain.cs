using IdSharp.Common.Utils;
using IdSharp.Tagging.ID3v1;
using IdSharp.Tagging.ID3v2;
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

namespace BehAudio
{
    public partial class formMain : Form
    {
        public formMain()
        {
            InitializeComponent();
        }

        private void listBoxFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBoxDetails.Text = listBoxFiles.SelectedIndex.ToString() + " - " + listBoxFiles.SelectedItem.ToString();
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            listBoxFiles.Items.Clear();

            try
            {
                foreach (string file in System.IO.Directory.EnumerateFiles("F:/", "*.*", SearchOption.AllDirectories))
                {
                    string f = file.ToLower();
                    if (f.EndsWith("m3u") | f.EndsWith("mp3") | f.EndsWith("wav") | f.EndsWith("wma"))
                    {
                        listBoxFiles.Items.Add(file);

                        if (ID3v2Tag.DoesTagExist(file))
                        {
                            IID3v2Tag id3v2 = new ID3v2Tag(file);

                            listBoxFiles.Items.Add("  - TagVersion: " + EnumUtils.GetDescription(id3v2.Header.TagVersion));
                            listBoxFiles.Items.Add(string.Format("  - Artist: {0}", id3v2.Artist));
                            listBoxFiles.Items.Add(string.Format("  - Title: {0}", id3v2.Title));
                            listBoxFiles.Items.Add(string.Format("  - Album: {0}", id3v2.Album));
                            listBoxFiles.Items.Add(string.Format("  - Pictures: {0}", id3v2.PictureList.Count));
                        }
                        else if (ID3v1Tag.DoesTagExist(file))
                        {
                            IID3v1Tag id3v1 = new ID3v1Tag(file);

                            listBoxFiles.Items.Add("  - TagVersion: " + EnumUtils.GetDescription(id3v1.TagVersion));
                            listBoxFiles.Items.Add(string.Format("  - Artist: {0}", id3v1.Artist));
                            listBoxFiles.Items.Add(string.Format("  - Title: {0}", id3v1.Title));
                            listBoxFiles.Items.Add(string.Format("  - Album: {0}", id3v1.Album));
                        }
                    }
                    else
                    {
                        listBoxFiles.Items.Add("UNSUPPORTED TYPE - " + file);
                    }
                }
            }
            catch (Exception ex)
            {
                richTextBoxDetails.Text = richTextBoxDetails.Text + "\r\n" + "EXCEPTION: " + ex.ToString();
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
