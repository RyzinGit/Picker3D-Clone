using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class BallHandler : MonoBehaviour
{
    bool pickedFlag = false;
    bool throwExecute = false;
    bool tagToPop = false;

    public void PickedEvent(bool isPicked){

        pickedFlag = isPicked;
    }
    
    public void throwEvent(){
        if(pickedFlag){
        throwExecute = true;
        }
     StartCoroutine(PopOtherBalls());
    }

    private void Update(){
        if(tagToPop){
        StartCoroutine(BallPopAnim());
        tagToPop = false;
        }
    }

    private void FixedUpdate(){
        if(throwExecute){
            
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        GetComponent<Rigidbody>().AddForce(new Vector3(0,50,4000f));
        
        throwExecute = false;
        }
    }

    private void OnTriggerEnter(Collider coll){

         if (coll.gameObject.CompareTag("BallCollectorInsideTrigger")){
            tagToPop = true;
         }
    }

    IEnumerator BallPopAnim()
    {
        yield return new WaitForSeconds(Random.Range(1f, 1.5f));
        GetComponent<ParticleSystem>().Play();
        GetComponent<MeshRenderer>().enabled = false;
        StartCoroutine(DisableBall());
        

    }

    IEnumerator DisableBall()
    {
        yield return new WaitForSeconds(Random.Range(1f, 2f));
        gameObject.SetActive(false);

    }

    IEnumerator PopOtherBalls()
    {
        yield return new WaitForSeconds(3.5f);
        GetComponent<MeshRenderer>().enabled = false;
        StartCoroutine(DisableBall());

    }


}

