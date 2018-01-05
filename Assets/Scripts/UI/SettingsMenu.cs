using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour {

    void OnDisable()
    {
        InputManager.instance.RevertSettings();
    }

    public void ApplySettings()
    {
        InputManager.instance.ApplySettings();
    }

    public void ResetSettings()
    {
        InputManager.instance.ResetSettings();
    }

    public void RevertSettings()
    {
        InputManager.instance.RevertSettings();
    }
}
