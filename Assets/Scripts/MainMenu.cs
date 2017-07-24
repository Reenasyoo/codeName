/*
	BUGS:
		-Player 2 name in character select not showing.
		-
	-END OF BUGS


 */


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : Menu {
	
		float xAxis;

	//public GameObject eventSystem;

	// Canvas Object
	public GameObject menuWrapper;
	public GameObject eventSystem;
	
	// Menu panel
	public GameObject menuPanel;
	public GameObject gameModeButton;
	public GameObject helpButton;
	public GameObject quitButton;
	
	// Game Mode panel
	public GameObject gameModePanel;
	public GameObject gm1Button;
	public GameObject gm2Button;
	public GameObject backButton;

	// Character Select panel
	// Character Panel Wrapper
	public GameObject characterPanel;
	public GameObject headerPanel;
	public GameObject p1Img;
	public GameObject p1Txt;

	public GameObject p2Img;
	public GameObject p2Txt;

	// Character selecting objects
	public GameObject characterSelectPanel;
	public GameObject chb;
	public GameObject chb2;
	public GameObject ch1p;
	public GameObject ch2p;
	
	// Random character selecting
	public GameObject randomPanel;
	public GameObject randomButton;

	public GameObject charBackButton;

	//Chracter selecting panel back button
	public GameObject backPanel;
	
	public Sprite randChar;
	public bool randPressed;
	public bool endRand;
	public float charL;
	public float randNumber;
	
	public Sprite locked;
	public Sprite rails;
	public Sprite charTest;
	public GameObject prevSel;
	public GameObject curSelGO;
	public Sprite[] charArray;
	public List<GameObject> charactersAll;

	public Selectable oldSelectable;


	public bool quit = false;

	// Use this for initialization
	void Start () {

		//eventSystem = new EventSystem(); LATER
		eventSystem = GameObject.Find("EventSystem");

		buttonTexture = Resources.Load<Sprite>("ButtonTest");
		buttonTextureH = Resources.Load<Sprite>("ButtonTestHover");
		
		charArray = Resources.LoadAll<Sprite>("32x32char");
		locked = Resources.Load<Sprite>("lock");
		rails = Resources.Load<Sprite>("rails");

		menuWrapper = CreateCanvas("Canvas");

		menuPanel = CreatePanel("Menu Panel", panelColor, menuWrapper, true);
		gameModeButton = CreateButton(60, "Game Modes", menuPanel, bask);
		helpButton = CreateButton(0, "Help", menuPanel, null);
		quitButton = CreateButton(-60, "Quit Game", menuPanel, quitGame);

		eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(gameModeButton);
		gameModePanel = CreatePanel("Game Mode Panel", Color.blue, menuWrapper, false);
		gm1Button = CreateButton(60, "character select", gameModePanel, chr);
		gm2Button = CreateButton(0, "TEST LVL2", gameModePanel, task);
		backButton = CreateButton(-60, "Back", gameModePanel, back);
		characterPanel = CreatePanel("character wrapper pannel", emptyColor, 0f, 0f, 1f, 1f, menuWrapper, false);
		fillCharacters();
		character();
		
		
	}
	
	// Update is called once per frame
	void Update () {
		curSelGO = eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject;
		Selectable newSelectable = curSelGO.GetComponent<Button>().FindSelectableOnLeft();

		if(curSelGO.name == "Random Button" && !endRand ) {
			if(prevSel != null) {
				p1Txt.GetComponent<Text> ().text = prevSel.name;
				p1Img.GetComponent<Image> ().sprite = prevSel.GetComponent<Image> ().sprite;
			}
			else {
				p1Img.GetComponent<Image> ().sprite = charArray[2];
			}
		}

		if(curSelGO.name == "Random Button" && endRand ) {
			p1Img.GetComponent<Image> ().sprite = randChar;
			p1Txt.GetComponent<Text> ().text = randChar.name;
		}

		if (curSelGO.name != "Random Button" && !endRand ) {
			p1Txt.GetComponent<Text> ().text = curSelGO.name;
			p1Img.GetComponent<Image> ().sprite = curSelGO.GetComponent<Image> ().sprite;
			if (characterPanel.activeSelf)
			{
				prevSel = curSelGO;	
			}
		}

		if (curSelGO.name != "Random Button" && endRand ) {
			p1Txt.GetComponent<Text> ().text = curSelGO.name;
			p1Img.GetComponent<Image> ().sprite = curSelGO.GetComponent<Image> ().sprite;
			endRand = false;
			prevSel = curSelGO;
		}

		oldSelectable = newSelectable;

		if(quit) {
			Application.Quit();
		}

	}
	// Test function for  button onClick event
	public void task () {
		SceneManager.LoadScene("SceneTest");
		Debug.Log ("play scene");
		gameModePanel.gameObject.SetActive(false);
	}
	public void bask () {
		Debug.Log ("game mode panel");
		menuPanel.gameObject.SetActive(false);
		gameModePanel.gameObject.SetActive(true);
		characterPanel.gameObject.SetActive(false);
		eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(gm1Button);
	}
	public void back () {
		Debug.Log ("back");
		menuPanel.gameObject.SetActive(true);
		gameModePanel.gameObject.SetActive(false);
		eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(gameModeButton);
	}
	public void backG () {
		Debug.Log ("back");
		menuPanel.gameObject.SetActive(true);
		gameModePanel.gameObject.SetActive(false);
		characterPanel.gameObject.SetActive(false);
		eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(gameModeButton);
	}
	public void chr() {
		eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(randomButton);
		gameModePanel.gameObject.SetActive(false);
		characterPanel.gameObject.SetActive(true);
	}

	public void quitGame() {
		quit = true;
		Debug.Log ("Quit");
	}

	public void character() {
		headerPanel = CreatePanel("header", Color.blue, 0.15f, 0.6f, 0.85f, 0.85f, characterPanel, true);
		GameObject p1Panel = CreatePanel ("P1 Panel", Color.red, 0f, 0f, 0.5f, 1f, headerPanel, true);
		p1Img = createImage("p1img", charArray[2], p1Panel, 0.65f, 0f, 1f, 1f);
		p1Txt = createText("p1txt", p1Panel, 0f, 0f, 0.65f, 1f);

		GameObject p2Panel = CreatePanel ("P2 Panel", Color.red, 0.5f, 0f, 1f, 1f, headerPanel, true);
		p2Img = createImage("p2img", charArray[2], p2Panel, 0f, 0f, 0.35f, 1f);
		p2Txt = createText("p2txt", p2Panel, 0.35f, 1f, 1f, 1f);

		// TODO : Remove this after player 2 integration
		p2Txt.GetComponent<Text> ().text = "Test Text";

		characterSelectPanel = CreatePanel("character", Color.red, 0.15f, 0.15f, 0.85f, 0.55f, characterPanel, true);
		chb = CreatePanel("chb", Color.red, 0f, 0f, 0.45f, 1f, characterSelectPanel, true);
		chb.AddComponent<GridLayoutGroup> ();
		RectTransform chbRT = chb.GetComponent<RectTransform>();
		chb.GetComponent<GridLayoutGroup> ().cellSize = new Vector2(chbRT.rect.width/4, chbRT.rect.height/3);
		ch1p = CreatePanel("ch1p", emptyColor, 0f, 0f, 0.45f, 1f, characterSelectPanel, true);
		ch1p.AddComponent<GridLayoutGroup> ();
		RectTransform ch1pRT = ch1p.GetComponent<RectTransform>();
		ch1p.GetComponent<GridLayoutGroup> ().cellSize = new Vector2(ch1pRT.rect.width/4, ch1pRT.rect.height/3);
		chb2 = CreatePanel("chb2", Color.red, 0.55f, 0f, 1f, 1f, characterSelectPanel, true);
		chb2.AddComponent<GridLayoutGroup> ();
		RectTransform chbRT2 = chb2.GetComponent<RectTransform>();
		chb2.GetComponent<GridLayoutGroup> ().cellSize = new Vector2(chbRT2.rect.width/4, chbRT2.rect.height/3);
		ch2p = CreatePanel("ch2p", emptyColor, 0.55f, 0f, 1f, 1f, characterSelectPanel, true);
		ch2p.AddComponent<GridLayoutGroup> ();
		RectTransform ch2pRT = ch2p.GetComponent<RectTransform>();
		ch2p.GetComponent<GridLayoutGroup> ().cellSize = new Vector2(ch2pRT.rect.width/4, ch2pRT.rect.height/3);
		for(int i = 0; i < 12; i++) {
			
			GameObject img = CreateImage(0, charactersAll[i].name, chb);
			img.GetComponent<Image> ().sprite = charactersAll[i].GetComponent<Image>().sprite;
			GameObject btn = CreateButton(0, "button: " +i, ch1p, bask, rails);
			btn.GetComponent<Image> ().sprite = charactersAll[i].GetComponent<Image>().sprite;
		}
		for(int i = 12; i < 24; i++) {
			
			GameObject img = CreateImage(0, charactersAll[i].name, chb2);
			img.GetComponent<Image> ().sprite = charactersAll[i].GetComponent<Image>().sprite;
			GameObject btn2 = CreateButton(0, "button: " +i , ch2p, bask, rails);
			btn2.GetComponent<Image> ().sprite = charactersAll[i].GetComponent<Image>().sprite;
		}
		randomPanel = CreatePanel("Random Button panel", Color.red, 0.45f, 0f, 0.55f, 1f, characterSelectPanel, true);
		randomPanel.AddComponent<HorizontalLayoutGroup> ();
		randomPanel.GetComponent<HorizontalLayoutGroup> ().childForceExpandHeight = true;
		randomPanel.GetComponent<HorizontalLayoutGroup> ().childForceExpandWidth = true;
		randomButton = CreateButton(0, "Random Button", randomPanel, randEffect);
		backPanel = CreatePanel("back button", emptyColor, 0.15f, 0f, 0.85f, 0.15f, characterPanel, true);

		charBackButton = CreateButton(0, "Back", backPanel, bask);
	}

		public void charRandom() {
			endRand = true;
			// set random selcected character but only sprite
			charL = (float)charArray.Length;
			randNumber = UnityEngine.Random.Range(0f, charL);
			randChar = charArray[(int)randNumber];
		}

		public void randEffect() {
			InvokeRepeating("charRandom", 0f, 0.05f);
			Invoke("endRep", 1);
		}

		public void endRep() {
			CancelInvoke();
		}

		public void fillCharacters() {

			for (int i = 0; i < 24; i++) {	
				GameObject ch = new GameObject();
				ch.name = i.ToString();
				ch.AddComponent<Image> ();
				
				if(charArray.Length > i) {
					ch.GetComponent<Image> ().sprite = charArray[i];
					ch.name = charArray[i].name;
				}
				else {
					ch.GetComponent<Image> ().sprite = locked;
					ch.name = "locked";
				}
				
				charactersAll.Add(ch);
			}
		}
}