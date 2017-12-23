using UnityEngine;
using System.Collections.Generic;

public static class InputManager {

    private static Dictionary<string, KeyCode[]> keybinds;

    public static float sens = 1f;
    public static float zoomedSens = 0.5f;
    public static float fov = 80f;
    public static float zoomedFov = 40f;

    private static string[] defaultKeys = new string[]
    {
        "Attack1",
        "Attack2",
        "Zoom",
        "Strafe Up",
        "Strafe Left",
        "Strafe Down",
        "Strafe Right",
        "Jump",
        "Crouch",
        "Lean Left",
        "Lean Right",
        "Weapon1",
        "Weapon2",
        "Weapon3",
        "Reset"
    };

    private static KeyCode[][] defaultValues = new KeyCode[][]
    {
        new KeyCode[2]{ KeyCode.Mouse0, KeyCode.None },
        new KeyCode[2]{ KeyCode.Mouse1, KeyCode.None },
        new KeyCode[2]{ KeyCode.Mouse1, KeyCode.None },
        new KeyCode[2]{ KeyCode.W, KeyCode.UpArrow },
        new KeyCode[2]{ KeyCode.A, KeyCode.LeftArrow },
        new KeyCode[2]{ KeyCode.S, KeyCode.DownArrow },
        new KeyCode[2]{ KeyCode.D, KeyCode.RightArrow },
        new KeyCode[2]{ KeyCode.Space, KeyCode.None },
        new KeyCode[2]{ KeyCode.LeftControl, KeyCode.None },
        new KeyCode[2]{ KeyCode.Q, KeyCode.None },
        new KeyCode[2]{ KeyCode.E, KeyCode.None },
        new KeyCode[2]{ KeyCode.Alpha1, KeyCode.None },
        new KeyCode[2]{ KeyCode.Alpha2, KeyCode.None },
        new KeyCode[2]{ KeyCode.Alpha3, KeyCode.None },
        new KeyCode[2]{ KeyCode.F, KeyCode.None }
    };


    static InputManager()
    {
        InitializeKeybinds();
    }

    private static void InitializeKeybinds()
    {
        keybinds = new Dictionary<string, KeyCode[]>();
        for (int i = 0; i < defaultKeys.Length; i++)
        {
            keybinds.Add(defaultKeys[i], defaultValues[i]);
        }

        // TODO: check if there are stored keybinds (i.e. in a config file)
    }

    public static bool GetKeyDown(string key)
    {
        foreach (KeyCode val in keybinds[key])
        {
            if (Input.GetKeyDown(val))
                return true;
        }
        return false;
    }

    public static bool GetKey(string key)
    {
        foreach (KeyCode val in keybinds[key])
        {
            if (Input.GetKey(val))
                return true;
        }
        return false;
    }

    public static bool GetKeyUp(string key)
    {
        foreach (KeyCode val in keybinds[key])
        {
            if (Input.GetKeyUp(val))
                return true;
        }
        return false;
    }

    public static void OverwriteKeybind(string key, KeyCode val, int index)
    {
        if (!keybinds.ContainsKey(key) || (index != 0 && index != 1))
            return;
        
        keybinds[key][index] = val;
    }

    public static Dictionary<string, KeyCode[]> GetKeybindDictionary()
    {
        return keybinds;
    }
}
