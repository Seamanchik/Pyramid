using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pyramid
{
    public partial class Form1 : Form
    {
        private Pyramid _pyramid;
        private Point _lastMousePos;
        private bool _isLeftMouseDown;

        public Form1()
        {
            InitializeComponent();
        }

        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (_pyramid != null)
                _pyramid.Draw(e.Graphics);
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
            _pyramid = new Pyramid(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            HandleKey(e.KeyCode, true);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            HandleKey(e.KeyCode, false);
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

    class Point3D
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

        public Point To2D()
        {
            return new Point((int)X + 200, (int)Y + 190);
        }
    }

    class Pyramid
    {
        private readonly Point3D[] _vertices;

        public Pyramid(float width, float height)
        {
            width /= 6;
            height /= 6;
            
            _vertices = new[]
            {
                new Point3D(width, height, width), //Левая нижняя вершина
                new Point3D(-width, height, width), //Правая верхняя вершина
                new Point3D(-width, height, -width), //Правая нижняя вершина
                new Point3D(width, height, -width), //Левая верхняя вершина
                new Point3D(0, -height, 0), //Верхняя вершина
            };
        }

        public void RotateX(float angle)
        {
            foreach (var vertex in _vertices)
            {
                vertex.RotateX(angle);
            }
        }

        public void RotateY(float angle)
        {
            foreach (var vertex in _vertices)
            {
                vertex.RotateY(angle);
            }
        }

        public void Zoom(float factor)
        {
            foreach (var vertex in _vertices)
            {
                vertex.Zoom(factor);
            }
        }

        public void Draw(Graphics g)
        {
            Pen pen = new Pen(Color.Black);

            for (int i = 0; i < 4; i++)
            {
                g.DrawLine(pen, _vertices[i].To2D(), _vertices[(i + 1) % 4].To2D());
                g.DrawLine(pen, _vertices[i].To2D(), _vertices[4].To2D());
            }
        }
    }
}
