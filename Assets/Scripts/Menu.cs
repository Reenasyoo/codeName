
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour{
	
	// Canvas Object
	public GameObject menuWrapper;
	public GameObject eventSystem;
	
	// Menu panel
	public GameObject menuPanel;
	public GameObject gameModeButton;
	public GameObject helpButton;
	
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

	// Character selecting objects
	public GameObject characterSelectPanel;
	public GameObject ch1p;
	public GameObject ch2p;
	
	// Random character selecting
	public GameObject randomPanel;
	public GameObject randomButton;

	//Chracter selecting panel back button
	public GameObject backPanel;
	
	
	
	// Tests
	// Working
	public Color32 panelColor = new Color32(122, 55, 163, 255);
	public Color32 emptyColor = new Color32(122, 55, 163, 0);
	public Sprite textureT;
	public Sprite buttonTexture;
	public Sprite buttonTextureH;

	public Sprite charTest;

	// public Sprite[] charArray = new Sprite[5];
	public Sprite[] charArray;

	// Use this for initialization
	void Start () {
		eventSystem = GameObject.Find("EventSystem");

		textureT = Resources.Load<Sprite>("PanelTest");
		buttonTexture = Resources.Load<Sprite>("ButtonTest");
		buttonTextureH = Resources.Load<Sprite>("ButtonTestHover");
		charArray = Resources.LoadAll<Sprite>("32x32char");

		//string spriteSheet = AssetDatabase.GetAssetPath(charTest);
 		// charArray = AssetDatabase.LoadAllAssetsAtPath( spriteSheet ).OfType<Sprite>().ToArray();

/*
		for(int i = 0; i < charArray.Length; i++) {
			string charname = "Char_" + i.ToString();
			Debug.Log(charname);
			charArray[i] = Resources.Load<Sprite>(charname);
		}
*/
		CreateCanvas("Canvas");

		menuPanel = CreatePanel("Menu Panel", panelColor, menuWrapper, true);
		gameModeButton = CreateButton(60, "Game Modes", menuPanel, bask);
		helpButton = CreateButton(0, "Help", menuPanel, null);
 		eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(gameModeButton);

		characterPanel = CreatePanel("character wrapper pannel", emptyColor, 0f, 0f, 1f, 1f, menuWrapper, false);
		character();

		// TEST
		gameModePanel = CreatePanel("Game Mode Panel", Color.blue, menuWrapper, false);
		gm1Button = CreateButton(60, "character select", gameModePanel, chr);
		gm2Button = CreateButton(0, "TEST LVL2", gameModePanel, task);
		backButton = CreateButton(-60, "Back", gameModePanel, back);


		
	}
	
	// Update is called once per frame
	void Update () {
		GameObject curSelGO = eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject;
		p1Txt.GetComponent<Text> ().text = curSelGO.name;
		p1Img.GetComponent<Image> ().sprite = curSelGO.GetComponent<Image> ().sprite;
	}

	// Test function for  button onClick event
	public void task () {
		SceneManager.LoadScene("SceneTest");
		Debug.Log ("play scene");
		// TODO: change to .setActive()
		gameModePanel.gameObject.active = false;
	}

	public void bask () {
		Debug.Log ("game mode panel");
		menuPanel.gameObject.active = false;
		gameModePanel.gameObject.active = true;
		characterPanel.gameObject.active = false;
		eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(gm1Button);
	}

	public void back () {
		Debug.Log ("back");
		menuPanel.gameObject.active = true;
		gameModePanel.gameObject.active = false;
		eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(gameModeButton);
	}

	public void backG () {
		Debug.Log ("back");
		menuPanel.gameObject.active = true;
		gameModePanel.gameObject.active = false;
		characterPanel.gameObject.active = false;
		eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(gameModeButton);
	}
	public void chr() {
		gameModePanel.gameObject.active = false;
		characterPanel.gameObject.active = true;
		eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(randomButton);
	}

	public void character() {
		
		headerPanel = CreatePanel("header", Color.blue, 0.15f, 0.6f, 0.85f, 0.85f, characterPanel, true);
		GameObject p1Panel = CreatePanel ("P1 Panel", Color.red, 0f, 0f, 0.5f, 1f, headerPanel, true);
		p1Img = createImage("p1img", buttonTexture, p1Panel, 0.65f, 0f, 1f, 1f);
		p1Txt = createText("p1txt", p1Panel, 0f, 0f, 0.65f, 1f);
		GameObject p2Panel = CreatePanel ("P2 Panel", Color.red, 0.5f, 0f, 1f, 1f, headerPanel, true);

		characterSelectPanel = CreatePanel("character", Color.red, 0.15f, 0.15f, 0.85f, 0.55f, characterPanel, true);
		ch1p = CreatePanel("ch1p", Color.red, 0f, 0f, 0.45f, 1f, characterSelectPanel, true);
		ch1p.AddComponent<GridLayoutGroup> ();
		RectTransform ch1pRT = ch1p.GetComponent<RectTransform>();
		ch1p.GetComponent<GridLayoutGroup> ().cellSize = new Vector2(ch1pRT.rect.width/4, ch1pRT.rect.height/3);	
		for(int i = 0; i < 12; i++) {
			CreateButton(0, "suuds", ch1p, bask);
		}

		ch2p = CreatePanel("ch2p", Color.red, 0.55f, 0f, 1f, 1f, characterSelectPanel, true);
		ch2p.AddComponent<GridLayoutGroup> ();
		RectTransform ch2pRT = ch2p.GetComponent<RectTransform>();
		ch2p.GetComponent<GridLayoutGroup> ().cellSize = new Vector2(ch2pRT.rect.width/4, ch2pRT.rect.height/3);	
		for(int i = 0; i < 9; i++) {
			GameObject btn = CreateButton(0, "suuds : " + i, ch2p, bask);
			btn.GetComponent<Image> ().sprite = charArray[i];
		}

		randomPanel = CreatePanel("Random Button panel", Color.red, 0.45f, 0f, 0.55f, 1f, characterSelectPanel, true);
		randomPanel.AddComponent<HorizontalLayoutGroup> ();
		randomPanel.GetComponent<HorizontalLayoutGroup> ().childForceExpandHeight = true;
		randomPanel.GetComponent<HorizontalLayoutGroup> ().childForceExpandWidth = true;
		randomButton = CreateButton(0, "Random Button", randomPanel, bask);

		backPanel = CreatePanel("back button", emptyColor, 0.15f, 0f, 0.85f, 0.15f, characterPanel, true);
		GameObject charBackButton = CreateButton(0, "Back", backPanel, bask);

	}

	public void CreateCanvas(string cName ) {
		// initialzie canvas
		menuWrapper = new GameObject();
		menuWrapper.name = cName;
		menuWrapper.AddComponent<Canvas> ();
		menuWrapper.GetComponent<Canvas> ().renderMode = RenderMode.ScreenSpaceOverlay;
		menuWrapper.AddComponent<CanvasScaler> ();
		menuWrapper.AddComponent<GraphicRaycaster> ();
		menuWrapper.AddComponent<Image> ();
		menuWrapper.GetComponent<Image> ().color = new Color(0.53f, 0.31f, 0.99f, 0f);
	}

	public GameObject CreatePanel(string pName, Color32 pColor, GameObject parent, bool pActive) {
		// create panel
		GameObject panel;
		panel = new GameObject();
		panel.gameObject.active = pActive;
		panel.name = pName;
		panel.AddComponent<CanvasRenderer> ();
		panel.AddComponent<RectTransform> ();
		panel.transform.SetParent (parent.transform);
		RectTransform panelTransform = panel.GetComponent<RectTransform> ();
		panelTransform.offsetMax = Vector2.one;
		panelTransform.offsetMin = Vector2.zero;
		panelTransform.anchorMin = new Vector2 (0.25f, 0.25f);
		panelTransform.anchorMax = new Vector2 (0.75f, 0.75f);
		//panelTransform.pivot = new Vector2(0,1);
		panel.AddComponent<Image> ();
		panel.GetComponent<Image>().sprite = textureT;
		Image panelImage = panel.GetComponent<Image> ();
		panelImage.color = pColor;
		panelImage.fillCenter = true;

		return panel;
	}
	
	public GameObject CreatePanel(string pName, Color32 pColor, float minX, float minY, float maxX, float maxY, GameObject parent, bool pActive) {
		// create panel
		GameObject panel;
		panel = new GameObject();
		panel.gameObject.active = pActive;
		panel.name = pName;
		panel.AddComponent<CanvasRenderer> ();
		panel.AddComponent<RectTransform> ();
		panel.transform.SetParent (parent.transform);
		RectTransform panelTransform = panel.GetComponent<RectTransform> ();
		panelTransform.offsetMax = Vector2.one;
		panelTransform.offsetMin = Vector2.zero;
		panelTransform.anchorMin = new Vector2 (minX, minY);
		panelTransform.anchorMax = new Vector2 (maxX, maxY);
		//panelTransform.pivot = new Vector2(0,1);
		panel.AddComponent<Image> ();
		panel.GetComponent<Image>().sprite = textureT;
		Image panelImage = panel.GetComponent<Image> ();
		panelImage.color = pColor;
		panelImage.fillCenter = true;

		return panel;
	}

	public GameObject CreateButton(int y, string bText, GameObject parent, Action func) {

		// Creates Button with static event
		GameObject b1Text;
		GameObject b1;
		b1 = new GameObject ();
		b1.name = bText;
		b1.AddComponent<RectTransform> ();
		b1.AddComponent<CanvasRenderer> ();
		b1.AddComponent<Image> ();
		b1.GetComponent<Image> ().sprite = buttonTexture;
		b1.transform.SetParent (parent.transform);
		RectTransform b1Transform = b1.GetComponent<RectTransform> ();
		b1Transform.offsetMin = Vector2.zero;
		b1Transform.offsetMax = Vector2.zero;
		b1Transform.sizeDelta = new Vector2 (100, 50);
		b1Transform.anchorMin = new Vector2 (0.5f, 0.5f);
		b1Transform.anchorMax = new Vector2 (0.5f, 0.5f);
		b1Transform.localPosition = new Vector3(0,y,0);
		b1.AddComponent<Button> ();
		b1.GetComponent<Button>().onClick.AddListener(() => func());
		b1.GetComponent<Button>().transition = Selectable.Transition.SpriteSwap;

		SpriteState spriteState = new SpriteState();
        spriteState = b1.GetComponent<Button>().spriteState;
		spriteState.highlightedSprite = buttonTextureH;
		b1.GetComponent<Button>().spriteState = spriteState;
		
		// Button text comonent
		b1Text = new GameObject();
		b1Text.name = "Button Text";
		b1Text.AddComponent<RectTransform> ();
		b1Text.AddComponent<Text> ();
		b1Text.GetComponent<Text> ().alignment = TextAnchor.MiddleCenter;
		b1Text.transform.SetParent (b1.transform);
		RectTransform b1TextT = b1Text.GetComponent<RectTransform> ();
		b1TextT.offsetMin = Vector2.zero;
		b1TextT.offsetMax = Vector2.zero;
		b1TextT.anchorMin = Vector2.zero;
		b1TextT.anchorMax = Vector2.one;
		Text b1TextComponent = b1Text.GetComponent<Text> ();
		Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
     	b1TextComponent.font = ArialFont;
     	b1TextComponent.material = ArialFont.material;
		b1TextComponent.text = bText;
		b1TextComponent.color = Color.black;



		return b1;
	}

	public GameObject createImage(string iName, Sprite iSprite, GameObject parent, float minX , float minY, float maxX, float maxY) {
		// initialzie canvas
		GameObject img = new GameObject();
		img.name = iName;
		img.AddComponent<RectTransform> ();
		img.transform.SetParent (parent.transform);
		RectTransform imgTransform = img.GetComponent<RectTransform> ();
		imgTransform.offsetMax = Vector2.one;
		imgTransform.offsetMin = Vector2.zero;
		imgTransform.anchorMin = new Vector2 (minX, minY);
		imgTransform.anchorMax = new Vector2 (maxX, maxY);

		img.AddComponent<CanvasRenderer> ();
		img.AddComponent<Image> ();
		//img.GetComponent<Image> ().color = Color.blue;
		img.GetComponent<Image>().sprite = iSprite;

		return img;
	}

	public GameObject createText(string tName, GameObject parent, float minX , float minY, float maxX, float maxY) {
		string bText = "test";
		
		GameObject txt = new GameObject();
		txt.name = tName;
		txt.AddComponent<RectTransform> ();
		txt.transform.SetParent (parent.transform);
		RectTransform txtTransform = txt.GetComponent<RectTransform> ();
		txtTransform.offsetMax = Vector2.one;
		txtTransform.offsetMin = Vector2.zero;
		txtTransform.anchorMin = new Vector2 (minX, minY);
		txtTransform.anchorMax = new Vector2 (maxX, maxY);

		txt.AddComponent<CanvasRenderer> ();

		txt.AddComponent<Text> ();
		Text txtComp = txt.GetComponent<Text> ();
		Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
		txtComp.alignment = TextAnchor.MiddleCenter;
     	txtComp.font = ArialFont;
     	txtComp.material = ArialFont.material;
		txtComp.text = bText;
		txtComp.color = Color.black;
		txtComp.fontSize = 40;

		return txt;
	}
}
