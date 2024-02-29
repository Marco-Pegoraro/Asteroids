using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 30.0f;

    private Rigidbody2D objectRb;

    void Start()
    {
        objectRb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        objectRb.AddRelativeForce(Vector2.up * speed);
    }

}
