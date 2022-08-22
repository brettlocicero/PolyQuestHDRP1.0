using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshWalkerAI : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] Transform target;
    [SerializeField] Transform neckLookAt;
    [SerializeField] Vector2 lookAtClamp;
    [SerializeField] Animator anim;
    [SerializeField] GameObject ragdollFX;
    [SerializeField] GameObject takeDMGFX;

    [Header("Stats")]
    [SerializeField] int health = 30;
    [SerializeField] float engageDistance = 20f;
    [SerializeField] int dmg = 15;

    [Header("Attack Info")]
    [SerializeField] Attack[] attacks;

    bool inAttack;
    bool inRange;
    bool playerInTrigger;

    void Start () 
    {
        agent = GetComponent<NavMeshAgent>();
        target = PlayerInstance.instance.transform;
    }

    void Update ()
    {
        float dist = Vector3.Distance(target.position, transform.position);

        if (!inAttack && dist > agent.stoppingDistance && dist <= engageDistance) 
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
        
        // for lookat
        if (dist <= engageDistance) 
            NeckLookAtPlayer();
    }

    void NeckLookAtPlayer () 
    {
        Vector3 dir = target.position - neckLookAt.position;
        neckLookAt.rotation = Quaternion.Lerp(neckLookAt.rotation, Quaternion.LookRotation(dir), 15f * Time.deltaTime);

        /*float rawY = neckLookAt.localEulerAngles.y;
        float clampedY = Mathf.Clamp(rawY, lookAtClamp.x, lookAtClamp.y);
        Vector3 eulerAngs = new Vector3(neckLookAt.localEulerAngles.x, clampedY, neckLookAt.localEulerAngles.z);
        neckLookAt.localEulerAngles = eulerAngs;*/
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

    void OnTriggerEnter (Collider col) 
    {
        if (col.CompareTag("Player"))
            playerInTrigger = true;
    }

    void OnTriggerExit (Collider col) 
    {
        if (col.CompareTag("Player")) 
        {
            inRange = false;
            playerInTrigger = false;
        }
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

    public void TryToHurtPlayer () 
    {
        if (!playerInTrigger) return;
        PlayerInstance.instance.TakeDamage(dmg);
    }

    public void TakeDamage (object[] data) 
    {
        health -= (int)data[0];
        Vector3 pos = (Vector3)data[1];

        GameObject bloodFX = Instantiate(takeDMGFX, pos, transform.rotation);
        Destroy(bloodFX, 1.5f);

        if (health <= 0) 
        {
            GameObject ragdoll = Instantiate(ragdollFX, transform.position, transform.rotation);
            Rigidbody[] childRBs = ragdoll.GetComponentsInChildren<Rigidbody>();

            foreach (Rigidbody rb in childRBs) 
            {
                rb.AddExplosionForce(15f, pos, 5f, 1f, ForceMode.Impulse);
            }

            Destroy(gameObject);
        }
    }
}

[System.Serializable]
public struct Attack 
{
    public string _name;
    public float attackTime;
}