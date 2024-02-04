using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;
using Pyramid.Classes.Controls;

namespace Pyramid.Classes.JsonClasses
{
    public class JsonDataActivity
    {
        private ProgramSettings LoadData()
        {
            if (!File.Exists("settings.json")) 
                return null;
            string json = File.ReadAllText("settings.json");
            ProgramSettings settings = JsonConvert.DeserializeObject<ProgramSettings>(json);

            return settings;
        }

        private void SaveData(string json)
        {
            StreamWriter streamWriter = File.CreateText("settings.json");
            streamWriter.WriteLine(json);
            streamWriter.Close();
        }

        public void CloseApp(ControlTextBox controlTextBox, int trackBarValue, Color pictureBoxBackColor, TableLayoutPanel tableLayoutPanel)
        {
            if (controlTextBox.Text == @"Введите кол-во пирамид" || !int.TryParse(controlTextBox.Text, out int num))
                return;
            ProgramSettings settings = new ProgramSettings
            {
                PyramidsNumber = num,
                PyramidSpeed = trackBarValue,
                PictureBoxColor = pictureBoxBackColor,
                CheckBoxList = AxisCheck.Instance.GetActualCheckBoxList(tableLayoutPanel)
            };
            string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
            SaveData(json);
        }

        public void FirstStart(ControlTextBox controlTextBox, TrackBar trackBar, PictureBox pictureBox, TableLayoutPanel tableLayoutPanel)
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
        }
    }
}