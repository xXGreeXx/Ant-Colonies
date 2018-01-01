using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour {

    //define global variables
    public static GameObject tempHud;
    public static GameObject menuHud;
    public static GameObject environmentHud;
    bool tempHudActive = false;
    bool menuHudActive = false;
    bool environmentHudActive = false;

    //start
    void Start()
    {
        tempHud = GameObject.Find("temperatureHUD");
        tempHud.SetActive(false);

        menuHud = GameObject.Find("menuHUD");
        menuHud.SetActive(false);

        environmentHud = GameObject.Find("environmentHUD");
        environmentHud.SetActive(false);
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
        if (button.Equals("Environment"))
        {
            environmentHudActive = !environmentHudActive;
            environmentHud.SetActive(environmentHudActive);
        }
    }
}
