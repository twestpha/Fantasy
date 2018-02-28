using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class OptionalLayerTriggerComponent : MonoBehaviour {

    public GameObject cameraObject;
    private CameraCompositorComponent compositor;

	void Start(){
        if(!GetComponent<BoxCollider>().isTrigger){
            Debug.LogWarning("GameObject " + gameObject + "with Optional Layer Trigger Component should be trigger");
            GetComponent<BoxCollider>().isTrigger = true;
        }

        compositor = cameraObject.GetComponent<Camera>().GetComponent<CameraCompositorComponent>();
	}

	void Update(){

	}

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player"){
            compositor.renderingForePlateOptional = true;
        }
    }

    void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Player"){
            compositor.renderingForePlateOptional = false;
        }
    }
}
