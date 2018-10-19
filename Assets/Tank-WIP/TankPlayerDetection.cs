using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPlayerDetection : MonoBehaviour
{
    public Tank tank;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (tank.coolDown == false)
        {
            if (collision.tag == "Player" && tank.Occupied == false)
            {
                collision.gameObject.transform.position = this.gameObject.transform.position;
                collision.gameObject.transform.parent = this.gameObject.transform.parent;
                collision.gameObject.SetActive(false);
                tank.Occupied = true;
            }
        }
    }
}
