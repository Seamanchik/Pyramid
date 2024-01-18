namespace Pyramid
{
    public class Pyramid : PyramidDrawing
    {
        public readonly Point3D[] Vertices;
        public Pyramid(float width, float height)
        {
            width /= 6;
            height /= 6;
            
            Vertices = FillingPyramid(width, height);
        }
    }
}