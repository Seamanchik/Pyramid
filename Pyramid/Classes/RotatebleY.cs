﻿using System;
namespace Pyramid.Classes
{
    public class RotatebleY : RotatebleAxis
    {
        protected override void Manipulate(Point3D point,float angle) //Вращение вокруг оси Y
        {
            float newX = (float)(point.X * Math.Cos(angle) + point.Z * Math.Sin(angle));
            float newZ = (float)(-point.X * Math.Sin(angle) + point.Z * Math.Cos(angle));
            point.X = newX;
            point.Z = newZ;
        }
    }
}