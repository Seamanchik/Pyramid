using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pyramid.Classes.PointClasses
{
    public class Point3D
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Point3D(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
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