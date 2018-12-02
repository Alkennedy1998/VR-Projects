using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour {

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One))
            ChangeScene();
        if (OVRInput.GetDown(OVRInput.Button.Two))
            ChangeScene2();

    }
    public void ChangeScene()
    {
        
        Debug.Log("Scene Change");
        SceneManager.LoadScene("MainScene");
        
    }
    public void ChangeScene2()
    {

        Debug.Log("Scene Change");
        SceneManager.LoadScene("UI");

    }
}
