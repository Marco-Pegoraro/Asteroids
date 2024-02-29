using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
    public PlayerController player;

    public TextMeshProUGUI livesText;

    public TextMeshProUGUI scoreText;

    public TextMeshProUGUI gameOverText;

    public bool gameOver = false;

    public int lives = 3;

    private float respawnInvulnerabilityTime = 3.0f;

    private float respawnTime = 3.0f;

    public int score = 0;

    void Start()
    {
        livesText.text = "x" + lives.ToString();
    }

    void Update()
    {
        if (gameOver && Input.GetKeyUp(KeyCode.R))
        {
            gameOver = false;
            lives = 3;
            score = 0;
            scoreText.text = score.ToString();
            livesText.text = "x" + lives.ToString();
            gameOverText.gameObject.SetActive(false);
            player.transform.position = Vector3.zero;
            player.gameObject.SetActive(true);
        }
    }

    public void AsteroidDestroyed(Asteroid asteroid)
    {
        if (asteroid.size < 0.75f)
        {
            score += 10;
        }
        else if (asteroid.size < 1.25f)
        {
            score += 15;
        }
        else
        {
            score += 20;
        }

        scoreText.text = score.ToString();
    }

    public void PlayerDie()
    {
        lives--;
        livesText.text = "x" + lives.ToString();

        if (lives <= 0)
        {
            GameOver();
        }
        else
        {
            Invoke("Respawn", respawnTime);
        }
    }

    private void Respawn()
    {
        player.gameObject.layer = LayerMask.NameToLayer("IgnoreCollisions");
        player.transform.position = Vector3.zero;
        player.gameObject.SetActive(true);
        Invoke("TurnOnCollisions", respawnInvulnerabilityTime);
    }

    private void TurnOnCollisions()
    {
        player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        gameOver = true;
    }
}
