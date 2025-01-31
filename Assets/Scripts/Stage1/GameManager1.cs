using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;

public class GameManager1 : MonoBehaviour
{
  // 各図形に該当するint
	public const int YELLOW_CIRCLE = 0;
	public const int YELLOW_TRIANGLE = 1;
	public const int YELLOW_SQUARE = 2;
	public const int RED_CIRCLE = 3;
	public const int RED_TRIANGLE = 4;
	public const int RED_SQUARE = 5;
	public const int BLUE_CIRCLE = 6;
	public const int BLUE_TRIANGLE = 7;
	public const int BLUE_SQUARE = 8;

  // 初期のアイテム数
  public const int INIT_ITEM_COUNT = 3;
  
  // imageに描くObjectの配列
  public Sprite[] Obje = new Sprite[9];

  // 表示するテキスト
  public TMP_Text scoreDisplay;
	public TMP_Text TextGameOver;
	public TMP_Text textHighestScore;
  
  // 非表示にするボタンはGameObjectで実装
  public GameObject ButtonRetry;

  // GameのUIで表示するImage
	public GameObject ImageTarget;

	// public AudioClip clearSE;
	// public AudioClip GameBGM;
	private AudioSource source;

  // ロジックのための変数
  public int shapeNum;
  public int score = 0;
	public int highestScore = 0;
  private int[] buttonselect = new int[99];
  private int target;

  // SerializeField：privateな値をConsole出力するためのもの
  [SerializeField] private GameObject itemPrefab;
  [SerializeField] private Transform itemGroup;

  // private List<GameObject> itemList = new List<GameObject>();
  private List<GameObject> itemList = new List<GameObject>();

  // スクリプトが適用されたオブジェクトがアクティブになった際に、一度だけ呼び出される
  void Start(){
    InitPrefab();
    InitGame();
  }

  // 毎フレーム実行される処理
  void Update(){
        
  }

  // ゲーム開始時に呼ばれるメソッド
  void InitGame(){
    // スコアの初期化
    DisplayScore(0);
    // アイテム数を元の数に戻す
    ChangeItemCount(INIT_ITEM_COUNT);
    // ターゲットアイテムを元に戻す
    InitTarget();
    // アイテム3つの図形を初期化
    InitItem3();

		// source = gameObject.GetComponent<AudioSource>();
    ButtonRetry.SetActive(false);
    TextGameOver.gameObject.SetActive (false);
  }

  // プレハブの初期設定
  void InitPrefab(){
    int n = itemList.Count;
    GameObject button = itemPrefab.transform.Find("ButtonPrefab").gameObject;
    GameObject image = itemPrefab.transform.Find("ImagePrefab").gameObject;
    button.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = n.ToString();
    button.GetComponent<Button>().onClick.AddListener(() => PushButtonItem(n));
    ItemTest itemTest = itemPrefab.AddComponent<ItemTest>();
    itemList.Add(itemPrefab);
  }

  // ターゲットの図形を初期値で設定
  void InitTarget(){
    target = YELLOW_CIRCLE;
    ImageTarget.GetComponent<Image> ().sprite = Obje[0];
		ImageTarget.SetActive(true);
  }

  // アイテム3つの図形を初期値で設定
  void InitItem3(){
    for(int i = 0;i < 3;i++){
      GameObject button = itemList[i].transform.Find("ButtonPrefab").gameObject;
      GameObject image = itemList[i].transform.Find("ImagePrefab").gameObject;
      buttonselect[i] = i*3;
			button.GetComponent<Image> ().sprite = Obje [i*3];
			image.GetComponent<Image> ().sprite = Obje [i*3];
			button.SetActive (true);
			image.SetActive (false);
		}
  }

  // アイテムの個数を設定
  void ChangeItemCount(int num){
    if(num > itemList.Count){
      while(itemList.Count != num){
        CreateItem();
      }
    }else{
      while(itemList.Count != num){
        print(itemList.Count);
        DeleteItem();
      }
    }
  }

  // アイテムを1つ作成
  void CreateItem(){
    int n = itemList.Count;
    GameObject item = Instantiate(itemPrefab, itemGroup) as GameObject;
    item.transform.SetParent(itemGroup,false);
    GameObject button = item.transform.Find("ButtonPrefab").gameObject;
    GameObject image = item.transform.Find("ImagePrefab").gameObject;
    button.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = n.ToString();
    button.GetComponent<Button>().onClick.AddListener(() => PushButtonItem(n));
    itemList.Add(item);
  }

