using System;
using System.Windows.Forms;

namespace Pyramid
{
    public class ControlCheckBox : CheckBox
    {
        public ControlCheckBox()
        {
            _check = AxisCheck.Instance;
            CheckedChanged += OnCheckedChanged;
        }

        private readonly AxisCheck _check;
        private void OnCheckedChanged(object sender, EventArgs e)
        {
            if (Checked)
                _check.Check(this);
        }
    }
}