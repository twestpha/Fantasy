using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggerComponent : MonoBehaviour {

    public GameObject fromCameraObject;
    public GameObject toCameraObject;

    public GameObject battleObject;

	void Start(){

	}

	void Update(){

	}

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player"){
            fromCameraObject.GetComponent<Camera>().enabled = false;
            fromCameraObject.GetComponent<CameraCompositorComponent>().rendering = false;

            toCameraObject.GetComponent<Camera>().enabled = true;
            toCameraObject.GetComponent<CameraCompositorComponent>().rendering = true;
            toCameraObject.GetComponent<CameraCompositorComponent>().SetFadeTint(1.0f);

            battleObject.GetComponent<BattleComponent>().BeginBattle();
        }
    }
}
