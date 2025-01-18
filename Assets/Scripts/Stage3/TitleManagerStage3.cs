using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleManagerStage3 : MonoBehaviour
{
    // SerializeField：privateな値をConsole出力するためのもの
    [SerializeField] private GameObject _btnPrefab;
    [SerializeField] private Transform _Parent;

    private List<GameObject> prefabList = new List<GameObject>();

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

    public void PushCreateButton(){
      // SceneManager.LoadScene("MainScene3");
      n = prefabList.Count + 1;
      GameObject button = Instantiate(_btnPrefab, _Parent) as GameObject;
      button.transform.SetParent(_Parent,false);
      // print(n);
      button.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = n.ToString();
      //以下、追加---------
      //引数に何番目のボタンかを渡す
      int t = n;
      button.GetComponent<Button>().onClick.AddListener(() => PushButtonShape(t));
      prefabList.Add(button);
    }

    public void PushDeleteButton(){
      // if (instantiatedPrefab != null) {
      //   Destroy(instantiatedPrefab); 
      // }
      int index = prefabList.Count-1;
      if (index >= 0 && index < prefabList.Count) {
        GameObject prefabToDelete = prefabList[index];
        prefabList.RemoveAt(index);
        Destroy(prefabToDelete);
        // 指定されたPrefabを削除 
      }
    }
}
