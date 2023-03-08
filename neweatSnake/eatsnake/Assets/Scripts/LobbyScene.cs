using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

class LobbyScene : MonoBehaviour
{
    public void Awake()
    {
        Application.targetFrameRate = 60;
    }
    public void onStartBtn()
    {
        SceneManager.LoadScene("GameScene");
    }
}

