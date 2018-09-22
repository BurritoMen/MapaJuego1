using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShowCurrentKey : MonoBehaviour {


    private Text T;
    [SerializeField]
    private string KeyName;

    private void Awake()
    {
        T = GetComponent<Text>();
    }
    // Update is called once per frame
    void Update () {

        T.text = InputController.instance.Keys[KeyName].ToString();
	}
}
