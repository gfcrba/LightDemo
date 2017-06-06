using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PersuiteState : DefaultState {

    private NavMeshAgent agent;
    private Transform playerTransform;
    private GameManager gameManager;
    private EnemyAI ai;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(gameManager == null)
        {
            gameManager = GameManager.Instance();
        }

        if (playerTransform == null)
        {
            playerTransform = gameManager.player.transform;
        }

        if(agent == null)
        {
            agent = animator.GetComponent<NavMeshAgent>();
        }

        if(ai == null)
        {
            ai = animator.GetComponent<EnemyAI>();
        }

        agent.enabled = true;
        agent.SetDestination(playerTransform.position);
    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(gameManager.PlayerInDistance(animator.transform.position, ai.attackDistance))
        {
            TransitionToState(animator, "toAttack");
        } 
        else
        {
            if(gameManager.PlayerInDistance(animator.transform.position, ai.sightDistance))
            {
                agent.SetDestination(playerTransform.position);
            }
            else
            {
                ai.LostPlayer();
                TransitionToState(animator, "toSearch");
            }
        }
    }

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.enabled = false;
        animator.SetBool("toPersuite", false);
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
