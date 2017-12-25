using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml;
using System.Xml.Serialization;
using System.IO;

using System.Linq;
using System.Text;

public class SettingsManager: MonoBehaviour {
    
    public static SettingsManager instance = null;
    private Settings settings = null;
    public float Sensitivity { get; set; }
    public float Fov { get; set; }
    public float ZoomSensitivity { get; set; }
    public float ZoomFov { get; set; }
    public Dictionary<string, KeyCode[]> Keybinds { get; private set; }

    // Needed to update settings UI
    public GameplayPanel gpScript;
    public KeybindsPanel kbScript;

    // XML variables
    private string filePath;


    void Awake()
    {
        // Singleton pattern
        instance = this;

        // Get settings file if it exists
        this.filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "settings.xml");

        LoadSettings();
    }

    // Load this.settings from an external file, or use its default values if no such file exists
    public void LoadSettings()
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Settings));
            Encoding encoding = Encoding.GetEncoding("UTF-8");
            StreamReader stream = new StreamReader(filePath, encoding);
            this.settings = serializer.Deserialize(stream) as Settings;
            stream.Close();
            InitSettings();
        }
        catch // TODO: Should be more specific about which exceptions to catch
        {
            // Use default settings if the file is not found
            this.settings = new Settings();
            InitSettings();
            SaveSettings();
        }

        gpScript.RefreshFloatsUI();
        kbScript.RefreshKeybindsUI();
        gpScript.UpdateCameraFov();
    }

    public void InitSettings()
    {
        // Initialize settings
        this.Sensitivity = this.settings.sens;
        this.Fov = this.settings.fov;
        this.ZoomSensitivity = this.settings.sens;
        this.ZoomFov = this.settings.zoomFov;

        InitKeybinds();
    }

    public void InitKeybinds()
    {
        // Initialize keybinds from the settings key/command pair of arrays
        this.Keybinds = new Dictionary<string, KeyCode[]>();
        for (int i = 0; i < this.settings.keys.Length; i++)
        {
            this.Keybinds.Add(this.settings.keys[i], this.settings.commands[i]);
        }
    }

    // Save this.settings to an external file
    public void SaveSettings()
    {
        PushSettings();

        XmlSerializer serializer = new XmlSerializer(typeof(Settings));
        Encoding encoding = Encoding.GetEncoding("UTF-8");
        StreamWriter stream = new StreamWriter(filePath, false, encoding);
        serializer.Serialize(stream, this.settings);
        stream.Close();

        #if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
        #endif
    }

    // Push setting changes to this.settings
    private void PushSettings()
    {
        this.settings.sens = this.Sensitivity;
        this.settings.fov = this.Fov;
        this.settings.zoomSens = this.ZoomSensitivity;
        this.settings.zoomFov = this.ZoomFov;

        this.settings.keys = this.Keybinds.Keys.ToArray();
        this.settings.commands = this.Keybinds.Values.ToArray();
    }

    // Reset this.settings using default values
    public void ResetSettings()
    {
        this.settings = new Settings();
        InitSettings();

        gpScript.RefreshFloatsUI();
        kbScript.RefreshKeybindsUI();
        gpScript.UpdateCameraFov();
    }

    void OnApplicationQuit()
    {
        SaveSettings();
    }
}