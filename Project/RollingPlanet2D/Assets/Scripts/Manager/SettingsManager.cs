using LitJson;
using Utility;
using System.IO;
using UnityEngine;
using System.Collections.Generic;

namespace Manager
{
    public class SettingsManager : Manager
    {
        private readonly string muteBGM = "muteBGM";
        private readonly string muteEffect = "muteEffect";

        private string settingsPath;
        // private string[] jsonKeyArray = { muteBGM, muteEffect };

        private void Awake()
        {
            settingsPath = Application.dataPath + "/Resources/Data/Settings.json";
        }

        // Load와 Save를 try - catch로 감싸기(IOException)
        public bool Load(out List<object> returnData)
        {
            returnData = null;
            if (File.Exists(settingsPath))
            {
                string data = File.ReadAllText(settingsPath);

                string editedString = EditJsonString(data);
                JsonData jsonData = JsonMapper.ToObject(editedString);
                // JsonData jsonData = new JsonReader(data).Read();
 
                string bgm = jsonData[0][muteBGM].ToString();
                string effect = jsonData[0][muteEffect].ToString();

                List<object> returnList = new List<object>();

                returnList.Add(bgm);
                returnList.Add(effect);

                returnData = returnList;
                return true;
            }
            else
            {
                Debug.Log("Settings.json file doesn't exist.");
                return false;
            }
        }

        public void Save(bool muteBGM, bool muteEffect)
        {
            Data.ModifyData data = new Data.ModifyData();
            data.MuteBGM = muteBGM;
            data.MuteEffect = muteEffect;

            List<Data.ModifyData> list = new List<Data.ModifyData>();
            list.Add(data);

            JsonData jsonData = JsonMapper.ToJson(list);
            jsonData.SetJsonType(JsonType.String);

            File.WriteAllText(settingsPath, jsonData.ToJson());
        }

        public string DataToJson(JsonData data)
        {
            return data.ToJson();
        }

        /// <summary>
        /// Edit json string because KeyNotFoundException
        /// cause jsonString's first end last " and string key's \
        /// </summary>
        private string EditJsonString(string jsonString)
        {
            string editedString = "";
            for (int i = 0; i < jsonString.Length; i++)
            {
                if (i == 0 || i == jsonString.Length)
                {
                    continue;
                }
                if (jsonString[i] == '\\')
                {
                    continue;
                }
                editedString += jsonString[i];
            }
            return editedString;
        }
    }
}
