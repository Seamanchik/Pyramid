﻿using System.Collections.Generic;
using System.Drawing;
using Pyramid.Classes.PointClasses;

namespace Pyramid.Interface
{
    public interface IRotateble
    {
        void Transform((List<Point3D[]>, List<Color>) vertices, float value, int num);
    }
}