using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int Oscore=0;
    int Bscore=0;
    private bool lockGoal = false;
    private bool gameEnd = false;
    private int lastTeamGoal = 1;
    public TextMesh scoreDisplay;

    public ChronoController chrono;

    public static GameManager instance;

    public GameObject[] bonusPrefab;
    public float bonusDelay = 10;
    public float bonusRandomFactor = 0.3f;

    public MeshRenderer[] playerLogos;

    private const int goalsToWin = 6;

    void Start()
    {
        instance = this;
        StartCoroutine(SpawnBonusLoop());

       // playerLogos[0].material.mainTexture = TeamSelector.GetSelection(0).GetLogo();
        //playerLogos[1].material.mainTexture = TeamSelector.GetSelection(1).GetLogo();
    }

    IEnumerator SpawnBonusLoop()
    {
        while(!gameEnd)
        {
            yield return new WaitForSeconds(Random.Range(bonusDelay*(1-bonusRandomFactor), bonusDelay*(1+bonusRandomFactor)));
            SpawnBonus();
        }
    }

    private void SpawnBonus()
    {
        Vector3 pos = new Vector3(Random.Range(-100, 100) / 10f, 0.5f, Random.Range(-80, 80) / 10f);
        GameObject go = Instantiate(bonusPrefab[Random.Range(0, bonusPrefab.Length)], pos, Quaternion.identity);
    }

    private void Update()
    {
        if(Bscore != Oscore && chrono.getChrono() < 0){
            endGame();
        }

        if (Input.GetKey(KeyCode.P) && Input.GetKeyDown(KeyCode.T))
            SceneManager.LoadScene("Menu");
    }

    public static Vector3 FlattenVector(Vector3 vec)
    {
        return new Vector3(vec.x, 0, vec.z);
    }

    public void EndRound(int id)
    {
        chrono.pause();

        if (lockGoal || gameEnd)
            return;

        lockGoal = true;
        lastTeamGoal = id;

        if(id == 1)
            Oscore++;
        else
            Bscore++;

        // scoreDisplay.text = Bscore + "-" + Oscore;
        scoreDisplay.text =  ""+Oscore;

        if ( (Bscore >= goalsToWin || Oscore >= goalsToWin) || (Bscore != Oscore && chrono.getChrono() < 0 ))
        {
            endGame();
        }
        
        StartCoroutine(Reset(3));
    }

private void endGame(){
    gameEnd = true;
            int winningTeam = Bscore >= goalsToWin ? 0 : 1;
            //TODO
            TextMesh winText = GameObject.Find("Win Text").GetComponent<TextMesh>();
            winText.GetComponent<MeshRenderer>().enabled = true;
            //winText.text = "C'est\n" + TeamSelector.GetSelection(winningTeam).GetName() + "\nle plu for";
}

    IEnumerator Reset(int delay)
    {
        
        yield return new WaitForSeconds(delay);
        chrono.resume();
        Football.instance.Reset();
        GameObject.Find("Player").GetComponent<PlayerController>().ResetPos();
       // GameObject.Find("P2").GetComponent<PlayerController>().ResetPos();
        lockGoal = false;
    }

    public bool AreGoalsLocked()
    {
        return lockGoal;
    }

    public bool IsGameFinished()
    {
        return gameEnd;
    }

    public int GetLastTeamGoal()
    {
        return lastTeamGoal;
    }
}
