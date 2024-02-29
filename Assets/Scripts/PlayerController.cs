using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;

    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private GameObject spawnArea;

    private float horizontalInput;

    private Rigidbody2D playerRb;

    private GameManager gameManager;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        transform.Rotate(Vector3.back * horizontalInput);

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            playerRb.AddRelativeForce(Vector2.up * speed);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerShoot();
        }
    }

    private void PlayerShoot()
    {
        Instantiate(bulletPrefab, spawnArea.transform.position, transform.rotation);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            playerRb.velocity = Vector3.zero;
            playerRb.angularVelocity = 0.0f;

            gameObject.SetActive(false);

            gameManager.PlayerDie();
        }
    }

}
