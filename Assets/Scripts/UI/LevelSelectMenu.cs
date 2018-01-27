using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectMenu : MonoBehaviour {
    
    [SerializeField] private string sceneLevelPrefix;

	public void PlayLevel(int i)
    {
		SceneManager.LoadScene(sceneLevelPrefix + i);
    }
}
