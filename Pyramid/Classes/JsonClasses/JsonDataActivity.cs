using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Resources;
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
            if (controlTextBox.Text == $@"{new ResourceManager(typeof(Form1)).GetString("controltTextBox1.Text")}" ||
                !int.TryParse(controlTextBox.Text, out int num))
                return;
            ProgramSettings settings = new ProgramSettings
            {
                PyramidsNumber = num,
                PyramidSpeed = trackBarValue,
                PictureBoxColor = pictureBoxBackColor,
                CheckBoxList = AxisCheck.Instance.GetActualCheckBoxList(tableLayoutPanel),
            };
            Settings.Default.JsonData = JsonConvert.SerializeObject(settings, Formatting.Indented);
            Settings.Default.Save();
            Settings.Default.Language = comboBox.SelectedValue.ToString();
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

                foreach (var checkBox in tableLayoutPanel.Controls.OfType<ControlCheckBox>()) 
                    checkBox.Checked = settings.CheckBoxList.Any(cb => cb.Text == checkBox.Text && cb.Checked);
            }
            else
                controlTextBox.Text = @"1";

            comboBox.DataSource = new[]
            {
                CultureInfo.GetCultureInfo("ru-RU"),
                CultureInfo.GetCultureInfo("en-EN")
            };
            comboBox.ValueMember = "Name";
            if (!string.IsNullOrEmpty(Settings.Default.Language))
                comboBox.SelectedValue = Settings.Default.Language;
        }
    }
}