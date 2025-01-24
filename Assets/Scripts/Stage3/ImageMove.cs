using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ImageMove : MonoBehaviour
{
    public GameObject ImagePrefab;
    public RectTransform imageRectTransform;
    public float speed = 3f;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // hensu = Random.Range(0.01f,0.2f);
    }

    // Update is called once per frame
    void Update()
    {
      // if(Time.frameCount % 60 == 0){
      //   Instantiate(ImagePrefab, new Vector3(0,4,0), Quaternion.identify);
        
      // }        
    }
}
