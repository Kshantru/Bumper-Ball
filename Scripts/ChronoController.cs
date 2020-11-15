using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChronoController : MonoBehaviour
{

    public bool paused;
    private float chrono = 0f;

    //match time
    public float startValue = 5*60f;

    //chrono qui décompte 5 minutes

    void Start()
    {
        reset();
    }

    public void reset(){
        chrono = startValue;
    }

    // Update is called once per frame
    void Update()
    {
        if(! paused){
            chrono -= Time.deltaTime;
        }
    }

    public void pause(){
        paused = true;
    }
    public void resume(){
        paused = false;
    }

    public string getChronoDisplay(){
        int c = (int) chrono;
        return (c/60).ToString() + ":" + (c%60).ToString(); //pire méthode mais je me suis lassé de lire la doc de String.format au bout d'une ligne

    }

    public float getChrono(){
        return chrono;
    }
}
