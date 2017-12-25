using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager {

    public static bool GetKeyDown(string key)
    {
        foreach (KeyCode val in SettingsManager.instance.Keybinds[key])
        {
            if (Input.GetKeyDown(val))
                return true;
        }
        return false;
    }

    public static bool GetKey(string key)
    {
        foreach (KeyCode val in SettingsManager.instance.Keybinds[key])
        {
            if (Input.GetKey(val))
                return true;
        }
        return false;
    }

    public static bool GetKeyUp(string key)
    {
        foreach (KeyCode val in SettingsManager.instance.Keybinds[key])
        {
            if (Input.GetKeyUp(val))
                return true;
        }
        return false;
    }

    public static void OverwriteKeybind(string key, KeyCode val, int index)
    {
        if (!SettingsManager.instance.Keybinds.ContainsKey(key) || (index != 0 && index != 1))
            return;

        SettingsManager.instance.Keybinds[key][index] = val;
    }

    public static Dictionary<string, KeyCode[]> GetKeybindDictionary()
    {
        return SettingsManager.instance.Keybinds;
    }

    public static float Sensitivity
    {
        get { return SettingsManager.instance.Sensitivity; }
        set { SettingsManager.instance.Sensitivity = value; }
    }

    public static float Fov
    {
        get { return SettingsManager.instance.Fov; }
        set { SettingsManager.instance.Fov = value; }
    }

    public static float ZoomSensitivity
    {
        get { return SettingsManager.instance.ZoomSensitivity; }
        set { SettingsManager.instance.ZoomSensitivity = value; }
    }

    public static float ZoomFov
    {
        get { return SettingsManager.instance.ZoomFov; }
        set { SettingsManager.instance.ZoomFov = value; }
    }
}
