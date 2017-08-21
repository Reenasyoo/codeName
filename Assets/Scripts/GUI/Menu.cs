﻿
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;
	using UnityEngine.SceneManagement;
	using UnityEngine.EventSystems;

	public class Menu : MonoBehaviour{

		public Color32 panelColor = new Color32(122, 55, 163, 255);
		public Color32 emptyColor = new Color32(122, 55, 163, 0);
		public Color panelColor2 = new Color(122, 55, 163, 255);
		public Sprite textureT;
		public Sprite buttonTexture;
		public Sprite buttonTextureH;

		// Use this for initialization
		void Start () {

		}
		
		// Update is called once per frame
		void Update () {
		}

		public GameObject CreateEventSystem() {
			
			GameObject EventSystem = new GameObject();
			EventSystem.name = "Event System";
			EventSystem.AddComponent<EventSystem> ();
			EventSystem.AddComponent<StandaloneInputModule> ();

			return EventSystem;
		}

		public GameObject CreateCanvas(string cName ) {
			// initialzie canvas
			GameObject menuWrapper = new GameObject();
			menuWrapper.name = cName;
			menuWrapper.AddComponent<Canvas> ();
			menuWrapper.GetComponent<Canvas> ().renderMode = RenderMode.ScreenSpaceOverlay;
			menuWrapper.AddComponent<CanvasScaler> ();
			menuWrapper.AddComponent<GraphicRaycaster> ();
			menuWrapper.AddComponent<Image> ();
			menuWrapper.GetComponent<Image> ().color = new Color(0.53f, 0.31f, 0.99f, 0f);

			return menuWrapper;
		}

		public GameObject CreatePanel(string pName, Color32 pColor, GameObject parent, bool pActive) {
			
			Sprite textureT = Resources.Load<Sprite>("PanelTest");
			
			// create panel
			GameObject panel;
			panel = new GameObject();
			panel.gameObject.SetActive(pActive);
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
			Image panelImageComponent = panel.GetComponent<Image> ();
			panelImageComponent.color = pColor;
			panelImageComponent.fillCenter = true;

			return panel;
		}
		
		public GameObject CreatePanel(string pName, Color32 pColor, float minX, float minY, float maxX, float maxY, GameObject parent, bool pActive) {
			Sprite textureT = Resources.Load<Sprite>("PanelTest");
			
			// create panel
			GameObject panel;
			panel = new GameObject();
			panel.gameObject.SetActive(pActive);
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
			Image panelImageComponent = panel.GetComponent<Image> ();
			panelImageComponent.color = pColor;
			panelImageComponent.fillCenter = true;

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

		public GameObject CreateButton(int y, string bText, GameObject parent, Action func, Sprite hSprite) {

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
			spriteState.highlightedSprite = hSprite;
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

		public GameObject CreateImage(string imgText, GameObject parent, float aMinX = 0.5f, float aMinY = 0.5f, float aMaxX = 0.5f, float aMaxY = 0.5f, Vector2 sDelta = default(Vector2) ) {
			
			// Create Image
			GameObject img;
			img = new GameObject ();
			img.name = imgText;
			img.AddComponent<RectTransform> ();
			img.AddComponent<CanvasRenderer> ();
			img.AddComponent<Image> ();
			img.GetComponent<Image> ().sprite = buttonTexture;

			img.transform.SetParent (parent.transform);
			RectTransform imgTransform = img.GetComponent<RectTransform> ();
			imgTransform.offsetMin = Vector2.zero;
			imgTransform.offsetMax = Vector2.zero;
			imgTransform.sizeDelta = sDelta;
			imgTransform.anchorMin = new Vector2 (aMinX, aMinY);
			imgTransform.anchorMax = new Vector2 (aMaxX, aMaxY);
			imgTransform.anchoredPosition = new Vector2(0,0);

			return img;
		}

		public GameObject CreateLoader(string sName, GameObject parent,Sprite sBackgroundImage,Sprite sFillerImage, float aMinX = 0.5f, float aMinY = 0.5f, float aMaxX = 0.5f, float aMaxY = 0.5f, Vector2 aPos = default(Vector2), Vector2 sDelta = default(Vector2)) {	
			// TODO : Add text
			
			// Slider
			GameObject slider = new GameObject();
			
			slider.transform.SetParent (parent.transform, false);
			slider.name = sName;
			slider.AddComponent<Slider> ();
			slider.AddComponent<RectTransform> ();
			RectTransform sTransform = slider.GetComponent<RectTransform>();
			sTransform.offsetMin = Vector2.zero;
			sTransform.offsetMax = Vector2.zero;
			sTransform.anchorMin = new Vector2 (aMinX, aMinY);
			sTransform.anchorMax = new Vector2 (aMaxX, aMaxY);
			sTransform.sizeDelta = sDelta;
			sTransform.anchoredPosition = aPos;

			// Background
			GameObject sBackground = new GameObject();
			sBackground.name = "Background";
			sBackground.AddComponent<RectTransform> ();
			sBackground.transform.SetParent (slider.transform);

			RectTransform bTransform = sBackground.GetComponent<RectTransform>(); 
			bTransform.offsetMin = Vector2.zero;
			bTransform.offsetMax = Vector2.zero;
			bTransform.anchorMin = new Vector2 (0f, 0f);
			bTransform.anchorMax = new Vector2 (1f, 1f);

			sBackground.AddComponent<CanvasRenderer> ();
			sBackground.AddComponent<Image> ();
			sBackground.GetComponent<Image> ().sprite = sBackgroundImage;

			// Fill
			GameObject sFiller = new GameObject();
			sFiller.name = "Filler";
			sFiller.AddComponent<RectTransform> ();
			sFiller.transform.SetParent (sBackground.transform);

			RectTransform fTransform = sFiller.GetComponent<RectTransform>(); 
			fTransform.offsetMin = Vector2.zero;
			fTransform.offsetMax = Vector2.zero;
			fTransform.anchorMin = new Vector2 (0f, 0f);
			fTransform.anchorMax = new Vector2 (0f, 1f);
			
			sFiller.AddComponent<CanvasRenderer> ();
			sFiller.AddComponent<Image> ();
			sFiller.GetComponent<Image> ().sprite = sFillerImage;

			slider.GetComponent<Slider>().name = "slider";
			slider.GetComponent<Slider>().targetGraphic = sFiller.GetComponent<Image> ();
			slider.GetComponent<Slider>().fillRect = sFiller.GetComponent<RectTransform> ();
			//slider.GetComponent<Slider>().maxValue = 100;

			return slider;
		}

		public void quitGame() {
			Application.Quit();
			Debug.Log ("Quit");
		}


		public void LoadScene( string sceneName, GameObject parent) {
			StartCoroutine(LoadAsync(sceneName, parent));
		}


		IEnumerator LoadAsync(string sceneName, GameObject parent) {
			
			Sprite bSprite = Resources.Load<Sprite>("sliderBg");
			Sprite fSprite = Resources.Load<Sprite>("SliderFiller");
			GameObject slider = CreateLoader("Slider", parent, bSprite, fSprite);
			AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
			operation.allowSceneActivation = false;

			while (!operation.isDone) {

				slider.GetComponent<Slider>().value = operation.progress;
				if (operation.progress == 0.9f){

					slider.GetComponent<Slider>().value = 1f;
					operation.allowSceneActivation = true;
				}
				
				yield return null;
			}
			
		}

	// END	
	}
/*


 */
