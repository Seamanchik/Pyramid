using System.Collections.Generic;
using System.Drawing;
using Pyramid.Classes.PointClasses;

namespace Pyramid.Classes.PyramidClasses
{
    public class Pyramids : PyramidDrawing
    {
        private readonly (List<Point3D[]>, List<Color>) _pyramidsList;
        public Pyramids(float width, float height, int n, int num)
        {
            _pyramidsList = (new List<Point3D[]>(n), new List<Color>(n));
            width /= num;
            height /= num;

            InitializePyramid(_pyramidsList, width, height);
        }
        
        public override (List<Point3D[]>, List<Color>) GetVertices() => _pyramidsList;
    }
}