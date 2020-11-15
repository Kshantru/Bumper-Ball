using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBonus : Bonus
{
    public float force = 0.5f;
    // Start is called before the first frame update
    public override void OnPick(Collider collider)
    {
        collider.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * force, ForceMode.Impulse);
        collider.gameObject.GetComponent<PlayerController>().Stun(3f);
    }

    public override void End()
    {

    }
}
