using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowResults : MonoBehaviour
{

    public GameObject textResults;

    private float distance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textResults.GetComponent<Text>().text = "Félicitations, vous avez parcouru "+distance+" mètres!";
    }

    void OnEnable()
    {
        distance  =  PlayerPrefs.GetFloat("distance");
    }
}
