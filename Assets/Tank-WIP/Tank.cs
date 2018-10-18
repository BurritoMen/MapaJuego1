using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    [Header("Attributes Tank")]
    public bool Occupied;
    public int health = 100;
    public float runSpeed = 40f;

    [Header("Collider 2D")]
    public BoxCollider2D tankCollider1;
    public BoxCollider2D tankCollider2;
    public BoxCollider2D entryCollider;
    public int distanceToActivateEntry = 2;

    //Get Component
    Rigidbody2D rb2D;
    CharacterController2D controller;
    Transform player;

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
    }
	
	void Update ()
    {
        if (!Occupied)
        {
            tankCollider1.isTrigger = true;
            tankCollider2.isTrigger = true;
            entryCollider.enabled = false;

            rb2D.bodyType = RigidbodyType2D.Kinematic;

            ActivateTankEntry();
        }

        if (Occupied)
        {
            player.gameObject.SetActive(false);

            tankCollider1.isTrigger = false;
            tankCollider2.isTrigger = false;

            rb2D.bodyType = RigidbodyType2D.Dynamic;

            if (Input.GetKey(InputController.instance.Keys[left]))
                HorizontalAxis = -1;
            else if (Input.GetKey(InputController.instance.Keys[right]))
                HorizontalAxis = 1;
            else HorizontalAxis = 0;
            horizontalMove = HorizontalAxis * runSpeed;

            Jump();

            Crouch();
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

    void GetComponent()
    {
        rb2D = GetComponent<Rigidbody2D>();

        controller = GetComponent<CharacterController2D>();

        player = GameObject.FindGameObjectWithTag("Player").transform;

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
        if (player.position.y >= entryCollider.transform.position.y + distanceToActivateEntry)
        {
            entryCollider.enabled = true;
        }

        if (player.position.y < entryCollider.transform.position.y)
        {
            entryCollider.enabled = false;
        }
    }
}
