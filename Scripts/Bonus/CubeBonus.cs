using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBonus : Bonus
{
 
    public Mesh cubeMesh; //cube par défaut de Unity
    public Mesh sphereMesh; //uv_sphere : modèle de base de la balle

    public override void OnPick(Collider collider)
    {
        Football.instance.GetComponent<MeshFilter>().mesh = cubeMesh;
        Football.instance.gameObject.AddComponent<BoxCollider>();
    }

    public override void End()
    {
        Football.instance.GetComponent<MeshFilter>().mesh = sphereMesh;
        Destroy(Football.instance.GetComponent<BoxCollider>());
    }

}
