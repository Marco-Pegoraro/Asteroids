using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public Asteroid asteroidPrefab;

    public float spawnRate = 2.0f;

    public float trajectoryVariance = 15.0f;

    public float spawnDistance = 15.0f;

    public int spawnAmount = 1;

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        InvokeRepeating("Spawn", spawnRate, spawnAmount);
    }

    private void Spawn()
    {
        if (!gameManager.gameOver)
        {
            for (int i = 0; i < spawnAmount; ++i)
            {
                Vector3 spawnDirection = Random.insideUnitCircle.normalized * spawnDistance;
                Vector3 spawnPosition = transform.position + spawnDirection;

                float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
                Quaternion asteroidRotation = Quaternion.AngleAxis(variance, Vector3.forward);

                Asteroid asteroid = Instantiate(asteroidPrefab, spawnPosition, asteroidRotation);
                asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);
                asteroid.SetTrajectory(asteroidRotation * -spawnDirection);
            }
        }
    }
}
