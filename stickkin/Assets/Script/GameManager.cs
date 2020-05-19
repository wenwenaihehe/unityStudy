using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform startPoint;
    private Transform spawnPoint;
    

    public GameObject pinPrefab;
    private Pin CurrentPin;
    private bool isover = false;
    public Text score;
    private int sc = 0;
    void Start()
    {
        score.text = "0";
        startPoint = GameObject.Find("StartPoint").transform;
        spawnPoint = GameObject.Find("SpawnPoint").transform;
        SpawnPin();
        //Destory(this);
    }
    void SpawnPin()
    {
        //Instantiate(pinPrefab,this);
        CurrentPin = GameObject.Instantiate(pinPrefab,spawnPoint.position,pinPrefab.transform.rotation).GetComponent<Pin>();
    }
    // Update is called once per frame
    void Update()
    {
        if(isover) return ;
        if(Input.GetMouseButtonDown(0))
        {
            sc++;
            score.text = sc.ToString();
            CurrentPin.StartFly();
            SpawnPin();
        }
    }
    public void GameOver()
    {
        if(isover) return ;
        GameObject.Find("Circle").GetComponent<RoateSelf>().enabled = false;
        isover = true;
    }
}
