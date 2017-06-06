using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchState : DefaultState
{
    private EnemyAI ai;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(ai == null)
        {
            ai = animator.GetComponent<EnemyAI>();
        }
        animator.transform.LookAt(GameManager.Instance().player.transform);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(ai.playerFound)
        {
            if(GameManager.Instance().PlayerInDistance(animator.transform.position, ai.attackDistance))
            {
                TransitionToState(animator, "toAttack");
                return;
            }

            TransitionToState(animator, "toPersuite");
        }
        else
        {
            if(!ai.alerted)
            {
                TransitionToState(animator, "toIdle");
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("toSearch", false);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
