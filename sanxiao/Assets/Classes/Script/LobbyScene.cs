using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyScene : MonoBehaviour
{
    private GameObject m_pStartButton;
    // Start is called before the first frame update
    void Start()
    {
        m_pStartButton = GameObject.Find("button");
        m_pStartButton.GetComponent<Button>().onClick.AddListener(onBtnStartGame);
    }

    void onBtnStartGame()
    {
        SceneManager.LoadScene("GameScene");
        //int t = 1;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
