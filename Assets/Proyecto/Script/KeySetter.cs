using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class KeySetter : MonoBehaviour {

    [SerializeField]
    private string Key;
    [SerializeField]
    private bool WaitingForKey;
    private static bool Busy;
    Event KeyEvent;

    //para testear nomas
    public bool waiting()
    {
        return WaitingForKey;
    }

    private void OnGUI()
    {

        KeyEvent = Event.current;
        if (WaitingForKey && KeyEvent.isKey) 
        {
            
            KeyCode newKey = KeyEvent.keyCode;
            SetKey(newKey);
            WaitingForKey = false;
            Busy = false;
        }
       
    }
    private void Update()
    {
        if(WaitingForKey)
        {
         //necesito una forma de asignar el D pad a un keycode
                for (int c = 0; c < 20; c++)
                {

                    if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode),
                        ("JoystickButton" + c))))
                    {
                        KeyCode thisKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode),
                            ("JoystickButton" + c));
                        SetKey(thisKeyCode);
                        WaitingForKey = false;
                        Busy = false;

                    }
                }
        for (int c = 0; c < 20; c++)
            {

                if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode),
                    ("JoystickButton" + c))))
                {
                    KeyCode thisKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode),
                        ("JoystickButton" + c));
                    SetKey(thisKeyCode);
                    WaitingForKey = false;
                    Busy = false;

                }
            }
        }
        
       
          
           

    }
    private void SetKey(KeyCode NewKey)
    {
        InputController.instance.ChangeKeyCode(Key, NewKey);
    }
    
    public void ButtonPress()
    {
        if (!Busy)
        {
            WaitingForKey = true;
            Busy = true;
        }
        
    }
}
