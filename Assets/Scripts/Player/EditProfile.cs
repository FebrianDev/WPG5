
using Authentication;
using Firebase.Database;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace scripts
{

    public class EditProfile : MonoBehaviour
    {
        private new string name;
        private string password, id;

        [SerializeField] private Text message;

        [Header("Input Field")] [SerializeField]
        private InputField inputName;

        [SerializeField] private InputField inputPassword;
        [SerializeField] private InputField inputScore;
        [SerializeField] private InputField inputCoin;

        [Header("Error Field")] [SerializeField]
        private Text errName;

        [SerializeField] private Text errPwd;


        void Start()
        {
            FirebaseDatabase.DefaultInstance
                .GetReference("Users")
                .ValueChanged += GetUser;
        }

        private void GetUser(object sender2, ValueChangedEventArgs e2)
        {
            if (e2.DatabaseError != null)
            {
                Debug.LogError(e2.DatabaseError.Message);
            }

            if (e2.Snapshot != null && e2.Snapshot.ChildrenCount > 0)
            {
                foreach (var childSnapshot in e2.Snapshot.Children)
                {
                    var myName = childSnapshot.Child("name").Value.ToString();

                    if (myName == PlayerPrefs.GetString(Constant.KEY_NAME))
                    {
                        id = childSnapshot.Child("id").Value.ToString();
                        name = childSnapshot.Child("name").Value.ToString();
                        password = childSnapshot.Child("password").Value.ToString();
                        inputName.text = name;
                        inputPassword.text = password;
                        inputScore.text = childSnapshot.Child("score").Value.ToString();
                        inputCoin.text = childSnapshot.Child("coin").Value.ToString();
                    }

                    Debug.Log(PlayerPrefs.GetString(Constant.KEY_NAME));
                }
            }
        }

        public void UpdateData()
        {
            if (NameCheck() && PwdCheck())
            {
                var reference = FirebaseDatabase.DefaultInstance.GetReference("Users").Child(id);
                reference.Child("name").SetValueAsync(inputName.text);
                reference.Child("password").SetValueAsync(inputPassword.text);
                PlayerPrefs.DeleteKey(Constant.KEY_NAME);
                PlayerPrefs.SetString(Constant.KEY_NAME, inputName.text);

                var myMessage = "Success";
                message.text = myMessage;
                print(myMessage);
            }
            else
            {
                var myMessage = "Gagal!";
                message.text = myMessage;
            }
        }

        private bool PwdCheck()
        {
            if (inputPassword != null && inputPassword.text != "")
            {
                errPwd.text = "";
                return true;
            }

            var message = "Password Harus Diisi";
            errPwd.text = message;
            print(message);

            return false;
        }

        private bool NameCheck()
        {
            if (inputName != null && inputName.text != "" && name != inputName.text)
            {
                errName.text = "";
                return true;
            }

            else if (name == inputName.text)
            {
                var message = "Username Sudah Ada!";
                print(message);
                errName.text = message;
                return false;
            }
            else
            {
                var message = "Username Harus Diisi!";
                print(message);
                errName.text = message;
                return false;
            }
        }

        public void Back()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void Logout()
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene("Login");
        }
    }
}