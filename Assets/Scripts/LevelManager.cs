using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelManager : MonoBehaviour
{
    public int GetLevel(){
        int currLevel = PlayerPrefs.GetInt("Level");
        return currLevel;
    }

    public void SetLevel(int _level){
        PlayerPrefs.SetInt("Level",_level);
    }
    public void SetLevelNextLevel(){
        PlayerPrefs.SetInt("Level",GetLevel()+1);
    }
    public void SetLevelCurrLevel(){
        PlayerPrefs.SetInt("Level",GetLevel());
    }

    private void Start(){
        if(PlayerPrefs.GetInt("Level") == 0){
            LevelLoader(1);
        }
    }

    private void LevelLoader(int requestedLevel){
        var currLevel = AssetDatabase.LoadAssetAtPath("Assets/Scenes/Levels/Level"+requestedLevel+".prefab", typeof(GameObject));
        var nextLevel = AssetDatabase.LoadAssetAtPath("Assets/Scenes/Levels/Level"+(requestedLevel+1)+".prefab", typeof(GameObject));
        Instantiate(currLevel, new Vector3(0,0,-2+((requestedLevel-1)*25.3f)), Quaternion.identity);
        Instantiate(nextLevel, new Vector3(0,0,-2+(requestedLevel*25.3f)), Quaternion.identity);        
    }
}
