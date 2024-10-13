using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject CanvasPause;
    void Start(){
        CanvasPause.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }

        Time.timeScale = isPaused ? 0 : 1;

        if(isPaused){
            CanvasPause.SetActive(true);
        }else{
            CanvasPause.SetActive(false);
        }
    }

    public void PauseGame(){
        isPaused = !isPaused;
    }
}
