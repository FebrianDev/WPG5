using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashLoader : MonoBehaviour
{
    public int registered; 
    public float inputSplashDuration;
    float splashDuration;

    private void Start()
    {
        registered = PlayerPrefs.GetInt("isRegistered", 0);
        transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
        splashDuration = inputSplashDuration;
    }

    void Update()
    {
        //PlayerPrefs.Save();
        transform.localScale += new Vector3(0.025f, 0.025f, 0.025f) * Time.deltaTime;
        splashDuration -= Time.deltaTime;

        if(splashDuration < 0)
        {
            if (registered == 1)
            {
                SceneManager.LoadScene("MainMenu");
            }

            else
            {
                SceneManager.LoadScene("Login");
            }
        }
        
    }
}
