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
        Random rdm = new System.Random();
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            initialize_world();
        }

        public void initialize_world()
        {
            //C:\\Users\\kris\\Google Drive\\pcvo\\c#\\visualstudio2015\\projects\\Game_of_life\\Game_of_life\\
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

            //foreach (amoebe aAmoebe in aAmoe)
            //{
            //    txtAmoebe.Text += aAmoebe.name + "\t" + aAmoebe.sex + "\t" + aAmoebe.age + "\t" + aAmoebe.xPos + "\t" + aAmoebe.yPos + Environment.NewLine;

            //}
        }
        public void draw_world()
        {
            Graphics paper = pictureBox1.CreateGraphics();
            SolidBrush draw_male = new  SolidBrush(System.Drawing.Color.Blue);
            SolidBrush draw_female = new SolidBrush(System.Drawing.Color.Red);

            paper.Clear(Color.Teal);
            foreach( amoebe a in aAmoe)
            {
                if (a.sex == "F")
                {
                    if (a.age <= 10)
                    {
                        paper.FillEllipse(draw_female, a.xPos, a.yPos, a.age, a.age);
                    }
                    else if (a.age > 10)
                    {
                        paper.FillEllipse(draw_female, a.xPos, a.yPos, 10, 10);
                    }
                    
                }
                else if (a.sex == "M")
                {
                    if (a.age <= 10)
                    {
                        paper.FillEllipse(draw_male, a.xPos, a.yPos, a.age, a.age);
                    }
                    else if (a.age > 10)
                    {
                        paper.FillEllipse(draw_male, a.xPos, a.yPos, 10, 10);
                    }
                }
            }

        }

        public void move_amoebes()
        {
            foreach (amoebe a in aAmoe)
            {
                Int32 rdm_move = rdm.Next(0, 25);
                if (rdm_move < 5)
                {
                    a.xPos += 5;
                }
                else if (rdm_move >= 5 && rdm_move < 15)
                {
                    a.xPos -= 10;
                }
                else if (rdm_move >= 15 && rdm_move < 20)
                {
                    a.yPos += 5;
                }
                else if (rdm_move >= 20)
                {
                    a.yPos -= 5;
                }

                //switch (a.xPos)
                //{
                //    case >880:
                //        a.xPos = 1;
                //        break;
                //    case 0:
                //        a.xPos = 880;
                //        break;
                //}
                //switch (a.yPos)
                //{
                //    case 580:
                //        a.yPos = 1;
                //        break;
                //    case 0:
                //        a.yPos = 580;
                //        break;
                //}
                if (a.xPos >= pictureBox1.Width)
                {
                    a.xPos = 1;
                }
                else if (a.xPos <=1)
                {
                    a.xPos = pictureBox1.Width;
                }

                if (a.yPos >= pictureBox1.Height)
                {
                    a.yPos = 1;
                }
                else if (a.yPos <= 1)
                {
                    a.yPos = pictureBox1.Height;
                }

            }
            draw_world();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Interval = 100;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            move_amoebes();
        }
    }
}
