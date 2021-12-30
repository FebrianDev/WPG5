using System.Collections;
using Items;
using MLAPI;
using MLAPI.Messaging;
using scripts;
using UnityEngine;

public class ObstacleActive : NetworkedBehaviour
{
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;
    private Rigidbody2D rigidbody2D;
    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (DataItems.obstacle && !DataItems.shield && IsLocalPlayer)
        {
            StartCoroutine(nameof(ObstacleOn));
        }
    }

    public IEnumerator ObstacleOn()
    {
        PlayerMovement.speed.Value = 0;
        ComponentActive(false);
        rigidbody2D.bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(3);
        rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        PlayerMovement.speed.Value = 5;
        ComponentActive(true);
        DataItems.obstacle = false;
    }

    [ClientRPC]
    void ComponentActive(bool state)
    {
        spriteRenderer.enabled = state;
        boxCollider2D.enabled = state;
    }
}