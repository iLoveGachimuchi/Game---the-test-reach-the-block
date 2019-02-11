using System;
using System.Timers;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace cs_Test
{
    class rnd
    {
        private Random random = new Random();
        private object syncLock = new object();
        public int GetRandomNumber(int minimum, int maximum)
        {
            lock (syncLock)
            {
                return random.Next(minimum, maximum);
            }
        }

    };

    internal class mouseNew
    {
        private rnd rand = new rnd();

        private Point pos = Cursor.Position;

        private bool invertX;

        private bool invertY;

        private bool running;

        private bool exit;

        public bool InvertX
        {
            get
            {
                return this.invertX;
            }
            set
            {
                this.invertX = value;
                if (this.InvertSettingsChanged != null)
                {
                    this.InvertSettingsChanged(this, new EventArgs());
                }
            }
        }

        public bool InvertY
        {
            get
            {
                return this.invertY;
            }
            set
            {
                this.invertY = value;
                if (this.InvertSettingsChanged != null)
                {
                    this.InvertSettingsChanged(this, new EventArgs());
                }
            }
        }

        public bool Running
        {
            get
            {
                return this.running;
            }
        }

        public mouseNew(bool x, bool y)
        {
            this.invertX = x;
            this.invertY = y;
            this.pos = Cursor.Position;
        }

        private void MouseLoop()
        {
            Thread.CurrentThread.IsBackground = true;
            Thread.CurrentThread.Priority = ThreadPriority.Highest;
            while (!this.exit)
            {
                Point position = Cursor.Position;
                int right = (this.invertX ? this.pos.X - (position.X - this.pos.X) : position.X);
                if (this.invertX)
                {
                    if (right < 2)
                    {
                        right = 2;
                    }
                    if (right > Screen.FromPoint(position).Bounds.Right - 2)
                    {
                        Rectangle bounds = Screen.FromPoint(position).Bounds;
                        right = bounds.Right - 2;
                    }
                }
                int bottom = (this.invertY ? this.pos.Y - (position.Y - this.pos.Y) : position.Y);
                if (this.invertY)
                {
                    if (bottom < 2)
                    {
                        bottom = 2;
                    }
                    if (bottom > Screen.FromPoint(position).Bounds.Bottom - 2)
                    {
                        Rectangle rectangle = Screen.FromPoint(position).Bounds;
                        bottom = rectangle.Bottom - 2;
                    }
                }
                Cursor.Position = new Point(right, bottom);
                this.pos = Cursor.Position;
                Thread.Sleep(1);
            }
            this.exit = false;
        }

        public void Start()
        {
            this.pos = Cursor.Position;
            this.running = true;
            (new Thread(new ThreadStart(this.MouseLoop))).Start();
        }

        public void Stop()
        {
            this.running = false;
            this.exit = true;
        }

        public event EventHandler InvertSettingsChanged;
    };
    
}
