namespace Pyramid
{
    public class ChangePyramid
    {
        private IRotateble _rotate;
        private readonly Point3D[] _vertices;
        private readonly Point3D[] _scaledVertices;

        public ChangePyramid(Point3D[] vertices, Point3D[] scaledVertices)
        {
            _vertices  = vertices;
            _scaledVertices = scaledVertices;
        }

        private void SetActiveAction(IRotateble rotateble)
        {
            _rotate = rotateble;
        }

        public void ChangePyramids(IRotateble rotateble, float value)
        {
            SetActiveAction(rotateble);
            _rotate.Transform(_vertices, value);
            _rotate.Transform(_scaledVertices, value);
        }
    }
}