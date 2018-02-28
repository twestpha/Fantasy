using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionTriggerComponent : MonoBehaviour {

    public GameObject transitionGameObject;
    private TransitionComponent component;

	void Start(){
        component = transitionGameObject.GetComponent<TransitionComponent>();
	}

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player"){
            component.PlayerEnteredTrigger(gameObject);
        }
    }
}
