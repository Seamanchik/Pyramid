using System.Collections.Generic;
using System.Drawing;

namespace Pyramid
{
    public class Pyramids : PyramidDrawing
    {
        private readonly (List<Point3D[]>, List<Color>) _pyramidsList;
        public Pyramids(float width, float height, string n)
        {
            _pyramidsList = (new List<Point3D[]>(int.Parse(n)), new List<Color>(int.Parse(n)));
            width /= 5;
            height /= 5;

            InitializePyramid(_pyramidsList, width, height);
        }

        public override (List<Point3D[]>, List<Color>) GetVertices() => _pyramidsList;
    }
}