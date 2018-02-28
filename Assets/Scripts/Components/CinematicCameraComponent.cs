using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicCameraComponent : MonoBehaviour {

    public float time;
    public float timeSkip;

    public GameObject start;
    public GameObject end;

    private Timer timer;
    private Timer skipTimer;

	void Start(){
        timer = new Timer(time);
        timer.Start();
        skipTimer = new Timer(timeSkip);
	}

	void Update(){
        if(skipTimer.Finished()){
            float t = Mathf.SmoothStep(0.0f, 1.0f, timer.Parameterized());

            transform.position = Vector3.Lerp(start.transform.position, end.transform.position, t);
            transform.rotation = Quaternion.Slerp(start.transform.rotation, end.transform.rotation, t);

            skipTimer.Start();
        }

        if(timer.Finished()){
            timer.Start();
        }
	}
}
