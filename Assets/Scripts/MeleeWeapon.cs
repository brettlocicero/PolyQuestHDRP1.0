using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] AnimationClip[] basicAttacks;
    [SerializeField] Vector2 comboRange;
    [SerializeField] float swordSwingMin = 0.6f;
    [SerializeField] float attackRadius;
    [SerializeField] float damage = 20f;
    [SerializeField] Transform attackSphereCenter;
    [SerializeField] LayerMask impactLayerMask;
    [SerializeField] AudioClip[] comboSounds;
    [SerializeField] AudioClip hitSound;
    [SerializeField] float swingDebuffTime = 0.4f;
    [SerializeField] float swingMoveSpeed = 5f;
    int comboIndex;
    float swingCounter;
    float ogMoveSpeed;

    [Header("Animation")]
    [SerializeField] CharacterController cc;
    [SerializeField] Animator anim;
    [SerializeField] Animation swingAnim;
    [SerializeField] CinemachineShake cs;
    [SerializeField] float shakeAmt = 0.5f;
    [SerializeField] float shakeTime = 0.1f;
    [SerializeField] float shakeFreq = 1f;

    [Header("Blocking")]
    [SerializeField] BlockingWeapon blockingWeapon;

    AudioSource aud;

    void Start () 
    {
        aud = GetComponent<AudioSource>();
        ogMoveSpeed = cc.gameObject.GetComponent<FirstPersonController>().movementSpeed;
        CursorManager.LockCursor();
    }

    void Update ()
    {
        BasicAnimation();
        SwordSwing();
    }

    void BasicAnimation () 
    {
        // check for mid-air anim
        if (!cc.isGrounded) 
        {
            anim.SetBool("Walking", false);
            anim.SetBool("Idle", true);
            return;
        }

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) 
        {
            anim.SetBool("Walking", true);
            anim.SetBool("Idle", false);
        }

        else 
        {
            anim.SetBool("Walking", false);
            anim.SetBool("Idle", true);
        }
    }

    void SwordSwing () 
    {
        if (blockingWeapon)
            if (blockingWeapon.blocking) return;

        swingCounter += Time.deltaTime;
        
        if (Input.GetMouseButtonDown(0) && swingCounter >= swordSwingMin) 
        {
            StartCoroutine(SwingDebuffs());
            swingAnim.Rewind(basicAttacks[comboIndex].name);
            swingAnim.Play(basicAttacks[comboIndex].name);
            cs.ShakeCamera(shakeAmt, shakeTime, shakeFreq, 80);

            comboIndex++;
            if (comboIndex >= basicAttacks.Length) comboIndex = 0;

            swingCounter = 0;
        }
    }

    public void AttackRadius ()
    {
        RandomizePitch();
        aud.PlayOneShot(comboSounds[Random.Range(0, comboSounds.Length)]);

        Collider[] attackSphere = Physics.OverlapSphere(attackSphereCenter.position, attackRadius, impactLayerMask);
        List<GameObject> hitList = new List<GameObject>();

        if (attackSphere.Length > 0)
        {
            GetComponent<AudioSource>().PlayOneShot(hitSound);
        }

        foreach (Collider obj in attackSphere)
        {
            if (!hitList.Contains(obj.gameObject))
            {
                obj.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
                hitList.Add(obj.gameObject);
            }
        }
    }

    IEnumerator SwingDebuffs () 
    {
        cc.gameObject.GetComponent<FirstPersonController>().movementSpeed = swingMoveSpeed;
        yield return new WaitForSeconds(swingDebuffTime);
        cc.gameObject.GetComponent<FirstPersonController>().movementSpeed = ogMoveSpeed;
    }

    void RandomizePitch () 
    {
        aud.pitch = Random.Range(0.7f, 0.9f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(attackSphereCenter.position, attackRadius);
    }
}
