namespace Pyramid
{
    public class RotatebleY : IRotateble
    {
        public void Transform(Point3D[] vertices, float angle)
        {
            foreach (var vertex in vertices)
            {
                vertex.RotateY(angle);
            }
        }
    }
}