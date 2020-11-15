using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMalus : Bonus
{
    /* Malus de dash donné au joueur.
     * Le cooldown est multiplié par 'dashCooldownFactor'.
     */

    [SerializeField] private float dashCooldownFactor = 2.0f;
    private PlayerController controll;

    public override void OnPick(Collider collider)
    {
        controll = collider.GetComponent<PlayerController>();
        controll.dashCooldown *= dashCooldownFactor;
    }

    public override void End()
    {
        controll.dashCooldown /= dashCooldownFactor;
    }

}
