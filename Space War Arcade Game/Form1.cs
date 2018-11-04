using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Space_War_Arcade_Game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
            {
                moveup();
            }
            else if(e.KeyCode == Keys.Down || e.KeyCode == Keys.S)
            {
                movedown();
            }
            else if(e.KeyCode == Keys.Space)
            {
                fire();
            }
        }

        private void moveup()
        {
            Point pos = pictureBox1.Location;
            if(pos.Y > 0)
            {
                pos.Y -= 20;
                pictureBox1.ImageLocation = @"ship/left.png";
            }
            pictureBox1.Location = pos;
            
        }

        private void movedown()
        {
            Point pos = pictureBox1.Location;
            if(pos.Y < panel1.Height-pictureBox1.Height)
            {
                pos.Y += 20;
                pictureBox1.ImageLocation = @"ship/right.png";
            }
            pictureBox1.Location = pos;
            
        }

        private void fire()
        {
            PictureBox bullet = new PictureBox();
            bullet.Height = 15;
            bullet.Width = 25;
            bullet.ImageLocation = @"ship/fire.png";
            bullet.Location = new Point(pictureBox1.Location.X + pictureBox1.Width, pictureBox1.Location.Y + pictureBox1.Height / 2);
            bullet.Name = "bullet";
            panel1.Controls.Add(bullet);
        }

        int kindOfEnemies = 4;

        private void CreateEnemies()
        {
            Random rnd = new Random();
            int x = rnd.Next(1, kindOfEnemies + 1);
            PictureBox enamy = new PictureBox();
            int loc = rnd.Next(0, panel1.Height - enamy.Height);
            enamy.SizeMode = PictureBoxSizeMode.StretchImage;
            enamy.ImageLocation = "Aliens/"+x+".png";
            enamy.Location = new Point(panel1.Width, loc);
            enamy.Name = "enamy";
            panel1.Controls.Add(enamy);

        }

        private void createstar()
        {
            Random rnd = new Random();
            PictureBox star = new PictureBox();
            star.Width = 10;
            star.Height = 10;
            int loc = rnd.Next(0, panel1.Height);
            star.ImageLocation = @"star/star.png";
            star.SizeMode = PictureBoxSizeMode.StretchImage;
            star.Location = new Point(panel1.Width - star.Width, loc);
            star.Name = "star";
            star.BackColor = Color.Transparent;
            panel1.Controls.Add(star);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Down || e.KeyCode == Keys.W)
            {
                pictureBox1.ImageLocation = @"ship/ship.png";
            }

           else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.S)
            {
                pictureBox1.ImageLocation = @"ship/ship.png";
            }
        }

        int counter = 0, counter1 = 0, score = 0,life = 3;

        private void timer1_Tick(object sender, EventArgs e)
        {
            counter++;
            counter1++;
            label3.Text = life.ToString();
            for(int i=0; i<panel1.Controls.Count; i++)
            {
                PictureBox pb = ((PictureBox)panel1.Controls[i]); 

                if(panel1.Controls[i].Name == "bullet")
                {
                    if(pb.Location.X+pb.Width > panel1.Width)
                    {
                        panel1.Controls.RemoveAt(i);
                    }
                    else
                    {
                        pb.Location = new Point(pb.Location.X + 5, pb.Location.Y);
                    }

                    for(int x = 0; x<panel1.Controls.Count; x++)
                    {
                        if(panel1.Controls[x].Name == "enamy")
                        {
                            if(pb.Location.X + pb.Width > panel1.Controls[x].Location.X && pb.Location.X + pb.Width < panel1.Controls[x].Location.X + panel1.Controls[x].Width)
                            {
                                if(pb.Location.Y+pb.Height>panel1.Controls[x].Location.Y && pb.Location.Y + pb.Height < panel1.Controls[x].Location.Y + panel1.Controls[x].Width)
                                {
                                    panel1.Controls.RemoveAt(x);
                                    panel1.Controls.Remove(pb);
                                    score++;
                                    label2.Text = score.ToString();
                                }
                            }
                        }
                    }
                }

                else if (panel1.Controls[i].Name == "enamy")
                {
                    if (pb.Location.X < 0)
                    {
                        panel1.Controls.RemoveAt(i);
                    }
                    else
                    {
                        pb.Location = new Point(pb.Location.X - 5, pb.Location.Y);

                        if(pb.Location.X > pictureBox1.Location.X && pb.Location.X < pictureBox1.Location.X + pictureBox1.Width)
                        {
                            if(pb.Location.Y > pictureBox1.Location.Y && pb.Location.Y < pictureBox1.Location.Y + pictureBox1.Height)
                            {
                                panel1.Controls.RemoveAt(i);
                                life--;
                                axWindowsMediaPlayer1.URL = @"sounds/explode.mp3";
                                axWindowsMediaPlayer1.Ctlcontrols.play();

                                if(life == 0)
                                {
                                    timer1.Stop();
                                    panel2.Visible = true;
                                }
                            }
                        }
                    }
                }

                else if (panel1.Controls[i].Name == "star")
                {
                    if (pb.Location.X < 0)
                    {
                        panel1.Controls.RemoveAt(i);
                    }
                    else
                    {
                        pb.Location = new Point(pb.Location.X - 5, pb.Location.Y);
                    }
                }
            }
            if (counter == 50)
            {
                counter = 0;     
                CreateEnemies();
            }
            if(counter1 == 20)
            {
                counter1 = 0;
                createstar();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            axWindowsMediaPlayer2.URL = @"sounds/background.mp3";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            playagain();
        }

        private void playagain()
        {
            if(Application.OpenForms[0] == this)
            {
                Application.Restart();
            }
        }

        private void count()
        {
            if (score >= 0 && score <= 10)
            {
                if (counter == 50)
                {
                    counter = 0;
                    CreateEnemies();
                }
            }

            else if (score > 10 && score <= 20)
            {
                if (counter == 30)
                {
                    counter = 0;
                    CreateEnemies();
                }
            }

            if (score > 20)
            {
                if (counter == 10)
                {
                    counter = 0;
                    CreateEnemies();
                }
            }
        }
    }
}
