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
    private static extern void GameFinished (int score);
    // end react unity web gl


    public SliderScripts fuelBar;


    private float startFuelValue = 1.0f;
    private float currentFuelValue;
    private DateTime startingTime;
    private DateTime timeFuel;

    private float distance=0.0f;



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
                GameFinished((int)(((DateTime.Now-startingTime).TotalSeconds)*14.0));
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

    void OnDisable()
    {
        PlayerPrefs.SetFloat("distance", distance);
    }


}
