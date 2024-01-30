using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pyramid
{
    public partial class Form1 : Form
    {
        private Pyramids _pyramids;
        private ChangePyramid _changePyramid;
        private Point _lastMousePos;
        private bool _isLeftMouseDown;
        private readonly Timer _timer = new Timer();
        
        public Form1()
        {
            InitializeComponent();
            _timer.Tick += timer1_Tick;
            trackBar1.Scroll -= trackBar1_Scroll;
            trackBar1.Scroll += trackBar1_Scroll;
        }

        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (_pyramids != null)
                _pyramids.Draw(e.Graphics, pictureBox1, _pyramids.GetVertices());
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isLeftMouseDown = true;
                _lastMousePos = e.Location;
            }
        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isLeftMouseDown && _pyramids != null)
            {
                int deltaX = e.X - _lastMousePos.X;
                int deltaY = e.Y - _lastMousePos.Y;

                _changePyramid.ChangePyramids(new RotatebleX(), deltaY * 0.01f);
                _changePyramid.ChangePyramids(new RotatebleY(), deltaX * 0.01f);
                _changePyramid.ChangePyramids(new RotatebleZ(), deltaX * 0.01f);

                _lastMousePos = e.Location;
                pictureBox1.Invalidate();
            }
        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                _isLeftMouseDown = false;
        }
        
        private void PictureBox_Scroll(object sender, MouseEventArgs e)
        {
            if (_pyramids != null)
            {
                float deltaZoom = e.Delta > 0 ? 1.1f : 0.9f;

                _changePyramid.ChangePyramids(new Zoomable(), deltaZoom);
                pictureBox1.Invalidate();
            }
        }
        
        private void ChangePictureBoxBackColor(object sender, EventArgs e)
        {
            if (pictureBox1.BackColor == Color.White)
            {
                pictureBox1.BackColor = Color.Black;
                button1.Text = @"Светлая тема";
            }
            else
            {
                pictureBox1.BackColor = Color.White;
                button1.Text = @"Тёмная тема";
            }
        }

        private void btnCreatePyramid_Click(object sender, EventArgs e)
        {
            if (controltTextBox1.CheckPyramidValue())
            {
                _timer.Stop();
                _pyramids = new Pyramids(pictureBox1.Width, pictureBox1.Height, controltTextBox1.Text);
                _changePyramid = new ChangePyramid(_pyramids.GetVertices());
                pictureBox1.Invalidate();
            }
        }

        private void TimerStart()
        {
            if (trackBar1.Value == 0)
            {
                MessageBox.Show(@"Введите значение больше нуля");
                return;
            }
            _timer.Interval = trackBar1.Value;
            _timer.Start(); 
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e) => HandleKey(e.KeyCode, true);
        
        private void Form1_KeyUp(object sender, KeyEventArgs e) => HandleKey(e.KeyCode, false); 

        private void timer1_Tick(object sender, EventArgs e)
        {
            AxisCheck.Instance.ActiveCheck(_changePyramid);
            pictureBox1.Invalidate();
        }

        private void HandleKey(Keys key, bool isKeyDown)
        {
            if (_pyramids != null)
            {
                float delta = isKeyDown ? 0.1f : 0;

                switch (key)
                {
                    case Keys.W:
                        _changePyramid.ChangePyramids(new RotatebleX(), delta);
                        break;
                    case Keys.S:
                        _changePyramid.ChangePyramids(new RotatebleX(), -delta);
                        break;
                    case Keys.A:
                        _changePyramid.ChangePyramids(new RotatebleY(), delta);
                        break;
                    case Keys.D:
                        _changePyramid.ChangePyramids(new RotatebleY(), -delta);
                        break;
                }

                pictureBox1.Invalidate();
            }
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            AxisCheck.Instance.Delete();
            trackBar1.Value = 0;
            label1.Text = $@"Текущая скорость: {trackBar1.Value}"; 
            _timer.Stop();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {         
            label1.Text = $@"Текущая скорость: {trackBar1.Value}"; 
            _timer.Stop();
            TimerStart();
        }
    }
}