using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour 
{
    [Header("Attributes Bomb")]
    public int damege = 100;

    public float speedX = 8f;
    public float speedY = 8f;
    public float speedInitialY = 8f;
    public float acceleration = 2f;
    public float gravity = 8.1f;

    [Header("Effect")]
    public GameObject bombImpactEffect;
	
	void Start () 
	{
        speedY = speedInitialY;
	}

	void Update () 
	{
        speedY = speedY + (acceleration * Time.deltaTime);

		transform.Translate(speedX * Time.deltaTime, speedY * Time.deltaTime,0);

        speedY = speedY - (gravity * Time.deltaTime);
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyStatic enemy = collision.GetComponent<EnemyStatic>();
        if (enemy != null)
        {
            enemy.TakeDamage(damege);
        }

        GameObject clone = Instantiate(bombImpactEffect, transform.position, transform.rotation);

        Destroy(clone, 3f);

        Destroy(gameObject);
    }
}
