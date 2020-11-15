
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public int playerID = 1; //1 ou 2

    private Rigidbody body;
    public float speed = 10;
    public float friction = 10;

    private Vector3 dir;
    private string horizontalAxis, verticalAxis, dashButton;

    public string state = "normal";

    public float dashCooldown = 1;
    public float dashDuration = 0.5f;
    private float lastDashTime;
    public bool wasDashing = false;

    private Vector3 startPos;
    private float startAngle;

    //public ParticleSystem smokeParticles;

    public static bool quick = true;

    private void Start()
    {
//        Debug.Log("In start");
        body = GetComponent<Rigidbody>();
        startPos = transform.position;
        startAngle = transform.eulerAngles.y;

        horizontalAxis = "Horizontal";// + playerID;
        verticalAxis = "Vertical";// + playerID;
        dashButton = "Dash";// + playerID;

       // transform.GetChild(0).Find("Logo").GetComponent<MeshRenderer>().material.mainTexture = TeamSelector.GetSelection(playerID - 1).GetLogo();
    }

    private void Update()
    {
  //      Debug.Log("In update");
        //if (!GameManager.gameStarted)
        //    return;

        //animation du joueur
        //transform.GetChild(0).localEulerAngles = new Vector3(-body.velocity.z, 0, body.velocity.x) * 4;

        if (state!="stunned")
        {
           // Debug.Log("Not stunned");
            float currentSpeed = dir.sqrMagnitude;
            //Debug.Log("currentSpeed"+ currentSpeed);
            if (currentSpeed > 0.1f)
            {
                Quaternion targetRot = Quaternion.Euler(0, Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg, 0);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, 1000 * Time.deltaTime);

                //Debug.Log("Dash pressed: "+ Input.GetButtonDown(dashButton));
                //Debug.Log("Velocity: "+ body.velocity.sqrMagnitude);
                //Debug.Log("lastDashTime + dashCooldown: "+ (lastDashTime + dashCooldown));
                //Debug.Log("Time "+ Time.time);

                if (Input.GetButtonDown(dashButton) && body.velocity.sqrMagnitude > 1 && lastDashTime + dashCooldown < Time.time)
                {

                    body.AddForce(body.velocity.normalized * speed * 15, ForceMode.Acceleration);
                    lastDashTime = Time.time;
                    dashCooldown = 1;
                }

                if (IsDashing())
                {
                    Debug.Log("Player is Dashing");
                    transform.GetChild(0).localEulerAngles = Vector3.right * -30;
                }
                else
                {
                    if (wasDashing)
                    {
                        state = "normal";
                    }
                    transform.GetChild(0).localPosition = Vector3.up * Mathf.Abs(Mathf.Sin(Time.time * 10) * 0.15f);
                    transform.GetChild(0).localEulerAngles = Vector3.forward * (Mathf.Cos(Time.time * 10) * 5);
                }
            }
            else
            {
                transform.GetChild(0).localPosition = Vector3.zero;
                transform.GetChild(0).localEulerAngles = Vector3.zero;
            }

            //ParticleSystem.EmissionModule emission = smokeParticles.emission;
            //float smokeAmount = currentSpeed > 0.1f ? 15f : 0;
            //emission.rateOverTime = smokeAmount;
        }
        wasDashing = IsDashing();
    }
    private void FixedUpdate()
    {
        //if (!GameManager.gameStarted)
        //    return;
        if (state!="stunned")
        {
            dir = new Vector3(Input.GetAxis(horizontalAxis), 0, Input.GetAxis(verticalAxis));
            if (dir.sqrMagnitude > 1)
                dir.Normalize();
            body.AddForce(dir * speed, ForceMode.Acceleration);
            body.AddForce(GameManager.FlattenVector(-body.velocity * GetFriction()), ForceMode.Acceleration);
        }
    }


    public void OnCollisionEnter(Collision collision)
    {
        if (state=="charged" && IsDashing() && collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().Stun(2f);
            state = "normal";
        }
    }


    private float GetFriction()
    {
        return (lastDashTime < Time.time + 0.5f) ? friction : friction * 0.4f;
    }

    public void ResetPos()
    {
        transform.position = startPos;
        transform.eulerAngles = Vector3.up * startAngle;
    }

    public bool IsDashing()
    {
        //Debug.Log("lastDashTime + dashDuration: "+ (lastDashTime + dashDuration));
        //Debug.Log("Time in Dashing "+ Time.time);
        return lastDashTime + dashDuration > Time.time;
    }

    public void Stun(float time)
    {
        state = "stunned";
        Invoke("Unstun", time);
    }

    public void Unstun()
    {
        state = "normal";
    }
}
