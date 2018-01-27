using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using TMPro;

public class KeybindGridGenerator : MonoBehaviour {

    private Dictionary<string, KeyCode[]> keybinds;
    
	public TextMeshProUGUI keybindLabelText;
	public Button keybindValueButton;

//    public GameObject text;
//    public GameObject button;
    
    private bool isSettingKeybind;
    private string keyToSet;
    private int valIndex;
	private Button valButton;


    void Start()
    {
        DrawGrid();
    }
    
    void Update()
    {
        if (this.isSettingKeybind && Input.anyKeyDown)
        {
            // Note: we would rather use Input.inputString instead of a loop, but it doesn't seem to track mouse button input.
            // However, this code only runs once every time the player decides to change keybinds, so I guess it doesn't really matter...
            foreach(KeyCode keycode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keycode))
                {
                    //Debug.Log("New keybind: " + this.keyToSet + ", " + keycode.ToString() + ", " + this.valIndex);
                    // Update the InputManager and button text on the menu to reflect the keybind change
                    InputManager.instance.OverwriteKeybind(this.keyToSet, keycode, this.valIndex);
					this.valButton.GetComponentInChildren<TextMeshProUGUI>().SetText(keycode.ToString());
                    this.isSettingKeybind = false;
                    break;
                }
            }
        }
    }
    
    public void DrawGrid()
    {
        foreach (Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }

        this.keybinds = InputManager.instance.Keybinds;

        foreach(KeyValuePair<string, KeyCode[]> entry in this.keybinds)
        {
			string key = entry.Key;
			TextMeshProUGUI keybindLabel = Instantiate(this.keybindLabelText) as TextMeshProUGUI;
			keybindLabel.transform.SetParent(this.transform, false);
			keybindLabel.SetText(key);

			for (int i = 0; i < entry.Value.Length; i++)
			{
				int index = i;
				KeyCode val = entry.Value[index];
				Button keybindValue = Instantiate(this.keybindValueButton) as Button;
				keybindValue.transform.SetParent(this.transform, false);
				keybindValue.GetComponentInChildren<TextMeshProUGUI>().SetText(val.ToString());
				keybindValue.GetComponent<Button>().onClick.AddListener(() => SetKeybind(key, val, index, keybindValue));
			}

//            string key = entry.Key;
//            GameObject keyText = Instantiate(this.text) as GameObject;
//            keyText.transform.SetParent(this.transform, false);
//            keyText.GetComponent<Text>().text = key;

//            for (int i = 0; i < entry.Value.Length; i++)
//            {
//                int index = i;
//                KeyCode val = entry.Value[index];
//                GameObject valBtn = Instantiate(this.button) as GameObject;
//                valBtn.transform.SetParent(this.transform, false);
//                valBtn.GetComponentInChildren<Text>().text = val.ToString();
//                valBtn.GetComponent<Button>().onClick.AddListener(() => SetKeybind(key, val, index, valBtn));
//            }
        }
    }
	
    // Start the "in the middle of setting a keybind" mode
	void SetKeybind(string key, KeyCode val, int index, Button valBtn)
    {
        //Debug.Log("Setting: " + key + ", " + val + ", " + index);
        this.isSettingKeybind = true;
        this.keyToSet = key;
        this.valIndex = index;
        this.valButton = valBtn;
    }
}
