using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour {

    public void ChangeScene()
    {
        Debug.Log("Scene Change");
        SceneManager.LoadScene("MainScene");
        
    }
}
