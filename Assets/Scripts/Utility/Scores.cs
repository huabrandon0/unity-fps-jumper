using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scores {

    public List<float> ScoresList { get; private set; }

    public Scores()
    {
        SerializableScores ss = new SerializableScores();
        Set(ss);
    }

    public Scores(SerializableScores ss)
    {
        Set(ss);
    }

    public Scores(Scores s)
    {
        Set(s);
    }

    public SerializableScores AsSerializable()
    {
        SerializableScores ss = new SerializableScores();
        ss.scoresArray = this.ScoresList.ToArray();

        return ss;
    }

    public void Set(SerializableScores ss)
    {
        this.ScoresList = new List<float>(ss.scoresArray);
    }

    public void Set(Scores s)
    {
        Set(s.AsSerializable());
    }
}

[System.Serializable]
public class SerializableScores {

    public float[] scoresArray = new float[]
    {
        float.MaxValue
    };
}
