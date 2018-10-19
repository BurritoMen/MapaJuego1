using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    [Header("Attributes Tank")]
    public bool Occupied;
    public bool coolDown;
    public int coolDownTime;
    public int health = 100;
    public float runSpeed = 40f;
    bool grounded;

    [Header("Collider 2D")]
    public BoxCollider2D tankCollider1;
    public BoxCollider2D tankCollider2;
    public BoxCollider2D entryCollider;
    public int distanceToActivateEntry = 2;

    //Get Component
    Rigidbody2D rb2D;
    CharacterController2D controller;
    GameObject player;

    //input
    PlayerMovement inputPlayer;

    private string shoot;
    private string grenade;
    private string jumpB;
    private string up;
    private string down;
    private string left;
    private string right;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    private float HorizontalAxis;

    void Start ()
    {
        GetComponent();

        Occupied = false;

        coolDown = false;
    }
	
	void Update ()
    {
        if (!Occupied)
        {
            tankCollider1.isTrigger = true;
            tankCollider2.isTrigger = true;
            entryCollider.enabled = false;

            rb2D.bodyType = RigidbodyType2D.Static;

            ActivateTankEntry();
        }

        if (Occupied)
        {
            tankCollider1.isTrigger = false;
            tankCollider2.isTrigger = false;

            rb2D.bodyType = RigidbodyType2D.Dynamic;

            if (Input.GetKey(InputController.instance.Keys[left]))
                HorizontalAxis = -1;
            else if (Input.GetKey(InputController.instance.Keys[right]))
                HorizontalAxis = 1;
            else HorizontalAxis = 0;
            horizontalMove = HorizontalAxis * runSpeed;


            if (grounded == false)
            {
                if (Input.GetKey(InputController.instance.Keys[down]) && Input.GetKey(InputController.instance.Keys[jumpB]))
                {
                    EjectPlayer();
                }
            }

            Jump();

            Crouch();
        }

        if (!controller.m_Grounded)
        {
            grounded = true;
        }

        if (health <= 0)
        {
            Die();
        }
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    void Jump()
    {
        if (Input.GetKeyDown(InputController.instance.Keys[jumpB]))
        {
            jump = true;
        }
    }

    void Crouch()
    {
        if (Input.GetKeyDown(InputController.instance.Keys[down]))
        {
            crouch = true;
        }
        else if (Input.GetKeyUp(InputController.instance.Keys[down]))
        {
            crouch = false;
        }
    }

    public void OnLanding()
    {
        grounded = false;
    }

    public void OnCrouching(bool isCrouching)
    {
    }

    void GetComponent()
    {
        rb2D = GetComponent<Rigidbody2D>();

        controller = GetComponent<CharacterController2D>();

        player = GameObject.FindGameObjectWithTag("Player");

        inputPlayer = player.GetComponent<PlayerMovement>();

        //input
        shoot = inputPlayer.shoot;
        grenade = inputPlayer.grenade;
        jumpB = inputPlayer.jumpB;
        up = inputPlayer.up;
        down = inputPlayer.down;
        left = inputPlayer.left;
        right = inputPlayer.right;
    }

    void ActivateTankEntry()
    {
        if (player.transform.position.y >= entryCollider.transform.position.y + distanceToActivateEntry)
        {
            entryCollider.enabled = true;
        }

        if (player.transform.position.y < entryCollider.transform.position.y)
        {
            entryCollider.enabled = false;
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
        EjectPlayer();
        Destroy(gameObject);
    }

    void EjectPlayer()
    {
        coolDown = true;
        Occupied = false;
        player.SetActive(true);
        player.transform.parent = null;

        player.transform.rotation = Quaternion.Euler(0,0,0);

        StartCoroutine(CoolDown());
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(coolDownTime);

        coolDown = false;

        StopAllCoroutines();
    }
}
