using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectMenu : MonoBehaviour {
    
    [SerializeField] private string scene1;

    public void PlayLevel()
    {
        SceneManager.LoadScene(scene1);
    }
}
