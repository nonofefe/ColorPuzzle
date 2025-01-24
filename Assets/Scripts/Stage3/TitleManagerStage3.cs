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

    private GameObject imageprefab;
    private Vector3 targetPosition = new Vector3(1, 1, 0); // Target position for the image
    private float speed = 1f;
    private float timer = 0f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      imageprefab = itemPrefab.transform.Find("ImagePrefab").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
      // GameObject image = itemPrefab.transform.Find("ImagePrefab").gameObject;
      if (imageprefab != null){
        // Move the image towards the target position
        if(timer % 60 == 0){
          imageprefab.transform.localPosition = new Vector3(0,0,0);
        }else{
          imageprefab.transform.localPosition = imageprefab.transform.localPosition + targetPosition * speed;
        }
        timer++;
      }
    }

    public void PushButtonShape(int k){
      print(k);
    }

    public void PushCreateButton(){
      // SceneManager.LoadScene("MainScene3");
      n = prefabList.Count + 1;
      GameObject item = Instantiate(itemPrefab, _Parent) as GameObject;
      item.transform.SetParent(_Parent,false);
      // print(n);
      GameObject button = item.transform.Find("ButtonPrefab").gameObject;
      GameObject image = item.transform.Find("ImagePrefab").gameObject;
      
      button.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = n.ToString();
      int t = n;
      button.GetComponent<Button>().onClick.AddListener(() => PushButtonShape(t));

      ItemTest itemTest = item.AddComponent<ItemTest>();
      prefabList.Add(itemTest);
    }

    public void PushDeleteButton(){
      // if (instantiatedPrefab != null) {
      //   Destroy(instantiatedPrefab); 
      // }
      int index = prefabList.Count-1;
      if (index >= 0 && index < prefabList.Count) {
        print(index);
        ItemTest prefabToDelete = prefabList[index];
        prefabList.RemoveAt(index);
        Destroy(prefabToDelete.gameObject);
        // 指定されたPrefabを削除 
      }
    }
}
