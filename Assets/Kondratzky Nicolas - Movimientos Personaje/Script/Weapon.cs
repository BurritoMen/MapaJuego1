using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Gun")]
    public Transform firePointGun;
    public GameObject bulletPrefab;

    [Header("Bomb")]
    public Transform firePointBomb;
    public GameObject bombPrefab;

    [SerializeField]
    private string shoot;
    [SerializeField]
    private string grenade;


    void Update ()
    {
        FireGun();

        FireBomb();
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
}
