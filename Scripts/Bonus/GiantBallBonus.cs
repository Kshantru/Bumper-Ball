using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantBallBonus : Bonus
{
    /* Malus de vitesse donné au joueur.
     * La vitesse est divisée par 'speedfactor'
     */

    public float scaleFactor = 2.0f;

    public override void OnPick(Collider collider)
    {
		Football.instance.transform.localScale = Football.instance.transform.localScale * scaleFactor;
        Vector3 pos = Football.instance.transform.position;
        pos.y = Mathf.Max(pos.y, Football.instance.transform.localScale.y / 2f);
        Football.instance.transform.position = pos;
    }

    public override void End()
    {
		Football.instance.transform.localScale = Football.instance.transform.localScale / scaleFactor;

	}

}
