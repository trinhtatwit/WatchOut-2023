using UnityEngine;
using System;
using System.Collections;

namespace GoogleSheetsForUnity
{
    public class ImagesExample : MonoBehaviour
    {
        private Rect _texturePos = new Rect(400, 60, 256, 256);
        private Texture2D _text2d;
        private string _cloudFileID = "";
        private string _imagePath = "/Google Sheets For Unity/Examples/Images Example/UFO.png";

        private void OnEnable()
        {
            // Suscribe for catching cloud responses.
            Drive.responseCallback += HandleDriveResponse;
        }

        private void OnDisable()
        {
            Drive.responseCallback -= HandleDriveResponse;
        }

        private void OnGUI()
        {
            GUILayout.BeginArea(new Rect(10, 10, 600, 1000));
            GUILayout.BeginHorizontal();
            GUILayout.Space(10f);
            GUILayout.BeginVertical();

            GUILayout.Label("This example will load an image into a texture from either a local file or a screenshot," +
                " and enable user to upload the image to Drive as JPG or PNG file, as well as later retrieve the image back by its reported Drive id.",
                GUILayout.MaxWidth(600f));

            GUILayout.Space(10f);

            if (GUILayout.Button("Load PNG from local file", GUILayout.MinHeight(20f), GUILayout.MaxWidth(200f)))
            {
                LoadPNGFromFile(Application.dataPath + _imagePath);
            }

            if (GUILayout.Button("Take Screenshot", GUILayout.MinHeight(20f), GUILayout.MaxWidth(200f)))
            {
                TakeScreenshot();
            }

            GUILayout.Space(10f);

            if (GUILayout.Button("Save Image to Cloud as PNG", GUILayout.MinHeight(20f), GUILayout.MaxWidth(200f)))
            {
                if (_text2d == null)
                    Debug.Log("Cannot upload image: please load a file or take a screenshot first.");
                else
                    Drive.CreateImageFile(_text2d, "TextureFile", true);
            }

            if (GUILayout.Button("Save Image to Cloud as JPG", GUILayout.MinHeight(20f), GUILayout.MaxWidth(200f)))
            {
                if (_text2d == null)
                    Debug.Log("Cannot upload image: please load a file or take a screenshot first.");
                else
                    Drive.CreateImageFile(_text2d, "TextureFile", false, 90, null, null, true);
            }

            GUILayout.Space(10f);

            GUILayout.Label("Google Drive file id:");
            _cloudFileID = GUILayout.TextField(_cloudFileID, GUILayout.MaxWidth(200f));
            if (GUILayout.Button("Get From Cloud", GUILayout.MinHeight(20f), GUILayout.MaxWidth(200f)))
            {
                if (string.IsNullOrEmpty(_cloudFileID))
                    Debug.Log("Cannot retrieve a file: please provide an id for the image file on Google Drive.");
                else
                    Drive.GetImageFile(_cloudFileID);
            }

            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            GUILayout.EndArea();

            if (_text2d != null)
                GUI.DrawTexture(_texturePos, _text2d);
        }

        private void TakeScreenshot()
        {
#if UNITY_WEBGL
            StartCoroutine(TakeScreenshotManually());
#else
            _text2d = ScreenCapture.CaptureScreenshotAsTexture();
#endif
        }

        private IEnumerator TakeScreenshotManually()
        {
            yield return new WaitForEndOfFrame();

            _text2d = new Texture2D(Screen.width, Screen.height);
            _text2d.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            _text2d.Apply();
        }

        private void LoadPNGFromFile(string filePath)
        {
            byte[] fileData;
            if (System.IO.File.Exists(filePath))
            {
                fileData = System.IO.File.ReadAllBytes(filePath);
                _text2d = new Texture2D(2, 2);
                _text2d.LoadImage(fileData);
            }
        }

        // Processes the data received from the cloud.
        private void HandleDriveResponse(Drive.DataContainer dataContainer)
        {
            if (dataContainer.QueryType == Drive.QueryType.createImageFile)
            {
                Debug.LogFormat("Image file created with name {0} and id {1}, at folder {2}.", dataContainer.fileName, dataContainer.fileId, dataContainer.folderName);
            }

            if (dataContainer.QueryType == Drive.QueryType.getImageFile)
            {
                Debug.Log(dataContainer.msg);

                byte[] decodedBytes = Convert.FromBase64String(dataContainer.payload);
                Texture2D tex = new Texture2D(2, 2);
                tex.LoadImage(decodedBytes, false);
                _text2d = tex;
            }
        }
    }
}
