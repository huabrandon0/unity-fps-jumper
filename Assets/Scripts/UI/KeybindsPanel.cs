using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeybindsPanel : MonoBehaviour {

    [SerializeField] private KeybindGridGenerator kggScript;

    public void RefreshKeybindsUI()
    {
        kggScript.DrawGrid();
    }
}
