using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHandler : MonoBehaviour
{
    bool pickedFlag = false;
    bool throwExecute = false;

    public void PickedEvent(bool isPicked){

        pickedFlag = isPicked;
    }
    
    public void throwEvent(){
        if(pickedFlag){
        throwExecute = true;
        StartCoroutine(BallPopAnim());
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

    IEnumerator BallPopAnim()
    {
        yield return new WaitForSeconds(Random.Range(1f, 1.5f));

        GetComponent<ParticleSystem>().Play();
        GetComponent<MeshRenderer>().enabled = false;
    }


}
