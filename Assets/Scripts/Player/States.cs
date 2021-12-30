using System;
using Firebase.Database;
using Firebase.Extensions;
using Items;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace scripts
{
    public class States : MonoBehaviour
    {
        [SerializeField] private GameObject name;

        void Start()
        {
            for (int i = 0; i < Finish.GetListWinner().Count; i++)
            {
                var go = Instantiate(name, new Vector3(0, 0, 0), Quaternion.identity);
                go.transform.parent = GameObject.Find("Canvas").transform;
                go.transform.localPosition = new Vector2(-110, 60 - (i * 50));
                go.transform.localScale = new Vector3(1, 1, 1);
                go.GetComponent<Text>().text = Finish.GetListWinner()[i];

                var i1 = i;
                FirebaseDatabase.DefaultInstance.GetReference("Users").GetValueAsync().ContinueWithOnMainThread(task =>
                {
                    if (task.IsFaulted)
                    {
                        // Handle the error...
                        Debug.Log(task.Exception);
                    }
                    else if (task.IsCompleted)
                    {
                        var snapshot = task.Result;

                        foreach (var child in snapshot.Children)
                        {
                            var name = child.Child("name").Value.ToString();
                            Debug.Log(name);
                            if (name == Finish.GetListWinner()[i1])
                            {
                                var id = child.Child("id").Value.ToString();
                                var coin = Convert.ToUInt32(child.Child("coin").Value.ToString());
                                var score = Convert.ToUInt32(child.Child("score").Value.ToString());
                                var reference = FirebaseDatabase.DefaultInstance.GetReference("Users").Child(id);
                                Debug.Log(id);
                                reference.GetValueAsync().ContinueWith(t =>
                                {
                                    if (t.IsCompleted)
                                    {
                                        Debug.Log("OKe");
                                        reference.Child("coin").SetValueAsync(coin + DataItems.coin);
                                        reference.Child("score").SetValueAsync(score + 100 / (i1 + 1));
                                    }
                                    else
                                    {
                                        Debug.Log("Err " + t.Exception);
                                    }
                                });
                            }
                        }

                        // Do something with snapshot...
                    }
                    else
                    {
                        Debug.Log("Erro");
                    }
                });


                Debug.Log("Finish " + i + 1 + " " + Finish.GetListWinner()[i]);
            }

            Destroy(GameObject.FindWithTag("Finish"));
        }

        public void BackToMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

}