using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickTester : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.JoystickButton1))
            
            Debug.Log("joystick");
        }
	}

