using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml;
using System.Xml.Serialization;
using System.IO;

using System.Linq;
using System.Text;

public class ScoreManager : MonoBehaviour {

    public static ScoreManager instance = null; // Singleton

    public string fileName = "scores.xml"; // Name of external file

    public Scores scores { get; private set; }
    private string filePath; // Path to external file

	void Awake ()
    {
        if (ScoreManager.instance == null)
            ScoreManager.instance = this;
        else if (ScoreManager.instance != this)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);

        this.filePath = System.IO.Path.Combine(Application.streamingAssetsPath, fileName);

        LoadScores();
    }

    void OnApplicationQuit()
    {
        //SaveScores();
		//Bug: calling SaveScores() here seems to lose scores instead of saving them
    }
	
    public void LoadScores()
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SerializableScores));
            Encoding encoding = Encoding.GetEncoding("UTF-8");
            StreamReader stream = new StreamReader(filePath, encoding);
            this.scores = new Scores(serializer.Deserialize(stream) as SerializableScores);
            stream.Close();
        }
        catch
        {
            this.scores = new Scores(100);
            SaveScores();
        }
    }

    public void SaveScores()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(SerializableScores));
        Encoding encoding = Encoding.GetEncoding("UTF-8");
        StreamWriter stream = new StreamWriter(filePath, false, encoding);
        serializer.Serialize(stream, this.scores.AsSerializable());
        stream.Close();

        #if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
        #endif
    }

    public bool AddScore(float score, int index)
    {
        if (score < this.scores.ScoresList[index])
        {
            this.scores.ScoresList[index] = score;
            return true;
        }

        return false;
    }
}
