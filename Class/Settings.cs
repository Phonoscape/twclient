using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace twclient
{
    class Settings
    {
        //
        string settingFileName;
        Hashtable paramValue;

        // 設定名
        public const string PARAM_TOKEN               = "Token";
        public const string PARAM_TOKEN_SECRET = "Taken_Secret";
        public const string PARAM_CONSUMER_KEY = "Consumer_Key";
        public const string PARAM_CONSUMER_SECRET = "Consumer_Secret";
        public const string PARAM_MAINFORM_X = "Mainform_X";
        public const string PARAM_MAINFORM_Y = "Mainform_Y";
        public const string PARAM_MAINFORM_W = "Mainform_W";
        public const string PARAM_MAINFORM_H = "Mainform_H";
        public const string PARAM_MAINFORM_SPLIT_UPDOWN = "Mainform_Split_UpDown";
        public const string PARAM_MAINFORM_SPLIT_UP_LEFTRIGHT = "Mainform_Split_Up_LeftRight";
        public const string PARAM_MAINFORM_SPLIT_DOWN_LEFTRIGHT = "Mainform_Split_Down_LeftRight";
        public const string PARAM_MAINFORM_TWEETLINE_ITEM_W = "Mainform_Tweetline_Item_W";

        public Settings()
        {
            string exeFile = Application.ExecutablePath;
            settingFileName = exeFile + ".ini";
        }

        public void Open()
        {
            try
            {
                paramValue = new Hashtable();
                
                StreamReader sr = new StreamReader(settingFileName, Encoding.UTF8);

                while (true)
                {
                    string line = sr.ReadLine();
                    if (line == null) break;

                    string splitChar = "=";
                    string[] param = line.Split(splitChar.ToCharArray(), 2);
                    paramValue.Add(param[0], param[1]);
                }
                sr.Close();
            }
            catch (System.IO.FileNotFoundException)
            {
                paramValue.Clear();
            }

        }

        public void Flash()
        {
            try
            {
                StreamWriter sw = new StreamWriter(settingFileName, false, Encoding.UTF8);
                /*
                    foreach (string param in paramValue.Keys)
                    {
                        sw.WriteLine(param + "=" + paramValue[param]);
                    }
                */
                ArrayList keys = new ArrayList(paramValue.Keys);
                keys.Sort();

                foreach (string key in keys)
                {
                    sw.WriteLine(key + "=" + paramValue[key]);
                }
                sw.Close();
            }
            catch (System.IO.FileNotFoundException)
            {

            }
        }

        public string GetValue(string param)
        {
            string value = "";

            if (paramValue.ContainsKey(param))
            {
                value = (string)paramValue[param];
            }
            return value;
        }

        public int GetValueInt(string param)
        {
            int value = -1;

            if (paramValue.ContainsKey(param))
            {
                value = (int)int.Parse((string)paramValue[param]);
            }
            return value;
        }

        public int GetValueIntEx(string param)
        {
            int value = 0;

            if (paramValue.ContainsKey(param))
            {
                value = (int)int.Parse((string)paramValue[param]);
            }
            return value;
        }

        public void SetValue(string param,string value)
        {
            paramValue[param] = value;
        }
        public void SetValueInt(string param, int value)
        {
            paramValue[param] = value.ToString();
        }

    }
}
