using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelEditor : MonoBehaviour
{
    [SerializeField]GameObject inputFieldLevel;
    [SerializeField]GameObject inputFieldTotalBall;
    [SerializeField]GameObject inputFieldToCollectBall;
    public void Save(){

        string inputFieldLevelText = inputFieldLevel.GetComponent<TMP_InputField>().text;
        string inputFieldTotalBallText = inputFieldTotalBall.GetComponent<TMP_InputField>().text;
        string inputFieldToCollectBallText = inputFieldToCollectBall.GetComponent<TMP_InputField>().text;

        PlayerPrefs.SetInt("Level"+inputFieldLevelText+"TotalBallCount",int.Parse(inputFieldTotalBallText));
        PlayerPrefs.SetInt("Level"+inputFieldLevelText+"ToCollectBallCount",int.Parse(inputFieldToCollectBallText));
        
        Debug.LogWarning("Level"+inputFieldLevelText+" is changed to; TotalBallCount:"+inputFieldTotalBallText+ ", ToCollectBallCount:"+inputFieldToCollectBallText);
    }
}
