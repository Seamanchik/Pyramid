using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Pyramid.Classes.PyramidClasses;
using Pyramid.Classes.RotateClasses;

namespace Pyramid.Classes.Controls
{
    public class AxisCheck
    {
        private static AxisCheck _instance;

        private AxisCheck() => _checkList = new List<CheckBox>();

        public static AxisCheck Instance => _instance ?? (_instance = new AxisCheck());

        private readonly List<CheckBox> _checkList;

        public void Check(CheckBox check)
        {
            if (!_checkList.Contains(check))
                _checkList.Add(check);
        }

        public void ActiveCheck(RotatePyramid pyramids, int num)
        {
            if (_checkList == null)
                return;
            foreach (var checkBox in _checkList.Where(checkBox => checkBox.Checked))
            {
                switch (checkBox.TabIndex)
                {
                    case 0:
                        pyramids.ChangePyramids(new RotatebleX(), 0.02f, num);
                        break;
                    case 1:
                        pyramids.ChangePyramids(new RotatebleY(), 0.02f, num);
                        break;
                    case 2:
                        pyramids.ChangePyramids(new RotatebleZ(), 0.02f, num);
                        break;
                }
            }
        }

        public void Delete()
        {
            foreach (var checkbox in _checkList)
                checkbox.Checked = false;
            _checkList.Clear();
        }

        public List<CheckBoxInfo> GetActualCheckBoxList(TableLayoutPanel tableLayoutPanel4)
        {
            var actualCheckBoxList = new List<CheckBoxInfo>();

            foreach (var checkBox in tableLayoutPanel4.Controls.OfType<ControlCheckBox>())
            {
                actualCheckBoxList.Add(new CheckBoxInfo { Text = checkBox.Text, Checked = checkBox.Checked });
            }

            return actualCheckBoxList;
        }
    }
}