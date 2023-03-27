using UnityEditor;
using UnityEngine;

namespace GoogleSheetsForUnity
{

    /// <summary>
    /// We use ScriptableObjects for connection data, so the app can have multiple connection presets, that can be interchangeable.
    /// </summary>
    [CreateAssetMenu(fileName = "ConnectionData", menuName = "Google Sheets For Unity/Connection Data Asset", order = 0)]
    public class ConnectionData : ScriptableObject
    {
        [Tooltip("URL of the webapp deployed on Google Drive.")]
        public string webServiceUrl = "";
        [Tooltip("The Id of the spreadsheet to be used for the objects & tables operations. If more than one spreadsheet are required, different connections can be made, or the spreadsheet Ids manually stated on the requests forms.")]
        public string spreadsheetId = "";
        [Tooltip("The password to use on the individual queries. Will not be used if the connection is set to useSessionContext.")]
        public string servicePassword = "";
        [Tooltip("The time in seconds before declaring the connection as timed out.")]
        public float timeOutLimit = 15f;
        [Tooltip("WWW Request type: true will use POST and false will use GET.")]
        public bool usePOST = true;
        [Tooltip("When checked, instead of stateless independent queries that use a password, the queries will depend on the session context for authorization. See Login Session Example for more info on how to use sessions.")]
        public bool useSessionContext = false;
        [Tooltip("The session duration time, in seconds. The minimum recommended is 7200 seconds (2 hours) and the maximum possible is 21600 seconds (6 hours).")]
        public int sessionTime = 7200;
        [Tooltip("Username for the login session.")]
        public string sessionUser;
        [Tooltip("Password for the login session.")]
        public string sessionPassword;
        [Tooltip("The name of the table where the credentials data is holded.")]
        public string tableName;


#if UNITY_EDITOR
        [ContextMenu("Login to WebApp")]
        public void Login()
        {
            if (useSessionContext)
                Drive.Login(sessionUser, sessionPassword, tableName, false);
            else
                Debug.Log("Connection Data is not set to use session context, login is not needed for stateless queries.");
        }
#endif
    }
}
