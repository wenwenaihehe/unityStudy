using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform startPoint;
    private Transform spawnPoint;
    
    public GameObject pinPrefab;
    void Start()
    {
        startPoint = GameObject.Find("StartPoint").transform;
        spawnPoint = GameObject.Find("SpawnPoint").transform;
        SpawnPin();
        //Destory(this);
    }
    void SpawnPin()
    {
        //Instantiate(pinPrefab,this);
        GameObject wee = GameObject.Instantiate(pinPrefab,spawnPoint.position,pinPrefab.transform.rotation);
        wee.transform.parent = this.transform;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
