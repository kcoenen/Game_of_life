using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Game_of_life
{
    public partial class Form1 : Form
    {
        List<amoebe> aAmoe = new List<amoebe>();
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            initialize_world();
            draw_world();
        }

        public void initialize_world()
        {
            string file = "C:\\Users\\kris\\Google Drive\\pcvo\\c#\\visualstudio2015\\projects\\Game_of_life\\Game_of_life\\amoebe.csv";
            List<string> lines = new List<string>();
            List<string> aname = new List<string>();
            List<string> agender = new List<string>();
            List<string> aposx = new List<string>();
            List<string> aposy = new List<string>();
            List<string> aage = new List<string>();
            StreamReader reader = new StreamReader(file);


            string line;
            while ((line = reader.ReadLine()) != null)
            {
                // 4
                // Insert logic here.
                // ...
                // "line" is a line in the file. Add it to our List.
                //lines.Add(line);
                amoebe Data = new amoebe();
                string[] values = line.Split(';');
                Data.name = Convert.ToInt32(values[0]);
                Data.sex = values[1];
                Data.xPos = Convert.ToInt32(values[2]);
                Data.yPos = Convert.ToInt32(values[3]);
                Data.age = Convert.ToInt32(values[4]);

                aAmoe.Add(Data);
            }

            foreach (amoebe aAmoebe in aAmoe)
            {
                txtAmoebe.Text += aAmoebe.name + "\t" + aAmoebe.sex + "\t" + aAmoebe.age + "\t" + aAmoebe.xPos + "\t" + aAmoebe.yPos + Environment.NewLine;

            }
        }
        public void draw_world()
        {
            Graphics paper = pictureBox1.CreateGraphics();
            SolidBrush draw_male = new  SolidBrush(Color.Blue);
            SolidBrush draw_female = new SolidBrush(Color.Red);

            foreach( amoebe a in aAmoe)
            {
                if (a.sex == "F")
                {
                    paper.FillEllipse(draw_female, a.xPos, a.yPos, a.age, a.age);
                }
                else if (a.sex == "M")
                {
                    paper.FillEllipse(draw_male, a.xPos, a.yPos, a.age, a.age);
                }
            }

        }

        private void btnStart_Click(object sender, EventArgs e)
        {

        }
    }
}
