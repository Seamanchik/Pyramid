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
        private int _numberOfRotation;

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

        private void btnCreatePyramid_Click(object sender, EventArgs e)
        {
            if (CheckPyramidValue())
            {
                _timer.Stop();
                _pyramids = new Pyramids(pictureBox1.Width, pictureBox1.Height, textBox1.Text);
                _changePyramid = new ChangePyramid(_pyramids.GetVertices());
                pictureBox1.Invalidate();
            }
        }

        private bool CheckPyramidValue()
        {
            if (textBox1.Text == @"Введите кол-во пирамид" || string.IsNullOrEmpty(textBox1.Text) ||
                int.Parse(textBox1.Text) <= 0)
            {
                MessageBox.Show(@"Введите значение больше нуля", @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private bool CheckPyramidSpeed()
        {
            if (textBox2.Text == @"Введите скорость" || string.IsNullOrEmpty(textBox2.Text) ||
                int.Parse(textBox2.Text) <= 0)
            {
                MessageBox.Show(@"Введите скорость больше нуля", @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void ChangePictureBoxBackColor(object sender, EventArgs e)
        {
            if (pictureBox1.BackColor == Color.FromArgb(207, 215, 206))
            {
                pictureBox1.BackColor = Color.FromArgb(88, 105, 86);
                button1.Text = @"Светлый экран";
            }
            else
            {
                pictureBox1.BackColor = Color.FromArgb(207, 215, 206);
                button1.Text = @"Тёмный экран";
            }
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
                switch (_numberOfRotation)
                {
                    case 1:
                        _changePyramid.ChangePyramids(new RotatebleX(), 0.02f);
                        break;
                    case 2:
                        _changePyramid.ChangePyramids(new RotatebleY(), 0.02f);
                        break;
                    case 3:
                        _changePyramid.ChangePyramids(new RotatebleZ(), 0.02f);
                        break;
                }

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

        private void AxisXbutton_Click(object sender, EventArgs e)
        {
            if (_pyramids != null && CheckPyramidSpeed())
            {
                _numberOfRotation = 1;
                TimerStart(textBox2.Text);
            }
        }

        private void AxisYbutton_Click(object sender, EventArgs e)
        {
            if (_pyramids != null && CheckPyramidSpeed())
            {
                _numberOfRotation = 2;
                TimerStart(textBox2.Text);
            }
        }

        private void AxisZbutton_Click(object sender, EventArgs e)
        {
            if (_pyramids != null && CheckPyramidSpeed())
            {
                _numberOfRotation = 3;
                TimerStart(textBox2.Text);
            }
        }

        private void StopButton_Click(object sender, EventArgs e) => _timer.Stop();

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == @"Введите кол-во пирамид")
            {
                textBox1.Text = null;
                textBox1.ForeColor = Color.Black;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = @"Введите кол-во пирамид";
                textBox1.ForeColor = Color.FromArgb(91, 91, 91);
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == @"Введите скорость")
            {
                textBox2.Text = null;
                textBox2.ForeColor = Color.Black;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = @"Введите скорость";
                textBox2.ForeColor = Color.FromArgb(91, 91, 91);
            }
        }
    }
}