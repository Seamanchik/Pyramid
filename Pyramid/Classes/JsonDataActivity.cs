using System.IO;
using Newtonsoft.Json;

namespace Pyramid.Classes
{
    public class JsonDataActivity
    {
        public ProgramSettings LoadData()
        {
            if (!File.Exists("settings.json")) 
                return null;
            string json = File.ReadAllText("settings.json");
            ProgramSettings settings = JsonConvert.DeserializeObject<ProgramSettings>(json);

            return settings;

        }

        public void SaveData(string json)
        {
            StreamWriter streamWriter = File.CreateText("settings.json");
            streamWriter.WriteLine(json);
            streamWriter.Close();
        }
    }
}