namespace Pyramid
{
    public class RotatebleX : IRotateble
    {
        public void Transform(Point3D[] vertices, float angle)
        {
            foreach (var vertex in vertices)
            {
                vertex.RotateX(angle);
            }
        }
    }
}