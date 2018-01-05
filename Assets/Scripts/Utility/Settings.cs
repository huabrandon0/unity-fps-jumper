using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Settings {

    public float Sensitivity { get; set; }
    public float Fov { get; set; }
    public float ZoomSensitivity { get; set; }
    public float ZoomFov { get; set; }
    public Dictionary<string, KeyCode[]> Keybinds { get; set; }

    public Settings()
    {
        SerializableSettings ss = new SerializableSettings();
        Set(ss);
    }

    public Settings(Settings s)
    {
        Set(s.AsSerializable());
    }

    public Settings(SerializableSettings ss)
    {
        Set(ss);
    }

    public SerializableSettings AsSerializable()
    {
        SerializableSettings ss = new SerializableSettings();
        ss.sens = this.Sensitivity;
        ss.fov = this.Fov;
        ss.zoomSens = this.ZoomSensitivity;
        ss.zoomFov = this.ZoomFov;
        ss.keys = this.Keybinds.Keys.ToArray();
        ss.commands = this.Keybinds.Values.ToArray();

        return ss;
    }

    public void Set(SerializableSettings ss)
    {
        this.Sensitivity = ss.sens;
        this.Fov = ss.fov;
        this.ZoomSensitivity = ss.zoomSens;
        this.ZoomFov = ss.zoomFov;
        this.Keybinds = new Dictionary<string, KeyCode[]>();
        for (int i = 0; i < ss.keys.Length; i++)
        {
            this.Keybinds.Add(ss.keys[i], ss.commands[i].Clone() as KeyCode[]); // Note: Clone() is important to make sure the argument is deep copied
        }
    }

    public void Set(Settings s)
    {
        Set(s.AsSerializable());
    }
}

public class SerializableSettings {

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