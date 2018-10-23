using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class InputController : MonoBehaviour {

    public static InputController instance;
    [SerializeField]
    private KeyCode[] KeyCodes;
    [SerializeField]
    private string[] KeyNames;
    public Dictionary<string,KeyCode> Keys { get; set; }
    public bool Joystick { get; set; }
    [SerializeField]
    private string SaveName;

    private KeyCode[] SavedKeyCodes;

    private void Awake()
    {
        SavedKeyCodes = new KeyCode[KeyCodes.Length];
        //para testear joysticks
        Joystick = true;


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
    public void RestoreToSavedDefaults()
    {
        for (int C = 0; C < SavedKeyCodes.Length; C++)
        {
            Keys[KeyNames[C]] = SavedKeyCodes[C];
        }
           
    }
    public void Save()
    {
        for (int C = 0; C < KeyNames.Length; C++)
        {
            SavedKeyCodes[C] = Keys[KeyNames[C]];
        }
        string Destination = Application.persistentDataPath + SaveName;
        Debug.Log(Destination);
        FileStream file;
        if (File.Exists(Destination)) file = File.OpenWrite(Destination);
        else
            file = File.Create(Destination);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, SavedKeyCodes);
        file.Close();
    }
    private void Load()
    {
        string Destination = Application.persistentDataPath + SaveName;
        FileStream file;
        if (File.Exists(Destination)) file = File.OpenRead(Destination);
        else
        {
            Debug.Log("no hay archivo vieja");
            return;
        }
        BinaryFormatter bf = new BinaryFormatter();
        SavedKeyCodes = (KeyCode[])bf.Deserialize(file);
        file.Close();
    }

}
