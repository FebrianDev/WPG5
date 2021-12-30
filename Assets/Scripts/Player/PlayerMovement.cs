using System;
using Authentication;
using Items;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkedVar;
using UnityEngine;

namespace scripts
{

    public class PlayerMovement : NetworkedBehaviour
    {
        private Vector3 respawnPoint;
        private bool jumpClicked = false;
        private bool playerFall = false;
        public static NetworkedVarFloat speed = new NetworkedVarFloat(5);
        public NetworkedVarFloat jump = new NetworkedVarFloat(10);
        [SerializeField] private new GameObject camera;
        [SerializeField] private AudioSource source;

        private void Start()
        {
            DataItems.waterBall = 3;
            if (!IsLocalPlayer)
            {
                source.enabled = false;
                camera.SetActive(false);
            }
        }

        private void Update()
        {
            if (IsLocalPlayer)
            {
                respawnPoint = new Vector3(transform.position.x - 30, 20, 0);
                Move(InputPlayer());
                Jump();
            }

            //Mati
            if (transform.position.y < -25)
            {
                Respawn();
            }
        }

        [ServerRPC]
        private static Vector2 InputPlayer()
        {
            var h = Input.GetAxis("Horizontal");
            var v = Input.GetAxis("Vertical");

            var dir = new Vector3(h, 0, v);
            return dir;
        }

        [ServerRPC]
        private void Move(Vector3 dir)
        {
            if (!playerFall)
                transform.position += Vector3.right * (Time.deltaTime * speed.Value);
        }

        public void Jump()
        {
            if (jumpClicked || Input.GetKey(KeyCode.Space))
                transform.position += Vector3.up * (jump.Value * Time.deltaTime);
        }

        void Respawn()
        {
            transform.position = respawnPoint;
            playerFall = false;
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Slippery")
            {
                playerFall = true;
                Debug.Log("Gesek");
            }
        }

        public void JumpOn()
        {
            jumpClicked = true;
        }

        public void JumpOff()
        {
            jumpClicked = false;
        }
    }
}