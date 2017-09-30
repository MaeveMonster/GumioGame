using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        List<int> textures = new List<int> { 0, 1, 2, 3, 4, 5};
        Dictionary<int, string> picDict = new Dictionary<int, string>()
        {
            {0, "..\\..\\obj\\Character Previews\\PinkBodyNoHat.png" },
            {1, "..\\..\\obj\\Character Previews\\PinkBodyHatOne.png" },
            {2, "..\\..\\obj\\Character Previews\\PinkBodyHatTwo.png" },
            {3, "..\\..\\obj\\Character Previews\\BlueBodyNoHat.png" },
            {4, "..\\..\\obj\\Character Previews\\BlueBodyHatOne.png" },
            {5, "..\\..\\obj\\Character Previews\\BlueBodyHatTwo.png" }


        };
         public int current = 0;

        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = Image.FromFile(picDict[0]);

        }

        private void Back_Click(object sender, EventArgs e)
        {
            current = CycleBack(current);
            pictureBox1.Image = Image.FromFile(@picDict[current]);
            pictureBox1.Refresh();
        }

        private int CycleBack(int x)
        {
            int toReturn = 0;
            if(x == 0)
            {
                toReturn = 5;
            }
            else
            {
                toReturn = current - 1;
            }
            return toReturn;

        }

        private int CycleForward(int x)
        {
            int toReturn = 0;
            if (x == 5)
            {
                toReturn = 0;
            }
            else
            {
                toReturn = current + 1;
            }
            return toReturn;

        }

        private void Next_Click(object sender, EventArgs e)
        {
            current = CycleForward(current);
            pictureBox1.Image = Image.FromFile(@picDict[current]);
            pictureBox1.Refresh();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Ok_Click(object sender, EventArgs e)
        {
            StreamWriter writer = new StreamWriter("..\\..\\obj\\CharacterTransferFiles\\character.txt");
            writer.WriteLine(current);
            writer.Close();
            this.Close();
        }
    }
}
