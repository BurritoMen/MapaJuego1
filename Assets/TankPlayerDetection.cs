using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPlayerDetection : MonoBehaviour
{
    public Tank tank;

	void Start ()
    {
		
	}

	void Update ()
    {
		
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            tank.Occupied = true;
            Debug.Log("Activar EVA 01");
        }
    }
}
