using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionComponent : MonoBehaviour {

    public GameObject doorAToB;
    public GameObject doorBToA;

    public GameObject teleportA;
    public GameObject teleportB;

    public GameObject cameraA;
    public GameObject cameraB;

    private GameObject player;

    private Timer fadeTimer;

    private bool transitionAtoB;

    private enum FadeState {
        Idling,
        FadeOut,
        FadeIn,
    }

    private FadeState state;

	void Start(){
        player = GameObject.FindWithTag("Player");
        fadeTimer = new Timer(0.6f);
        state = FadeState.Idling;
	}

    void Update(){
        if(state == FadeState.FadeOut){
            float parameterized = 1.0f - fadeTimer.Parameterized();

            player.GetComponent<PlayerComponent>().moveSpeedDampener = parameterized * parameterized;

            if(transitionAtoB){
                cameraA.GetComponent<CameraCompositorComponent>().SetFadeTint(parameterized);
            } else {
                cameraB.GetComponent<CameraCompositorComponent>().SetFadeTint(parameterized);
            }

            if(fadeTimer.Finished()){
                player.GetComponent<PlayerComponent>().moveSpeedDampener = 1.0f;

                cameraA.GetComponent<Camera>().enabled = !transitionAtoB;
                cameraB.GetComponent<Camera>().enabled = transitionAtoB;

                if(transitionAtoB){
                    cameraA.GetComponent<CameraCompositorComponent>().rendering = false;
                } else {
                    cameraB.GetComponent<CameraCompositorComponent>().rendering = false;
                }

                if(transitionAtoB){
                    cameraB.GetComponent<CameraCompositorComponent>().SetFadeTint(1.0f);
                } else {
                    cameraA.GetComponent<CameraCompositorComponent>().SetFadeTint(1.0f);
                }

                player.transform.position = transitionAtoB ? teleportB.transform.position : teleportA.transform.position;
                state = FadeState.FadeIn;

                fadeTimer.Start();
            }
        } else if(state == FadeState.FadeIn){
            if(transitionAtoB){
                cameraB.GetComponent<CameraCompositorComponent>().SetFadeTint(fadeTimer.Parameterized());
                cameraB.GetComponent<CameraCompositorComponent>().rendering = true;
            } else {
                cameraA.GetComponent<CameraCompositorComponent>().SetFadeTint(fadeTimer.Parameterized());
                cameraA.GetComponent<CameraCompositorComponent>().rendering = true;
            }

            if(fadeTimer.Finished()){
                state = FadeState.Idling;
            }
        }
    }

    public void PlayerEnteredTrigger(GameObject source){
        if(fadeTimer.Finished()){
            transitionAtoB = source == doorAToB;
            state = FadeState.FadeOut;
            fadeTimer.Start();
        }
    }
}
