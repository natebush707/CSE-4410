using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject lavaTextObject;
    public AudioClip pickupSound;
    public Transform deathExplosion;
    
    [SerializeField] private float maxSpeed = 10f;

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;

    void Start()
    {
        count = 0;
        rb = GetComponent<Rigidbody>();
        winTextObject.SetActive(false);
        lavaTextObject.SetActive(false);
        deathExplosion.GetComponent<ParticleSystem>().enableEmission = false;

        SetCountText();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if(count >= 12)
        {
            winTextObject.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
        CheckSpeed();
    }

    void CheckSpeed()
    {
        if(rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position, 10f);
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }

        if (other.gameObject.CompareTag("Lava"))
        {
            lavaTextObject.SetActive(true);
            deathExplosion.transform.parent = null;
            deathExplosion.GetComponent<ParticleSystem>().enableEmission = true;
            rb.gameObject.SetActive(false);
        }

    }
}
