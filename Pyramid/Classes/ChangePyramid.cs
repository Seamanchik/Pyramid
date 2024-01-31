using System.Collections.Generic;
using System.Drawing;
using Pyramid.Interface;

namespace Pyramid.Classes
{
    public class ChangePyramid
    {
        private IRotateble _rotate;
        private readonly (List<Point3D[]>, List<Color>) _vertices;

        public ChangePyramid((List<Point3D[]>, List<Color>) vertices) => _vertices = vertices;

        private void SetActiveAction(IRotateble rotateble) => this._rotate = rotateble;

        public void ChangePyramids(IRotateble rotateble, float value, int num)
        {
            SetActiveAction(rotateble);
            _rotate.Transform(_vertices, value, num);
        }
    }
}