using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleManagerStage3 : MonoBehaviour
{
    [SerializeField] private GameObject _btnPrefab;
    [SerializeField] private Transform _Parent;

    int n;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PushButtonShape(int k){
      print(k);
    }

    public void PushStartButton(){
      // SceneManager.LoadScene("MainScene3");
      GameObject button = Instantiate(_btnPrefab, _Parent) as GameObject;
      button.transform.SetParent(_Parent,false);
      // print(n);
      button.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = n.ToString();
      //以下、追加---------
      //引数に何番目のボタンかを渡す
      int t = n;
      button.GetComponent<Button>().onClick.AddListener(() => PushButtonShape(t));
      n++;
    }
}
