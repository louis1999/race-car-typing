using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Globalization;
using System.Runtime.InteropServices;

public class FuelHandler : MonoBehaviour
{
    // start react unity web gl
    [DllImport("__Internal")]
    private static extern void GameFinished (string text, string text_typed, int duration);
    // end react unity web gl


    public SliderScripts fuelBar;


    private float startFuelValue = 1.0f;
    private float currentFuelValue;
    private DateTime startingTime;
    private DateTime timeFuel;

    private float distance=0.0f;


    private string text="";
    private string text_typed="";



    // Start is called before the first frame update
    void Start()
    {
        startingTime = DateTime.Now;
        timeFuel=startingTime.AddSeconds(10);
        currentFuelValue = startFuelValue;
        fuelBar.slider.value=currentFuelValue;
    }

    // Update is called once per frame
    void Update()
    {
        fuelBar.slider.value=currentFuelValue;
        if(DateTime.Now>timeFuel){
            timeFuel=DateTime.Now.AddSeconds(1);
            currentFuelValue=currentFuelValue-0.03f; 
        }

        if(currentFuelValue<0){
            // go to end scene 
            // the game is finished
            #if UNITY_WEBGL == true && UNITY_EDITOR == false
                GameFinished(text, text_typed, (int)(((DateTime.Now-startingTime).TotalSeconds)));
            #endif


            // lets say that the car goes at 50km/h ==> 13,8889m/s
            distance = (float)(((DateTime.Now-startingTime).TotalSeconds)*14.0);
            SceneManager.LoadScene("EndScene");

        }

    }


    public void GetFuel(float gain){
        if(currentFuelValue+gain>1){
            currentFuelValue=1.0f;
        }
        else{
            currentFuelValue+=gain;
        }
    }

    /* Update the text that the user should type */
    /* it will be sent to react afterwards */
    public void UpdateText(string letter){
        text = text + letter;
    }

    /* Update the text that the user is typing */
    /* it will be sent to react afterwards */
    public void UpdateTextTyped(string letter){
        text_typed = text_typed + letter;
    }

    public bool FirstCharacter(){
        return text.Length==0;
    }


    void OnDisable()
    {
        PlayerPrefs.SetFloat("distance", distance);
    }


}
