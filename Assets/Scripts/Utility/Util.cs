using UnityEngine;
using UnityEngine.Events;
using System;

public static class Util {

    public static void SetLayersRecursively(GameObject obj, int layer)
    {
        if (obj == null)
            return;

        if (layer < 0)
        {
            Debug.LogError("A layer must be greater than or equal to 0");
            return;
        }

        obj.layer = layer;

        foreach (Transform child in obj.transform)
        {
            if (child == null)
                continue;

            SetLayersRecursively(child.gameObject, layer);
        }
    }

    public static string GetTimeString(float time)
    {
        System.TimeSpan ts = System.TimeSpan.FromSeconds(time);
        return string.Format("{0:00}:{1:00}.{2:000}", ts.Minutes, ts.Seconds, ts.Milliseconds);
    }
}