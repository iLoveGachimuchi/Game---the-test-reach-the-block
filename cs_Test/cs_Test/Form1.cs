using System;
using System.Timers;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System.Runtime.InteropServices;


namespace cs_Test
{
    public partial class Form1 : Form
    {

        [DllImport("user32.dll")]
        static extern bool GetCursorPos(ref Point lpPoint);
        static protected int currX;
        static protected int currY;
        static protected int diffX;
        static protected int diffY;


        private rnd rand = new rnd();


        public int newXPoint;
        public int newYPoint;

        public void newLocation()
        {

            this.newXPoint = rand.GetRandomNumber(1, 1900 - 140);
            this.newYPoint = rand.GetRandomNumber(1, 1080 - 140);
            bool circlFlag = (Cursor.Position.X + 140 > newXPoint || Cursor.Position.X < newXPoint) && (Cursor.Position.Y + 141 > newYPoint || Cursor.Position.Y < newYPoint);
            while (!circlFlag)
            {
                newXPoint = rand.GetRandomNumber(1, 1900 - 140);
                newYPoint = rand.GetRandomNumber(1, 1070 - 140);
                circlFlag = (Cursor.Position.X + 141 > newXPoint || Cursor.Position.X < newXPoint) && (Cursor.Position.Y + 141 > newYPoint || Cursor.Position.Y < newYPoint);
            }
            this.Location = new Point(newXPoint, newYPoint);
            /* if (timerNewLocation != null)
                 timerNewLocation.Dispose();
             timerNewLocation = new System.Timers.Timer(0.000003);
             timerNewLocation.Interval = 0.000003;
             timerNewLocation.Elapsed += new ElapsedEventHandler(timer_newElapced);
             timerNewLocation.Start();*/
        }

        public Form1()
        {
            InitializeComponent();
            this.TopMost = true;
            this.StartPosition = FormStartPosition.Manual;


            newLocation();
            timer1.Start();
        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.P)
            {
                newLocation();
            }
            if (e.KeyCode == Keys.Q)
            {
                this.Dispose();
                Application.Exit();
            }
        }

        private void form_MouseEnter(object sender, EventArgs e)
        {
            newLocation();
        }


        private void goToPosit(ref int pos, int min, int max)
        {
            int tmp = 0;
            for (int i = 0; i < 9; i++)
                tmp += rand.GetRandomNumber(min, max);
            pos = tmp / 10;
        }


        private void tmrDef_Tick(object sender, EventArgs e)
        {
            Point defPnt = new Point();
            GetCursorPos(ref defPnt);
            int xDif = 0;
            int yDif = 0;
            if (currX != defPnt.X || currY != defPnt.Y)
            {
                //Блок находится слева
                if (1920 / 2 < this.Location.X)
                {
                    //Курсор близко слева
                    if (1920 / 2 - currX < 0)
                        goToPosit(ref xDif, -50, 100);
                    else
                        goToPosit(ref xDif, -100, 50);
                }
                //Блок находится справа
                else
                {
                    //Курсор близко слева
                    if (1920 / 2 - currX < 0)
                        goToPosit(ref xDif, -100, 50);
                    else
                        goToPosit(ref xDif, -50, 100); 
                }
                //Блок находится сверху
                if (1080 / 2 < this.Location.X)
                {
                    //Курсор близко снизу
                    if (1080 / 2 - currY < 0)
                        goToPosit(ref yDif, -43, 76);
                    else
                        goToPosit(ref yDif, -76, 43);
                }
                //Блок находится снизу
                else
                {
                    if (1080 / 2 - currY < 0)
                        goToPosit(ref yDif, -76, 43); 
                    else
                        goToPosit(ref yDif, -43, 76);

                }
                currX = defPnt.X + xDif;
                currY = defPnt.Y + yDif;


            }

            Cursor.Position = new Point(currX, currY);
            //this.Location = new Point(rand.GetRandomNumber(3, 1020), rand.GetRandomNumber(3, 1900));
        }

    }
}
