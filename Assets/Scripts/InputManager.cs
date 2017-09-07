// Input Manager remake
using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;


public class InputManager : MonoBehaviour {

    public static InputManager _InputManager;
    public string[] joystickNames;
    public int connectedJoystickCount;

    private Event currentKeyEvent;
	private Text buttonText;
	private KeyCode newKey;

    private Event newSelEvent;

    private int counter;

	bool waitingForKey;

    public bool def;

    public Dictionary <string, KeyCode> keys = new Dictionary<string, KeyCode>();
    public Dictionary <string, KeyCode> keys2 = new Dictionary<string, KeyCode>();
    public Dictionary <string, KeyCode> _kbDefaultInputKeys = new Dictionary<string, KeyCode>();

    public KeyCode[] _kbDefaultInput = { KeyCode.W, KeyCode.A, KeyCode.D, KeyCode.G, KeyCode.H, KeyCode.F, KeyCode.E};
    public KeyCode[] _kbSecondaryDefaultInput = { KeyCode.UpArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.Comma, KeyCode.Period, KeyCode.Semicolon, KeyCode.Return};

    public KeyCode Jump {get; set;}
    public KeyCode Left {get; set;}
    public KeyCode Right {get; set;}
    public KeyCode SpecO {get; set;}
    public KeyCode SpecW {get; set;}
    public KeyCode Shoot {get; set;}
    public KeyCode Esc {get; set;}
    public KeyCode Accept {get; set;}

    public string[] _keyString = {"Jump","Left","Right","SpecO","SpecW","Shoot","Accept", "Esc"};
    

    public bool done;

    public GameObject EventSystem;

    public InputManager() {
        //Start();
        keys.Add("Jump",KeyCode.Joystick1Button14);
        keys.Add("Left",KeyCode.LeftArrow);
        keys.Add("Right",KeyCode.RightArrow);
        keys.Add("SpecO",KeyCode.Joystick1Button10);
        keys.Add("SpecW",KeyCode.Joystick1Button11);
        keys.Add("Shoot",KeyCode.Joystick1Button13);
        keys.Add("Accept",KeyCode.Joystick1Button3);

        keys2.Add("Jump",KeyCode.Joystick1Button14);
        keys2.Add("Left",KeyCode.LeftArrow);
        keys2.Add("Right",KeyCode.RightArrow);
        keys2.Add("SpecO",KeyCode.Joystick1Button10);
        keys2.Add("SpecW",KeyCode.Joystick1Button11);
        keys2.Add("Shoot",KeyCode.Joystick1Button13);
        keys2.Add("Accept",KeyCode.Joystick1Button3);
        
        for (int i = 0; i < _kbDefaultInput.Length; i++)
        {
            _kbDefaultInputKeys.Add(_keyString[i],_kbDefaultInput[i]);
        }
    }

    void Awake() {
        def = false;
        counter = 0;
        done = false;
        waitingForKey = false;
        //Jump = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jumpKey", "Space"));
        if(_InputManager == null) {
            DontDestroyOnLoad(gameObject);
            _InputManager = this;
        }
        else if(_InputManager != this){
            Destroy(gameObject);
        }
        
        
    }

    void Start() {
        for (int i = 0; i < _kbDefaultInputKeys.Count; i++){
            PlayerPrefs.GetString(_keyString[i], _kbDefaultInput[i].ToString()); //save new key to PlayerPrefs
        }

        EventSystem =  GameObject.Find("Event System");
    }

    void OnGUI() {
        
        currentKeyEvent = Event.current;

        if(waitingForKey && counter > 2) {

		   // newKey = currentKeyEvent.keyCode;
		    waitingForKey = false;
            counter = 0;
		}
        
        if(def) {

            for (int i = 0; i < 7; i++){
					
             	PlayerPrefs.SetString(_keyString[i], _kbDefaultInput[i].ToString()); //save new key to PlayerPrefs
            }
            Debug.Log("saa");
            //def = false;
        }

    }

    // Get how much joysticks and witch are connected
    void connectedJostics(){
        connectedJoystickCount = Input.GetJoystickNames().Length;
        Debug.Log(connectedJoystickCount);
        for(int i = 0; i < connectedJoystickCount; i++){
            Debug.Log(Input.GetJoystickNames()[i]);
        }
    }

    // Get witch key was pressed
    void getKeyDown() {
        foreach(KeyCode kcode in Enum.GetValues(typeof(KeyCode))) {
            if (Input.GetKeyUp(kcode)) {
                 newKey = kcode;
                 counter++;
                 Debug.Log(" Counter : " + counter + " keycode :" + kcode + " ");
            }
        }
    }
   public IEnumerator WaitForKey(GameObject keyName)
	{   
		while(counter <2) {
            getKeyDown();
            if(counter == 1) {
                
                Text _keyNameText = keyName.transform.GetChild(0).GetComponent<Text>();
                _keyNameText.text = "Press Key";
            }
            yield return null;
        }
	}

    public void StartAssignment(GameObject keyName) {
		if(!waitingForKey) {
            Debug.Log(" Started to asign for input" + waitingForKey);
		    StartCoroutine(AssignKey(keyName));
        }
	}

    

    public IEnumerator AssignKey(GameObject keyName){   

		waitingForKey = true;
        
        yield return WaitForKey(keyName); //Executes endlessly until user presses a key
        Debug.Log(" 168 : " + keyName.name);

        Text _keyNameText = keyName.transform.GetChild(0).GetComponent<Text>();
        _kbDefaultInputKeys[keyName.name] = newKey;
        Debug.Log(newKey.ToString());
        _keyNameText.text = _kbDefaultInputKeys[keyName.name].ToString();
        PlayerPrefs.SetString(keyName.name.ToString(), newKey.ToString()); //save new key to PlayerPrefs

        EventSystem.GetComponent<EventSystem>().sendNavigationEvents = true;
		yield return null;
	}

    public void setDefaults() {
        def = true;
        

    }


    // TESTS
    void getA(KeyCode s) {
        if(Input.GetKeyDown(s)){
            Debug.Log("lohs");
        }
    }

    public void lohs(){
        foreach (KeyValuePair<string, KeyCode> key in keys)
        {
            Debug.Log(" Key : " + key.Key + " & Value : " + key.Value);
        }
    }

    // public string getKeyName(){
    //     foreach (KeyValuePair<string, KeyCode> key in keys)
    //     {   
    //         //return
    //         Debug.Log(" Key : " + key.Key + " & Value : " + key.Value);
    //     }
    // }
}