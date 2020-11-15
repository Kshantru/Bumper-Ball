using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashBonus : Bonus
{
    /* Bonus de dash donné au joueur.
     * Le cooldown est divisé par 'dashCooldownFactor'.
     */

    [SerializeField] private float dashCooldownFactor = 2.0f;
    private PlayerController controll;

    public override void OnPick(Collider collider)
    {
        controll = collider.gameObject.GetComponent<PlayerController>();
        controll.dashCooldown /= dashCooldownFactor;
    }

    public override void End()
    {
        controll.dashCooldown *= dashCooldownFactor;
    }

}
