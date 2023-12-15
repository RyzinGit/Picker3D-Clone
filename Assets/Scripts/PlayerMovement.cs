using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public AudioClip popSound;
    static public bool endGame = false;
    static public bool startGame = false;
    [SerializeField] private float horiztontalSpeed = 10f;
    [SerializeField] private float verticalSpeed = 5f;
    [SerializeField] GameController gameControllerSc;
    private Vector3 mousePos;
    private float horizontalChange;
    void Start(){
        MoveToLevel();
    }

    void Update()
    {

        if(!startGame){
            if(Input.GetMouseButtonDown(0) == true){
                startGame = true;
                gameControllerSc.DisableStartMenu();
            }
        }
        
         if (Input.GetMouseButtonDown(0))
            {
                mousePos = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {

                horizontalChange = (Input.mousePosition.x - mousePos.x) / Screen.width;
                mousePos = Input.mousePosition;
            }
            else
            {
                horizontalChange = 0;
            }   
        
    }

    private void FixedUpdate()
    {
        if(!endGame && startGame){
            GetComponent<Rigidbody>().MovePosition(new Vector3(Mathf.Clamp(transform.position.x + (horizontalChange * horiztontalSpeed * Time.fixedDeltaTime), -1f, 1f),transform.position.y, transform.position.z + verticalSpeed * Time.fixedDeltaTime));
        }
    
    }

    private void OnTriggerEnter(Collider coll){

         if (coll.gameObject.CompareTag("Ball")){
            StartCoroutine(PlayPopSound());
            coll.gameObject.GetComponent<BallHandler>().PickedEvent(true);
         }

        if (coll.gameObject.CompareTag("BallCollectorEntryTrigger")){
            if(!endGame){
            GetComponent<GameController>().GameEndEvents();
            }
            
            endGame = true;
        }
         
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            other.gameObject.GetComponent<BallHandler>().PickedEvent(false);
        }
    }

    IEnumerator PlayPopSound()
    {
        yield return new WaitForSeconds(Random.Range(0.01f, 0.04f));

        GetComponent<AudioSource>().PlayOneShot(popSound,0.4f);
    }

    public void MoveToLevel(){
        if(PlayerPrefs.GetInt("Level") != 0){
            transform.position = new Vector3(0,transform.position.y,-2+((PlayerPrefs.GetInt("Level")-1)*25.3f) );
        }
        else{
            PlayerPrefs.SetInt("Level",1);
            transform.position = new Vector3(0,transform.position.y,-2+((PlayerPrefs.GetInt("Level")-1)*25.3f) );
        }
        
        endGame = false;

    }

}
