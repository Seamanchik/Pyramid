using System.Drawing;
using System.Windows.Forms;

namespace Pyramid
{
    public abstract class PyramidDrawing
    {
        protected Point3D[] FillingPyramid(float width, float height)
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
        
        public void Draw(Graphics g, PictureBox pictureBox, Pen pen, Point3D[] vertices)
        {
            for (int i = 0; i < 4; i++)
            {
                g.DrawLine(pen, vertices[i].To2D(pictureBox), vertices[(i + 1) % 4].To2D(pictureBox));
                g.DrawLine(pen, vertices[i].To2D(pictureBox), vertices[4].To2D(pictureBox));
            }
        }

        public abstract Point3D[] GetVertices();
    }
}