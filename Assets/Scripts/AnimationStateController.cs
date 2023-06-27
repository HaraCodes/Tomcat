using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class AnimationStateController : NetworkBehaviour
{
    int IsWalkingHash = Animator.StringToHash("IsWalking");
    int IsLeftStrafingHash = Animator.StringToHash("IsLeftStrafing");
    int IsRightStrafingHash = Animator.StringToHash("IsRightStrafing");
    int IsWalkBackHash = Animator.StringToHash("IsWalkBack");

    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner)
            return;
        bool IsWalking = animator.GetBool(IsWalkingHash);
        bool IsWalkingBackward = animator.GetBool(IsWalkBackHash);
        bool IsLeftStrafing = animator.GetBool(IsLeftStrafingHash);
        bool IsRightStrafing = animator.GetBool(IsRightStrafingHash);        
        bool forwardPressed = Input.GetKey("w");
        bool LeftPressed = Input.GetKey("a");
        bool RightPressed = Input.GetKey("d");
        bool BackwardPressed = Input.GetKey("s");
        bool IsWalk = IsWalking && IsWalkingBackward;

        if (!IsWalking && forwardPressed)
            animator.SetBool(IsWalkingHash, true);
        if (IsWalking && !forwardPressed)
            animator.SetBool(IsWalkingHash ,false);
        if (!IsWalkingBackward && BackwardPressed)
            animator.SetBool(IsWalkBackHash, true);
        if (IsWalkingBackward && !BackwardPressed)
            animator.SetBool(IsWalkBackHash, false);
        if (LeftPressed && !IsLeftStrafing && !IsWalk)
            animator.SetBool(IsLeftStrafingHash, true);
        if (!LeftPressed && IsLeftStrafing || IsWalk)
            animator.SetBool(IsLeftStrafingHash, false);
        if (RightPressed && !IsRightStrafing && !IsWalk)
            animator.SetBool(IsRightStrafingHash, true);
        if (!RightPressed && IsRightStrafing || IsWalk)
            animator.SetBool(IsRightStrafingHash, false);
    }
}
