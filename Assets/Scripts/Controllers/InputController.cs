using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

    public static InputController instance;
    
    public Dictionary<string,KeyCode> Keys { get; set; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else return;
        Keys = new Dictionary<string, KeyCode>();
        Keys.Add("A", KeyCode.A);
    }
    // Update is called once per frame
    void Update () {
		
	}

    public void ChangeKeyCode(string KeyName, KeyCode KeyToBind)
    {
        Keys[KeyName] = KeyToBind;
    }

}
