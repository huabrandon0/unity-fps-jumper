using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class RecordsGenerator : MonoBehaviour {

	[SerializeField] private TextMeshProUGUI recordText;
	[SerializeField] private int numLevels;
	[SerializeField] private int maxLevelUnlocked;

	void Start()
	{
		var scoreList = ScoreManager.instance.scores.ScoresList;

		for(int i = 0; i < scoreList.Count && i < this.numLevels; i++)
		{
			int levelNum = i + 1;
			TextMeshProUGUI rtLabel = Instantiate(this.recordText) as TextMeshProUGUI;
			rtLabel.transform.SetParent(this.transform, false);
			rtLabel.SetText("Level " + levelNum);

			TextMeshProUGUI rtTime = Instantiate(this.recordText) as TextMeshProUGUI;
			rtTime.transform.SetParent(this.transform, false);
			if (levelNum > this.maxLevelUnlocked || scoreList[i] == float.MaxValue)
				rtTime.SetText("N/A");
			else
				rtTime.SetText(Util.GetTimeString(scoreList[i]));
		}
	}
}
