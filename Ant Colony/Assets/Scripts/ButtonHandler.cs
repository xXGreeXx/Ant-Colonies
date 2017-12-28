using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour {

    //define global variables
    public static GameObject tempHud;
    bool tempHudActive = false;

    //start
    void Start()
    {
        tempHud = GameObject.Find("temperatueHUD");
        tempHud.SetActive(false);
    }

    //handle button presses
    public void ButtonPressed(string button)
    {
        if (button.Equals("Temp"))
        {
            tempHudActive = !tempHudActive;
            tempHud.SetActive(tempHudActive);
        }
    }
}
