using System.Collections.Generic;
using System.Drawing;

namespace Pyramid
{
    public class Zoomable : IRotateble
    {
        public void Transform((List<Point3D[]>,List<Color>) vertices, float factor)
        {
            for (int i = 0; i < vertices.Item1.Count; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    vertices.Item1[i][j].Zoom(factor);
                }
            }
        }
    }
}