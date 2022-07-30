using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerAI : MonoBehaviour
{
    public enum State {Idle, Cautious, Agressive, Attack}
    public State currentState = State.Idle;

    [Header("")]
    [SerializeField] Transform target;
    [SerializeField] float attackTime = 2f;
    [SerializeField] Transform debugVector;
    [SerializeField] float speed = 5f;
    [SerializeField] Animator anim;
    [SerializeField] Transform neckLookAt;

    bool inAnim;

    void Start ()
    {
        UpdateState(State.Cautious);
    }

    void Update () 
    {
        //print(currentState);
    }

    void UpdateState (State reqState) 
    {
        if (inAnim) return;

        switch (reqState) 
        {
            case State.Idle:
                Idle();
                break;
            case State.Cautious:
                StartCoroutine(BeginCautiousAction());
                break;
            case State.Agressive:
                StartCoroutine(BeginAggressiveAction());
                break;
            case State.Attack:
                StartCoroutine(Attack());
                break;
        }
    }

    void Idle () 
    {
        // idle animation
        // can break out at any time
    }

    IEnumerator BeginCautiousAction () 
    {
        // circle for x seconds
        float d = Vector3.Distance(transform.position, target.position);
        float t = 0;
        while (d >= 2.5f)
        {
            d = Vector3.Distance(transform.position, target.position);
            t += Time.deltaTime;

            transform.LookAt(target);
            neckLookAt.transform.LookAt(target);
            Vector3 moveVec = -CalcCaution(t);
            debugVector.forward = moveVec;

            if (Vector3.Dot(transform.forward, moveVec) > 0)
                anim.transform.forward = moveVec;
            else
                anim.transform.forward = -moveVec;

            transform.Translate(moveVec * Time.deltaTime * 5);

            if (t >= 10f) 
            {
                UpdateState(State.Agressive);
                yield break;
            }

            yield return null;
        }
        // go for aggressive attack if enemy never comes within y meters
        UpdateState(State.Attack);

        Vector3 CalcCaution (float t) 
        {
            Vector3 ret = new Vector3();
            float noiseFactor = Mathf.Sin(t) / 2;
            Vector3 targVel = target.GetComponent<CharacterController>().velocity;
            targVel += new Vector3(noiseFactor, noiseFactor, noiseFactor);
            targVel = Vector3.Normalize(targVel);

            ret = new Vector3(-targVel.x, 0f, -targVel.z);
            return ret;
        }
    }

    IEnumerator BeginAggressiveAction () 
    {
        float t = 0;
        while (t < 10) 
        {
            t += Time.deltaTime;

            transform.LookAt(target);
            neckLookAt.transform.LookAt(target);
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            float d = Vector3.Distance(transform.position, target.position);
            anim.transform.LookAt(target);

            if (d <= 2.5f) 
            {
                UpdateState(State.Attack);
                yield break;
            }

            yield return null;
        }

        UpdateState(State.Cautious);
    }

    IEnumerator Attack () 
    {
        inAnim = true;
        yield return new WaitForSeconds(attackTime);
        inAnim = false;
        UpdateState(State.Cautious);
    }
}
