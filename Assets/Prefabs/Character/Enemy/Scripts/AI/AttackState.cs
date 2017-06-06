using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : DefaultState
{
    private GameManager gameManager;
    private EnemyAI ai;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (gameManager == null)
        {
            gameManager = GameManager.Instance();
        }

        if(ai == null)
        {
            ai = animator.GetComponent<EnemyAI>();
        }
    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (gameManager.PlayerInDistance(animator.transform.position, ai.attackDistance))
        {
            animator.transform.LookAt(GameManager.Instance().player.transform);
            ai.Attack();
            return;
        }
        TransitionToState(animator, "toSearch");
    }

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("toAttack", false);
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
