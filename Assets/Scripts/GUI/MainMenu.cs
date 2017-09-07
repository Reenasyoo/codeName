/*
	BUGS:
		- Get witch player selected character
		- 
	-END OF BUG

	TODO : 
		- rand name and accept rand player
		- Accept char select and move to level select
		- Add transition for scene change
		- Create level select
		- Comment code
	-END OF TODO

 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenu : Menu {
	
	float xAxis;

	// Canvas Object
	public GameObject menuWrapper, EventSystem;
	
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
	public GameObject randomImgPanel;
	public GameObject randomButton;
	public GameObject randomImage;

	public GameObject charBackButton;

	//Chracter selecting panel back button
	public GameObject backPanel;
	
	public Sprite randChar2;
	public bool randPressed;
	public bool endRand;
	public bool endRand2;
	public float charL;
	public float randNumber;
	
	public Sprite locked;
	public Sprite rails;
	public Sprite rails2;
	public Sprite randChar;
	public Sprite railsCombo;
	public Sprite charTest;

	public GameObject prevSel;
	public GameObject prevSel2;
	public GameObject curSelGO;
	public GameObject curSelGO2;
	public Sprite[] charArray;
	public List<GameObject> charactersAll;

	public GameObject stNewSelectable;
	public GameObject stLastSelectable;

	public GameObject ndNewSelectable;
	public GameObject ndLastSelectable;

	public Sprite lastSpr1;
	public Sprite lastSpr2;

	private int counter1;
	private int counter2;
	public int chID2;
	public int chID;

	public List<GameObject> btnChr;
	public List<GameObject> imgChr;


	private bool lastP1 = false;
	private bool lastP2 = false;

	public Sprite play;
	public Sprite playHover;
	public Sprite help;
	public Sprite helpHover;
	public Sprite quit;
	public Sprite quitHover;
	public Sprite chars;
	public Sprite charsHover;
	public Sprite random;
	public Sprite randomHover;

	// Level stuff
	public GameObject levelPanel;
	public GameObject infoPanel;
	public GameObject levelSelectPanel;
	public GameObject levelSelectPanel2;
	public GameObject levelPreviewImage;
	public GameObject levelPreviewText;
	public GameObject levelPreviewImage2;
	public GameObject levelPreviewText2;
	public GameObject levelRandomButton;
	public GameObject levelBackPanel;
	public GameObject levelBackButton;

	public Sprite lvlPrew;
	public Sprite bg;
	public Sprite bg_logo;


	public List<GameObject> lvlImgChr;
	public List<GameObject> lvlBtnChr;

	public Sprite[] lvlAll;
	public List<GameObject> levelsAll;

	public GameObject lvlSelGO;
	public GameObject lvlSelGO2;

	public int lvlID;
	public int lvlID2;
	public GameObject lvlPrevSel;
	public GameObject lvlPrevSel2;
	public Sprite randLvl;

	public GameObject lvlStLastSelectable;
	public GameObject lvlNdLastSelectable;

	public Sprite _lastSpr1;
	public Sprite _lastSpr2;

	public GameObject inputMappingPanelW;
	public GameObject inputMappingPanel;
	public GameObject iMapBackButton;

	public GameObject inputMappingPlayer1Panel;
	public GameObject inputMappingPlayer2Panel;

	// Use this for initialization
	void Start () {

		
		buttonTexture = Resources.Load<Sprite>("Sprites/ButtonTest");
		buttonTextureH = Resources.Load<Sprite>("Sprites/ButtonTestHover");

		play = Resources.Load<Sprite>("Sprites/Buttons/play");
		playHover = Resources.Load<Sprite>("Sprites/Buttons/playHover");
		help = Resources.Load<Sprite>("Sprites/Buttons/help_up");
		helpHover = Resources.Load<Sprite>("Sprites/Buttons/help_down");
		quit = Resources.Load<Sprite>("Sprites/Buttons/quit");
		quitHover = Resources.Load<Sprite>("Sprites/Buttons/quitHover");
		chars = Resources.Load<Sprite>("Sprites/Buttons/char");
		charsHover = Resources.Load<Sprite>("Sprites/Buttons/charHover");
		random = Resources.Load<Sprite>("Sprites/Buttons/random");
		randomHover = Resources.Load<Sprite>("Sprites/Buttons/randomHover");

		lvlPrew = Resources.Load<Sprite>("Sprites/lvlPrew");

		bg = Resources.Load<Sprite>("Sprites/background 2");
		bg_logo = Resources.Load<Sprite>("Sprites/background 1");


		lvlAll = Resources.LoadAll<Sprite>("Sprites/LevelPreviews");

		charArray = Resources.LoadAll<Sprite>("Sprites/Characters");
		locked = Resources.Load<Sprite>("Sprites/lock");
		rails = Resources.Load<Sprite>("Sprites/rails");
		rails2 = Resources.Load<Sprite>("Sprites/rails2");
		railsCombo = Resources.Load<Sprite>("Sprites/railsCombo");

		EventSystem = CreateEventSystem();
		menuWrapper = CreateCanvas("Canvas");
		menuWrapper.GetComponent<Image>().sprite = bg;
		menuWrapper.GetComponent<Image>().color = emptyColorA2;

		GameObject _version = createText("Version", menuWrapper, 0.85f, 0f, 1f, 0.06f);
		_version.GetComponent<Text>().color = Color.white;
		_version.GetComponent<Text>().fontSize = 20;
		_version.GetComponent<Text>().text = "Version : Prealfa 0.0.1";

		menuPanel = CreatePanel("Menu Panel", emptyColorA, menuWrapper, true);
		menuPanel.AddComponent<VerticalLayoutGroup>();
		menuPanel.GetComponent<VerticalLayoutGroup>().padding.left = 150;
		menuPanel.GetComponent<VerticalLayoutGroup>().padding.right = 150;
		gameModeButton = CreateButton("Game Modes", menuPanel, bask, playHover, 60);
		gameModeButton.transform.GetChild(0).GetComponent<Text> ().text = "";
		gameModeButton.GetComponent<Image> ().sprite = play;
		helpButton = CreateButton("Help", menuPanel, iMap, helpHover, 0);
		helpButton.GetComponent<Image> ().sprite = help;
		helpButton.transform.GetChild(0).GetComponent<Text> ().text = "";
		quitButton = CreateButton("Quit Game", menuPanel, quitGame, quitHover, -60);
		quitButton.transform.GetChild(0).GetComponent<Text> ().text = "";
		quitButton.GetComponent<Image> ().sprite = quit;

		EventSystem.GetComponent<EventSystem>().SetSelectedGameObject(gameModeButton);
		gameModePanel = CreatePanel("Game Mode Panel", emptyColorA, menuWrapper, false);
		gameModePanel.AddComponent<VerticalLayoutGroup>();
		gameModePanel.GetComponent<VerticalLayoutGroup>().padding.left = 150;
		gameModePanel.GetComponent<VerticalLayoutGroup>().padding.right = 150;
		gm1Button = CreateButton("character select", gameModePanel, chr, charsHover, 60);
		gm1Button.transform.GetChild(0).GetComponent<Text> ().text = "";
		gm1Button.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 100);
		gm1Button.GetComponent<Image> ().sprite = chars;
		gm2Button = CreateButton("TEST LVL2", gameModePanel, task, 0);
		backButton = CreateButton("Back", gameModePanel, back, -60);


		// character wrapper
		characterPanel = CreatePanel("character wrapper pannel", emptyColor, 0f, 0f, 1f, 1f, menuWrapper, false);
		fillCharacters();
		character();
		
		levelPanel = CreatePanel("level wrapper pannel", emptyColor, 0.15f, 0f, 0.85f, 1f, menuWrapper, false);
		fillLevels();
		levels();

		//im.AddComponent<InputManager>();
		//im.GetComponent<InputManager>().lohs();
		// InputManager._InputManager.lohs();
		inputMapping();

		counter1 = 0;
		counter2 = 0;

		stLastSelectable = randomButton;
		ndLastSelectable = randomButton;
		curSelGO = randomButton;
		curSelGO2 = randomButton;
		prevSel = randomButton;
		prevSel2 = randomButton;

		lvlStLastSelectable = levelRandomButton;
		lvlNdLastSelectable = levelRandomButton;
		lvlSelGO = levelRandomButton;
		lvlSelGO2 = levelRandomButton;
		lvlPrevSel = levelRandomButton;
		lvlPrevSel2 = levelRandomButton;

		
	}
	
	// Update is called once per frame
	void Update () {

		if(inputMappingPanelW.activeSelf) {
			if(InputManager._InputManager.def) {

            	for (int i = 0; i < inputMappingPlayer1Panel.transform.childCount; i++){
					Text _keyNameText = inputMappingPlayer1Panel.transform.GetChild(i).transform.GetChild(0).GetComponent<Text>();
					_keyNameText.text = InputManager._InputManager._keyString[i];
					
             		PlayerPrefs.SetString(InputManager._InputManager._kbDefaultInput[i].ToString(), InputManager._InputManager._keyString[i]); //save new key to PlayerPrefs
            }
            
            InputManager._InputManager.def = false;
        	}
		}
		
		if (menuPanel.activeSelf || gameModePanel.activeSelf) {
			menuWrapper.GetComponent<Image>().sprite = bg_logo;
		}
		else { 
			menuWrapper.GetComponent<Image>().sprite = bg; 
		}

		if(levelPanel.activeSelf) {

			if(levelRandomButton != EventSystem.GetComponent<EventSystem>().currentSelectedGameObject) {
				levelRandomButton.GetComponent<Image> ().sprite = random;
			}
			else if(lvlSelGO == EventSystem.GetComponent<EventSystem>().currentSelectedGameObject){
				levelRandomButton.GetComponent<Image> ().sprite = rails;
			}
			else if(lvlSelGO2 == EventSystem.GetComponent<EventSystem>().currentSelectedGameObject){
				levelRandomButton.GetComponent<Image> ().sprite = rails;
			}
			
			// First player choosing
			if (Input.GetAxisRaw ("Horizontal") != 0 || Input.GetAxisRaw ("Vertical") != 0f) {
				setLvlButtonSelected(EventSystem.GetComponent<EventSystem>().currentSelectedGameObject.name, rails);
				EventSystem.gameObject.GetComponent<StandaloneInputModule>().horizontalAxis = "Horizontal";
				EventSystem.gameObject.GetComponent<StandaloneInputModule>().verticalAxis = "Vertical";

				counter2 = 0;
				if(counter1 == 0) {
					lvlStLastSelectable.GetComponent<Image> ().sprite = _lastSpr1;
					EventSystem.GetComponent<EventSystem>().SetSelectedGameObject(lvlStLastSelectable);
					stNewSelectable = lvlSelGO;

				}
				counter1++;

				stNewSelectable = EventSystem.GetComponent<EventSystem>().currentSelectedGameObject;
				lvlSelGO = stNewSelectable;
				if(lvlPrevSel2.name == lvlSelGO.name) {
					setLvlButtonSelected(EventSystem.GetComponent<EventSystem>().currentSelectedGameObject.name, railsCombo);
				}
				lvlStLastSelectable = stNewSelectable;
			}

			if (counter1 == 0f){
				
				_lastSpr1 = getLvlImage(lvlStLastSelectable.name);
				lvlStLastSelectable.GetComponent<Image> ().sprite = rails;
			}

			if(lvlSelGO.name == "Level Random Button" && !endRand ) {
				if(lvlPrevSel != null) {
					lvlID = getLvlId(lvlPrevSel.name);
					levelPreviewText.GetComponent<Text> ().text = saveLvlName(lvlID);
					levelPreviewImage.GetComponent<Image> ().sprite = getLvlImage(lvlPrevSel.name);
				}
				else {
					levelPreviewImage.GetComponent<Image> ().sprite = lvlAll[2];
				}
			}

			if(lvlSelGO.name == "Level Random Button" && endRand ) {
				levelPreviewImage.GetComponent<Image> ().sprite = randLvl;
				levelPreviewText.GetComponent<Text> ().text = randLvl.name;
				lvlID = getCharId(levelPreviewText.GetComponent<Text> ().text);
			}

			if (lvlSelGO.name != "Level Random Button" && !endRand ) {
				lvlID = getLvlId(lvlSelGO.name);
				levelPreviewText.GetComponent<Text> ().text = saveLvlName(lvlID);
				levelPreviewImage.GetComponent<Image> ().sprite = getLvlImage(lvlSelGO.name);
				if (levelPanel.activeSelf)
				{
					lvlPrevSel = lvlSelGO;	
				}
			}

			if (lvlSelGO.name != "Level Random Button" && endRand ) {
				lvlID = getLvlId(lvlSelGO.name);
				levelPreviewText.GetComponent<Text> ().text = saveLvlName(lvlID);
				levelPreviewImage.GetComponent<Image> ().sprite = getLvlImage(lvlSelGO.name);
				endRand = false;
				lvlPrevSel = lvlSelGO;
			}

			// ------------------------

			if (Input.GetAxisRaw ("HorizontalP2") != 0f || Input.GetAxisRaw ("VerticalP2") != 0f) {
				setLvlButtonSelected(EventSystem.GetComponent<EventSystem>().currentSelectedGameObject.name, rails2);
				EventSystem.gameObject.GetComponent<StandaloneInputModule>().horizontalAxis = "HorizontalP2";
				EventSystem.gameObject.GetComponent<StandaloneInputModule>().verticalAxis = "VerticalP2";
			
				counter1 = 0;
				if(counter2 == 0) {
					lvlNdLastSelectable.GetComponent<Image> ().sprite = _lastSpr2;
					EventSystem.GetComponent<EventSystem>().SetSelectedGameObject(lvlNdLastSelectable);
					ndNewSelectable = lvlSelGO2;
				}
				counter2++;

				ndNewSelectable = EventSystem.GetComponent<EventSystem>().currentSelectedGameObject;
				lvlSelGO2 = ndNewSelectable;
				if(lvlPrevSel.name == lvlSelGO2.name) {
					setLvlButtonSelected(EventSystem.GetComponent<EventSystem>().currentSelectedGameObject.name, railsCombo);
				}
				lvlNdLastSelectable = ndNewSelectable;
			}

			if (counter2 == 0f){
				_lastSpr2 = getLvlImage(lvlNdLastSelectable.name);
				lvlNdLastSelectable.GetComponent<Image> ().sprite = rails2;
			}

			if(lvlSelGO2.name == "Level Random Button" && !endRand2 ) {
				if(lvlPrevSel2 != null) {
					lvlID2 = getLvlId(lvlPrevSel2.name); 
					levelPreviewText2.GetComponent<Text> ().text = saveLvlName(lvlID2);
					levelPreviewImage2.GetComponent<Image> ().sprite = getLvlImage(lvlPrevSel2.name);
				}
				else {
					levelPreviewImage2.GetComponent<Image> ().sprite = charArray[2];
				}
			}

			if(lvlSelGO2.name == "Level Random Button" && endRand2 ) {
				levelPreviewImage2.GetComponent<Image> ().sprite = randChar2;
				levelPreviewText2.GetComponent<Text> ().text = randChar2.name;
				lvlID2 = getLvlId(levelPreviewText2.GetComponent<Text> ().text);
				The.p2 = saveLvlName(lvlID2);
			}

			if (lvlSelGO2.name != "Level Random Button" && !endRand2 ) {
				lvlID2 = getLvlId(lvlSelGO2.name); 
				levelPreviewText2.GetComponent<Text> ().text = saveLvlName(lvlID2);
				levelPreviewImage2.GetComponent<Image> ().sprite = getLvlImage(lvlSelGO2.name);
				if (levelPanel.activeSelf)
				{
					lvlPrevSel2 = lvlSelGO2;	
				}
			}

			if (lvlSelGO2.name != "Level Random Button" && endRand2 ) {
				lvlID2 = getLvlId(lvlSelGO2.name);
				levelPreviewText2.GetComponent<Text> ().text = saveLvlName(lvlID2);
				levelPreviewImage2.GetComponent<Image> ().sprite = getLvlImage(lvlSelGO2.name);
				endRand2 = false;
				lvlPrevSel2 = lvlSelGO2;
			}
		}
		

		// ################################

		if(characterPanel.activeSelf) {
			if(randomButton != EventSystem.GetComponent<EventSystem>().currentSelectedGameObject) {
				randomButton.GetComponent<Image> ().sprite = random;
			}
			else if(curSelGO == EventSystem.GetComponent<EventSystem>().currentSelectedGameObject){
				randomButton.GetComponent<Image> ().sprite = rails;
			}
			else if(curSelGO2 == EventSystem.GetComponent<EventSystem>().currentSelectedGameObject){
				randomButton.GetComponent<Image> ().sprite = rails;
			}

			// First player choosing
			if (Input.GetAxisRaw ("Horizontal") != 0 || Input.GetAxisRaw ("Vertical") != 0f) {
				setButtonSelected(EventSystem.GetComponent<EventSystem>().currentSelectedGameObject.name, rails);
				EventSystem.gameObject.GetComponent<StandaloneInputModule>().horizontalAxis = "Horizontal";
				EventSystem.gameObject.GetComponent<StandaloneInputModule>().verticalAxis = "Vertical";
				counter2 = 0;
				if(counter1 == 0) {
					stLastSelectable.GetComponent<Image> ().sprite = lastSpr1;
					EventSystem.GetComponent<EventSystem>().SetSelectedGameObject(stLastSelectable);
					stNewSelectable = curSelGO;

				}
				counter1++;

				stNewSelectable = EventSystem.GetComponent<EventSystem>().currentSelectedGameObject;
				curSelGO = stNewSelectable;
				if(prevSel2.name == curSelGO.name) {
					setButtonSelected(EventSystem.GetComponent<EventSystem>().currentSelectedGameObject.name, railsCombo);
				}
				stLastSelectable = stNewSelectable;
			}

			if (counter1 == 0f){
				
				lastSpr1 = getButtonImage(stLastSelectable.name);
				stLastSelectable.GetComponent<Image> ().sprite = rails;
			}

			if(curSelGO.name == "Random Button" && !endRand ) {
				if(prevSel != null) {
					chID = getCharId(prevSel.name);
					p1Txt.GetComponent<Text> ().text = saveCharName(chID);
					p1Img.GetComponent<Image> ().sprite = getButtonImage(prevSel.name);
				}
				else {
					p1Img.GetComponent<Image> ().sprite = charArray[2];
				}
			}

			if(curSelGO.name == "Random Button" && endRand ) {
				p1Img.GetComponent<Image> ().sprite = randChar;
				p1Txt.GetComponent<Text> ().text = randChar.name;
				chID = getCharId(p1Txt.GetComponent<Text> ().text);
				The.p1= saveCharName(chID);
			}

			if (curSelGO.name != "Random Button" && !endRand ) {
				chID = getCharId(curSelGO.name);
				p1Txt.GetComponent<Text> ().text = saveCharName(chID);
				p1Img.GetComponent<Image> ().sprite = getButtonImage(curSelGO.name);
				if (characterPanel.activeSelf)
				{
					prevSel = curSelGO;	
				}
			}

			if (curSelGO.name != "Random Button" && endRand ) {
				chID = getCharId(curSelGO.name);
				p1Txt.GetComponent<Text> ().text = saveCharName(chID);
				p1Img.GetComponent<Image> ().sprite = getButtonImage(curSelGO.name);
				endRand = false;
				prevSel = curSelGO;
			}

			// Second player choosing
			if (Input.GetAxisRaw ("HorizontalP2") != 0f || Input.GetAxisRaw ("VerticalP2") != 0f) {
				setButtonSelected(EventSystem.GetComponent<EventSystem>().currentSelectedGameObject.name, rails2);
				EventSystem.gameObject.GetComponent<StandaloneInputModule>().horizontalAxis = "HorizontalP2";
				EventSystem.gameObject.GetComponent<StandaloneInputModule>().verticalAxis = "VerticalP2";
				counter1 = 0;
				if(counter2 == 0) {
					ndLastSelectable.GetComponent<Image> ().sprite = lastSpr2;
					EventSystem.GetComponent<EventSystem>().SetSelectedGameObject(ndLastSelectable);
					ndNewSelectable = curSelGO2;
				}
				counter2++;

				ndNewSelectable = EventSystem.GetComponent<EventSystem>().currentSelectedGameObject;
				curSelGO2 = ndNewSelectable;
				if(prevSel.name == curSelGO2.name) {
					setButtonSelected(EventSystem.GetComponent<EventSystem>().currentSelectedGameObject.name, railsCombo);
				}
				ndLastSelectable = ndNewSelectable;
			}

			if (counter2 == 0f){
				lastSpr2 = getButtonImage(ndLastSelectable.name);
				ndLastSelectable.GetComponent<Image> ().sprite = rails2;
			}

			if(curSelGO2.name == "Random Button" && !endRand2 ) {
				if(prevSel2 != null) {
					chID2 = getCharId(prevSel2.name); 
					p2Txt.GetComponent<Text> ().text = saveCharName(chID2);
					p2Img.GetComponent<Image> ().sprite = getButtonImage(prevSel2.name);
				}
				else {
					p2Img.GetComponent<Image> ().sprite = charArray[2];
				}
			}

			if(curSelGO2.name == "Random Button" && endRand2 ) {
				p2Img.GetComponent<Image> ().sprite = randChar2;
				p2Txt.GetComponent<Text> ().text = randChar2.name;
				chID2 = getCharId(p2Txt.GetComponent<Text> ().text);
				The.p2 = saveCharName(chID2);
			}

			if (curSelGO2.name != "Random Button" && !endRand2 ) {
				chID2 = getCharId(curSelGO2.name); 
				p2Txt.GetComponent<Text> ().text = saveCharName(chID2);
				p2Img.GetComponent<Image> ().sprite = getButtonImage(curSelGO2.name);
				if (characterPanel.activeSelf)
				{
					prevSel2 = curSelGO2;	
				}
			}

			if (curSelGO2.name != "Random Button" && endRand2 ) {
				chID2 = getCharId(curSelGO2.name);
				p2Txt.GetComponent<Text> ().text = saveCharName(chID2);
				p2Img.GetComponent<Image> ().sprite = getButtonImage(curSelGO2.name);
				endRand2 = false;
				prevSel2 = curSelGO2;
			}
		}

	}
	// Test functions for  button onClick event
	public void task () {
		LoadScene("SceneTest", menuWrapper);
		Debug.Log ("play scene");
		gameModePanel.gameObject.SetActive(false);
		
	}

	public void bask () {
		menuPanel.gameObject.SetActive(false);
		gameModePanel.gameObject.SetActive(true);
		characterPanel.gameObject.SetActive(false);
		EventSystem.GetComponent<EventSystem>().SetSelectedGameObject(gm1Button);
	}

	public void back () {
		Debug.Log ("back");
		menuPanel.gameObject.SetActive(true);
		gameModePanel.gameObject.SetActive(false);
		EventSystem.GetComponent<EventSystem>().SetSelectedGameObject(gameModeButton);
	}

	public void backG () {
		Debug.Log ("back");
		menuPanel.gameObject.SetActive(true);
		gameModePanel.gameObject.SetActive(false);
		characterPanel.gameObject.SetActive(false);
		EventSystem.GetComponent<EventSystem>().SetSelectedGameObject(gameModeButton);
	}

	public void chr() {
		EventSystem.GetComponent<EventSystem>().SetSelectedGameObject(randomButton);
		gameModePanel.gameObject.SetActive(false);
		characterPanel.gameObject.SetActive(true);

		curSelGO = EventSystem.GetComponent<EventSystem>().currentSelectedGameObject;
		curSelGO2 = EventSystem.GetComponent<EventSystem>().currentSelectedGameObject;
	}

	public void lvl() {
		EventSystem.GetComponent<EventSystem>().SetSelectedGameObject(levelRandomButton);
		characterPanel.gameObject.SetActive(false);
		levelPanel.gameObject.SetActive(true);

		lvlSelGO = EventSystem.GetComponent<EventSystem>().currentSelectedGameObject;
		curSelGO = EventSystem.GetComponent<EventSystem>().currentSelectedGameObject;
		curSelGO2 = EventSystem.GetComponent<EventSystem>().currentSelectedGameObject;
	}

	public void chr2() {
		EventSystem.GetComponent<EventSystem>().SetSelectedGameObject(randomButton);
		gameModePanel.gameObject.SetActive(false);
		characterPanel.gameObject.SetActive(true);
		levelPanel.gameObject.SetActive(false);

		curSelGO = EventSystem.GetComponent<EventSystem>().currentSelectedGameObject;
		curSelGO2 = EventSystem.GetComponent<EventSystem>().currentSelectedGameObject;
	}

	public void iMap () {
		EventSystem.GetComponent<EventSystem>().SetSelectedGameObject(iMapBackButton);
		menuPanel.gameObject.SetActive(false);
		gameModePanel.gameObject.SetActive(false);
		characterPanel.gameObject.SetActive(false);
		inputMappingPanelW.gameObject.SetActive(true);
		
	}

	public void backiMap () {
		EventSystem.GetComponent<EventSystem>().SetSelectedGameObject(gameModeButton);

		menuPanel.gameObject.SetActive(true);
		gameModePanel.gameObject.SetActive(false);
		characterPanel.gameObject.SetActive(false);
		inputMappingPanelW.gameObject.SetActive(false);
		
	}

	public void charSel() {

		int charId2 = getCharId(p1Txt.GetComponent<Text> ().text, 1);
		The.p1 = saveCharName(charId2);
		lastP2 = false;

		//  Test for transition to level select but now for test scene
		lvl();
	}

	public void startMapping(GameObject _gameObject){
		//EventSystem.GetComponent<EventSystem>().sendNavigationEvents = false;
		InputManager._InputManager.StartAssignment(_gameObject);
	}

	public void levels() {
		infoPanel = CreatePanel("level info pannel", emptyColorA, 0f, 0.45f, 1f, 0.95f, levelPanel, true);

		// Single player level select
		// levelPreviewImage = createImage("Preview image", lvlPrew, infoPanel, 0.02f, 0.04f, 0.98f, 0.96f);
		// levelPreviewText = createText("Preview Text", infoPanel, 0f, 0.1f, 1f ,0.2f);
		// levelPreviewText.GetComponent<Text>().color = Color.white;

		// PvP level select
		levelPreviewImage = createImage("Preview image", lvlPrew, infoPanel, 0.01f, 0.02f, 0.495f, 0.98f);
		levelPreviewText = createText("Preview Text", infoPanel, 0f, 0.1f, 1f ,0.2f);
		levelPreviewText.GetComponent<Text>().color = Color.white;

		levelPreviewImage2 = createImage("Preview image", lvlPrew, infoPanel, 0.505f, 0.02f, 0.99f, 0.98f);
		levelPreviewText2 = createText("Preview Text", infoPanel, 0f, 0.1f, 1f ,0.2f);
		levelPreviewText2.GetComponent<Text>().color = Color.white;

		

		levelRandomButton = CreateButton("Level Random Button", infoPanel, randLvlEffect, 0.25f, 0f, 0.75f, 0.08f);
		// TODO : add this when level rand img is added
		// levelRandomButton.transform.GetChild(0).GetComponent<Text>().text = "";

		levelSelectPanel = CreatePanel("Level Select Image Panel", emptyColorA, 0f, 0.1f, 1f, 0.45f, levelPanel, true);
		levelSelectPanel.AddComponent<GridLayoutGroup> ();
		RectTransform levelSelectRT = levelSelectPanel.GetComponent<RectTransform>();
		levelSelectRT.GetComponent<GridLayoutGroup> ().cellSize = new Vector2((levelSelectRT.rect.width-40) /4, (levelSelectRT.rect.height-30) / 3);
		levelSelectPanel.GetComponent<GridLayoutGroup>().spacing = new Vector2(10f, 10f);
	
        levelSelectPanel.GetComponent<GridLayoutGroup>().constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        levelSelectPanel.GetComponent<GridLayoutGroup>().constraintCount = 4;
		levelSelectRT.GetComponent<GridLayoutGroup> ().childAlignment = TextAnchor.MiddleCenter;

		levelSelectPanel2 = CreatePanel("Level Select Panel", emptyColor, 0f, 0.1f, 1f, 0.45f, levelPanel, true);
		levelSelectPanel2.AddComponent<GridLayoutGroup> ();
		RectTransform levelSelectRT2 = levelSelectPanel2.GetComponent<RectTransform>();
		levelSelectRT2.GetComponent<GridLayoutGroup> ().cellSize = new Vector2((levelSelectRT2.rect.width-40) /4, (levelSelectRT2.rect.height-30) / 3);
		levelSelectPanel2.GetComponent<GridLayoutGroup>().spacing = new Vector2(10f, 10f);
	
        levelSelectPanel2.GetComponent<GridLayoutGroup>().constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        levelSelectPanel2.GetComponent<GridLayoutGroup>().constraintCount = 4;
		levelSelectRT2.GetComponent<GridLayoutGroup> ().childAlignment = TextAnchor.MiddleCenter;


		for (int i = 0; i < 12; i++) {
			GameObject lvlImg = CreateImage(levelsAll[i].name, levelSelectPanel);
			lvlImg.GetComponent<Image> ().sprite = levelsAll[i].GetComponent<Image>().sprite;
			lvlImgChr.Add(lvlImg);
			GameObject lvlBtn = CreateButton("button: " +i, levelSelectPanel2, null, rails, 0);
			lvlBtn.GetComponent<Image> ().sprite = levelsAll[i].GetComponent<Image>().sprite;
            lvlBtn.transform.GetChild(0).GetComponent<Text>().text = "";
            lvlBtnChr.Add(lvlBtn);
		}


		levelBackPanel = CreatePanel("Level Back Panel", emptyColor, 0f, 0.03f, 1f, 0.1f, levelPanel, true);
		levelBackButton = CreateButton("Level Back", levelBackPanel, chr2, 0);
	}

	public void character() {
		headerPanel = CreatePanel("header", emptyColorA, 0.15f, 0.6f, 0.85f, 0.85f, characterPanel, true);
		// Create 1st character header panel
		GameObject p1Panel = CreatePanel ("P1 Panel", emptyColorA, 0f, 0f, 0.5f, 1f, headerPanel, true);
		p1Img = createImage("p1img", charArray[2], p1Panel, 0.65f, 0f, 1f, 1f);
		p1Txt = createText("p1txt", p1Panel, 0f, 0f, 0.65f, 1f);
		
		// Create 1st character header panel
		GameObject p2Panel = CreatePanel ("P2 Panel", emptyColorA, 0.5f, 0f, 1f, 1f, headerPanel, true);
		p2Img = createImage("p2img", charArray[2], p2Panel, 0f, 0f, 0.35f, 1f);
		p2Img.GetComponent<RectTransform> ().localScale = new Vector3 (-1f, 1f, 1f);
		p2Txt = createText("p2txt", p2Panel, 0.35f, 0f, 1f, 1f);

		characterSelectPanel = CreatePanel("character", emptyColorA, 0.15f, 0.15f, 0.85f, 0.55f, characterPanel, true);

		// image panel and button panel wrappers
		GameObject charImagePanel = CreatePanel("charImagePanel", emptyColor, 0f, 0f, 1f, 1f, characterSelectPanel, true);
		charImagePanel.AddComponent<HorizontalLayoutGroup> ();

		GameObject charButtonPanel = CreatePanel("charButtonPanel", emptyColor, 0f, 0f, 1f, 1f, characterSelectPanel, true);
		charButtonPanel.AddComponent<HorizontalLayoutGroup> ();

		chb = CreatePanel("Character image panel", emptyColor, 0f, 0f, 0.43f, 1f, charImagePanel, true);
		chb.AddComponent<GridLayoutGroup> ();
		RectTransform chbRT = chb.GetComponent<RectTransform>();
		chb.GetComponent<GridLayoutGroup> ().cellSize = new Vector2(chbRT.rect.width/4, chbRT.rect.height/3);
        chb.GetComponent<GridLayoutGroup>().constraint = GridLayoutGroup.Constraint.FixedRowCount;
        chb.GetComponent<GridLayoutGroup>().constraintCount = 3;

        ch1p = CreatePanel("Character button panel", emptyColor, 0f, 0f, 0.43f, 1f, charButtonPanel, true);
		ch1p.AddComponent<GridLayoutGroup> ();
		RectTransform ch1pRT = ch1p.GetComponent<RectTransform>();
		ch1p.GetComponent<GridLayoutGroup> ().cellSize = new Vector2(ch1pRT.rect.width/4, ch1pRT.rect.height/3);
        ch1p.GetComponent<GridLayoutGroup>().constraint = GridLayoutGroup.Constraint.FixedRowCount;
        ch1p.GetComponent<GridLayoutGroup>().constraintCount = 3;

        randomImgPanel = CreatePanel("Random Image panel", emptyColor, 0.45f, 0f, 0.55f, 1f, charImagePanel, true);
		randomImage = CreateImage("randomImage", randomImgPanel);
		randomImage.GetComponent<Image> ().sprite = random;

		randomPanel = CreatePanel("Random Button panel", emptyColor, 0.45f, 0f, 0.55f, 1f, charButtonPanel, true);
		randomButton = CreateButton("Random Button", randomPanel, randEffect, randomHover, 0);
		randomButton.GetComponent<Image> ().sprite = random;
        randomButton.transform.GetChild(0).GetComponent<Text>().text = "";

        chb2 = CreatePanel("Character image panel 2", emptyColor, 0.57f, 0f, 1f, 1f, charImagePanel, true);
		chb2.AddComponent<GridLayoutGroup> ();
		RectTransform chbRT2 = chb2.GetComponent<RectTransform>();
		chb2.GetComponent<GridLayoutGroup> ().cellSize = new Vector2(chbRT2.rect.width/4, chbRT2.rect.height/3);
        chb2.GetComponent<GridLayoutGroup>().constraint = GridLayoutGroup.Constraint.FixedRowCount;
        chb2.GetComponent<GridLayoutGroup>().constraintCount = 3;

        ch2p = CreatePanel("Character button panel", emptyColor, 0.57f, 0f, 1f, 1f, charButtonPanel, true);
		ch2p.AddComponent<GridLayoutGroup> ();
		RectTransform ch2pRT = ch2p.GetComponent<RectTransform>();
		ch2p.GetComponent<GridLayoutGroup> ().cellSize = new Vector2(ch2pRT.rect.width/4, ch2pRT.rect.height/3);
        ch2p.GetComponent<GridLayoutGroup>().constraint = GridLayoutGroup.Constraint.FixedRowCount;
        ch2p.GetComponent<GridLayoutGroup>().constraintCount = 3;

        for (int i = 0; i < 12; i++) {
			
			GameObject img = CreateImage(charactersAll[i].name, chb);
			img.GetComponent<Image> ().sprite = charactersAll[i].GetComponent<Image>().sprite;
			imgChr.Add(img);
			GameObject btn = CreateButton("button: " +i, ch1p, charSel, rails, 0);
			btn.GetComponent<Image> ().sprite = charactersAll[i].GetComponent<Image>().sprite;
            btn.transform.GetChild(0).GetComponent<Text>().text = "";
            btnChr.Add(btn);
		}
		for(int i = 12; i < 24; i++) {
			
			GameObject img = CreateImage(charactersAll[i].name, chb2);
			img.GetComponent<Image> ().sprite = charactersAll[i].GetComponent<Image>().sprite;
			imgChr.Add(img);
			GameObject btn2 = CreateButton("button: " +i , ch2p, charSel, rails, 0);
			btn2.GetComponent<Image> ().sprite = charactersAll[i].GetComponent<Image>().sprite;
            btn2.transform.GetChild(0).GetComponent<Text>().text = "";
            btnChr.Add(btn2);
		}

		
		backPanel = CreatePanel("back button", emptyColor, 0.15f, 0f, 0.85f, 0.15f, characterPanel, true);
		charBackButton = CreateButton("Back", backPanel, bask, 0);
	}

	
	public void inputMapping (){
		
		inputMappingPanelW = CreatePanel("Input Mapping Panel Wrapper", emptyColorA, menuWrapper, false);
		
		inputMappingPanel = CreatePanel("Input Mapping Panel", emptyColorA, 0f, 0.15f, 1f, 1f, inputMappingPanelW, true);
		inputMappingPanel.AddComponent<HorizontalLayoutGroup> ();
		inputMappingPanel.GetComponent<HorizontalLayoutGroup>().spacing = 10f;

		GameObject inputMappingTextPanel = CreatePanel("Input Text Panel", panelColor, inputMappingPanel, true);
		inputMappingTextPanel.AddComponent<VerticalLayoutGroup> ();

		inputMappingTextPanel.GetComponent<VerticalLayoutGroup>().padding.left = 20;
		//inputMappingTextPanel.GetComponent<VerticalLayoutGroup>().childControlWidth = false;

		foreach (KeyValuePair<string, KeyCode> key in InputManager._InputManager.keys) {

			GameObject textLohs = createText(key.Key, inputMappingTextPanel, 0f, 0f, 1f, 1f);
			textLohs.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
			textLohs.GetComponent<Text>().text = key.Key;
        }

		inputMappingPlayer1Panel = CreatePanel("Input Player 1 Panel", panelColor, inputMappingPanel, true);
		inputMappingPlayer1Panel.AddComponent<VerticalLayoutGroup> ();
		
		
		foreach (KeyValuePair<string, KeyCode> key in InputManager._InputManager._kbDefaultInputKeys) {
			GameObject lvlBtn = CreateButton(key.Value.ToString(), inputMappingPlayer1Panel, startMapping, rails, 0, true);
			lvlBtn.transform.GetChild(0).GetComponent<Text>().text = PlayerPrefs.GetString(key.Value.ToString());
			lvlBtn.transform.GetChild(0).GetComponent<Text>().resizeTextForBestFit = true;
			
		}

		inputMappingPlayer2Panel = CreatePanel("Input Player 2 Panel", panelColor, inputMappingPanel, true);	
		inputMappingPlayer2Panel.AddComponent<VerticalLayoutGroup> ();

		foreach (KeyValuePair<string, KeyCode> key in InputManager._InputManager.keys2) {
			GameObject lvlBtn = CreateButton("button: ", inputMappingPlayer2Panel, null, rails, 0);
			lvlBtn.transform.GetChild(0).GetComponent<Text>().text = key.Value.ToString();
			lvlBtn.transform.GetChild(0).GetComponent<Text>().resizeTextForBestFit = true;
			
		}

		GameObject _backPanel = CreatePanel("back button", emptyColor, 0f, 0f, 1f, 0.15f, inputMappingPanelW, true);
		_backPanel.AddComponent<HorizontalLayoutGroup> ();
		_backPanel.GetComponent<HorizontalLayoutGroup>().spacing = 10f;
		_backPanel.GetComponent<HorizontalLayoutGroup>().padding.top = 5;
		GameObject iMapDefaultButton = CreateButton("Defaults", _backPanel, InputManager._InputManager.setDefaults, 0);
		iMapBackButton = CreateButton("Back", _backPanel, backiMap, 0);
		
	}

	public void charRandom() {
		endRand = true;
		lastP1 = false;
		// set random selcected character but only sprite
		charL = (float)charArray.Length;
		randNumber = UnityEngine.Random.Range(0f, charL);
		randChar = charArray[(int)randNumber];
			
	}

	public void charRandom2() {
		endRand2 = true;
		lastP2 = false;
		// set random selcected character but only sprite
		charL = (float)charArray.Length;
		randNumber = UnityEngine.Random.Range(0f, charL);
		randChar2 = charArray[(int)randNumber];
			
	}
        
	public void randEffect() {
		if (Input.GetKey(KeyCode.Joystick2Button14) || Input.GetKey(KeyCode.Z)) {
            InvokeRepeating("charRandom", 0f, 0.05f);
				
		} 
		if (Input.GetKey(KeyCode.Joystick2Button14) || Input.GetKey(KeyCode.Return)) {
            InvokeRepeating("charRandom2", 0f, 0.05f);
		}
		Invoke("endRep", 1);
	}

	public void endRep() {
		CancelInvoke();
	}

	public void lvlRandom() {
		endRand = true;
		lastP1 = false;
		// set random selcected character but only sprite
		charL = (float)lvlAll.Length;
		randNumber = UnityEngine.Random.Range(0f, charL);
		randLvl = lvlAll[(int)randNumber];
			
	}

	public void randLvlEffect() {
		if (Input.GetKey(KeyCode.Joystick2Button14) || Input.GetKey(KeyCode.Return)) {
            InvokeRepeating("lvlRandom", 0f, 0.05f);
			Invoke("endRep", 1);
		}

        // TODO: add second char select button
		// if (Input.GetKey(KeyCode.Joystick2Button14) ) {
        // InvokeRepeating("lvlRandom", 0f, 0.05f);
		// 	Invoke("endRep", 1);
		// }
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


	public void fillLevels() {
		for (int i = 0; i < 12; i++) {	
			GameObject lvl = new GameObject();
			lvl.name = i.ToString();
			lvl.AddComponent<Image> ();
		
			if(lvlAll.Length > i) {
				lvl.GetComponent<Image> ().sprite = lvlAll[i];
				lvl.name = lvlAll[i].name;
			}
			else {
				lvl.GetComponent<Image> ().sprite = locked;
				lvl.name = "locked";
			}
				
			levelsAll.Add(lvl);
		}
	}

	public Sprite getButtonImage(string bName) {
		Sprite temp = rails;
		for(int i = 0; i< btnChr.Count; i++){
			if(bName == btnChr[i].name) {
				temp =  imgChr[i].GetComponent<Image>().sprite;
			}
		}
		return temp;
	}

	void setButtonSelected(string bName, Sprite sSelected){
		for(int i = 0; i< btnChr.Count; i++){
			if(bName == btnChr[i].name) {
				SpriteState spriteState = new SpriteState();
				spriteState = btnChr[i].GetComponent<Button>().spriteState;
				spriteState.highlightedSprite = sSelected;

				btnChr[i].GetComponent<Button>().spriteState = spriteState;
			}
		}
	}
		
	int getCharId(string bName) {
		int temp = 0;
		for(int i = 0; i< btnChr.Count; i++){
			if(bName == btnChr[i].name) {
				temp =  i;
			}
		}
		return temp;
	}

	int getCharId(string bName, int iss) {
		int temp = iss;
		for(int i = 0; i< btnChr.Count; i++){
			if(bName == imgChr[i].name) {
				temp =  i;
			}
		}
		return temp;
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

	string saveCharName(int id) {
		return imgChr[id].name;
	}

	int getLvlId(string bName) {
		int temp = 0;
		for(int i = 0; i< lvlBtnChr.Count; i++){
			if(bName == lvlBtnChr[i].name) {
				temp =  i;
			}
		}
		return temp;
	}

	string saveLvlName(int id) {
		return lvlImgChr[id].name;
	}

	public Sprite getLvlImage(string bName) {
		Sprite temp = rails;
		for(int i = 0; i< lvlBtnChr.Count; i++){
			if(bName == lvlBtnChr[i].name) {
				temp =  lvlImgChr[i].GetComponent<Image>().sprite;
			}
		}
		return temp;
	}

	void setLvlButtonSelected(string bName, Sprite sSelected){
		for(int i = 0; i< lvlBtnChr.Count; i++){
			if(bName == lvlBtnChr[i].name) {

				SpriteState spriteState = new SpriteState();
				spriteState = lvlBtnChr[i].GetComponent<Button>().spriteState;
				spriteState.highlightedSprite = sSelected;

				lvlBtnChr[i].GetComponent<Button>().spriteState = spriteState;
			}
		}
	}
}