using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class KeySetter : MonoBehaviour {

    [SerializeField]
    private string Key;
    [SerializeField]
    private bool WaitingForKey;

    Event KeyEvent;

    private void OnGUI()
    {
        KeyEvent = Event.current;
        if (WaitingForKey && KeyEvent.isKey)
        {
            KeyCode newKey = KeyEvent.keyCode;
            SetKey(newKey);
            WaitingForKey = false;
        }
    }
    
    private void SetKey(KeyCode NewKey)
    {
        InputController.instance.ChangeKeyCode(Key, NewKey);
    }
    
    public void ButtonPress()
    {
        WaitingForKey = true;
    }
}
