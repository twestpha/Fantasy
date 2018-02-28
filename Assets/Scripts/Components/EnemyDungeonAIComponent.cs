using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDungeonAIComponent : MonoBehaviour {

    public float moveSpeed;

    public int startIndex;
    public GameObject[] waypoints;
    private int currentIndex;

    private float previousRotation;
    private float rotationVelocity;

	void Start(){
        currentIndex = startIndex;
	}

	void Update(){
        Vector3 toNextPoint = waypoints[currentIndex].transform.position - transform.position;
        toNextPoint.y = 0.0f;

        if(toNextPoint.magnitude < 0.1f){
            currentIndex++;
            currentIndex = currentIndex % waypoints.Length;
        }

        Vector3 newPosition = transform.position + (toNextPoint.normalized * moveSpeed * Time.deltaTime); // need to clamp this?

        RaycastHit rayHit;
        Physics.Raycast(newPosition + Vector3.up, Vector3.down, out rayHit, 4.0f, NavMeshComponent.NavMeshLayer);
        newPosition.y = rayHit.point.y;

        transform.position = newPosition;

        float targetRotation = Mathf.Atan2(toNextPoint.x, toNextPoint.z) * Mathf.Rad2Deg;
        float rotation = Mathf.SmoothDamp(previousRotation, targetRotation, ref rotationVelocity, 0.1f);
        transform.rotation = Quaternion.Euler(-90.0f, 0.0f, rotation);
        previousRotation = rotation;
	}
}
