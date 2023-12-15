using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField] GameObject levelManagerObj;
    LevelManager levelManagerSc;
    [SerializeField] GameObject playerObj;
    PlayerMovement playerMovementSc;
    GameController gameControllerSc;

    private void Start(){
        levelManagerSc = levelManagerObj.GetComponent<LevelManager>();
        playerMovementSc = playerObj.GetComponent<PlayerMovement>();
        gameControllerSc = playerObj.GetComponent<GameController>();
    }

    public int GetLevel(){
        int currLevel = PlayerPrefs.GetInt("Level");
        return currLevel;
    }

    public void SetLevel(int _level){
        PlayerPrefs.SetInt("Level",_level);
    }
    public void SetLevelNextLevel(){
        
        
        if(PlayerPrefs.GetInt("Level") == 6){
            
            levelManagerSc.DestroyCurrLevel();
            PlayerPrefs.SetInt("Level",1);
            levelManagerSc.LevelLoader(1);

        }
        else{
            PlayerPrefs.SetInt("Level",GetLevel()+1);
            levelManagerSc.InitNextLevel(PlayerPrefs.GetInt("Level"));
            levelManagerSc.PlaceBalls(PlayerPrefs.GetInt("Level"));
        }

        playerMovementSc.MoveToLevel();
        gameControllerSc.SetInGameLevelText();
        gameControllerSc.DisableVictoryUI();

    }
    public void SetLevelCurrLevel(){
        
        levelManagerSc.PlaceBalls(PlayerPrefs.GetInt("Level"));

        playerMovementSc.MoveToLevel();
        gameControllerSc.DisableDefeatUI();
    }

}
