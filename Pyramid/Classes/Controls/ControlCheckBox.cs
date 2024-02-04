using System;
using System.Windows.Forms;

namespace Pyramid.Classes.Controls
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
    
    public class CheckBoxInfo
    {
        public string Text { get; set; }
        public bool Checked { get; set; }
    }
}