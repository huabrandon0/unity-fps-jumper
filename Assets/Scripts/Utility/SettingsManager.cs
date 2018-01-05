using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml;
using System.Xml.Serialization;
using System.IO;

using System.Linq;
using System.Text;

public class SettingsManager: MonoBehaviour {
    
    public static SettingsManager instance = null; // Singleton

    public string filename = "settings.xml"; // Name of external file

    private Settings settings = null;
    private string filePath; // Path to the external file


    void Awake()
    {
        if (SettingsManager.instance == null)
            SettingsManager.instance = this;
        else if (SettingsManager.instance != this)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);

        this.filePath = System.IO.Path.Combine(Application.streamingAssetsPath, filename);

        LoadSettings();
    }

    void OnApplicationQuit()
    {
        SaveSettings();
    }

    // Load settings from the external file
    // If the file cannot be accessed, use default settings
    public void LoadSettings()
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SerializableSettings));
            Encoding encoding = Encoding.GetEncoding("UTF-8");
            StreamReader stream = new StreamReader(filePath, encoding);
            this.settings = new Settings(serializer.Deserialize(stream) as SerializableSettings);
            stream.Close();
        }
        catch
        {
            this.settings = new Settings();
            SaveSettings();
        }
    }

    // Save settings to the external file
    public void SaveSettings()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(SerializableSettings));
        Encoding encoding = Encoding.GetEncoding("UTF-8");
        StreamWriter stream = new StreamWriter(filePath, false, encoding);
        serializer.Serialize(stream, this.settings.AsSerializable());
        stream.Close();

        #if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
        #endif
    }

    public Settings GetSettings()
    {
        return new Settings(this.settings);
    }

    // Overwrite settings with another Settings object
    public void OverwriteSettings(Settings s)
    {
        this.settings.Set(s);
    }
}