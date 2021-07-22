using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace twclient
{
    class ListSettings
    {
        public const string HASH_FILE = "\\hash.ini";
        public const string SEARCH_FILE = "\\search.ini";
        public const string USER_FILE = "\\user.ini";

        private const string splitChar = "\t";

        //
        string listSettingDirectory;
        string settingFileName;

        public ListSettings(string fileName)
        {
            listSettingDirectory = Application.StartupPath;
            settingFileName = listSettingDirectory + fileName;
        }

        public List<List<string>> Read()
        {
            string[] value;
            List<List<string>> values = new List<List<string>>();

            try
            {
                StreamReader sr = new StreamReader(settingFileName, Encoding.UTF8);

                while (true)
                {
                    string line = sr.ReadLine();
                    if (line == null) break;

                    value = line.Split(splitChar.ToCharArray());
                    values.Add(value.ToList<string>());
                }
                sr.Close();
            }
            catch (System.IO.FileNotFoundException)
            {
                values.Clear();
            }

            return values;
        }

        public void Write(List<List<string>> values)
        {
            try
            {
                StreamWriter sw = new StreamWriter(settingFileName, false, Encoding.UTF8);

                foreach (var value in values)
                {
                    int i;
                    for (i = 0; i < value.Count - 1; i++)
                    { 
                        sw.Write(value[i] + splitChar);
                    }
                    sw.WriteLine(value[i]);
                }
                sw.Close();
            }
            catch (System.IO.FileNotFoundException)
            {

            }
        }

    }
}
