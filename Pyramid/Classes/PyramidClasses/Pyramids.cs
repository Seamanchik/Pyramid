using System.Collections.Generic;
using System.Drawing;
using Pyramid.Classes.PointClasses;

namespace Pyramid.Classes.PyramidClasses
{
    public class Pyramids : PyramidDrawing
    {
        private readonly (List<Point3D[]>, List<Color>) _pyramidsList;
        
        public Pyramids(float width, float height, int n, int constNum)
        {
            _pyramidsList = (new List<Point3D[]>(n),new List<Color>(n));
            width /= constNum;
            height /= constNum;

            InitializePyramid(_pyramidsList, width, height);
        }
        
        public void ResizePyramids(float newWidth, float newHeight, int constNum)
        {
            _pyramidsList.Item1.Clear();
            _pyramidsList.Item2.Clear();
            newWidth /= constNum;
            newHeight /= constNum;
            Resize(_pyramidsList, newWidth, newHeight);
        }
        
        public override (List<Point3D[]>, List<Color>) GetVertices() => _pyramidsList;
    }
}