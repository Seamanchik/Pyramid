using System.Collections.Generic;
using System.Windows.Forms;

namespace Pyramid
{
    public class AxisCheck
    {
        private static AxisCheck _instance;

        private AxisCheck() => _checkArray = new List<CheckBox>();

        public static AxisCheck Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new AxisCheck();
                return _instance;
            }
        }

        private List<CheckBox> _checkArray;

        public void Check(CheckBox check)
        {
            if (!_checkArray.Contains(check))
                _checkArray.Add(check);
        }

        public void ActiveCheck(ChangePyramid pyramids)
        {
            if (_checkArray != null)
            {
                foreach (var checkBox in _checkArray)
                {
                    if (checkBox.Checked)
                    {
                        switch (checkBox.TabIndex)
                        {
                            case 0:
                                pyramids.ChangePyramids(new RotatebleX(), 0.02f);
                                break;
                            case 1:
                                pyramids.ChangePyramids(new RotatebleY(), 0.02f);
                                break;
                            case 2:
                                pyramids.ChangePyramids(new RotatebleZ(), 0.02f);
                                break;
                        }
                    }
                }
            }
        }

        public void Delete()
        {
            foreach (var checkbox in _checkArray)
            {
                checkbox.Checked = false;
            }
            _checkArray = new List<CheckBox>();
        }
    }
}