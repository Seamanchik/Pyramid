using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pyramid
{
    public partial class Form1 : Form
    {
        private Pyramid _pyramid;
        private readonly ChangePyramids _changePyramids = new ChangePyramids();
        private ScaledPyramid  _scaledPyramid;
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
            if (_pyramid != null)
            {
                _pyramid.Draw(e.Graphics, pictureBox1, Pens.Black, _pyramid.Vertices);
                _scaledPyramid.Draw(e.Graphics, pictureBox1, Pens.Red, _scaledPyramid.ScaledVertices);
            }
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
            if (_isLeftMouseDown && _pyramid != null)
            {
                int deltaX = e.X - _lastMousePos.X;
                int deltaY = e.Y - _lastMousePos.Y;

                _changePyramids.SetActiveAction(new RotatebleX());
                _changePyramids.ChangePyramid(_pyramid.Vertices, deltaY * 0.01f);
                _changePyramids.ChangePyramid(_scaledPyramid.ScaledVertices, deltaY * 0.01f);
                
                _changePyramids.SetActiveAction(new RotatebleY());
                _changePyramids.ChangePyramid(_pyramid.Vertices, deltaX * 0.01f);
                _changePyramids.ChangePyramid(_scaledPyramid.ScaledVertices, deltaX * 0.01f);

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
            _pyramid = new Pyramid(pictureBox1.Width, pictureBox1.Height);
            _scaledPyramid = new ScaledPyramid(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Invalidate();
            TimerStart();
        }

        private void TimerStart()
        {
            _timer.Interval = 25;
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
            if (_pyramid != null)
            {
                _changePyramids.SetActiveAction(new RotatebleY());
                _changePyramids.ChangePyramid(_pyramid.Vertices, 0.02f);
                _changePyramids.ChangePyramid(_scaledPyramid.ScaledVertices, 0.02f);
                pictureBox1.Invalidate();
            }
        }

        private void PictureBox_Scroll(object sender, MouseEventArgs e)
        {
            if (_pyramid != null)
            {
                float deltaZoom = e.Delta > 0 ? 1.1f : 0.9f;

                _changePyramids.SetActiveAction(new Zoomable());
                _changePyramids.ChangePyramid(_pyramid.Vertices, deltaZoom);
                _changePyramids.ChangePyramid(_scaledPyramid.ScaledVertices, deltaZoom);
                pictureBox1.Invalidate();
            }
        }

        private void HandleKey(Keys key, bool isKeyDown)
        {
            if (_pyramid != null)
            {
                float delta = isKeyDown ? 0.1f : 0;

                switch (key)
                {
                    case Keys.W:
                        _changePyramids.SetActiveAction(new RotatebleX());
                        break;
                    case Keys.S:
                        _changePyramids.SetActiveAction(new RotatebleX());
                        break;
                    case Keys.A:
                        _changePyramids.SetActiveAction(new RotatebleY());
                        delta = -delta;
                        break;
                    case Keys.D:
                        _changePyramids.SetActiveAction(new RotatebleY());
                        delta = -delta;
                        break;
                }
                
                _changePyramids.ChangePyramid(_pyramid.Vertices,delta);
                _changePyramids.ChangePyramid(_scaledPyramid.ScaledVertices,delta);
                pictureBox1.Invalidate();
            }
        }
    }
}