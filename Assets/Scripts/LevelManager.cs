using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject objPoolObj;
    private GameObject currLevelPrefab;
    private GameObject nextLevelPrefab;

    [SerializeField] GameObject gameControllerObj;
    GameController gameControllerSc;


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
        //PlayerPrefs.SetInt("Level",0); // DEBUG
        gameControllerSc = gameControllerObj.GetComponent<GameController>();
        
        //setLevels
        PlayerPrefs.SetInt("Level1TotalBallCount",15);
        PlayerPrefs.SetInt("Level1ToCollectBallCount",5);

        PlayerPrefs.SetInt("Level2TotalBallCount",20);
        PlayerPrefs.SetInt("Level2ToCollectBallCount",10);
        
        PlayerPrefs.SetInt("Level3TotalBallCount",18);
        PlayerPrefs.SetInt("Level3ToCollectBallCount",10);

        PlayerPrefs.SetInt("Level4TotalBallCount",30);
        PlayerPrefs.SetInt("Level4ToCollectBallCount",15);

        PlayerPrefs.SetInt("Level5TotalBallCount",35);
        PlayerPrefs.SetInt("Level5ToCollectBallCount",20);

        PlayerPrefs.SetInt("Level6TotalBallCount",50);
        PlayerPrefs.SetInt("Level6ToCollectBallCount",25);

        LevelLoader(PlayerPrefs.GetInt("Level"));
    }

    private void LevelLoader(int requestedLevel){
        if(requestedLevel == 0 || requestedLevel == 7){
            PlayerPrefs.SetInt("Level",1);
        }
        var currLevel = AssetDatabase.LoadAssetAtPath("Assets/Scenes/Levels/Level"+requestedLevel+".prefab", typeof(GameObject));
        Debug.Log(currLevel);
        var nextLevel = AssetDatabase.LoadAssetAtPath("Assets/Scenes/Levels/Level"+(requestedLevel+1)+".prefab", typeof(GameObject));
        Debug.Log(nextLevel+" DEBUG");
        if(currLevel != null){
            currLevelPrefab = Instantiate(currLevel, new Vector3(0,0,-2+((requestedLevel-1)*25.3f)), Quaternion.identity) as GameObject;
        }
        else{
            PlayerPrefs.SetInt("Level",1);
            LevelLoader(1);
        }
        if(nextLevel != null){
            nextLevelPrefab = Instantiate(nextLevel, new Vector3(0,0,-2+(requestedLevel*25.3f)), Quaternion.identity) as GameObject;
        }
        
        PlaceBalls(requestedLevel);

    }

    public void PlaceBalls(int requestedLevel){

       int currLevelBallCount = PlayerPrefs.GetInt("Level"+requestedLevel+"TotalBallCount");

       for(int i = 0 ; i < currLevelBallCount ; i++){
        GameObject ball = objPoolObj.GetComponent<ObjectPool>().GetPooledBall();
        ball.transform.position = new Vector3(Random.Range(-1f, 1f),1.22f,1+((requestedLevel-1)*25.3f+Random.Range(0, 15f)));
        ball.SetActive(true);
       }
       
       gameControllerSc.SetObjects();

    }

    public GameObject GetLevelPrefab(string requested){
        Debug.Log("GetLevelPrefab");
        if(requested == "Current"){
            //Debug.Log(currLevelPrefab);
            return currLevelPrefab;
        }
        else if (requested == "Next"){
            return nextLevelPrefab;
        }
        else{
            return null;
        }
    }

    public void InitNextLevel(int requestedLevel){
        Debug.Log("exec");
        var currLevelPrefabTemp = currLevelPrefab;
        
        currLevelPrefab = nextLevelPrefab;
        Destroy(currLevelPrefabTemp);

        var nextLevel = AssetDatabase.LoadAssetAtPath("Assets/Scenes/Levels/Level"+(requestedLevel+1)+".prefab", typeof(GameObject));
        if(nextLevel != null){
            nextLevelPrefab = Instantiate(nextLevel, new Vector3(0,0,-2+(requestedLevel*25.3f)), Quaternion.identity) as GameObject;
        }
        else{
            PlayerPrefs.SetInt("Level",1);
            LevelLoader(1);
        }
        
        gameControllerSc.SetObjects();

    }
}
