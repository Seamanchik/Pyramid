using System;
using Pyramid.Classes.PointClasses;

namespace Pyramid.Classes.RotateClasses
{
    public class RotatebleZ : RotatebleAxis
    {
        protected override void Manipulate(Point3D point,float angle) //Вращение вокруг оси Z
        {
            float newX = (float)(point.X * Math.Cos(angle) - point.Y * Math.Sin(angle));
            float newY = (float)(point.Y * Math.Cos(angle) + point.X * Math.Sin(angle));
            point.X = newX;
            point.Y = newY;
        }
    }
}