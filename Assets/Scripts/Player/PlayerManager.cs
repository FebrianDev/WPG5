using System.Collections;
using scripts;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private PlayerMovement pm;

    GameObject[] players;

    private void Start()
    {
        pm = GetComponent<PlayerMovement>();
        pm.enabled = false;
    }

    private void Update()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length == 2)
        {
            StartCoroutine(nameof(StartGame));
        }
        
        Debug.Log(players.Length);
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(3);
        pm.enabled = true;

        this.GetComponent<PlayerManager>().enabled = false;
    }
}