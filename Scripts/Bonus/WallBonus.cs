using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBonus : Bonus
{

    public GameObject wallPrefab;
    private GameObject myWall;

    public override void OnPick(Collider collider)
    {
        myWall = Instantiate(wallPrefab);
    }

    public override void End()
    {
        Destroy(myWall);
    }

}
