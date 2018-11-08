using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScene : MonoBehaviour {

    public void ChangeScene()
    {
        Debug.log("Scene Change");
        SceneManager.LoadScene("MainScene");
        
    }
}
