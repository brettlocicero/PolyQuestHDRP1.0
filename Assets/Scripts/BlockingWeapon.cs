using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockingWeapon : MonoBehaviour
{
    public bool blocking;
    [SerializeField] BlockState blockState;

    [SerializeField] FirstPersonController fpc;
    [SerializeField] float blockingMoveSpeed = 3f;
    [SerializeField] float blockingSensFactor = 0.1f;
    [SerializeField] float directionSens = 0.5f;
    [SerializeField] ParticleSystem sparks;
    [SerializeField] Animation blockAnim;

    [Header("")]
    [SerializeField] Transform weaponShift;
    [SerializeField] DisplayBlock[] displayBlocks;

    [Header("")]
    [SerializeField] CanvasGroup shieldGroup;
    [SerializeField] Transform leftBlock;
    [SerializeField] Transform middleBlock;
    [SerializeField] Transform rightBlock;

    [Header("")]
    [SerializeField] Color blockingColor;
    [SerializeField] Color inactiveColor;

    float cachedMoveSpeed;
    float cachedMouseSens;
    Vector3 reqBlockPos;
    Vector3 reqBlockRot;

    PlayerInstance playerInstance;
    CinemachineShake cs;

    enum BlockState 
    {
        Left,
        Middle,
        Right
    }

    public void TriggerBlock () 
    {
        sparks.Play();
        blockAnim.Play("Block Animation");
        cs.ShakeCamera(8f, 0.15f, 0.05f, 90);
    }

    void Start () 
    {
        cachedMoveSpeed = fpc.movementSpeed;
        cachedMouseSens = fpc.mouseSensitivity;
        playerInstance = PlayerInstance.instance;
        cs = CinemachineShake.instance;
    }

    void Update ()
    {
        if (InventoryManager.instance.open) return;

        if (Input.GetMouseButton(1)) 
        {
            fpc.movementSpeed = blockingMoveSpeed;
            fpc.mouseSensitivity = blockingSensFactor;
            DirectionalBlocking();
            shieldGroup.alpha = 0.8f;
            blocking = true;
        }

        else if (Input.GetMouseButtonUp(1))
        {
            fpc.movementSpeed = cachedMoveSpeed;
            fpc.mouseSensitivity = cachedMouseSens;
            shieldGroup.alpha = 0f;
            blocking = false;
            playerInstance.currentBlocking = "None";

            reqBlockPos = Vector3.zero;
            reqBlockRot = Vector3.zero;
        }

        UpdateBlockPosRot();
    }

    void UpdateBlockPosRot () 
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, reqBlockPos, 10f * Time.deltaTime);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(reqBlockRot), 10f * Time.deltaTime);
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

                playerInstance.currentBlocking = "Left";

                reqBlockPos = displayBlocks[0].pos;
                reqBlockRot = displayBlocks[0].rot;
                break;

            case BlockState.Middle:
                leftBlock.transform.localScale = new Vector3(0.5f, 1f, 1f);
                middleBlock.transform.localScale = new Vector3(1f, 1f, 1f);
                rightBlock.transform.localScale = new Vector3(0.5f, 1f, 1f);

                leftBlock.GetComponent<Image>().color = inactiveColor;
                middleBlock.GetComponent<Image>().color = blockingColor;
                rightBlock.GetComponent<Image>().color = inactiveColor;

                playerInstance.currentBlocking = "Middle";

                reqBlockPos = displayBlocks[1].pos;
                reqBlockRot = displayBlocks[1].rot;
                break;

            case BlockState.Right:
                leftBlock.transform.localScale = new Vector3(0.5f, 1f, 1f);
                middleBlock.transform.localScale = new Vector3(0.5f, 1f, 1f);
                rightBlock.transform.localScale = new Vector3(1f, 1f, 1f);

                leftBlock.GetComponent<Image>().color = inactiveColor;
                middleBlock.GetComponent<Image>().color = inactiveColor;
                rightBlock.GetComponent<Image>().color = blockingColor;

                playerInstance.currentBlocking = "Right";
                
                reqBlockPos = displayBlocks[2].pos;
                reqBlockRot = displayBlocks[2].rot;
                break; 
        }
    }
}

[System.Serializable]
struct DisplayBlock 
{
    public Vector3 pos;
    public Vector3 rot;
}