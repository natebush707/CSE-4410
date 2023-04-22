using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperWalls : MonoBehaviour
{
    [SerializeField] float bumperForce = 2f;

    public AudioClip bounceSound;

    private void OnTriggerEnter(Collider other)
    {
        // only bounce objects tagged with "Player"
        if (other.gameObject.CompareTag("Player"))
        {
            // get Player Rigidbody and make sure it exists
            Rigidbody playerRigidbody = other.gameObject.GetComponent<Rigidbody>();
            if (playerRigidbody)
            {
                // reverse velocity and apply bumper force
                Vector3 bounceVector = -playerRigidbody.velocity;
                playerRigidbody.AddForce(bounceVector * bumperForce);

                // play bounce sound effect
                AudioSource.PlayClipAtPoint(bounceSound, transform.position, 10f);
            }
        }
    }
}
