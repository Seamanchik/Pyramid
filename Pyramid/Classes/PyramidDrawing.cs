using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Pyramid.Classes
{
    public abstract class PyramidDrawing
    {
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
        
        public void Draw(Graphics g, PictureBox pictureBox, (List<Point3D[]>, List<Color>) vertices)
        {
            for (int i = 0; i < vertices.Item1.Count; i++)
            {
                Pen pen = new Pen(vertices.Item2[i]);
                for (int j = 0; j < 4; j++)
                {
                    g.DrawLine(pen, vertices.Item1[i][j].To2D(pictureBox), vertices.Item1[i][(j + 1) % 4].To2D(pictureBox));
                    g.DrawLine(pen, vertices.Item1[i][j].To2D(pictureBox), vertices.Item1[i][4].To2D(pictureBox)); 
                }
            }
        }

        protected void InitializePyramid((List<Point3D[]>, List<Color>) pyramidList, float width, float height)
        {
            Random rnd = new Random();
            for (int i = 0; i < pyramidList.Item1.Capacity; i++)
            {
                pyramidList.Item1.Add(FillingPyramid(width,height));
                pyramidList.Item2.Add(Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255)));
                width /= 1.2f;
                height /= 1.2f;
            }
        }

        public abstract (List<Point3D[]>,List<Color>) GetVertices();
    }
}