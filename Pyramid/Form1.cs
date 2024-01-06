using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Pyramid
{
    public partial class Form1 : Form
    {
        private Pyramids _pyramid;
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
                _pyramid.Draw(e.Graphics, pictureBox1);
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

                _pyramid.RotateY(deltaX * 0.01f);
                _pyramid.RotateX(deltaY * 0.01f);

                _lastMousePos = e.Location;
                pictureBox1.Invalidate();
            }
        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isLeftMouseDown = false;
            }
        }

        private void btnCreatePyramid_Click(object sender, EventArgs e)
        {
            _pyramid = new Pyramids(pictureBox1.Width, pictureBox1.Height);
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
                _pyramid.RotateY(0.02f);
                pictureBox1.Invalidate();
            }
        }

        private void PictureBox_Scroll(object sender, MouseEventArgs e)
        {
            if (_pyramid != null)
            {
                float deltaZoom = e.Delta > 0 ? 1.1f : 0.9f;
                _pyramid.Zoom(deltaZoom);
                pictureBox1.Invalidate();
            }
        }

        private void HandleKey(Keys key, bool isKeyDown)
        {
            if (_pyramid != null)
            {
                float delta = 0.1f;

                switch (key)
                {
                    case Keys.W:
                        _pyramid.RotateX(isKeyDown ? delta : 0);
                        break;
                    case Keys.S:
                        _pyramid.RotateX(isKeyDown ? -delta : 0);
                        break;
                    case Keys.A:
                        _pyramid.RotateY(isKeyDown ? -delta : 0);
                        break;
                    case Keys.D:
                        _pyramid.RotateY(isKeyDown ? delta : 0);
                        break;
                }

                pictureBox1.Invalidate();
            }
        }
    }

    public class Point3D
    {
        private float X { get; set; }
        private float Y { get; set; }
        private float Z { get; set; }

        public Point3D(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public void RotateX(float angle) //Вращение вокруг оси Х
        {
            float newY = (float)(Y * Math.Cos(angle) - Z * Math.Sin(angle));
            float newZ = (float)(Y * Math.Sin(angle) + Z * Math.Cos(angle));
            Y = newY;
            Z = newZ;
        }

        public void RotateY(float angle) //Вращение вокруг оси Y
        {
            float newX = (float)(X * Math.Cos(angle) + Z * Math.Sin(angle));
            float newZ = (float)(-X * Math.Sin(angle) + Z * Math.Cos(angle));
            X = newX;
            Z = newZ;
        }

        public void Zoom(float factor)
        {
            X *= factor;
            Y *= factor;
            Z *= factor;
        }

        public Point To2D(PictureBox pictureBox)
        {
            if (pictureBox != null)
            {
                int centerX = pictureBox.Width / 2;
                int centerY = pictureBox.Height / 2;

                return new Point((int)X + centerX, (int)Y + centerY);
            }
            else
                throw new Exception("PictureBox не задан");
        }
    }

    public class Pyramids
    {
        private readonly Point3D[] _vertices;
        private readonly Point3D[] _scaledVertices;

        public Pyramids(float width, float height)
        {
            width /= 6;
            height /= 6;

            float reWidth = width * 0.5f;
            float reHeight = height * 0.5f;

            _vertices = FillingPyramid(width, height);
            _scaledVertices = FillingPyramid(reWidth, reHeight);
        }

        private Point3D[] FillingPyramid(float width, float height)
        {
            Point3D[] pointsArray = new[]
            {
                new Point3D(width, height, width),
                new Point3D(-width, height, width),
                new Point3D(-width, height, -width),
                new Point3D(width, height, -width),
                new Point3D(0, -height, 0),
            };
            return pointsArray;
        }

        public void RotateX(float angle)
        {
            RotateXVertices(_vertices, angle);
            RotateXVertices(_scaledVertices, angle);
        }

        public void RotateY(float angle)
        {
            RotateYVertices(_vertices, angle);
            RotateYVertices(_scaledVertices, angle);
        }

        public void Zoom(float factor)
        {
            ZoomVertices(_vertices, factor);
            ZoomVertices(_scaledVertices, factor);
        }

        private void RotateXVertices(Point3D[] vertices, float angle)
        {
            foreach (var vertex in vertices)
            {
                vertex.RotateX(angle);
                vertex.RotateX(angle);
            }
        }

        private void RotateYVertices(Point3D[] vertices, float angle)
        {
            foreach (var vertex in vertices)
            {
                vertex.RotateY(angle);
                vertex.RotateY(angle);
            }
        }

        private void ZoomVertices(Point3D[] vertices, float factor)
        {
            foreach (var vertex in vertices)
            {
                vertex.Zoom(factor);
            }
        }

        public void Draw(Graphics g, PictureBox pictureBox)
        {
            Pen pen = new Pen(Color.Black);
            Pen pens = new Pen(Color.Red);
            pens.DashStyle = DashStyle.Dash;

            for (int i = 0; i < 4; i++)
            {
                g.DrawLine(pen, _vertices[i].To2D(pictureBox), _vertices[(i + 1) % 4].To2D(pictureBox));
                g.DrawLine(pen, _vertices[i].To2D(pictureBox), _vertices[4].To2D(pictureBox));
            }

            for (int i = 0; i < 4; i++)
            {
                g.DrawLine(pens, _scaledVertices[i].To2D(pictureBox), _scaledVertices[(i + 1) % 4].To2D(pictureBox));
                g.DrawLine(pens, _scaledVertices[i].To2D(pictureBox), _scaledVertices[4].To2D(pictureBox));
            }
        }
    }
}