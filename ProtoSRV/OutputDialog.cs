using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProtoSRV
{
    public partial class OutputDialog : Form
    {
        public OutputDialog(string cmdOutput)
        {
            InitializeComponent();
            textBox3.Text = cmdOutput;
        }

        public string cmdOutput;

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string filePath = @"c:\_.PROTOMRCX\res\_.WINDOWSOSX\cmd\Output-" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".txt";
            string fileText = textBox3.Text;
            System.IO.File.WriteAllText(filePath, fileText);
            Process.Start(filePath);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
        }

        private void label4_Click_1(object sender, EventArgs e)
        {

        }
    }
}
