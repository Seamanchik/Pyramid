namespace Pyramid
{
    public class ScaledPyramid : PyramidDrawing
    {
        public readonly Point3D[] ScaledVertices;
        public ScaledPyramid(float width, float height) 
        {
            width /= 6;
            height /= 6;
            
            float reWidth = width * 0.5f;
            float reHeight = height * 0.5f;

            ScaledVertices = FillingPyramid(reWidth, reHeight);
        }
    }
}