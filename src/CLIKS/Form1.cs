using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CLIKS
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtralnfo);
        
        private const int MOUSEEVENTF_LEFDOWN = 0x02; 
        private const int MOUSEEVENTF_LEFTUP = 0x04; 
        private const int MOUSEEVENTF_RIGNDWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;

        public void DoMouseClick(int x, int y)
        {
            Cursor.Position = new Point(x, y);
            mouse_event(MOUSEEVENTF_LEFDOWN | MOUSEEVENTF_LEFTUP, (uint)x, (uint)y, 0, 0);
        }

        public Form1()
        {
            InitializeComponent();
        }
        BackgroundWorker bw;
        BackgroundWorker bw2;
        int click = 0;
        void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

       void button2_Click(object sender, EventArgs e)
        {
            bw = new BackgroundWorker();
            bw.DoWork += (obj, ea) => TasksAsync(1);
            bw.RunWorkerAsync();
        }

       async void TasksAsync(int times)
        {
            int x, y;
            x = Cursor.Position.X;
            y = Cursor.Position.Y;
            textBox1.Text += x.ToString() + Environment.NewLine;
            textBox2.Text += y.ToString() + Environment.NewLine;
        }

      void button3_Click(object sender, EventArgs e)
        {
            bw2 = new BackgroundWorker();
            bw2.DoWork += (obj, ea) => TasksAsync2(1);
            bw2.RunWorkerAsync();

            for (int j = 0; j < textBox1.Lines.Length; j++)
            {
                for (int i = 0; i < textBox1.Lines.Length - 1; i++)
                {
                    int x = Convert.ToInt16(textBox1.Lines[i]);
                    int y = Convert.ToInt16(textBox2.Lines[i]);

                    Thread.Sleep(int.Parse(textBox3.Text));
                    DoMouseClick(x, y);
                  
                }
            }
        }
         async void TasksAsync2(int times)
        {
            click++;
            label9.Text = "Цыклы  " + click;

        }


        void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.F2)
            {
                button2_Click(button2, null);
            }

            if (e.KeyValue == (char)Keys.Delete)
            {
                button1_Click(button1, null);
            }

            if (e.KeyValue == (char)Keys.F3)
            {
                button3_Click(button3, null);
            }
            if (e.KeyValue == (char)Keys.F4)
            {
                button4_Click(button4, null);
            }

            if (e.KeyValue == (char)Keys.F1)
            {
                button5_Click(button5, null);
            }

            if (e.KeyValue == (char)Keys.Escape)
            {
                System.Environment.Exit(0);
            }
        }

        void button4_Click(object sender, EventArgs e)
        {
            int q = (int.Parse(textBox4.Text));
            do
            {
                button3_Click(button3, null);
                q--;
            }
            while (q > 0);
        }
      

         void button5_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label4.Text = DateTime.Now.ToString("HH:mm:ss");
        }
    }
    }
