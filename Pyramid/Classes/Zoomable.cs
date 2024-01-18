namespace Pyramid
{
    public class Zoomable : IRotateble
    {
        public void Transform(Point3D[] vertices, float factor)
        {
            foreach (var vertex in vertices)
            {
                vertex.Zoom(factor);
            }
        }
    }
}