using System.Collections.Generic;
using System.Drawing;

namespace Pyramid
{
    public interface IRotateble
    {
        void Transform((List<Point3D[]>, List<Color>) vertices, float value);
    }
}