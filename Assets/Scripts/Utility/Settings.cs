using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Settings {
    
    public float sens = 1f;
    public float zoomSens = 0.5f;
    public float fov = 80f;
    public float zoomFov = 40f;

    public string[] keys = new string[]
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

    public KeyCode[][] commands = new KeyCode[][]
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
}
