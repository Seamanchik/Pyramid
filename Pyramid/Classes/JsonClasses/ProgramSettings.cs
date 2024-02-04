using System.Collections.Generic;
using System.Drawing;
using Pyramid.Classes.Controls;

namespace Pyramid.Classes
{
    public class ProgramSettings
    {
        public int PyramidsNumber { get; set; }
        public int PyramidSpeed { get; set; }
        public Color PictureBoxColor { get; set; }
        public List<CheckBoxInfo> CheckBoxList { get; set; }
    }
}