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

namespace ColorTool
{
    public partial class Form1 : Form
    {

        String color;

        public Form1()
        {
            InitializeComponent();
            color = "";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            color = "Default";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            color = "Blue";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            color = "MediumSeaGreen";
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            color = "Orange";
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            color = "Red";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StreamWriter output = new StreamWriter("..\\..\\..\\ColorChoice.txt");
            output.WriteLine(color);
            output.Close();
            Close();
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            color = "Black";
        }
    }
}
