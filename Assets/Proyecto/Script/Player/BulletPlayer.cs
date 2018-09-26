﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    [Header("Attributes Bullet")]
    public float speed = 20f;
    public int damege = 40;

    [Header("Effect")]
    public GameObject bulletImpactEffect;

    Rigidbody2D rb2D;

    void Start ()
    {
        GetComponent();
    }

    private void Update()
    {
        rb2D.velocity = transform.right * speed;
    }

    void GetComponent()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyStatic enemy = collision.GetComponent<EnemyStatic>();
        if(enemy != null)
        {
            enemy.TakeDamage(damege);
        }

        GameObject clone = Instantiate(bulletImpactEffect, transform.position, transform.rotation);

        Destroy(clone, 3f);

        Destroy(gameObject);
    }
}