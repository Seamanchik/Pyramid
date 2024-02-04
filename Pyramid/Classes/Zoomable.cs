﻿namespace Pyramid.Classes
{
    public class Zoomable : RotatebleAxis
    {
        protected override void Manipulate(Point3D point,float factor)
        {
            point.X *= factor;
            point.Y *= factor;
            point.Z *= factor;
        }
    }
}