using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHelicopter : MonoBehaviour
{
    [Header("Attributes Enemy")]
    public int health = 100;
    public int speedMovement = 6;
    public float everyHowMuchShoots; //every how much shoots

    [Header("Effect")]
    public GameObject enemyDeathEffect;

    [Header("Shoot Cycle")]
    public int bulletMax = 3;
    public float repeatCycle = 3;
    public List<GameObject> bulletEnemyList;

    [Header("Shoot")]
    public GameObject firePoint;
    private float timeBtwShots;
    public GameObject bombHelicopter;

    GameObject player;

    void Start ()
    {
        GetComponent();
    }

	void Update ()
    {
        FollowPlayer();
        CycleShooting();
    }

    void GetComponent()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FollowPlayer()
    {
        if (player.transform.position.x > transform.position.x)
        {
            transform.Translate(Vector3.right * speedMovement * Time.deltaTime);
        }

        if (player.transform.position.x < transform.position.x)
        {
            transform.Translate(Vector3.left * speedMovement * Time.deltaTime);
        }
    }

    public void Shooting()
    {
        if (timeBtwShots <= 0)
        {
            GameObject clone = Instantiate(bombHelicopter, firePoint.transform.position, firePoint.transform.rotation);
            bulletEnemyList.Add(clone);
            Destroy(clone, 3f);
            timeBtwShots = everyHowMuchShoots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    public void CycleShooting()
    {
        if (bulletEnemyList.Count < bulletMax)
        {
            Shooting();
        }

        if (bulletEnemyList.Count >= bulletMax)
        {
            StartCoroutine(ResetBulletEnemyList());
        }
    }

    IEnumerator ResetBulletEnemyList()
    {
        yield return new WaitForSeconds(repeatCycle);

        bulletEnemyList.Clear();

        StopAllCoroutines();
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
}
