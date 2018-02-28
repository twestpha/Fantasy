using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonUIComponent : MonoBehaviour {

    public GameObject canvasObject;
    private Canvas canvas;
    public GameObject textBoxObject;
    public GameObject titleObject;
    public GameObject messageObject;

    private GameObject player;

    public Vector3 topPosition;
    public Vector3 bottomPosition;

    private Timer textDisplayTimer;
    private int characterIndex;

    private string fullMessage;

    public bool displaying;

	void Start(){
        textDisplayTimer = new Timer(0.01f);
        player = GameObject.FindWithTag("Player");

        canvas = canvasObject.GetComponent<Canvas>();
	}

	void Update(){
        if(displaying && textDisplayTimer.Finished()){
            characterIndex++;
            characterIndex = Mathf.Min(fullMessage.Length, characterIndex);
            messageObject.GetComponent<Text>().text = fullMessage.Substring(0, characterIndex);
            textDisplayTimer.Start();
        }

        if(displaying && characterIndex == fullMessage.Length && Input.GetKeyDown(KeyCode.JoystickButton0 /* A button */)){
            canvas.enabled = false;
            displaying = false;
            player.GetComponent<PlayerComponent>().moveSpeedDampener = 1.0f;
        } else if(displaying && Input.GetKeyDown(KeyCode.JoystickButton0 /* A button */) && characterIndex > 1){
            messageObject.GetComponent<Text>().text = fullMessage;
            characterIndex = fullMessage.Length;
            textDisplayTimer.SetParameterized(1.0f);
        }
	}

    public void DisplayMessage(bool top, string title, string message){
        displaying = true;

        canvasObject.GetComponent<Canvas>().enabled = true;
        player.GetComponent<PlayerComponent>().moveSpeedDampener = 0.0f;

        textBoxObject.GetComponent<RectTransform>().anchoredPosition3D = top ? topPosition : bottomPosition;

        titleObject.GetComponent<Text>().text = title;
        messageObject.GetComponent<Text>().text = "";
        fullMessage = message;

        characterIndex = 0;
        textDisplayTimer.Start();
    }
}
