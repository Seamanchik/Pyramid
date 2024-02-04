using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Pyramid.Classes.PointClasses;
using Pyramid.Interface;

namespace Pyramid.Classes.RotateClasses
{
    public abstract class RotatebleAxis : IRotateble
    {
        public void Transform((List<Point3D[]>, List<Color>) vertices, float angle, int num)
        {
            foreach (var pyramid in vertices.Item1)
            {
                foreach (var point in pyramid.Take(num))
                {
                    Manipulate(point, angle);
                }
            }
        }
        
        protected abstract void Manipulate(Point3D point, float angle);
    }
}