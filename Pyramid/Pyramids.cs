using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Pyramid
{
    public class Pyramids
    {
        public readonly Point3D[] Vertices;
        public readonly Point3D[] ScaledVertices;

        public Pyramids(float width, float height)
        {
            width /= 6;
            height /= 6;

            float reWidth = width * 0.5f;
            float reHeight = height * 0.5f;

            Vertices = FillingPyramid(width, height);
            ScaledVertices = FillingPyramid(reWidth, reHeight);
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
        
        public void Draw(Graphics g, PictureBox pictureBox)
        {
            Pen pen = new Pen(Color.Black);
            Pen pens = new Pen(Color.Red);
            pens.DashStyle = DashStyle.Dash;

            for (int i = 0; i < 4; i++)
            {
                g.DrawLine(pen, Vertices[i].To2D(pictureBox), Vertices[(i + 1) % 4].To2D(pictureBox));
                g.DrawLine(pen, Vertices[i].To2D(pictureBox), Vertices[4].To2D(pictureBox));
            }

            for (int i = 0; i < 4; i++)
            {
                g.DrawLine(pens, ScaledVertices[i].To2D(pictureBox), ScaledVertices[(i + 1) % 4].To2D(pictureBox));
                g.DrawLine(pens, ScaledVertices[i].To2D(pictureBox), ScaledVertices[4].To2D(pictureBox));
            }
        }
    }
}