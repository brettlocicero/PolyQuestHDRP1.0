using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedProjEnemy : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float tickSpeed = 0.5f;
    [SerializeField] Rigidbody proj;
    [SerializeField] float fireRate = 1f;

    Rigidbody rb;
    Vector3 targetPos;
    float moveCounter;
    float fireRateCounter;

    void Start ()
    {
        target = PlayerInstance.instance.transform;
        rb = GetComponent<Rigidbody>();
    }

    void Update () 
    { 
        moveCounter += Time.deltaTime;
        if (moveCounter >= tickSpeed)
            targetPos = PositionTick();

        fireRateCounter += Time.deltaTime;
        if (fireRateCounter >= fireRate)
            ShootProj();
    }

    void FixedUpdate ()
    {
        rb.MovePosition(transform.position + targetPos * Time.deltaTime);
    }

    Vector3 PositionTick () 
    {
        float dist = Vector3.Distance(target.position, transform.position);
        Vector3 targetPos;

        if (dist >= 10f) 
            targetPos = Vector3.Normalize(target.position - transform.position);
        else 
            targetPos = -Vector3.Normalize(target.position - transform.position);

        moveCounter = 0f;
        return targetPos;
    }

    void ShootProj () 
    {
        Rigidbody p = Instantiate(proj, transform.position, Quaternion.identity) as Rigidbody;
        p.transform.LookAt(target);
        p.AddForce(p.transform.forward * 1000f);
        fireRateCounter = 0f;
    }
}
