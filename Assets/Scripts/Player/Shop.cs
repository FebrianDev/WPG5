using System;
using Authentication;
using Firebase.Database;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private Text coinText;
    private string id = null;

    [SerializeField] private GameObject buy, btnChar1, btnChar2;
    [SerializeField] private RawImage char2;

    private int coin;
    private int selected;

    void Start()
    {
        FirebaseDatabase.DefaultInstance
            .GetReference("Users")
            .ValueChanged += GetUser;
        
        selected = PlayerPrefs.GetInt(Constant.KEY_SELECTED, 0);
        CharacterSelected();
    }

    private void Update()
    {
        selected = PlayerPrefs.GetInt(Constant.KEY_SELECTED, 0);
        Debug.Log(selected);
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
                var name = childSnapshot.Child("name").Value.ToString();

                if (name == PlayerPrefs.GetString(Constant.KEY_NAME))
                {
                    id = childSnapshot.Child("id").Value.ToString();
                    coin = Convert.ToUInt16(childSnapshot.Child("coin").Value.ToString());
                    coinText.text = $"$ {coin}";

                    if (childSnapshot.Child("character2").Value.Equals(true))
                    {
                        char2.color = Color.white;
                        buy.SetActive(false);
                    }
                    else
                    {
                        char2.color = Color.black;
                        buy.SetActive(true);
                    }
                }

                Debug.Log(PlayerPrefs.GetString(Constant.KEY_NAME));
            }
        }
    }

    public void BuyCharacter()
    {
        Debug.Log(id);
        if (id != null && coin >= 100)
        {
            var reference = FirebaseDatabase.DefaultInstance.GetReference("Users").Child(id);
            reference.GetValueAsync().ContinueWith(task =>
            {
                if (task.IsCompleted)
                {
                    reference.Child("character2").SetValueAsync(true);
                    reference.Child("coin").SetValueAsync(coin - 100);
                }
            });
        }
    }

    public void CharacterSelected()
    {
        if (selected == 0)
        {
            btnChar1.GetComponent<Button>().enabled = false;
            btnChar2.GetComponent<Button>().enabled = true;
            
            btnChar2.GetComponent<Image>().color = new Color32(45,63,221,255);
            btnChar2.GetComponentInChildren<Text>().text = "Select";
            btnChar2.GetComponentInChildren<Text>().color = Color.white;
            
            btnChar1.GetComponent<Image>().color = Color.white;
            btnChar1.GetComponentInChildren<Text>().text = "Selected";
            btnChar1.GetComponentInChildren<Text>().color = new Color32(45,63,221,255);
            PlayerPrefs.SetInt(Constant.KEY_SELECTED, 1);
        }
        else if(selected == 1)
        {
            btnChar1.GetComponent<Button>().enabled = true;
            btnChar2.GetComponent<Button>().enabled = false;
           
            btnChar1.GetComponent<Image>().color = new Color32(45,63,221,255);
            btnChar1.GetComponentInChildren<Text>().text = "Select";
            btnChar1.GetComponentInChildren<Text>().color = Color.white;
            btnChar2.GetComponent<Image>().color = Color.white;
            btnChar2.GetComponentInChildren<Text>().text = "Selected";
            btnChar2.GetComponentInChildren<Text>().color = new Color32(45,63,221,255);
            PlayerPrefs.SetInt(Constant.KEY_SELECTED, 0);
        }
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
}