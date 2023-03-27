using UnityEngine;

namespace GoogleSheetsForUnity
{
    public class LoginSessionExample : MonoBehaviour
    {
        public Texture2D clock;
        public string credentialsTableName = "Credentials";

        private string _usernameText;
        private string _passwdText = "";
        private string _mailText;
        private string _lastLogon;
        private string _signupDate;
        private bool _showRegistrationForm;
        private bool _isLoggedIn;
        private bool _waitingForServerResponse;

        private void OnEnable()
        {
            // Suscribe for catching cloud responses.
            Drive.responseCallback += HandleDriveResponse;
            Drive.errorResponseCallback += HandleErrorResponse;
        }

        private void OnDisable()
        {
            Drive.responseCallback -= HandleDriveResponse;
            Drive.errorResponseCallback -= HandleErrorResponse;
        }

        private void OnGUI()
        {
            GUILayout.BeginArea(new Rect(10, 10, 600, 1000));
            GUILayout.BeginHorizontal();
            GUILayout.Space(10f);
            GUILayout.BeginVertical();

            GUILayout.Label("This example shows how to use the custom login system, which purpuse is to manage a session context" +
                " in case its needed instead of stateless individual queries with password." +
                "\n\nNote that upon login, 'Use Session Context' on the ConnectionData asset is set to true.", GUILayout.MaxWidth(600f));

            GUILayout.Space(10f);

            GUILayout.Label("If you dont have a table created on the spreadsheet for this purpose, use this button:");
            if (GUILayout.Button("Create Login Data Table", GUILayout.MinHeight(20f), GUILayout.MaxWidth(200f)))
            {
                Drive.CreateCredentialsTable(credentialsTableName);
                _waitingForServerResponse = true;
            }

            GUILayout.Space(20f);
            GUILayout.BeginHorizontal();

            GUILayout.BeginVertical("Login", GUI.skin.box, GUILayout.MaxWidth(230));
            GUILayout.Space(20f);
            GUILayout.BeginHorizontal();
            GUILayout.Space(10f);
            GUILayout.BeginVertical();
            GUILayout.Label("Username:");
            _usernameText = GUILayout.TextField(_usernameText, GUILayout.MaxWidth(200f));
            GUILayout.Label("Password:");
            _passwdText = GUILayout.PasswordField(_passwdText, "•"[0], 25, GUILayout.MaxWidth(200f));

            if (GUILayout.Button(_isLoggedIn ? "Logout" : "Login", GUILayout.MinHeight(20f), GUILayout.MaxWidth(200f)))
            {
                if (_isLoggedIn)
                {
                    Drive.Logout();
                    _waitingForServerResponse = true;
                }
                else
                {
                    Drive.Login(_usernameText, _passwdText, credentialsTableName);
                    _waitingForServerResponse = true;
                }
            }
            GUILayout.Space(10f);
            GUILayout.Label("Last Session: " + _lastLogon);
            GUILayout.Label("Member since: " + _signupDate);

            GUILayout.Space(5f);
            GUILayout.Label("Dont have an account yet?", GUILayout.Width(160));
            if (GUILayout.Button("Register", GUILayout.Width(90)))
                _showRegistrationForm = true;
            GUILayout.Space(10f);

            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

            if (_showRegistrationForm)
            {
                GUILayout.BeginVertical("Register", GUI.skin.box, GUILayout.MaxWidth(230));
                GUILayout.Space(20f);
                GUILayout.BeginHorizontal();
                GUILayout.Space(10f);
                GUILayout.BeginVertical();
                GUILayout.Label("Username:");
                _usernameText = GUILayout.TextField(_usernameText, GUILayout.MaxWidth(200f));
                GUILayout.Label("Password:");
                _passwdText = GUILayout.PasswordField(_passwdText, "•"[0], 25, GUILayout.MaxWidth(200f));
                GUILayout.Label("Email:");
                _mailText = GUILayout.TextField(_mailText, GUILayout.MaxWidth(200f));
                if (GUILayout.Button("Signup", GUILayout.MinHeight(20f), GUILayout.MaxWidth(200f)))
                {
                    Drive.Signup(_usernameText, _mailText, _passwdText, "someId", credentialsTableName);
                    _waitingForServerResponse = true;
                }
                GUILayout.Space(10f);
                GUILayout.EndVertical();
                GUILayout.EndHorizontal();
                GUILayout.EndVertical();
            }

            GUILayout.EndHorizontal();

            if (_waitingForServerResponse)
            {
                GUILayout.Space(10f);
                GUILayout.Label(clock);
            }

            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }

        // Processes the data received from the cloud.
        private void HandleDriveResponse(Drive.DataContainer dataContainer)
        {
            if (dataContainer.QueryType == Drive.QueryType.createTable && dataContainer.objType == "Credentials")
            {
                _waitingForServerResponse = false;
                Debug.Log(dataContainer.msg);
            }

            if (dataContainer.QueryType == Drive.QueryType.signup)
            {
                _waitingForServerResponse = false;
                Debug.Log(dataContainer.msg);
            }

            if (dataContainer.QueryType == Drive.QueryType.login)
            {
                _waitingForServerResponse = false;
                _isLoggedIn = true;

                _lastLogon = Drive.SessionContext.lastLogon;
                _signupDate = Drive.SessionContext.registrationDate;

                Debug.Log(dataContainer.msg + "\n" + Drive.SessionContext);
            }

            if (dataContainer.QueryType == Drive.QueryType.logout)
            {
                _waitingForServerResponse = false;
                _isLoggedIn = false;

                Debug.Log(dataContainer.msg);
            }
        }

        private void HandleErrorResponse(string errorMsg)
        {
            _waitingForServerResponse = false;
        }
    }
}
