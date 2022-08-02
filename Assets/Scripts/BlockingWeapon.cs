using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockingWeapon : MonoBehaviour
{
    [SerializeField] BlockState blockState;

    [SerializeField] FirstPersonController fpc;
    [SerializeField] float blockingMoveSpeed = 3f;
    [SerializeField] float blockingSensFactor = 0.1f;
    [SerializeField] float directionSens = 0.5f;

    [Space]
    [SerializeField] CanvasGroup shieldGroup;
    [SerializeField] Transform leftBlock;
    [SerializeField] Transform middleBlock;
    [SerializeField] Transform rightBlock;

    [Space]
    [SerializeField] Color blockingColor;
    [SerializeField] Color inactiveColor;

    float cachedMoveSpeed;
    float cachedMouseSens;

    enum BlockState 
    {
        Left,
        Middle,
        Right
    }

    void Start () 
    {
        cachedMoveSpeed = fpc.movementSpeed;
        cachedMouseSens = fpc.mouseSensitivity;
    }

    void Update ()
    {
        if (Input.GetMouseButton(1)) 
        {
            fpc.movementSpeed = blockingMoveSpeed;
            fpc.mouseSensitivity = blockingSensFactor;
            DirectionalBlocking();
            shieldGroup.alpha = 0.8f;
        }

        else if (Input.GetMouseButtonUp(1))
        {
            fpc.movementSpeed = cachedMoveSpeed;
            fpc.mouseSensitivity = cachedMouseSens;
            shieldGroup.alpha = 0f;
        }
    }

    void DirectionalBlocking () 
    {
        float mouseX = Input.GetAxis("Mouse X") * directionSens;
        float mouseY = Input.GetAxis("Mouse Y") * directionSens;
        
        if (mouseX > 0.1f && mouseX > mouseY) 
        {
            ChangeBlockState(BlockState.Right);
        }

        else if (mouseX < -0.1f || mouseY < -0.1f) 
        {
            ChangeBlockState(BlockState.Left);
        }

        else if (mouseY > Mathf.Abs(mouseX)) 
        {
            ChangeBlockState(BlockState.Middle);
        }
    }

    void ChangeBlockState (BlockState state) 
    {
        blockState = state;
        switch (state) 
        {
            case BlockState.Left:
                leftBlock.transform.localScale = new Vector3(1f, 1f, 1f);
                middleBlock.transform.localScale = new Vector3(0.5f, 1f, 1f);
                rightBlock.transform.localScale = new Vector3(0.5f, 1f, 1f);

                leftBlock.GetComponent<Image>().color = blockingColor;
                middleBlock.GetComponent<Image>().color = inactiveColor;
                rightBlock.GetComponent<Image>().color = inactiveColor;
                break;

            case BlockState.Middle:
                leftBlock.transform.localScale = new Vector3(0.5f, 1f, 1f);
                middleBlock.transform.localScale = new Vector3(1f, 1f, 1f);
                rightBlock.transform.localScale = new Vector3(0.5f, 1f, 1f);

                leftBlock.GetComponent<Image>().color = inactiveColor;
                middleBlock.GetComponent<Image>().color = blockingColor;
                rightBlock.GetComponent<Image>().color = inactiveColor;
                break;

            case BlockState.Right:
                leftBlock.transform.localScale = new Vector3(0.5f, 1f, 1f);
                middleBlock.transform.localScale = new Vector3(0.5f, 1f, 1f);
                rightBlock.transform.localScale = new Vector3(1f, 1f, 1f);

                leftBlock.GetComponent<Image>().color = inactiveColor;
                middleBlock.GetComponent<Image>().color = inactiveColor;
                rightBlock.GetComponent<Image>().color = blockingColor;
                break; 
        }
    }
}
