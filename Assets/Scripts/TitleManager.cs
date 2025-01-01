using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    void start(){

    }

    void update(){

    }

    public void PushStartButton(){
      SceneManager.LoadScene("StageTitle1");
    }
}