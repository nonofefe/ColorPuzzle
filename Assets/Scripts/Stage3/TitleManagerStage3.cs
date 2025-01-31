using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleManagerStage3 : MonoBehaviour
{
    // 一旦動かすimageは別で作成しておいて、そちらをtargetとpushしたオブジェクトの座標の内分点とすることでアニメーションを制御するように設定する方針にする。

    // SerializeField：privateな値をConsole出力するためのもの
    [SerializeField] private GameObject itemPrefab;
    // [SerializeField] private GameObject _btnPrefab;
    [SerializeField] private Transform _Parent;

    // private List<GameObject> prefabList = new List<GameObject>();
    private List<ItemTest> prefabList = new List<ItemTest>();
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
      GameObject item = Instantiate(itemPrefab, _Parent) as GameObject;
      item.transform.SetParent(_Parent,false);
      GameObject button = item.transform.Find("ButtonPrefab").gameObject;
      GameObject image = item.transform.Find("ImagePrefab").gameObject;
      
      button.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = n.ToString();
      int t = n;
      button.GetComponent<Button>().onClick.AddListener(() => PushButtonShape(t));

      ItemTest itemTest = item.AddComponent<ItemTest>();
      prefabList.Add(itemTest);
    }

    public void PushDeleteButton(){
      int index = prefabList.Count-1;
      if (index >= 0 && index < prefabList.Count) {
        print(index);
        ItemTest prefabToDelete = prefabList[index];
        prefabList.RemoveAt(index);
        Destroy(prefabToDelete.gameObject);
      }
    }
}
