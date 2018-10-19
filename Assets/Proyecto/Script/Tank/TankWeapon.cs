using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankWeapon : MonoBehaviour
{
    [Header("Input")]
    public Weapon WeaponPlayer;

    [Header("Gun")]
    public Transform firePointGun;
    public GameObject bulletPrefab;

    [Header("Bomb")]
    public Transform firePointBomb;
    public GameObject bombPrefab;

    //get component
    Tank tank;
    private string shoot;
    private string grenade;

    void Start()
    {
        GetComponent();
    }

    void Update()
    {
        if(tank.Occupied == true)
        {
            FireGun();

            FireBomb();
        }
    }

    void FireGun()
    {
        if (Input.GetKeyDown(InputController.instance.Keys[shoot]))
        {
            GameObject clone = Instantiate(bulletPrefab, firePointGun.position, firePointGun.rotation);
            Destroy(clone, 3f);
        }
    }

    void FireBomb()
    {
        if (Input.GetKeyDown(InputController.instance.Keys[grenade]))
        {
            GameObject clone = Instantiate(bombPrefab, firePointBomb.position, firePointBomb.rotation);
            Destroy(clone, 3f);
        }
    }

    void GetComponent()
    {
        tank = GetComponent<Tank>();

        shoot = WeaponPlayer.shoot;
        grenade = WeaponPlayer.grenade;
    }
}
