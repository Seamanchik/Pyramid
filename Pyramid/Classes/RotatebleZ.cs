using System.Collections.Generic;
using System.Drawing;
using Pyramid.Interface;

namespace Pyramid.Classes
{
    public class RotatebleZ : IRotateble
    {
        public void Transform((List<Point3D[]>,List<Color>) vertices, float angle, int num)
        {
            for (int i = 0; i < vertices.Item1.Count; i++)
            {
                for (int j = 0; j < num; j++)
                {
                    vertices.Item1[i][j].RotateZ(angle);
                }
            }
        }
    }
}