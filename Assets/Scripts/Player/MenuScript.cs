using Items;
using MLAPI;
using UnityEngine;
using UnityEngine.UI;
public class MenuScript : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    [SerializeField] private Text shoe;

    private void Start()
    {
        
    }

    private void Update()
    {
        shoe.text = "Shoe " + DataItems.shoe;
    }

    public void Host()
    {
        panel.SetActive(false);
        NetworkingManager.Singleton.StartHost();
    }

    public void Join()
    {
        panel.SetActive(false);
        NetworkingManager.Singleton.StartClient();
    }
}