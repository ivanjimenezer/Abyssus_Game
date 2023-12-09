using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAnimatorBool: StateMachineBehaviour
{
 
  public string targetBool;
  public bool status;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
    // Debug.LogError("TargetBool:  "+targetBool);
    // Debug.LogError("BoolStatus: ");
    // Debug.LogError(status);
    animator.SetBool(targetBool, status); 
  }
 
}
