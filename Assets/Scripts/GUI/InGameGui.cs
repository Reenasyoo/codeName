/*
	BUGS:
		- 
	-END OF BUG

	TODO : 
		- change winner text to char name
		-
	-END OF TODO

 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameGui : Menu {

	public GameObject EventSystem, pauseMenu, pausePanel, resumeButton, restartButton, mainMenuButton, quitButton;
	public GameObject ingameGui, playerHp, enemyHp, winner;
	public Slider pVal, eVal, p1Spec1Val , p1Spec2Val, p2Spec1Val, p2Spec2Val;
	public bool isPaused = false;
	public bool isEnd = false;
	public string playerWin = "Game Paused";

	public int counter = 0;

	private Sprite bSprite;
	private Sprite fSprite;
	private Sprite[] charArray;

	public int chID;
	public int chID2;

	private Camera cam;

	// Use this for initialization
	void Start () {
		EventSystem = CreateEventSystem();
		ingameGui = CreateCanvas("ingameCanvas");

		bSprite = Resources.Load<Sprite>("sliderBg");
		fSprite = Resources.Load<Sprite>("SliderFiller");
		buttonTexture = Resources.Load<Sprite>("ButtonTest");
		buttonTextureH = Resources.Load<Sprite>("ButtonTestHover");
		charArray = Resources.LoadAll<Sprite>("Characters");

		chID = getCharId(The.p1, charArray);
		chID2 = getCharId(The.p2, charArray);
		
		charGui();
		
		pausePanel = CreatePanel("Pause Panel", Color.black, 0f, 0f, 1f, 1f, ingameGui, false);
		pausePanel.AddComponent<VerticalLayoutGroup>();
		pausePanel.GetComponent<VerticalLayoutGroup>().padding.left = 150;
		pausePanel.GetComponent<VerticalLayoutGroup>().padding.right = 150;

		winner = createText("winner", pausePanel, 0.4f, 0.6f, 0.6f, 0.8f);
		winner.GetComponent<Text> ().color = Color.white;
		winner.GetComponent<Text> ().text = playerWin;
		
		resumeButton = CreateButton("Resume", pausePanel, resume, 60);
		restartButton = CreateButton("Restart", pausePanel, restartScene, 0);
		mainMenuButton = CreateButton("Main Menu", pausePanel, menu, 60);
		quitButton = CreateButton("Quit Game", pausePanel, quitGame, -120);


	}
	
	// Update is called once per frame
	void Update () {

		// c = canvas object accesor
		Canvas c = ingameGui.GetComponent<Canvas>();
		cam = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera>();
		if (c != null) {
			c.renderMode = RenderMode.ScreenSpaceCamera;
			c.worldCamera = cam;
		}
		
		pVal.value = The.player.hp;
		eVal.value = The.enemy.hp;
		p1Spec1Val.value = The.player.GetComponent<Shotgun>().ready;
		p1Spec2Val.value = The.player.GetComponent<Charge>().ready;
		p2Spec1Val.value = The.enemy.GetComponent<Shotgun>().ready;
		p2Spec2Val.value = The.enemy.GetComponent<Charge>().ready;
			
		if(The.enemy.hp <= 0){
			playerWin = "1st player";
		} 
		else if(The.player.hp <= 0) {
			playerWin = "2nd player"; 
		}

		if((Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.JoystickButton3)) && !isEnd) {
			isPaused = !isPaused;
			if(isPaused) {
				counter++;
			}
			else {
				EventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
			}
		}

		if (isPaused) {
			pausePanel.SetActive(true);
			Time.timeScale = 0f;
		}
		else {
			pausePanel.SetActive(false);
			Time.timeScale = 1f;
		}

		if( counter == 1) {
			EventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(resumeButton);
			counter = 0;
		}

		if((The.player.hp <= 0 || The.enemy.hp <= 0) && !isEnd) {
			endGame();
			isPaused = true;
			isEnd = true;
		}
	
	}

	public void resume(){
		isPaused = false;
		counter = 0;
		EventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
	}

	public void restartScene(){
		LoadScene(SceneManager.GetActiveScene().name, ingameGui);
		Debug.Break();
	}

	public void menu () {
		LoadScene("MainMenu", ingameGui );
	}

	public void endGame() {
		pausePanel.SetActive(true);
		EventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(restartButton);
		resumeButton.SetActive(false);
		
		Time.timeScale = 0f;
		winner.GetComponent<Text> ().text = playerWin + " Won!";
	
	}

	void charGui() {
		GameObject p1Panel = CreatePanel("Player 1 Gui",Color.black, 0f, 0.85f, 0.4f, 1f,ingameGui, true);
		GameObject p1pImg = CreatePanel("Player 1 Image",Color.black, 0.01f, 0.06f, 0.3f, 0.98f,p1Panel, true);
		p1pImg.AddComponent<Mask>();
		GameObject p1Image = CreateImage("Image", p1pImg, 0, 0, 1, 1);
		p1Image.GetComponent<Image> ().sprite = charArray[chID];
		p1Image.GetComponent<RectTransform> ().pivot = new Vector2(0.5f,1);
		p1Image.GetComponent<RectTransform> ().localScale = new Vector3 (1f, 2f, 1f);
		p1Image.GetComponent<RectTransform> ().anchoredPosition = new Vector2(0f,0f);

		GameObject p1Info = CreatePanel("Player 1 Info",Color.black, 0.31f, 0.06f, 0.98f, 0.98f, p1Panel, true);
		GameObject p1Text = createText("Player 1 Text", p1Info, 0f, 0.7f, 1f, 1f);
		p1Text.GetComponent<Text> ().fontSize = 20;
		p1Text.GetComponent<Text> ().color = Color.white;
		p1Text.GetComponent<Text> ().text = The.p1;

		playerHp = CreateLoader("PlayerHp", p1Info, bSprite, fSprite, 0f, 0.4f, 1f, 0.7f, new Vector2(0,0));
		playerHp.GetComponent<Slider>().maxValue = 100;
		pVal = playerHp.GetComponent<Slider>();
		
		GameObject p1Spec1 = CreateLoader("Spec1", p1Info, bSprite, fSprite, 0f, 0f, 0.49f, 0.37f,new Vector2(0,0));
		GameObject p1Spec2 = CreateLoader("Spec1", p1Info, bSprite, fSprite, 0.51f, 0f, 1f, 0.37f, new Vector2(0,0));


		GameObject p2Panel = CreatePanel("Player 2 Gui",Color.black, 0.6f, 0.85f, 1f, 1f,ingameGui, true);
		p2Panel.GetComponent<RectTransform> ().localScale = new Vector3 (-1f, 1f, 1f);
		GameObject p2pImg = CreatePanel("Player 2 Image",Color.black, 0.01f, 0.06f, 0.3f, 0.98f, p2Panel, true);
		p2pImg.AddComponent<Mask>();
		GameObject p2Image = CreateImage("Image", p2pImg, 0, 0, 1, 1);
		p2Image.GetComponent<Image> ().sprite = charArray[chID2];
		p2Image.GetComponent<RectTransform> ().pivot = new Vector2(0.5f,1);
		p2Image.GetComponent<RectTransform> ().localScale = new Vector3 (-1f, 2f, 1f);
		p2Image.GetComponent<RectTransform> ().anchoredPosition = new Vector2(0f,0f);

		GameObject p2Info = CreatePanel("Player 2 Info",Color.black, 0.31f, 0.06f, 0.98f, 0.98f, p2Panel, true);
		GameObject p2Text = createText("p2 Text", p2Info, 0f, 0.7f, 1f, 1f);
		p2Text.GetComponent<Text> ().fontSize = 20;
		p2Text.GetComponent<Text> ().color = Color.white;
		p2Text.GetComponent<Text> ().text = The.p2;

		enemyHp = CreateLoader("PlayerHp", p2Info, bSprite, fSprite, 0f, 0.4f, 1f, 0.7f, new Vector2(0,0));
		enemyHp.GetComponent<Slider>().maxValue = 100;
		RectTransformUtility.FlipLayoutOnAxis(enemyHp.GetComponent<RectTransform> (), 0, true, false);
		enemyHp.GetComponent<Slider>().direction = Slider.Direction.RightToLeft;
		eVal = enemyHp.GetComponent<Slider>();
		
		GameObject p2Spec1 = CreateLoader("Spec1", p2Info, bSprite, fSprite, 0.51f, 0f, 1f, 0.37f, new Vector2(0,0));
		GameObject p2Spec2 = CreateLoader("Spec2", p2Info, bSprite, fSprite, 0f, 0f, 0.49f, 0.37f,new Vector2(0,0));


		p2Spec1.GetComponent<Slider>().direction = Slider.Direction.RightToLeft;
		p2Spec2.GetComponent<Slider>().direction = Slider.Direction.RightToLeft;

		p1Spec1.GetComponent<Slider>().maxValue = The.player.GetComponent<Shotgun>().maxCooldown;
		p1Spec2.GetComponent<Slider>().maxValue = The.player.GetComponent<Charge>().maxCooldown;
		p2Spec1.GetComponent<Slider>().maxValue = The.enemy.GetComponent<Shotgun>().maxCooldown;
		p2Spec2.GetComponent<Slider>().maxValue = The.enemy.GetComponent<Charge>().maxCooldown;

		p1Spec1Val = p1Spec1.GetComponent<Slider>();
		p1Spec2Val = p1Spec2.GetComponent<Slider>();
		p2Spec1Val = p2Spec1.GetComponent<Slider>();
		p2Spec2Val = p2Spec2.GetComponent<Slider>();
		
	}

	public int getCharId(string bName, Sprite[] chA) {
			int temp = 0;
			for(int i = 0; i< chA.Length; i++){
				if(bName == chA[i].name) {
					temp =  i;
				}
			}
			return temp;
		}
	
}

