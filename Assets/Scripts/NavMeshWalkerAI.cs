using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshWalkerAI : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] Transform target;
    [SerializeField] Transform neckLookAt;
    [SerializeField] Animator anim;

    [Header("Stats")]
    [SerializeField] float health = 30f;

    [Header("Attack Info")]
    [SerializeField] Attack[] attacks;

    bool inAttack;
    bool inRange;

    void Start () 
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update ()
    {
        if (!inAttack && Vector3.Distance(target.position, transform.position) > agent.stoppingDistance) 
        {
            agent.SetDestination(target.position);
            anim.SetBool("Walking", true);
            anim.SetBool("Idle", false);
        }

        else 
        {
            anim.SetBool("Idle", true);
            anim.SetBool("Walking", false);
        }
    }

    void FixedUpdate () 
    {
        Vector3 dir = target.position - transform.position;
        neckLookAt.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), 20f * Time.deltaTime);
    }

    void OnTriggerStay (Collider col) 
    {
        if (col.CompareTag("Player") && !inAttack) 
        {
            Attack attack = attacks[Random.Range(0, attacks.Length)];
            inRange = true;
            StartCoroutine(PlayAttack(attack));
        }
    }

    void OnTriggerExit (Collider col) 
    {
        if (col.CompareTag("Player")) 
            inRange = false;
    }

    IEnumerator PlayAttack (Attack attack) 
    {
        Vector3 dir = target.position - transform.position;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), 500f * Time.deltaTime);
        print("played " + attack._name);
        inAttack = true;
        anim.SetTrigger(attack._name);

        yield return new WaitForSeconds(attack.attackTime);

        inAttack = false;
    }
}

[System.Serializable]
public struct Attack 
{
    public string _name;
    public float attackTime;
}