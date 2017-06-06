using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultState : StateMachineBehaviour
{
    protected void TransitionToState(Animator animator, string state)
    {
        animator.SetBool(state, true);
    }
}
