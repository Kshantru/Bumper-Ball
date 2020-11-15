using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunBonus : Bonus
{

    public new void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player") && !taken)
        {
            OnPick(collider);
            taken = true;
            Destroy(transform.GetChild(0).gameObject);
            GetComponent<Collider>().enabled = false;
            collider.gameObject.GetComponent<PlayerController>().state = "charged";
            this.DestroyBonus();
        }
    }

    public override void OnPick(Collider collider)
    {
    }

    public override void End()
    {
    }
}
