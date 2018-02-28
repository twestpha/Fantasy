using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCompositorComponent : MonoBehaviour {

    public bool rendering;
    public bool renderingForePlateOptional = false;
    public bool renderingBackPlateAnimation = false;

    public float animationTime;

    // private float texelsToScreenX;
    // private float texelsToScreenY;

    public Texture backPlate;
    public Texture backPlateAnimation;
    public RenderTexture renderTexture;
    public Texture forePlate;
    public Texture forePlateOptional;
    public RenderTexture uiRenderTexture;

    public Texture fadePlate;

    private Color fadeColor;
    private Timer animationTimer;

    void Start(){
        Cursor.visible = false;

        fadeColor = rendering ? Color.white : Color.black;

        GetComponent<Camera>().targetTexture = renderTexture;

        if(backPlateAnimation){
            animationTimer = new Timer(animationTime);
            animationTimer.Start();
        }

        // texelsToScreenX = (float) Screen.width / (float) renderTexture.width;
        // texelsToScreenY = (float) Screen.height / (float) renderTexture.height;
    }

    void OnGUI(){
        if(!rendering){
            return;
        }

        GUI.depth = 0;
        GUI.color = fadeColor;

        // Back Plate Drawing
        if(backPlate){
            GUI.DrawTexture(new Rect(0,0, Screen.width, Screen.height), backPlate);
        }

        // Animation Drawing
        if(backPlateAnimation && animationTimer.Finished()){
            renderingBackPlateAnimation = !renderingBackPlateAnimation;
            animationTimer.Start();
        }

        if(backPlateAnimation && renderingBackPlateAnimation){
            GUI.DrawTexture(new Rect(0,0, Screen.width, Screen.height), backPlateAnimation);
        }

        // Render Texture Drawing
        GUI.DrawTexture(new Rect(0,0, Screen.width, Screen.height), renderTexture);

        // Fore Plate Drawing
        if(forePlate){
            GUI.DrawTexture(new Rect(0,0, Screen.width, Screen.height), forePlate);
        }

        // Optional Fore Plate Drawing
        if(renderingForePlateOptional){
            GUI.DrawTexture(new Rect(0,0, Screen.width, Screen.height), forePlateOptional);
        }

        // Ui Render Texture Drawing
        if(uiRenderTexture){
            GUI.DrawTexture(new Rect(0,0, Screen.width, Screen.height), uiRenderTexture);
        }
    }

    public void SetFadeTint(float brightness){
        fadeColor.r = brightness;
        fadeColor.g = brightness;
        fadeColor.b = brightness;
    }
}
