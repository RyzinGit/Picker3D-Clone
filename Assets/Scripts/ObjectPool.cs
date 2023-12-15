using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    private List<GameObject> pooledBalls = new List<GameObject>();
    [SerializeField] private int totalPooledObjectCount = 60;
    [SerializeField] private GameObject ballPrefab;
    private void Awake(){
        if(instance = null){
            instance = this;
        }
    }

    void Start()
    {
        for(int i=0 ; i < totalPooledObjectCount ; i++){
            GameObject obj = Instantiate(ballPrefab);
            obj.SetActive(false);
            pooledBalls.Add(obj);
        }
        
    }

    public GameObject GetPooledBall(){
        for(int i = 0 ; i < pooledBalls.Count; i++){
            if(!pooledBalls[i].activeInHierarchy){
                pooledBalls[i].GetComponent<MeshRenderer>().enabled = true;
                return pooledBalls[i];
            }
        }

        GameObject obj = Instantiate(ballPrefab);
        obj.SetActive(false);
        pooledBalls.Add(obj);
        return obj;
    }

}