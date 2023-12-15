using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public AudioClip victorySound;
    public AudioClip defeatSound;

    [SerializeField] GameObject levelManagerObj;
    LevelManager levelManagerSc;
    GameObject Level;
    GameObject victoryParticleObj;
    GameObject ballWall;
    [SerializeField] GameObject startMenuUI;
    [SerializeField] GameObject gameMenuUI;
    [SerializeField] GameObject victoryMenuUI;
    [SerializeField] GameObject defeatMenuUI;
    [SerializeField] GameObject InGameUIHandlerObj;
    GameObject barrierLPivot;
    GameObject barrierRPivot;
    GameObject platformObj;
    GameObject ballCollectorObj;
    GameObject ballCollectorScoreTextObj;
    [SerializeField] private float barrierRotateSpeed = 50;
    [SerializeField] private float platformMoveSpeed = 0.8f;
    private BallCollectorHandler ballCollectorSc;
    private InGameUIHandler InGameUIHandlerSc;
    private bool rotateBarriersFlag = false;
    private bool movePlatFormFlag = false;
    
    void Start()
    {

        startMenuUI.SetActive(true);
        levelManagerSc = levelManagerObj.GetComponent<LevelManager>();
        InGameUIHandlerSc = InGameUIHandlerObj.GetComponent<InGameUIHandler>();

    }

    public void SetObjects(){
        levelManagerSc = levelManagerObj.GetComponent<LevelManager>();
        Level = levelManagerSc.GetLevelPrefab("Current");
        

        Transform TballCollectorObj = Level.transform.Find("BallCollectorParent/BallCollectorInsideTrigger");
        Transform TvictoryParticleObj = Level.transform.Find("BallCollectorParent/VictoryParticles");
        Transform TballWall = Level.transform.Find("BallCollectorParent/BallWall");
        Transform TbarrierLPivot = Level.transform.Find("BarrierParent/BarrierSupportL");
        Transform TbarrierRPivot = Level.transform.Find("BarrierParent/BarrierSupportR");
        Transform TplatformObj = Level.transform.Find("BallCollectorParent/BallCollectorPlatform");
        Transform TballCollectorScoreTextObj = Level.transform.Find("BallCollectorParent/BallCollectorScoreText");


        ballCollectorScoreTextObj = TballCollectorScoreTextObj.gameObject;
        ballCollectorObj = TballCollectorObj.gameObject;
        victoryParticleObj = TvictoryParticleObj.gameObject;
        ballWall = TballWall.gameObject;
        barrierLPivot = TbarrierLPivot.gameObject;
        barrierRPivot = TbarrierRPivot.gameObject;
        platformObj = TplatformObj.gameObject;

        ballCollectorSc = ballCollectorObj.GetComponent<BallCollectorHandler>();

        ballCollectorSc.SetTextObj(ballCollectorScoreTextObj);
    }

    void Update()
    {
        if(rotateBarriersFlag){
        RotateBarriers();
        }
        if(movePlatFormFlag){
        MovePlatform();
        }
    }

    public void GameEndEvents(){

        ThrowBalls();
        StartCoroutine(EnableWall());
        StartCoroutine(CheckGameResult());

    }

    private void ThrowBalls(){
        BallHandler[] balls = FindObjectsOfType<BallHandler>();

        foreach (BallHandler ball in balls)
        {
        ball.throwEvent();
        }
    }

    IEnumerator EnableWall()
    {
        yield return new WaitForSeconds(0.7f);

        ballWall.GetComponent<BoxCollider>().enabled = true;

    }

    IEnumerator CheckGameResult()
    {
        yield return new WaitForSeconds(2f);
        
        if(ballCollectorSc.GameResult()){
            
            VictoryEvent();
        }
        else{
            DefeatEvent();
        }
    }

    IEnumerator EnableVictoryMenu(){

        yield return new WaitForSeconds(1f);
        victoryMenuUI.SetActive(true);
    }

    IEnumerator EnableDefeatMenu(){

        yield return new WaitForSeconds(1f);
        GetComponent<AudioSource>().PlayOneShot(defeatSound,0.2f);
        defeatMenuUI.SetActive(true);
    }

    private void VictoryEvent(){
        victoryParticleObj.GetComponent<ParticleSystem>().Play();
        GetComponent<AudioSource>().PlayOneShot(victorySound,0.5f);
        rotateBarriersFlag = true;
        movePlatFormFlag = true;
        StartCoroutine(EnableVictoryMenu());
        ballCollectorSc.ResetCollectedBallCount();
        
    }
    
    private void DefeatEvent(){

        
        StartCoroutine(EnableDefeatMenu());
        ballCollectorSc.ResetCollectedBallCount();
        ballWall.GetComponent<BoxCollider>().enabled = false;
    }
    
    public void SetInGameLevelText(){
        InGameUIHandlerSc.UpdateLevelText();
    }

    public void DisableDefeatUI(){
        defeatMenuUI.SetActive(false);
    }

    public void DisableVictoryUI(){
        victoryMenuUI.SetActive(false);
    }

    public void DisableStartMenu(){
        startMenuUI.SetActive(false);
        EnableGameMenu();

    }

    public void EnableGameMenu(){
        gameMenuUI.SetActive(true);
        
    }

    private void RotateBarriers(){
        if(barrierLPivot.transform.rotation.eulerAngles.x >= 45f){
            barrierLPivot.transform.Rotate(Vector3.up*Time.deltaTime*barrierRotateSpeed);
        }
        if(barrierRPivot.transform.rotation.eulerAngles.x <= 315f){
            barrierRPivot.transform.Rotate(Vector3.down*Time.deltaTime*barrierRotateSpeed);
        }
        else{
            rotateBarriersFlag = false;
        }
        
    }

    private void MovePlatform(){
        if(platformObj.transform.position.y < 1f){
        platformObj.transform.Translate(Vector3.up *platformMoveSpeed * Time.deltaTime, Space.World);
        }
        else{
            movePlatFormFlag = false;
        }
    }
}
