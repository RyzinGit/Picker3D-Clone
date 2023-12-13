using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public AudioClip victorySound;
    public AudioClip defeatSound;
    [SerializeField] GameObject ballCollectorObj;
    [SerializeField] GameObject victoryParticleObj;
    [SerializeField] GameObject ballWall;
    [SerializeField] GameObject startMenuUI;
    [SerializeField] GameObject gameMenuUI;
    [SerializeField] GameObject victoryMenuUI;
    [SerializeField] GameObject defeatMenuUI;
    [SerializeField] GameObject barrierLPivot;
    [SerializeField] GameObject barrierRPivot;
    [SerializeField] GameObject platformObj;
    [SerializeField] private float barrierRotateSpeed = 50;
    [SerializeField] private float platformMoveSpeed = 0.8f;
    private BallCollectorHandler ballCollector;
    private bool rotateBarriersFlag = false;
    private bool movePlatFormFlag = false;
    void Start()
    {
        ballCollector = ballCollectorObj.GetComponent<BallCollectorHandler>();
        startMenuUI.SetActive(true);
    }

    void Update()
    {
        if(rotateBarriersFlag){
        rotateBarriers();
        }
        if(movePlatFormFlag){
        movePlatform();
        }
    }

    public void GameEndEvents(){
        throwBalls();
        StartCoroutine(EnableWall());
        StartCoroutine(CheckGameResult());

    }

    private void throwBalls(){
        BallHandler[] balls = FindObjectsOfType<BallHandler>();

        foreach (BallHandler ball in balls)
        {
        ball.throwEvent();
        }
    }

    IEnumerator EnableWall()
    {
        yield return new WaitForSeconds(0.5f);

        ballWall.GetComponent<BoxCollider>().enabled = true;

    }

    IEnumerator CheckGameResult()
    {
        yield return new WaitForSeconds(2f);

        if(ballCollector.currCollectedBallCount >= ballCollector.totalReqBallCount){
            
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

    private void VictoryEvent(){
        victoryParticleObj.GetComponent<ParticleSystem>().Play();
        GetComponent<AudioSource>().PlayOneShot(victorySound,0.5f);
        rotateBarriersFlag = true;
        movePlatFormFlag = true;
        StartCoroutine(EnableVictoryMenu());
        

    }
    private void DefeatEvent(){

        GetComponent<AudioSource>().PlayOneShot(defeatSound,0.2f);
        defeatMenuUI.SetActive(true);
    }

    public void DisableStartMenu(){
        startMenuUI.SetActive(false);
        EnableGameMenu();

    }

    public void EnableGameMenu(){
        gameMenuUI.SetActive(true);
        
    }

    private void rotateBarriers(){
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

    private void movePlatform(){
        if(platformObj.transform.position.y < 1f){
        platformObj.transform.Translate(Vector3.up *platformMoveSpeed * Time.deltaTime, Space.World);
        }
        else{
            movePlatFormFlag = false;
        }
    }
}
