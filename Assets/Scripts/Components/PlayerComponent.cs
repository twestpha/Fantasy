using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponent : MonoBehaviour {

    public float walkSpeed;
    public float runSpeed;

    public float moveSpeedDampener = 1.0f;

    public GameObject exclamationPoint;

	void Start(){

	}

	void FixedUpdate(){
        Vector3 input = new Vector3(-Input.GetAxis("Horizontal"), 0.0f, -Input.GetAxis("Vertical"));

        if(input.sqrMagnitude < 0.01f || moveSpeedDampener ==  0.0f){
            return;
        }

        transform.rotation = Quaternion.Euler(0.0f, Mathf.Atan2(input.x, input.z) * Mathf.Rad2Deg, 0.0f);

        float moveSpeed = walkSpeed;
        if(Input.GetKey(KeyCode.JoystickButton1)){
            moveSpeed = runSpeed;
        }

        Vector3 newPosition = transform.position + input * moveSpeed * moveSpeedDampener * Time.deltaTime;
        float height = newPosition.y;
        newPosition.y = 0.0f;

        RaycastHit firstRayHit;
        if(Physics.Raycast(newPosition + Vector3.up * (height + 1.0f), Vector3.down, out firstRayHit, 4.0f, NavMeshComponent.FlatNavMeshLayer)){

            RaycastHit rayHit;
            Physics.Raycast(firstRayHit.point + Vector3.up * (height + 1.0f), Vector3.down, out rayHit, 4.0f, NavMeshComponent.NavMeshLayer);

            transform.position = new Vector3(newPosition.x, rayHit.point.y, newPosition.z);
        } else {
            RaycastHit sphereCastHit;
            Physics.SphereCast(newPosition + Vector3.up * 2.0f, 1.0f, Vector3.down, out sphereCastHit, 3.0f, NavMeshComponent.FlatNavMeshLayer);

            RaycastHit rayHit;
            Physics.Raycast(sphereCastHit.point + Vector3.up * (height + 1.0f), Vector3.down, out rayHit, 4.0f, NavMeshComponent.NavMeshLayer);

            transform.position = new Vector3(sphereCastHit.point.x, rayHit.point.y, sphereCastHit.point.z);
        }
	}

    public void ShowExclamationPoint(){
        exclamationPoint.GetComponent<MeshRenderer>().enabled = true;
    }

    public void HideExclamationPoint(){
        exclamationPoint.GetComponent<MeshRenderer>().enabled = false;
    }
}
