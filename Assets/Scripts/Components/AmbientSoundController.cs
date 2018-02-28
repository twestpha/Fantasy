using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSoundController : MonoBehaviour {

    public AudioSource sourceA;
    public AudioSource sourceB;

    private bool fadingAToB;

    private Timer fadeTimer;

	void Start(){
        fadeTimer = new Timer(1.0f);
	}

	void Update(){
        if(!fadeTimer.Finished()){
            if(fadingAToB){
                sourceA.volume = 1.0f - fadeTimer.Parameterized();
                sourceB.volume = fadeTimer.Parameterized();
            } else {
                sourceA.volume = fadeTimer.Parameterized();
                sourceB.volume = 1.0f - fadeTimer.Parameterized();
            }
        }
	}

    public void SetAmbientSound(AudioClip clip){
        if(!sourceA.isPlaying){
            fadingAToB = false;

            sourceA.clip = clip;
            sourceA.Play();
            sourceB.Stop();
        } else if(!sourceB.isPlaying){
            fadingAToB = true;

            sourceB.clip = clip;
            sourceB.Play();
            sourceA.Stop();
        }

        fadeTimer.Start();
    }
}
