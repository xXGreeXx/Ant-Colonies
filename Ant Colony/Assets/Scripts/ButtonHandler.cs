using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour {

    //define global variables
    public static GameObject tempHud;
    public static GameObject menuHud;
    bool tempHudActive = false;
    bool menuHudActive = false;

    //start
    void Start()
    {
        tempHud = GameObject.Find("temperatueHUD");
        tempHud.SetActive(false);

        menuHud = GameObject.Find("menuHUD");
        menuHud.SetActive(false);
    }

    //handle button presses
    public void ButtonPressed(string button)
    {
        if (button.Equals("Temp"))
        {
            tempHudActive = !tempHudActive;
            tempHud.SetActive(tempHudActive);
        }
        if (button.Equals("Menu"))
        {
            menuHudActive = !menuHudActive;
            menuHud.SetActive(menuHudActive);
        }
    }
}
