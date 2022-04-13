using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ReactWebController : MonoBehaviour
{

    public List<string> userWords = new List<string>();



    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("UserWords");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }


   

    public void SetWord (string word){
        userWords.Add(word);
    }

 

    

}
