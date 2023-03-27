using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GoogleSheetsForUnity
{
    public class FilesExample : MonoBehaviour
    {
        private string _filePath;
        private string _cloudFileID;
        private string _binaryFileName = "PlayerInfo.bin";
        private string _textFileName = "PlayerInfo.txt";
        private PlayerInfo _playerData;

        // Simple struct for the example.
        [System.Serializable]
        public struct PlayerInfo
        {
            public string name;
            public int level;
            public float health;
            public string role;
        }

        private void OnEnable()
        {
            // Subscribe for catching cloud responses.
            Drive.responseCallback += HandleDriveResponse;
        }

        private void OnDisable()
        {
            Drive.responseCallback -= HandleDriveResponse;
        }

        private void Start()
        {
            _filePath = Application.dataPath + "/Google Sheets For Unity/Examples/Files Example/";
            _cloudFileID = "";
            _playerData = new PlayerInfo()
            {
                name = "Mithrandir",
                level = 99,
                health = 98.6f,
                role = "Wizzard",
            };
        }

        private void OnGUI()
        {
            GUILayout.Space(10f);
            GUILayout.BeginHorizontal();
            GUILayout.Space(10f);
            GUILayout.Label("This example will show how to create or retrieve files from Google Drive." +
                "This example does not cover further posibilities of the API such as deleting files, and creating or deleting folders.", GUILayout.MaxWidth(600f));
            GUILayout.EndHorizontal();
            GUILayout.Space(10f);
            GUILayout.BeginVertical("Example 'Player' Object Data:", GUI.skin.box, GUILayout.MaxWidth(230));
            GUILayout.Space(20f);
            
            GUILayout.BeginHorizontal();
            GUILayout.Space(10f);
            GUILayout.Label("Player Name:", GUILayout.MaxWidth(100f));
            _playerData.name = GUILayout.TextField(_playerData.name, GUILayout.MaxWidth(100f));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Space(10f);
            GUILayout.Label("Player Level:", GUILayout.MaxWidth(100f));
            _playerData.level = int.Parse(GUILayout.TextField(_playerData.level.ToString(), GUILayout.MaxWidth(100f)));             
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Space(10f);
            GUILayout.Label("Player Health:", GUILayout.MaxWidth(100f));
            _playerData.health = float.Parse(GUILayout.TextField(_playerData.health.ToString(), GUILayout.MaxWidth(100f)));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Space(10f);
            GUILayout.Label("Player Role:", GUILayout.MaxWidth(100f));
            _playerData.role = GUILayout.TextField(_playerData.role, GUILayout.MaxWidth(100f));
            GUILayout.EndHorizontal();
            GUILayout.Space(5f);
            GUILayout.EndVertical();

            GUILayout.BeginArea(new Rect(0, 200, 600, 1000));
            GUILayout.BeginHorizontal();
            GUILayout.Space(10f);
            GUILayout.BeginVertical();

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Save data to local binary File", GUILayout.MinHeight(20f), GUILayout.MaxWidth(220f)))
            {
                SaveLocalBinaryFile(_filePath + _binaryFileName);
            }
            
            if (GUILayout.Button("Save data to local text File", GUILayout.MinHeight(20f), GUILayout.MaxWidth(220f)))
            {
                SaveLocalTextFile(_filePath + _textFileName);
            }

            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Load data from local Binary File", GUILayout.MinHeight(20f), GUILayout.MaxWidth(220f)))
            {
                LoadLocalBinaryFile(_filePath + _binaryFileName);
            }

            if (GUILayout.Button("Load data from local Text File", GUILayout.MinHeight(20f), GUILayout.MaxWidth(220f)))
            {
                LoadLocalTextFile(_filePath + _textFileName);
            }

            GUILayout.EndHorizontal();

            GUILayout.Space(10f);

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Save Data to Cloud as Binary File", GUILayout.MaxWidth(220f)))
            {
                UploadBinaryFile();
            }

            if (GUILayout.Button("Save Data to Cloud as Text File", GUILayout.MaxWidth(220f)))
            {
                UploadTextFile();
            }

            GUILayout.EndHorizontal();
            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Google Drive file id:", GUILayout.MaxWidth(120f));
            _cloudFileID = GUILayout.TextField(_cloudFileID, GUILayout.MaxWidth(220f));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Get Binary File From Cloud", GUILayout.MinHeight(20f), GUILayout.MaxWidth(220f)))
            {
                if (string.IsNullOrEmpty(_cloudFileID))                
                    Debug.Log("Cannot retrieve a file: please provide an id for the file on Google Drive.");                
                else
                    Drive.GetBinaryFile(_cloudFileID);
            }
            if (GUILayout.Button("Get Text File From Cloud", GUILayout.MinHeight(20f), GUILayout.MaxWidth(220f)))
            {
                if (string.IsNullOrEmpty(_cloudFileID))
                    Debug.Log("Cannot retrieve a file: please provide an id for the file on Google Drive.");
                else
                    Drive.GetTextFile(_cloudFileID);
            }

            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }
        
        private void SaveLocalTextFile(string filePath)
        {
            string jsonPlayer = JsonUtility.ToJson(_playerData);
            File.WriteAllText(filePath, jsonPlayer, System.Text.Encoding.UTF8);

            Debug.Log("Data saved locally as a text file.");
        }

        private void LoadLocalTextFile(string filePath)
        {
            if (!File.Exists(filePath))
                return;

            string fileData = File.ReadAllText(filePath, System.Text.Encoding.UTF8);
            _playerData = JsonUtility.FromJson<PlayerInfo>(fileData);

            OutputData("local text file");
        }

        private void SaveLocalBinaryFile(string filePath)
        {
            FileStream fileStream = File.Open(filePath, FileMode.CreateNew);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(fileStream, _playerData);
            fileStream.Flush();

            Debug.Log("Data saved locally as a binary file.");
        }

        private void LoadLocalBinaryFile(string filePath)
        {
            if (!File.Exists(filePath))
                return;

            FileStream fileStream = File.Open(filePath, FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            _playerData = (PlayerInfo) binaryFormatter.Deserialize(fileStream);

            OutputData("local binary file");
        }
        
        private void HandleDriveResponse(Drive.DataContainer dataContainer)
        {
            if (dataContainer.QueryType == Drive.QueryType.createBinaryFile)
            {
                Debug.LogFormat("Binary file created with name {0} and id {1}, at folder {2}.", dataContainer.fileName, dataContainer.fileId, dataContainer.folderName);
            }

            if (dataContainer.QueryType == Drive.QueryType.createTextFile)
            {
                Debug.LogFormat("Text file created with name {0} and id {1}, at folder {2}.", dataContainer.fileName, dataContainer.fileId, dataContainer.folderName);
            }

            if (dataContainer.QueryType == Drive.QueryType.getBinaryFile)
            {
                Debug.Log(dataContainer.msg);

                byte[] decodedBytes = Convert.FromBase64String(dataContainer.payload);
                MemoryStream memStream = new MemoryStream(decodedBytes);
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                _playerData = (PlayerInfo)binaryFormatter.Deserialize(memStream);
                OutputData("Drive binary file");
            }

            if (dataContainer.QueryType == Drive.QueryType.getTextFile)
            {
                Debug.Log(dataContainer.msg);
                _playerData = JsonUtility.FromJson<PlayerInfo>(dataContainer.payload);
                OutputData("Drive text file");
            }
        }

        private void UploadBinaryFile()
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter format = new BinaryFormatter();
            format.Serialize(memStream, _playerData);

            Drive.CreateBinaryFile(memStream.ToArray(), _binaryFileName);
        }

        private void UploadTextFile()
        {
            string jsonPlayer = JsonUtility.ToJson(_playerData);

            Drive.CreateTextFile(jsonPlayer, _textFileName);
        }

        private void OutputData(string source)
        {
            Debug.Log("<color=yellow>Object data retrieved from " + source + ":\n</color>" +
                            "Name: " + _playerData.name + "\n" +
                            "Level: " + _playerData.level + "\n" +
                            "Health: " + _playerData.health + "\n" +
                            "Role: " + _playerData.role + "\n");
        }
    }
}
