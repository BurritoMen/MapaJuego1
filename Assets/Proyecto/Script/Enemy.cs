using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Attributes Enemy")]
    public int health = 100;

    [Header("Effect")]
    public GameObject enemyDeathEffect;

    public void TakeDamage(int damage)
    {
        health -= damage;

        if(health <= 0)
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
}