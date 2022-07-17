using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class WordGenerator : MonoBehaviour
{

    // for the visual part
    public Vector2 center = new Vector2(0,-220);
    private float middle = 0F;
    private float bottom=100.0f;
    private float letterOffset = 110.0F;
    public GameObject letterPrefab;
    public GenerateCar car;
    public GameObject canvas;
    private FuelHandler fuel;


    // for the typing logic part
    List<GameObject> letters = new List<GameObject>();
    private int currentIndex=0;
    private int currentWord=0;
    private bool currentWordStarted=false;
    private string [] words = {"a", "e", "i", "o", "u", "y", "maison", "lapin", "youpi", "cheval", "lait", "le", "la", "voiture", "crosse"};
    private bool gameFinished = false;

    // variables for the typing of ONE word
    private int errors=0;

    private bool is_letter_false=false;// if the user typed wrongly once the error, we only count it once


    void Start(){


        middle = canvas.GetComponent<RectTransform>().transform.position.x;
        GameObject[] objs = GameObject.FindGameObjectsWithTag("UserWords");
        ReactWebController rwc = objs[0].GetComponent<ReactWebController>();
        if(rwc.userWords.ToArray().Length!=0){
            words = rwc.userWords.ToArray();
        }
        currentWord=UnityEngine.Random.Range(0, words.Length);

    }

    void FixedUpdate(){
        if(fuel==null){
            fuel = car.CarGO.GetComponent<FuelHandler>();
        }
    }

    


    // Update is called once per frame
    void Update()
    {
        if(!gameFinished){
            if(!currentWordStarted){
                GenerateWord(words[currentWord]);
                currentWordStarted=true;
                if(!fuel.FirstCharacter()){
                    fuel.UpdateText(" ");
                    fuel.UpdateTextTyped(" ");
                }
                
            }
        }
       

        foreach (char letterPressed in Input.inputString)
        {
            // the user has typed the right letter
            if(letterPressed==words[currentWord][currentIndex]){
                letters[currentIndex].GetComponent<Image>().color = Color.green;
                currentIndex++; // we increment the value here, so the user has to type the letter again if he is false

                if(!is_letter_false){
                    fuel.UpdateTextTyped(Char.ToString(letterPressed)); // increment the text the user is typing
                    fuel.UpdateText(Char.ToString(letterPressed));// increment the text that the user should to type
                }
                is_letter_false=false; // reset is_letter_false to false for the new letter

            }
            else{ // the user has type a wrong letter
                letters[currentIndex].GetComponent<Image>().color = Color.red;
                errors+=1;

                if(!is_letter_false){ // if it is the first time the user type a false key for that letter
                    is_letter_false=true; // set to true so we don't add multiple times the wrong letter
                    fuel.UpdateTextTyped(Char.ToString(letterPressed));  // add the error to the text typed
                    fuel.UpdateText(Char.ToString(words[currentWord][currentIndex]));// increment the text that the user should to type

                }
            }

            
            // check if words is finished
            if(words[currentWord].Length <= currentIndex){

                
                
                // if a word is finished, we can gain some fuel
                int totalLength = words[currentWord].Length;
                GainFuel(totalLength, errors);
                errors=0;
                currentWordStarted=false;
                
                // get a random index for a word in the list of words
                currentWord  = UnityEngine.Random.Range(0, words.Length);  
                currentIndex=0;
                DestroyList();
            }

            // check if the exercise is finished
        }
    }

    // function that display a word on the screen canva
    void GenerateWord(string word){
        float length = word.Length;
        
        float start = middle-letterOffset*length/2f+letterOffset/2f;
        for (int i = 0; i < length; i++){
            float ii = (float) i;
            GenerateLetter(word[i].ToString(), start+letterOffset*ii);
        }
    }

    // function that display a letter at a particular position on the screen canva
    void GenerateLetter(string letter, float x){
        GameObject letterGO = Instantiate(letterPrefab, new Vector3(x, bottom, 0), Quaternion.identity);

        letterGO.transform.SetParent(this.transform);
        GameObject TextUI = GameObject.Find("Text");
        var children = letterGO.GetComponentsInChildren<Transform>();
        foreach (var child in children){
            if (child.name == "Text"){
                Text text = child.GetComponent<Text>();
                text.text = letter;
            }
        }
        letters.Add(letterGO);
    }

    // destroy the game object inside the list letters
    void DestroyList(){
        for (int i = 0; i < letters.Count; i++)
        {
            Destroy(letters[i]);
        }
        letters.Clear();
    }


    void GainFuel(int length, int errors){

        float fuelValue = (length-errors)/100.0f;
        fuel.GetFuel(fuelValue);
    }
}
