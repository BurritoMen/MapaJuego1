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

    [Header("Input Player")]
    public string shoot;
    public string grenade;
    public string jumpB;
    public string up;
    public string down;
    public string left;
    public string right;

    private float HorizontalAxis;

    void Start()
    {
        GetComponent();
    }

    void Update()
    {
        if (InputController.instance.Joystick)
        {
            if (Input.GetAxis("Horizontal") < 0)
                HorizontalAxis = -1;
            else if (Input.GetAxis("Horizontal") > 0)
                HorizontalAxis = 1;
            else
                HorizontalAxis = 0;
        }
        else
        {
            if (Input.GetKey(InputController.instance.Keys[left]))
                HorizontalAxis = -1;
            else if (Input.GetKey(InputController.instance.Keys[right]))
                HorizontalAxis = 1;
            else HorizontalAxis = 0;
        }
       
        horizontalMove = HorizontalAxis * runSpeed;

        animatorPlayer.SetFloat("Speed", Mathf.Abs(horizontalMove));

        Jump();

        Crouch();

        AnimatorWapon();

        CheckCharacterAddressRegardingSprite();
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
        if (InputController.instance.Joystick)
        {
            if (Input.GetAxis("Vertical") < 0)
                crouch = true;
            else if (Input.GetAxis("Vertical") >= 0)
                crouch = false;
        }
        else
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
       
    }

    void AnimatorWapon()
    {
        if (InputController.instance.Joystick)
        {
            if (Input.GetAxis("Vertical") > 0)
                weaponCenterAnimator.SetBool("WeaponUp", true);
            else if (Input.GetAxis("Vertical") <= 0)
                weaponCenterAnimator.SetBool("WeaponUp", false);
        }
        else
        {
            if (Input.GetKeyDown(InputController.instance.Keys[up]))
            {
                weaponCenterAnimator.SetBool("WeaponUp", true);
            }
            else if (Input.GetKeyUp(InputController.instance.Keys[up]))
            {
                weaponCenterAnimator.SetBool("WeaponUp", false);
            }
        }
       

        if (controller.m_Grounded == false &&((InputController.instance.Joystick&&
            Input.GetAxis("Vertical") > 0)
            || (Input.GetKeyDown(InputController.instance.Keys[down]))))
        {
            weaponCenterAnimator.SetBool("WeaponJumpDown", true);
        }
        else if (controller.m_Grounded == true &&((InputController.instance.Joystick &&
            Input.GetAxis("Vertical") <= 0) || (Input.GetKey(InputController.instance.Keys[down])))
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

    void CheckCharacterAddressRegardingSprite()
    {
        if (HorizontalAxis > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (HorizontalAxis < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
