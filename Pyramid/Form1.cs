using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;
using Pyramid.Classes;

namespace Pyramid
{
    public partial class Form1 : Form
    {
        private Pyramids _pyramids;
        private ChangePyramid _changePyramid;
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
        
        private void Form1_FormClosing(object sender, FormClosingEventArgs e) => CloseApp();

        private void Form1_FirstStart(object sender, EventArgs e)
        {
            var settings = _dataActivity.LoadData();
            if (settings != null)
            {
                controltTextBox1.Text = settings.PyramidsNumber.ToString();
                trackBar1.Value = settings.PyramidSpeed;
                pictureBox1.BackColor = settings.PictureBoxColor;
                
                foreach (var checkBox in tableLayoutPanel4.Controls.OfType<ControlCheckBox>())
                    checkBox.Checked = settings.CheckBoxList.Any(cb => cb.Text == checkBox.Text && cb.Checked);
            }
            CheckPyramid();
            TimerStart();
            UpdateLabelText();
        }
        
        private void StopButton_Click(object sender, EventArgs e) => Stop();
        
        private void CloseApp()
        {
            if (controltTextBox1.Text == @"Введите кол-во пирамид")
                return;
            ProgramSettings settings = new ProgramSettings
            {
                PyramidsNumber = int.Parse(controltTextBox1.Text),
                PyramidSpeed = trackBar1.Value,
                PictureBoxColor = pictureBox1.BackColor,
                CheckBoxList = AxisCheck.Instance.GetActualCheckBoxList(tableLayoutPanel4)
            };
            string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
            _dataActivity.SaveData(json);
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
            Stop();
            CheckPyramid();
        }

        private void CheckPyramid()
        {
            if (!controltTextBox1.CheckPyramidValue())
                return;
            _timer.Stop();
            _pyramids = new Pyramids(pictureBox1.Width, pictureBox1.Height, int.Parse(controltTextBox1.Text));
            _changePyramid = new ChangePyramid(_pyramids.GetVertices());
            pictureBox1.Invalidate();
        }

        private void TimerStart()
        {
            _timer.Stop();
            if (trackBar1.Value == 0)
            {
                MessageBox.Show(@"Введите значение больше нуля");
                return;
            }
            _timer.Interval = trackBar1.Value;
            _timer.Start(); 
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            AxisCheck.Instance.ActiveCheck(_changePyramid);
            pictureBox1.Invalidate();
        }

        private void HandleKey(Keys key, bool isKeyDown)
        {
            if (_pyramids == null)
                return;
            float delta = isKeyDown ? 0.01f : 0;

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

            _changePyramid.ChangePyramids(new RotatebleX(), deltaY * 0.01f);
            _changePyramid.ChangePyramids(new RotatebleY(), deltaX * 0.01f);
            _changePyramid.ChangePyramids(new RotatebleZ(), deltaX * 0.01f);

            _lastMousePos = e.Location;
            Invalidate();
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

            _changePyramid.ChangePyramids(new Zoomable(), deltaZoom);
            Invalidate();
        }
    }
}