  // アイテムを1つ削除
  void DeleteItem(){
    int index = itemList.Count-1;
    if (index >= 0 && index < itemList.Count) {
      print(index);
      GameObject prefabToDelete = itemList[index];
      itemList.RemoveAt(index);
      Destroy(prefabToDelete);
    }
  }

  // 左からbuttonNo番目の図形が押された際の挙動
  void PushButtonItem(int buttonNo){
    // 一旦buttonを非表示、imageを表示にする
		for (int i = 0; i < itemList.Count; i++) {
			itemList[i].transform.Find("ButtonPrefab").gameObject.SetActive(false);
      itemList[i].transform.Find("ImagePrefab").gameObject.SetActive(true);
		}
    // スコアの更新
    score += target+1;
		StartCoroutine(PostProcess(buttonNo));
	}

  // 後処理
  IEnumerator PostProcess(int buttonNo){
    yield return StartCoroutine(ItemMove(buttonNo));
    ItemUpdate(buttonNo);
    DisplayScore (score);
    CheckGameOver();
	}

  // Itemを動かすアニメーション
  IEnumerator ItemMove(int buttonNo){
    GameObject itemSelected = itemList[buttonNo];
    GameObject imageSelected = itemSelected.transform.Find("ImagePrefab").gameObject;
    GameObject buttonSelected = itemSelected.transform.Find("ButtonPrefab").gameObject;
    RectTransform rectTransform = imageSelected.GetComponent<RectTransform>();
    Vector3 startPosition = rectTransform.position;
    Vector3 targetPosition = ImageTarget.GetComponent<RectTransform>().position;
    float duration = 0.5f;
    float elapsedTime = 0f;
    while(elapsedTime < duration){
      rectTransform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime/duration);
      elapsedTime += Time.deltaTime;
      yield return null;
    }
    rectTransform.position = startPosition;
  }

  public void ItemUpdate(int buttonNo){
    GameObject itemSelected = itemList[buttonNo];
    GameObject imageSelected = itemSelected.transform.Find("ImagePrefab").gameObject;
    GameObject buttonSelected = itemSelected.transform.Find("ButtonPrefab").gameObject;
    // 以下itemとtargetの形の更新
    target = buttonselect [buttonNo];
		ImageTarget.GetComponent<Image> ().sprite = Obje [target];
		buttonselect [buttonNo] = UnityEngine.Random.Range(0, 9);
		buttonSelected.GetComponent<Image> ().sprite = Obje [buttonselect [buttonNo]];
		imageSelected.GetComponent<Image> ().sprite = Obje [buttonselect [buttonNo]];
    // 以下それぞれのitemがpushできるかの設定
    for (int i = 0; i < itemList.Count; i++) {
      GameObject button = itemList[i].transform.Find("ButtonPrefab").gameObject;
      GameObject image = itemList[i].transform.Find("ImagePrefab").gameObject;
			if ((buttonselect[i] / 3 != target / 3) && (buttonselect [i] % 3 != target % 3)) {
				button.SetActive (false);
				image.SetActive (true);
			} else {
				button.SetActive (true);
				image.SetActive (false);
			}
		}
  }

  public void CheckGameOver(){
    if (itemList.All(item => !item.transform.Find("ButtonPrefab").gameObject.activeSelf)) {
      GameOver();
    }
  }

  public void GameOver(){
    print(score);
    TextGameOver.text = "GAME OVER!\n";
		TextGameOver.text += "得点 : " + score.ToString();
    TextGameOver.gameObject.SetActive (true);
		StartCoroutine ("RetryShowAnimation");
	}

	IEnumerator RetryShowAnimation(){
		// source.Stop();
		// source.PlayOneShot(clearSE);
		yield return new WaitForSeconds(1);
		ButtonRetry.SetActive(true);
	}


	public void DisplayScore(int score){
    this.score = score;
		scoreDisplay.text = "得点 : " + score.ToString();
	}

  public void PushButtonReset(){
		if(highestScore < score){  
			textHighestScore.text = "最高得点 : " + score.ToString();
			highestScore = score;
		}
		// source.PlayOneShot(GameBGM);
		InitGame();
	}
}
