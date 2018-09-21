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

    void Start()
    {
        GetComponent();
    }

    void Update ()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

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
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animatorPlayer.SetBool("IsJumping", true);
        }
    }

    void Crouch()
    {
        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
    }

    void AnimatorWapon()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            weaponCenterAnimator.SetBool("WeaponUp", true);
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            weaponCenterAnimator.SetBool("WeaponUp", false);
        }

        if (controller.m_Grounded == false && Input.GetKey(KeyCode.S))
        {
            weaponCenterAnimator.SetBool("WeaponJumpDown", true);
        }
        else if (controller.m_Grounded == true && Input.GetKey(KeyCode.S) || Input.GetKeyUp(KeyCode.S))
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
}
