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

namespace Adjustment_Tool
{
    public partial class Form1 : Form
    {
        private int type;
        StreamWriter writer;
        private String[] textHolder;
        public Form1()
        {
            InitializeComponent();
            textHolder = new String[3];
            try
            {
                StreamReader read = new StreamReader("Variables.txt");
                textHolder[0] = read.ReadLine();
                textHolder[1] = read.ReadLine();
                String line = read.ReadLine();
                while (line != null && line != "")
                {
                    textHolder[2] += line;
                    line = read.ReadLine();
                }
                read.Close();
            }
            catch
            {

            }
            
            writer = new StreamWriter("Variables.txt");
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            type = 0;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            type = 1;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            type = 2;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textHolder[type] = textBox1.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < textHolder.Length; i++)
            {
                writer.WriteLine(textHolder[i]);
            }
            writer.Close();
            Form1.ActiveForm.Close();
        }
    }
}
