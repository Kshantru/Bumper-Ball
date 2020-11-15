using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int id = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Football"))
        {
            GameManager.instance.EndRound(id);
        }
    }
}
