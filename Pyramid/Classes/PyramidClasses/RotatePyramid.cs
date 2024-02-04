using System.Collections.Generic;
using System.Drawing;
using Pyramid.Interface;

namespace Pyramid.Classes.Pyramid
{
    public class RotatePyramid
    {
        private IRotateble _rotate;
        private readonly (List<Point3D[]>, List<Color>) _vertices;

        public RotatePyramid((List<Point3D[]>, List<Color>) vertices) => _vertices = vertices;
        
        public void ChangePyramids(IRotateble rotateble, float value, int num)
        {
            _rotate = rotateble;
            _rotate.Transform(_vertices, value, num);
        }
    }
}