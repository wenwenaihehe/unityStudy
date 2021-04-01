using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    // Use this for initialization
    [RuntimeInitializeOnLoadMethod]
    static void Initialize()
    {
        string startSceneName = "SampleScene";
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name.Equals(startSceneName))
        {
            return;
        }
        SceneManager.LoadScene(startSceneName);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
