using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum TypeEnemies { Static, WithMovement }

    [Header("Type Enemies")]
    public TypeEnemies typeEnemies;

    [Header("Attributes Enemy")]
    public int health = 100;
    public float everyHowMuchShoots; //every how much shoots
    public float speedMovement;

    [Header("Effect")]
    public GameObject enemyDeathEffect;

    [Header("Shoot")]
    public GameObject firePointGun;
    private float timeBtwShots;
    public GameObject bulletEnemy;
    Transform player;

    [Header("Patrol")]
    public float distanceRaycastGroundInfo;
    private bool movingRight = true;
    public Transform groundDetection;

    [Header("Eyes")]
    bool seePlayer;
    public float distanceRaycastEyes;
    public Transform eyes;
    Vector2 directionView;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        timeBtwShots = everyHowMuchShoots;

        directionView = Vector2.right;
    }

    void Update()
    {
        Eyes();

        if (typeEnemies == TypeEnemies.Static)
        {
            Static();
        }

        if (typeEnemies == TypeEnemies.WithMovement)
        {
            WithMovement();
        }
    }

    public void Shooting()
    {
        if (timeBtwShots <= 0)
        {
            GameObject clone = Instantiate(bulletEnemy, firePointGun.transform.position, firePointGun.transform.rotation);
            Destroy(clone, 3f);
            timeBtwShots = everyHowMuchShoots;
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

    void Patrol()
    {
        transform.Translate(Vector2.right * speedMovement * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distanceRaycastGroundInfo);
        //Debug.DrawRay(groundDetection.position, Vector2.down * distanceRaycastGroundInfo, Color.green);

        if (groundInfo.collider == false)
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0f, -180f, 0f);
                movingRight = false;
                directionView = Vector2.left;
            }
            else
            {
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
                movingRight = true;

                directionView = Vector2.right;
            }
        }
    }

    void Eyes()
    {
        RaycastHit2D eyesInfo = Physics2D.Raycast(eyes.position, directionView, distanceRaycastEyes);
        Debug.DrawRay(eyes.position, directionView * distanceRaycastEyes, Color.red);

        if (eyesInfo.collider != null && eyesInfo.collider.tag == "Player")
        {
            seePlayer = true;
        }
        else
        {
            seePlayer = false;
        }
    }

    #region "TypeEnemies"
    void Static()
    {
        if (seePlayer == true)
        {
            VerifyPlayerPosition();
            Shooting();
        }
    }

    void WithMovement()
    {
        if (seePlayer == false)
        {
            Patrol();
        }

        if (seePlayer == true)
        {
            VerifyPlayerPosition();
            Shooting();
        }
    }
    #endregion
}