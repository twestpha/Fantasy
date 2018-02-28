using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSoundTriggerComponent : MonoBehaviour {

    public AudioClip clip;

    private AmbientSoundController ambientSoundController;

	void Start(){
        ambientSoundController = GameObject.FindWithTag("AmbientSoundController").GetComponent<AmbientSoundController>();
	}

	void Update(){

	}

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player"){
            ambientSoundController.SetAmbientSound(clip);
        }
    }
}
