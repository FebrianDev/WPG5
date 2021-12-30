using UnityEngine;
using UnityEngine.SceneManagement;

public class BgMusic : MonoBehaviour
{
    private AudioSource audioSource;
    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("AudioManager");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.Play();
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            print("OKE");
            Destroy(GameObject.FindGameObjectWithTag("AudioManager"));
        }
    }
}