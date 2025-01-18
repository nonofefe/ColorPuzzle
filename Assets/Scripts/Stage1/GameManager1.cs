using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

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
  
  // 表示するテキスト
  public TMP_Text scoreDisplay;
	public TMP_Text TextGameOver;
	public TMP_Text textHighestScore;

  // imageに描くObjectの配列
  public Sprite[] Obje = new Sprite[9];
  
	// 無効化するボタンはButtonで実装　-> 結局はGameObjectで非表示にするように変更
  public GameObject[] buttonShape = new GameObject[3];
  
  // 非表示にするボタンはGameObjectで実装
  public GameObject ButtonRetry;

  // GameのUIで表示するImage
  public GameObject[] ImageShape = new GameObject[3];
	public GameObject ImageTarget;
  public GameObject[] ImageMove = new GameObject[3];

	// public AudioClip clearSE;
	// public AudioClip GameBGM;
	private AudioSource source;

  // ロジックのための変数
  public int score = 0;
	public int highestscore = 0;
  private int[] buttonselect = new int[3];
  private int target;

  // スクリプトが適用されたオブジェクトがアクティブになった際に、一度だけ呼び出される
  void Start(){
    buttonselect [0] = YELLOW_CIRCLE;
		buttonselect [1] = RED_CIRCLE;
		buttonselect [2] = BLUE_CIRCLE;
		target = YELLOW_TRIANGLE;
		// source = gameObject.GetComponent<AudioSource>();
		ButtonRetry.SetActive(false);
  }

  // 毎フレーム実行される処理
  void Update(){
        
  }

  public void PushButonLeft(){
		ChangeTarget (0);
	}
	public void PushButonCenter(){
		ChangeTarget (1);
	}
	public void PushButonRight(){
		ChangeTarget (2);
	}

  // 左からbuttonNo番目のボタンが押された際の挙動
  void ChangeTarget(int buttonNo){
		target = buttonselect [buttonNo];
		buttonselect [buttonNo] = UnityEngine.Random.Range(0, 9);
		// ImageTarget.GetComponent<Image> ().sprite = Obje [target];
		ImageMove[buttonNo].GetComponent<Image> ().sprite = Obje [target];//

		buttonShape [buttonNo].GetComponent<Image> ().sprite = Obje [buttonselect [buttonNo]];
		ImageShape [buttonNo].GetComponent<Image> ().sprite = Obje [buttonselect [buttonNo]];
		for (int i = 0; i < 3; i++) {
			buttonShape[i].SetActive(false);
      ImageShape[i].SetActive(true);
		}
		for (int i = 0; i < 3; i++) {//
			if (i == buttonNo) {
        ImageMove [i].SetActive (false);
				ImageMove [i].SetActive (true);
			} else {
				ImageMove [i].SetActive (false);
			}
		}
		switch (target) {
		case 0:
			score++;
			break;
		case 1:
			score += 2;
			break;
		case 2:
			score += 3;
			break;
		case 3:
			score += 2;
			break;
		case 4:
			score += 4;
			break;
		case 5:
			score += 6;
			break;
		case 6:
			score += 3;
			break;
		case 7:
			score += 6;
			break;
		case 8:
			score += 9;
			break;
		}
		StartCoroutine ("TargetVanish");
	}

  IEnumerator TargetVanish(){
		ImageTarget.SetActive (true);
		yield return new WaitForSeconds(1f);
		// ImageTarget.SetActive (false);
		ImageTarget.GetComponent<Image> ().sprite = Obje [target];

    DisplayScore (score);

    for (int i = 0; i < 3; i++) {
			if ((buttonselect [i] / 3 != target / 3) && (buttonselect [i] % 3 != target % 3)) {
				buttonShape [i].SetActive (false);
				ImageShape [i].SetActive (true);
			} else {
				buttonShape [i].SetActive (true);
				ImageShape [i].SetActive (false);
			}
		}

		if ((buttonShape [0].activeSelf == false) && (buttonShape [1].activeSelf == false) && (buttonShape [2].activeSelf == false)) {
			GameOver();
		}
	}

	IEnumerator Ending (){
		// source.Stop();
		// source.PlayOneShot(clearSE);
		TextGameOver.gameObject.SetActive (true);
		yield return new WaitForSeconds(4);

		for(int i = 0;i < 3;i++){
			buttonShape [i].SetActive (false);
			ImageShape [i].SetActive (false);
			ImageMove[i].SetActive(false);
		}
		ImageTarget.SetActive(false);

		TextGameOver.gameObject.SetActive (false);
		ButtonRetry.SetActive(true);
	}


	public void DisplayScore(int score){
		scoreDisplay.text = score.ToString();
	}

	public void GameOver(){
    print(score);
		TextGameOver.text = "得点 : " + score.ToString();
		// if (score < 30) {
		// 	TextGameOver.text += "\nもっと\n上を目指そう!";
		// } else if (30 <= score && score < 70) {
		// 	TextGameOver.text += "\nこれが・・・\nあなたの・・・\n実力・・・!";
		// } else if (70 <= score && score < 100) {
		// 	TextGameOver.text += "\n神社に来たのに５円玉がない\nそんな気分";
		// } else if (100 <= score && score < 200) {
		// 	TextGameOver.text += "\nおめでとう!\n今日は焼肉だ!";
		// } else if (200 <= score) {
		// 	TextGameOver.text += "\nすごい!\nこのゲームで食っていけるぞ！";
		// }
		StartCoroutine ("Ending");
		//TextGameOver.SetActive (true);
		//yield return new WaitForSeconds(2);
		//TextGameOver.SetActive (false);
	}

  public void PushButtonReset(){
		if(highestscore < score){  
			textHighestScore.text = "最高得点 : " + score.ToString();
			highestscore = score;
		}
		// source.PlayOneShot(GameBGM);
		score = 0;
		DisplayScore (score);
		for(int i = 0;i < 3;i++){
			buttonShape [i].GetComponent<Image> ().sprite = Obje [i*3];
			ImageShape [i].GetComponent<Image> ().sprite = Obje [i*3];
			buttonShape [i].SetActive (true);
			ImageShape [i].SetActive (false);
		}
		ImageTarget.GetComponent<Image> ().sprite = Obje[0];
		ImageTarget.SetActive(true);

		Start();
	}
}
