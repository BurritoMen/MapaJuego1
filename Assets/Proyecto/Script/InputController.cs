using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputController : MonoBehaviour {

    public static InputController instance;
    [SerializeField]
    private KeyCode[] KeyCodes;
    [SerializeField]
    private string[] KeyNames;
    public Dictionary<string,KeyCode> Keys { get; set; }

    private void Awake()
    {
       // Debug.Log("entro1");
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }
        Keys = new Dictionary<string, KeyCode>();
        for(int C=0;C<KeyCodes.Length;C++)
        {
            Keys.Add(KeyNames[C], KeyCodes[C]);
           // Debug.Log("entro");
        }
    }

    public void ChangeKeyCode(string KeyName, KeyCode KeyToBind)
    {
        //intercambia teclas cuando queres asignar una tecla que ya esta siendo usada
        if(Keys.ContainsValue(KeyToBind))
        {
            string key = Keys.First(x => x.Value == KeyToBind).Key;
            Keys[key] = Keys[KeyName];
        }
        Keys[KeyName] = KeyToBind;
    }
    public void RestoreDefaults()
    {
        for (int C = 0; C < KeyCodes.Length; C++)
        {
            Keys[KeyNames[C]] = KeyCodes[C];
          
        }
    }

}
