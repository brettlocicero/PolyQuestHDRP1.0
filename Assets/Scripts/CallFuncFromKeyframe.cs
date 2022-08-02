using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallFuncFromKeyframe : MonoBehaviour
{
    [SerializeField] GameObject obj;
    [SerializeField] string methodName;

    public void Call ()
    {
        obj.SendMessage(methodName, SendMessageOptions.DontRequireReceiver);
    }
}
