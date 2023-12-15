using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallCollectorHandler : MonoBehaviour
{ 
    public AudioClip popSound;
    [SerializeField] private TextMeshPro collecedBallText;
    private int currCollectedBallCount = 0;
    private int totalReqBallCount = 0;
    void Update()
    {
        totalReqBallCount = PlayerPrefs.GetInt("Level"+PlayerPrefs.GetInt("Level")+"ToCollectBallCount");
        collecedBallText.text = currCollectedBallCount + " / " + totalReqBallCount.ToString();
    }

    private void OnTriggerEnter(Collider coll){
        
         if (coll.gameObject.CompareTag("Ball")){
            currCollectedBallCount++;
            //Debug.Log(currCollectedBallCount);
            collecedBallText.text = currCollectedBallCount.ToString() + " / " + totalReqBallCount.ToString();
            StartCoroutine(PlayPopSound());
         }
    }

    IEnumerator PlayPopSound()
    {
        yield return new WaitForSeconds(Random.Range(0.01f, 0.06f));

        GetComponent<AudioSource>().PlayOneShot(popSound,0.4f);
    }

    public bool GameResult(){
        if(currCollectedBallCount >= totalReqBallCount){
            return true;
        }
        else{
            return false;
        }
    }

    public void SetTextObj(GameObject textObj){
        collecedBallText = textObj.GetComponent<TextMeshPro>();
        //Debug.Log(collecedBallText);

    }

    public void ResetCollectedBallCount(){
        StartCoroutine(ResetCollectedBallCountCoroutine());
        
    }

        IEnumerator ResetCollectedBallCountCoroutine()
    {
        yield return new WaitForSeconds(1f);

        currCollectedBallCount = 0;
    }

}
