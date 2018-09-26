using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatic : MonoBehaviour
{
    [Header("Attributes Enemy")]
    public int health = 100;

    [Header("Effect")]
    public GameObject enemyDeathEffect;

    [Header("Shoot")]
    public GameObject firePointGun;
    public float startTimeBtwShots; //every how much shoots
    private float timeBtwShots;
    public GameObject bulletEnemy;
    public Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        timeBtwShots = startTimeBtwShots;
    }

    void Update()
    {
        VerifyPlayerPosition();
        Shooting();
    }

    public void Shooting()
    {
        if (timeBtwShots <= 0)
        {
            GameObject clone = Instantiate(bulletEnemy, firePointGun.transform.position, firePointGun.transform.rotation);
            Destroy(clone, 3f);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GameObject clone = Instantiate(enemyDeathEffect, transform.position, Quaternion.identity);
        Destroy(clone, 3f);
        Destroy(gameObject);
    }

    void VerifyPlayerPosition()
    {
        if (player.transform.position.x < transform.position.x)
        {
            transform.localRotation = Quaternion.Euler(0, 180f, 0);//player on the left
        }

        if (player.transform.position.x > transform.position.x)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);//player on the right
        }
    }
}