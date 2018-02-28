using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class SignpostComponent : MonoBehaviour {

    private PlayerComponent playerComponent;
    private DungeonUIComponent uiControllerComponent;

    private bool playerNear;
    private bool displaying;

    // Make these an array and then handle input for iterating through it...
    public bool top;
    public string title;
    public string message;

	void Start(){
        playerComponent = GameObject.FindWithTag("Player").GetComponent<PlayerComponent>();
        uiControllerComponent = GameObject.FindWithTag("UIController").GetComponent<DungeonUIComponent>();
	}

	void Update(){
        if(Input.GetKeyDown(KeyCode.JoystickButton0 /* A button */) && playerNear && !uiControllerComponent.displaying){
            uiControllerComponent.DisplayMessage(top, title, message);
            playerComponent.HideExclamationPoint();
        } else if(playerNear && !uiControllerComponent.displaying){
            playerComponent.ShowExclamationPoint();
        }
        // state for when player's done reading
	}

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player"){
            playerNear = true;
            other.gameObject.GetComponent<PlayerComponent>().ShowExclamationPoint();
        }
    }

    void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Player"){
            playerNear = false;
            other.gameObject.GetComponent<PlayerComponent>().HideExclamationPoint();
        }
    }
}
