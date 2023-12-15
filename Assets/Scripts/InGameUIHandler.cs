using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameUIHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI LevelText;

    void Start()
    {
        UpdateLevelText();
        
    }

    public void UpdateLevelText(){

        int level = PlayerPrefs.GetInt("Level");
        LevelText.text = "Level:" + level.ToString();

    }
}
