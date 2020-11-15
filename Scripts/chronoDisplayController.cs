using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chronoDisplayController : MonoBehaviour
{

    public TextMesh text;
    public ChronoController chrono;
    public Gradient suddentDeathColor;
    public string suddentDeathString;

    public Transform suddentDeathPos;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int c = (int) chrono.getChrono();
        if(c > 0){
            int secondes = c%60;
            int minutes = c/60;
            text.text = minutes.ToString() + ":" + ((secondes < 10) ? "0":"") + secondes.ToString(); //pire méthode mais je me suis lassé de lire la doc de String.format au bout d'une ligne
        }
        else{

            text.color = suddentDeathColor.Evaluate(Mathf.Sin(Time.time)/2.0f + 0.5f);
            text.text = suddentDeathString;
            text.fontSize = 24;
        }
    }
}
