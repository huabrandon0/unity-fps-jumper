using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager: MonoBehaviour {

    public static InputManager instance = null; // Singleton

    private Settings settings = null;

    // Scripts that need to be updated based on settings changes
    public GameplayPanel gpScript;
    public KeybindsPanel kbScript;

    void Awake()
    {
        InputManager.instance = this;
       
        // Must change script execution order to SettingsManager before InputManager in order to call LoadSettings()
        SettingsManager.instance.LoadSettings();
        this.settings = SettingsManager.instance.GetSettings();
        UpdateOthers();
    }

    public bool GetKeyDown(string key)
    {
        foreach (KeyCode val in this.settings.Keybinds[key])
        {
            if (Input.GetKeyDown(val))
                return true;
        }
        return false;
    }

    public bool GetKey(string key)
    {
        foreach (KeyCode val in this.settings.Keybinds[key])
        {
            if (Input.GetKey(val))
                return true;
        }
        return false;
    }

    public bool GetKeyUp(string key)
    {
        foreach (KeyCode val in this.settings.Keybinds[key])
        {
            if (Input.GetKeyUp(val))
                return true;
        }
        return false;
    }

    public void OverwriteKeybind(string key, KeyCode val, int index)
    {
        if (!this.settings.Keybinds.ContainsKey(key) || (index != 0 && index != 1))
            return;

        this.settings.Keybinds[key][index] = val;
    }

    public void ApplySettings()
    {
        SettingsManager.instance.OverwriteSettings(this.settings);
        SettingsManager.instance.SaveSettings();
    }

    public void ResetSettings()
    {
        this.settings = new Settings();
        UpdateOthers();
    }

    public void RevertSettings()
    {
        this.settings = SettingsManager.instance.GetSettings();
        UpdateOthers();
    }

    public Dictionary<string, KeyCode[]> Keybinds
    {
        get { return new Dictionary<string, KeyCode[]>(this.settings.Keybinds); }
        private set { this.settings.Keybinds = value; }
    }

    public float Sensitivity
    {
        get { return this.settings.Sensitivity; }
        set { this.settings.Sensitivity = value; }
    }

    public float Fov
    {
        get { return this.settings.Fov; }
        set { this.settings.Fov = value; }
    }

    public float ZoomSensitivity
    {
        get { return this.settings.ZoomSensitivity; }
        set { this.settings.ZoomSensitivity = value; }
    }

    public float ZoomFov
    {
        get { return this.settings.ZoomFov; }
        set { this.settings.ZoomFov = value; }
    }

    private void UpdateOthers()
    {
        gpScript.RefreshFloatsUI();
        kbScript.RefreshKeybindsUI();
        gpScript.UpdateCameraFov();
    }
}
