using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ShadowTriggerComponent : MonoBehaviour {

    public GameObject playerBody;
    public GameObject playerHair;

    public Material shadowOverlay;

	void Start(){
        if(!GetComponent<BoxCollider>().isTrigger){
            Debug.LogWarning("GameObject " + gameObject + "with Shadow Trigger Component should be trigger");
            GetComponent<BoxCollider>().isTrigger = true;
        }
	}

	void Update(){

	}

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player"){
            Material[] bodyMaterials = new Material[2];
            Material[] hairMaterials = new Material[2];

            bodyMaterials[0] = playerBody.GetComponent<MeshRenderer>().material;
            hairMaterials[0] = playerHair.GetComponent<MeshRenderer>().material;

            bodyMaterials[1] = shadowOverlay;
            hairMaterials[1] = shadowOverlay;

            playerBody.GetComponent<MeshRenderer>().materials = bodyMaterials;
            playerHair.GetComponent<MeshRenderer>().materials = hairMaterials;
        }
    }

    void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Player"){
            Material[] bodyMaterials = new Material[1];
            Material[] hairMaterials = new Material[1];

            bodyMaterials[0] = playerBody.GetComponent<MeshRenderer>().material;
            hairMaterials[0] = playerHair.GetComponent<MeshRenderer>().material;

            // should probably destroy shadow material instances

            playerBody.GetComponent<MeshRenderer>().materials = bodyMaterials;
            playerHair.GetComponent<MeshRenderer>().materials = hairMaterials;
        }
    }
}
