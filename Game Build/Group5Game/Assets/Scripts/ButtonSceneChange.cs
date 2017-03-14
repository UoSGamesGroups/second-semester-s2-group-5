using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonSceneChange : MonoBehaviour
{
    public string sceneToLoad;

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

}


