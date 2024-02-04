using System;
using System.Drawing;
using System.Windows.Forms;
using Pyramid.Classes;
using Pyramid.Classes.Controls;
using Pyramid.Classes.JsonClasses;
using Pyramid.Classes.PyramidClasses;
using Pyramid.Classes.RotateClasses;
using Timer = System.Windows.Forms.Timer;

namespace Pyramid
{
    public partial class Form1 : Form
    {
        private const int NumberOfVertice = 5;
        private Pyramids _pyramids;
        private RotatePyramid _rotatePyramid;
        private bool _isLeftMouseDown;
        private Point _lastMousePos;
        private readonly Timer _timer = new Timer();
        private readonly JsonDataActivity _dataActivity = new JsonDataActivity();
        
        public Form1()
        {
            InitializeComponent();
            _timer.Tick += timer1_Tick;
            trackBar1.Scroll -= trackBar1_Scroll;
            trackBar1.Scroll += trackBar1_Scroll;
        } 
        
        private void Form1_KeyDown(object sender, KeyEventArgs e) => HandleKey(e.KeyCode, true);
        
        private void Form1_KeyUp(object sender, KeyEventArgs e) => HandleKey(e.KeyCode, false);
        
        private void Form1_FormClosing(object sender, FormClosingEventArgs e) => _dataActivity.CloseApp(controltTextBox1, trackBar1.Value, pictureBox1.BackColor, tableLayoutPanel4);

        private void Form1_FirstStart(object sender, EventArgs e)
        {
            _dataActivity.FirstStart(controltTextBox1, trackBar1, pictureBox1, tableLayoutPanel4);
            InitializePyramids();
            UpdateLabelText();
            TimerStart();
            pictureBox1.Invalidate();
        }

        private void InitializePyramids()
        {
            _pyramids = new Pyramids(pictureBox1.Width, pictureBox1.Height, int.Parse(controltTextBox1.Text), NumberOfVertice);
            _rotatePyramid = new RotatePyramid(_pyramids.GetVertices());
        }
        
        private void StopButton_Click(object sender, EventArgs e) => Stop();
        
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
            Stop();
            CheckPyramid();
        }
        
        private void CheckPyramid()
        {
            if (!controltTextBox1.CheckPyramidValue())
                return;
            _timer.Stop();
            InitializePyramids();
            pictureBox1.Invalidate();
        }

        private void TimerStart()
        {
            _timer.Stop();
            if (trackBar1.Value == 0)
            {
                trackBar1.Value = 1;
                UpdateLabelText();
            }
            _timer.Interval = trackBar1.Value;
            _timer.Start(); 
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            AxisCheck.Instance.ActiveCheck(_rotatePyramid, NumberOfVertice);
            pictureBox1.Invalidate();
        }

        private void HandleKey(Keys key, bool isKeyDown)
        {
            if (_pyramids == null)
                return;
            float delta = isKeyDown ? 0.02f : 0;

            switch (key)
            {
                case Keys.W:
                    _rotatePyramid.ChangePyramids(new RotatebleX(), delta, NumberOfVertice);
                    break;
                case Keys.S:
                    _rotatePyramid.ChangePyramids(new RotatebleX(), -delta, NumberOfVertice);
                    break;
                case Keys.A:
                    _rotatePyramid.ChangePyramids(new RotatebleY(), -delta, NumberOfVertice);
                    break;
                case Keys.D:
                    _rotatePyramid.ChangePyramids(new RotatebleY(), delta , NumberOfVertice);
                    break;
            }

            pictureBox1.Invalidate();
        }
        
        private void Stop()
        {
            AxisCheck.Instance.Delete();
            trackBar1.Value = 0;
            UpdateLabelText();
            _timer.Stop();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {       
            UpdateLabelText();
            TimerStart();
        }

        private void UpdateLabelText() => label1.Text = $@"Текущая скорость: {trackBar1.Value}";

        private void PictureBox_Paint(object sender, PaintEventArgs e) => _pyramids?.Draw(e.Graphics, pictureBox1, _pyramids.GetVertices());

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            _isLeftMouseDown = true;
            _lastMousePos = e.Location;
        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isLeftMouseDown || _pyramids == null)
                return;
            int deltaX = e.X - _lastMousePos.X;
            int deltaY = e.Y - _lastMousePos.Y;

            _rotatePyramid.ChangePyramids(new RotatebleX(), deltaY * 0.02f, NumberOfVertice);
            _rotatePyramid.ChangePyramids(new RotatebleY(), deltaX * 0.02f, NumberOfVertice);
            _rotatePyramid.ChangePyramids(new RotatebleZ(), deltaX * 0.02f, NumberOfVertice);

            _lastMousePos = e.Location;
            pictureBox1.Invalidate();
        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                _isLeftMouseDown = false;
        }
        
        private void PictureBox_Scroll(object sender, MouseEventArgs e)
        {
            if (_pyramids == null)
                return;
            float deltaZoom = e.Delta > 0 ? 1.1f : 0.9f;

            _rotatePyramid.ChangePyramids(new Zoomable(), deltaZoom, NumberOfVertice);
            pictureBox1.Invalidate();
        }
    }
}