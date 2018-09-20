using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyTester : MonoBehaviour {
    //solo para propositos de testeo
    public Button Test;
    public string KeyName;
    public KeySetter ks;
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(InputController.instance.Keys[KeyName]))
        {
            Test.image.color = Color.blue;
        }
        else Test.image.color = Color.red;
        if (ks.waiting())
            Test.image.color = Color.yellow;
	}
}
