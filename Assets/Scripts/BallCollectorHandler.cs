using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class BallCollectorHandler : MonoBehaviour
{ 
    public AudioClip popSound;
    [SerializeField] private TextMeshPro collecedBallText;
    public int currCollectedBallCount = 0;
    public int totalReqBallCount = 20;
    // Start is called before the first frame update
    void Start()
    {
        collecedBallText.text = "0/" + totalReqBallCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider coll){

         if (coll.gameObject.CompareTag("Ball")){
            currCollectedBallCount++;
            collecedBallText.text = currCollectedBallCount.ToString() + " / " + totalReqBallCount.ToString();
            StartCoroutine(PlayPopSound());
         }
    }

    IEnumerator PlayPopSound()
    {
        yield return new WaitForSeconds(Random.Range(0.01f, 0.06f));

        GetComponent<AudioSource>().PlayOneShot(popSound,0.4f);
    }

}
