﻿using System;
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
                    listBoxFiles.Items.Add(file);
                    Console.WriteLine(file);
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
