using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDisplay : MonoBehaviour
{
    [SerializeField] GameObject attackDisplay;
    [SerializeField] Transform attackCenter;

    public void LeftSideAttack () 
    {
        GameObject obj = Instantiate(attackDisplay, attackCenter.position, attackCenter.rotation);
        Vector3 rot = new Vector3(0f, 0f, 0f);
        obj.transform.GetChild(0).localRotation = Quaternion.Euler(rot);
        transform.parent.GetComponent<NavMeshWalkerAI>().attackSide = "Left";

        Destroy(obj, 3f);
    }

    public void TopAttack () 
    {
        GameObject obj = Instantiate(attackDisplay, attackCenter.position, attackCenter.rotation);
        Vector3 rot = new Vector3(0f, 0f, 90f);
        obj.transform.GetChild(0).localRotation = Quaternion.Euler(rot);
        transform.parent.GetComponent<NavMeshWalkerAI>().attackSide = "Middle";


        Destroy(obj, 3f);
    }

    public void RightSideAttack () 
    {
        GameObject obj = Instantiate(attackDisplay, attackCenter.position, attackCenter.rotation);
        Vector3 rot = new Vector3(0f, 0f, 180f);
        obj.transform.GetChild(0).localRotation = Quaternion.Euler(rot);
        transform.parent.GetComponent<NavMeshWalkerAI>().attackSide = "Right";

        Destroy(obj, 3f);
    }

    public void SetLeftSideAttack () 
    {
        transform.parent.GetComponent<NavMeshWalkerAI>().attackSide = "Left";
    }

    public void SetMiddleAttack () 
    {
        transform.parent.GetComponent<NavMeshWalkerAI>().attackSide = "Middle";
    }

    public void SetRightSideAttack () 
    {
        transform.parent.GetComponent<NavMeshWalkerAI>().attackSide = "Right";
    }
}
