using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshRenderer))]
public class NavMeshComponent : MonoBehaviour {

    public const int NavMeshLayer     = 1 << 8;
    public const int FlatNavMeshLayer = 1 << 9;

	void Start(){
        if(1 << gameObject.layer != NavMeshLayer){
            Debug.LogError("Nav Mesh is layer " + (1 << gameObject.layer) + ", but should be layer " + NavMeshLayer);
        }

        GetComponent<MeshRenderer>().enabled = false;
	}

	void Update(){

	}
}
