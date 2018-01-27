using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelButtonGenerator : MonoBehaviour {

	[SerializeField] private Button levelButton;
	[SerializeField] private int numLevels;
	[SerializeField] private int maxLevelUnlocked;
	[SerializeField] private string scenePrefix;

	void Start()
	{
		for(int i = 1; i <= numLevels; i++)
		{
			Button lb = Instantiate(this.levelButton) as Button;
			if (i > maxLevelUnlocked)
				lb.interactable = false;
			lb.transform.SetParent(this.transform, false);
			lb.GetComponentInChildren<TextMeshProUGUI>().SetText(i.ToString());
			int levelNum = i;
			lb.onClick.AddListener(() => SceneManager.LoadScene(scenePrefix + levelNum));
		}
	}
}
