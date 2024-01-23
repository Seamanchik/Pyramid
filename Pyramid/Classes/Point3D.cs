using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pyramid
{
    public sealed class Point3D
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

        public void RotateZ(float angle)
        {
            float newX = (float)(X * Math.Cos(angle) - Y * Math.Sin(angle));
            float newY = (float)(Y * Math.Cos(angle) + X * Math.Sin(angle));
            X = newX;
            Y = newY;
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
}