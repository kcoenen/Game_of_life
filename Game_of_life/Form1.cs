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
            pictureBox1.BackColor = Color.Teal;
            initialize_world();
        }

        public void initialize_world()
        {
            //C:\\Users\\kris\\Google Drive\\pcvo\\c#\\visualstudio2015\\projects\\Game_of_life\\Game_of_life\\
            var fileloc = @"C:\Users\%username%\Google Drive\pcvo\c#\visualstudio2015\projects\Game_of_life\Game_of_life\amoebe.csv";
            string file = Environment.ExpandEnvironmentVariables(fileloc);
            List<string> lines = new List<string>();
            List<string> aname = new List<string>();
            List<string> agender = new List<string>();
            List<string> aposx = new List<string>();
            List<string> aposy = new List<string>();
            List<string> aage = new List<string>();
            StreamReader reader = new StreamReader(file);

            //goes through a textfile and ad these to the list amoebe
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

        }
        public void draw_world()
        {
            Graphics paper = pictureBox1.CreateGraphics();
            SolidBrush draw_male = new  SolidBrush(System.Drawing.Color.Blue);
            SolidBrush draw_female = new SolidBrush(System.Drawing.Color.Red);
            //goes through the list amoebe and draws a cirkel on the location from x and y loccation and age
            // If the age is above 10 the amoebe is fully grown and stays on size 10
            // females are red and males are blue
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
            // generates a random number and if this number is in a certain range it moves the x or y position in a certain direction.
            foreach (amoebe a in aAmoe)
            {
                Int32 rdm_move = rdm.Next(0, 25);
                if (rdm_move < 5)
                {
                    a.xPos += 5;
                }
                else if (rdm_move >= 5 && rdm_move < 15)
                {
                    a.xPos -= 20;
                }
                else if (rdm_move >= 15 && rdm_move < 20)
                {
                    a.yPos += 5;
                }
                else if (rdm_move >= 20)
                {
                    a.yPos -= 5;
                }

                //The folowing sees if the x and y position is over the wide of the picturebox and if so sets the amoebe on the other side of the textbox

                if (a.xPos >= pictureBox1.Width)
                {
                    a.xPos = 1;
                }
                else if (a.xPos <= 1)
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
                //goes throug each amoebe and if 1 is close to another and they or male and female ad a new amoebe with age 1 is created
                foreach (amoebe am in aAmoe)
                {
                    if (am.xPos - a.xPos <= 10 || am.yPos - a.xPos <= 10)
                    {
                        if (am.sex == "F" && a.sex == "M")
                        {
                            create_amoebe(am.xPos,am.yPos, 1);
                        }
                        else if (am.sex == "M" && a.sex == "F")
                        {
                            create_amoebe(a.xPos, a.yPos, 1);
                        }
                    }

                }
            }


            draw_world();
        }

        public void create_amoebe(Int32 x, Int32 y, Int32 age)
        {
            Int32 rdm_sex = rdm.Next(1, 101);
            amoebe baby = new amoebe();
            if (rdm_sex < 50)
            {
                baby.sex = "M";
                baby.xPos = x;
                baby.yPos = y;
                baby.age = age;
                baby.name = aAmoe.Count() + 1;
                aAmoe.Add(baby);
            }
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
