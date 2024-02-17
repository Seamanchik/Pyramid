using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;
using Pyramid.Classes.Controls;
using Pyramid.Properties;

namespace Pyramid.Classes.JsonClasses
{
    public class JsonDataActivity
    {
        private ProgramSettings LoadData()
        {
            ProgramSettings settings = JsonConvert.DeserializeObject<ProgramSettings>(Settings.Default.JsonData);
            return settings;
        }

        public void CloseApp(ControlTextBox controlTextBox, int trackBarValue, Color pictureBoxBackColor,
            TableLayoutPanel tableLayoutPanel, ComboBox comboBox)
        {
            if (controlTextBox.Text == @"Введите кол-во пирамид" || !int.TryParse(controlTextBox.Text, out int num))
                return;
            ProgramSettings settings = new ProgramSettings
            {
                PyramidsNumber = num,
                PyramidSpeed = trackBarValue,
                PictureBoxColor = pictureBoxBackColor,
                CheckBoxList = AxisCheck.Instance.GetActualCheckBoxList(tableLayoutPanel),
                Language = comboBox.SelectedItem
            };
            Settings.Default.JsonData = JsonConvert.SerializeObject(settings, Formatting.Indented);
            Settings.Default.Save();
        }


        public void FirstStart(ControlTextBox controlTextBox, TrackBar trackBar, PictureBox pictureBox,
            TableLayoutPanel tableLayoutPanel, ComboBox comboBox)
        {
            var settings = LoadData();
            if (settings != null)
            {
                controlTextBox.Text = settings.PyramidsNumber.ToString();
                trackBar.Value = settings.PyramidSpeed;
                pictureBox.BackColor = settings.PictureBoxColor;
                comboBox.SelectedItem = settings.Language;
                
                Thread.CurrentThread.CurrentUICulture =
                    CultureInfo.GetCultureInfo((string)comboBox.SelectedItem == "EN" ? "en-EN" : "ru-RU");
                
                foreach (var checkBox in tableLayoutPanel.Controls.OfType<ControlCheckBox>())
                    checkBox.Checked = settings.CheckBoxList.Any(cb => cb.Text == checkBox.Text && cb.Checked);
            }
            else
                controlTextBox.Text = @"1";
        }
    }
}