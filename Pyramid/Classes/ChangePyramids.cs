namespace Pyramid
{
    public class ChangePyramids
    {
        private IRotateble _rotate;
        
        public void SetActiveAction(IRotateble rotateble)
        {
            this._rotate = rotateble;
        }

        public void ChangePyramid(Point3D[] vertices, float value)
        {
            _rotate.Transform(vertices, value);
        }
    }
}