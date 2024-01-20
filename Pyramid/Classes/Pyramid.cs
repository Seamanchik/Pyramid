namespace Pyramid
{
    public class Pyramid : PyramidDrawing
    {
        private readonly Point3D[] _vertices;
        public Pyramid(float width, float height)
        {
            width /= 6;
            height /= 6;
            
            _vertices = FillingPyramid(width, height);
        }

        public override Point3D[] GetVertices() => _vertices;
    }
}