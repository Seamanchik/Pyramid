namespace Pyramid
{
    public class ScaledPyramid : PyramidDrawing
    {
        private readonly Point3D[] _scaledVertices;
        public ScaledPyramid(float width, float height) 
        {
            width /= 6;
            height /= 6;
            
            float reWidth = width * 0.5f;
            float reHeight = height * 0.5f;

            _scaledVertices = FillingPyramid(reWidth, reHeight);
        }
        
        public override Point3D[] GetVertices() => _scaledVertices;
    }
}