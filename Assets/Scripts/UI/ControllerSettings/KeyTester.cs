using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyTester : MonoBehaviour {

    public Button Test;

	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(InputController.instance.Keys["A"]))
        {
            Test.image.color = Color.blue;
        }
        else Test.image.color = Color.red;
	}
}
