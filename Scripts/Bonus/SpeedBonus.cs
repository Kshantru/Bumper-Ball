using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBonus : Bonus
{
    /* Bonus de vitesse donné au joueur.
     * La vitesse est multipliée par 'speedfactor'
     */

    [SerializeField] private float speedFactor = 2.0f;
    private PlayerController controll;

    public override void OnPick(Collider collider)
    {
        controll = collider.gameObject.GetComponent<PlayerController>();
        controll.speed *= speedFactor;
    }

    public override void End()
    {
        controll.speed /= speedFactor;
    }

}
