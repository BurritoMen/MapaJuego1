using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Attributes Player")]
    public int health = 100;

    public float runSpeed = 40f;

    [Header("Animator Weapon")]
    public Animator weaponCenterAnimator;

    CharacterController2D controller;
    Animator animatorPlayer;
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

    [SerializeField]
    private string shoot;
    [SerializeField]
    private string grenade;
    [SerializeField]
    private string jumpB;
    [SerializeField]
    private string up;
    [SerializeField]
    private string down;
    [SerializeField]
    private string left;
    [SerializeField]
    private string right;

    private float HorizontalAxis;

    void Start()
    {
        GetComponent();
    }

    void Update()
    {
        if (Input.GetKey(InputController.instance.Keys[left]))
            HorizontalAxis = -1;
        else if (Input.GetKey(InputController.instance.Keys[right]))
            HorizontalAxis = 1;
        else HorizontalAxis = 0;
        horizontalMove = HorizontalAxis * runSpeed;

        animatorPlayer.SetFloat("Speed", Mathf.Abs(horizontalMove));

        Jump();

        Crouch();

        AnimatorWapon();
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    void GetComponent()
    {
        controller = GetComponent<CharacterController2D>();

        animatorPlayer = GetComponent<Animator>();
    }

    void Jump()
    {
        if (Input.GetKeyDown(InputController.instance.Keys[jumpB]))
        {
            jump = true;
            animatorPlayer.SetBool("IsJumping", true);
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

    void AnimatorWapon()
    {
        if (Input.GetKeyDown(InputController.instance.Keys[up]))
        {
            weaponCenterAnimator.SetBool("WeaponUp", true);
        }
        else if (Input.GetKeyUp(InputController.instance.Keys[up]))
        {
            weaponCenterAnimator.SetBool("WeaponUp", false);
        }

        if (controller.m_Grounded == false && (Input.GetKeyDown(InputController.instance.Keys[down])))
        {
            weaponCenterAnimator.SetBool("WeaponJumpDown", true);
        }
        else if (controller.m_Grounded == true && (Input.GetKey(InputController.instance.Keys[down]))
            || (Input.GetKeyUp(InputController.instance.Keys[shoot])))
        {
            weaponCenterAnimator.SetBool("WeaponJumpDown", false);
        }
    }

    public void OnLanding()
    {
        animatorPlayer.SetBool("IsJumping", false);
    }

    public void OnCrouching(bool isCrouching)
    {
        animatorPlayer.SetBool("IsCrouching", isCrouching);
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
        //Death Animation
        Destroy(gameObject);
    }
}
