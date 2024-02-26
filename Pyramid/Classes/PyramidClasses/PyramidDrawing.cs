using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Pyramid.Classes.PointClasses;

namespace Pyramid.Classes.PyramidClasses
{
    public abstract class PyramidDrawing
    {
        private readonly Dictionary<Color, Pen> _pens = new Dictionary<Color, Pen>();
        private readonly List<Color> _colors = new List<Color>();
        private const float ScaleNum = 1.1f;
        
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
            Rectangle visibleRect = pictureBox.ClientRectangle;

            for (int i = 0; i < vertices.Item1.Count; i++)
            {
                if (!IsPyramidVisible(vertices.Item1[i], visibleRect, pictureBox)) 
                    continue;
                Pen pen = GetPen(vertices.Item2[i]);
                for (int j = 0; j < 4; j++)
                {
                    g.DrawLine(pen, vertices.Item1[i][j].To2D(pictureBox), vertices.Item1[i][(j + 1) % 4].To2D(pictureBox));
                    g.DrawLine(pen, vertices.Item1[i][j].To2D(pictureBox), vertices.Item1[i][4].To2D(pictureBox));
                }
            }
        }
        
        private bool IsPyramidVisible(Point3D[] pyramid, Rectangle visibleRect, PictureBox pictureBox)
        {
            foreach (var point in pyramid)
            {
                Point point2D = point.To2D(pictureBox);
                if (visibleRect.Contains(point2D))
                    return true;
            }
            return false;
        }
        
        private Pen GetPen(Color color)
        {
            if (!_pens.ContainsKey(color))
            {
                Pen pen = new Pen(color);
                _pens.Add(color, pen);
            }
            
            return _pens[color];
        }
        
        protected void InitializePyramid((List<Point3D[]>, List<Color>) pyramidList, float width, float height)
        {
            Random rnd = new Random();
            for (int i = 0; i < pyramidList.Item1.Capacity; i++)
            {
                _pens.Clear();
                pyramidList.Item1.Add(FillingPyramid(width,height));
                Color color = Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
                pyramidList.Item2.Add(color);
                _colors.Add(color);
                width /= ScaleNum;
                height /= ScaleNum;
            }
        }

        protected void Resize((List<Point3D[]>, List<Color>) pyramidList,float newWidth, float newHeight)
        {
            foreach (Color color in _colors)
            {
                pyramidList.Item1.Add(FillingPyramid(newWidth, newHeight));
                pyramidList.Item2.Add(color);
                newWidth /= ScaleNum;
                newHeight /= ScaleNum;
            }
        }
        
        public abstract (List<Point3D[]>,List<Color>) GetVertices();
    }
}