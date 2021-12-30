
using Authentication;
using UnityEngine;
using UnityEngine.UI;
using MLAPI;

public class PlayerName : NetworkedBehaviour
{
    [SerializeField] private Text name;

    void Start()
    {
        if (IsLocalPlayer)
        {
            int _stringLength = 8;
            string randomString = "";
            string[] characters = new string[]
            {
                "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u",
                "v", "w", "x", "y", "z"
            };
            for (int i = 0; i <= _stringLength; i++)
            {
                randomString = randomString + characters[Random.Range(0, characters.Length)];
            }

            name.text = PlayerPrefs.GetString(Constant.KEY_NAME);
        }
    }
}