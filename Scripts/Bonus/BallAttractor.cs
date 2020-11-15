using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAttractor : Bonus
{
    /* Le joueur qui prend le bonus attire la balle
     */

    [SerializeField] private float force = 40f;
    private GameObject ball;
    private GameObject player;
    

    private void FixedUpdate()
    {
        if (taken)
        {
            Vector3 director = player.transform.position - ball.transform.position;
            float r = director.magnitude;
            director.Normalize();
            ball.GetComponent<Rigidbody>().AddForce(force/(r*r) * director);
        }
    }

    public override void OnPick(Collider collider)
    {
        player = collider.gameObject;
        ball = GameObject.FindGameObjectWithTag("Football");
    }

    public override void End()
    {
        
    }
}
