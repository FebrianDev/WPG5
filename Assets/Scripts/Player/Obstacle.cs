using System.Collections;
using System.Collections.Generic;
using Items;
using MLAPI;
using UnityEngine;

namespace scripts
{

    public class Obstacle : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!DataItems.shield)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    var player = other.gameObject.GetComponent<NetworkedObject>();
                    if (player.IsLocalPlayer && !DataItems.shield)
                        DataItems.obstacle = true;
                    StartCoroutine("DestroyItems");
                }
            }
        }

        IEnumerator DestroyItems()
        {
            yield return new WaitForSeconds(0.1f);
            Destroy(gameObject);
        }
    }
}