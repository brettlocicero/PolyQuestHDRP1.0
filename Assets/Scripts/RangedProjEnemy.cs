using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedProjEnemy : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float tickSpeed = 0.5f;

    Rigidbody rb;
    Vector3 targetPos;
    float counter;

    void Start ()
    {
        target = PlayerInstance.instance.transform;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate ()
    {
        counter += Time.deltaTime;
        if (counter >= tickSpeed)
            targetPos = PositionTick();

        rb.MovePosition(transform.position + targetPos * Time.deltaTime);
    }

    Vector3 PositionTick () 
    {
        float dist = Vector3.Distance(target.position, transform.position);
        Vector3 randomOffset, targetPos;

        if (dist >= 10f) 
            targetPos = Vector3.Normalize(target.position - transform.position);

        else 
        {
            randomOffset = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), Random.Range(-2f, 2f));
            targetPos = -Vector3.Normalize((target.position - transform.position)) + randomOffset;
        }

        counter = 0f;
        return targetPos;
    }
}
