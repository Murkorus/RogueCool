using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneToLoad; // Change this to the exact name of the scene you want to load
    public void ChangeTheScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
