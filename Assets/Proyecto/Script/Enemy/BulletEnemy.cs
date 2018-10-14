using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    [Header("Attributes Bullet")]
    public bool hasMovement;
    public float speed = 20f;
    public int damege = 40;

    private Transform player;
    private Vector2 target;

    private Rigidbody2D rb2D;

    [Header("Effect")]
    public GameObject bulletImpactEffect;

    void Start ()
    {
        GetComponent();
    }
	
	void Update ()
    {
        if(hasMovement == true)
        {
            rb2D.velocity = transform.right * speed;
        }
    }

    void GetComponent()
    {
        rb2D = GetComponent<Rigidbody2D>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Enemy")
        {
            PlayerMovement player = collision.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.TakeDamage(damege);
            }

            GameObject clone = Instantiate(bulletImpactEffect, transform.position, transform.rotation);

            Destroy(clone, 3f);

            Destroy(gameObject);
        }
    }
}
