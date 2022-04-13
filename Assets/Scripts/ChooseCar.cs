using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseCar : MonoBehaviour
{
    static string car="";

    void OnMouseDown() 
    {
        car = this.name;
        SceneManager.LoadScene("RaceScene");
    }

    void OnDisable()
    {
        PlayerPrefs.SetString("car", car);
    }

    public void SetWords (string [] words) {
        Debug.Log("SetWords called cicada parent");
        Debug.Log(words);
        Debug.Log ($"Spawning {words} enemies!");
    }
}
