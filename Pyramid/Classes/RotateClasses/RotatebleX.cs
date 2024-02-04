using System;

namespace Pyramid.Classes.RotateClasses
{
    public class RotatebleX : RotatebleAxis
    {
        protected override void Manipulate(Point3D point,float angle) //Вращение вокруг оси Х
        {
            float newY = (float)(point.Y * Math.Cos(angle) - point.Z * Math.Sin(angle));
            float newZ = (float)(point.Y * Math.Sin(angle) + point.Z * Math.Cos(angle));
            point.Y = newY;
            point.Z = newZ;
        }
    }
}