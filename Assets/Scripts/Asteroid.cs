using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Sprite[] sprites;

    public float size = 1.0f;

    public float speed = 50.0f;

    public float minSize = 0.5f;

    public float maxSize = 1.5f;

    private SpriteRenderer asteroidSr;

    private Rigidbody2D asteroidRb;

    private GameManager gameManager;

    private float maxLifeTime = 20.0f;

    void Awake()
    {
        asteroidSr = GetComponent<SpriteRenderer>();
        asteroidRb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void Start()
    {
        asteroidSr.sprite = sprites[Random.Range(0, sprites.Length)];

        transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        transform.localScale = Vector3.one * size;

        asteroidRb.mass = size;
    }

    void Update()
    {
        if (gameManager.gameOver)
        {
            Destroy(gameObject);
        }
    }

    public void SetTrajectory(Vector2 direction)
    {
        asteroidRb.AddForce(direction * speed);

        Destroy(gameObject, maxLifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Destroy(collision.gameObject);
            gameManager.AsteroidDestroyed(this);

            if ((size * 0.5) >= minSize)
            {
                CreateSplit();
                CreateSplit();
            }

            Destroy(gameObject);
        }
    }

    private void CreateSplit()
    {
        Vector2 asteroidPosition = transform.position;
        asteroidPosition += Random.insideUnitCircle * 0.5f;

        Asteroid asteroid = Instantiate(this, asteroidPosition, transform.rotation);
        asteroid.size = size / 2;
        asteroid.SetTrajectory(Random.insideUnitCircle.normalized * speed);
    }

}
