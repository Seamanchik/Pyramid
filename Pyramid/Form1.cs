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

                _lastMousePos = e.Location;
                pictureBox1.Invalidate();
            }
        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                _isLeftMouseDown = false;
        }

        private void btnCreatePyramid_Click(object sender, EventArgs e)
        {
            if (CheckPyramidValue(textBox2.Text) && CheckPyramidValue(textBox1.Text))
            {
                _pyramids = new Pyramids(pictureBox1.Width, pictureBox1.Height, textBox2.Text);
                _changePyramid = new ChangePyramid(_pyramids.GetVertices());
                pictureBox1.Invalidate();
                TimerStart(textBox1.Text);
            }
        }

        private bool CheckPyramidValue(string text)
        {
            if (string.IsNullOrEmpty(text) || int.Parse(text) == 0 || int.Parse(text) < 0)
            {
                MessageBox.Show(@"Введите значение больше нуля", @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        
        private void ChangePictureBoxBackColor(object sender, EventArgs e)
        {
            pictureBox1.BackColor = pictureBox1.BackColor == Color.White
                ? Color.Black : Color.White;
        }

        private void TimerStart(string number)
        {
            _timer.Interval = int.Parse(number);
            _timer.Start();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            HandleKey(e.KeyCode, true);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            HandleKey(e.KeyCode, false);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_pyramids != null)
            {
                _changePyramid.ChangePyramids(new RotatebleY(), 0.02f);
                pictureBox1.Invalidate();
            }
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
    }
}