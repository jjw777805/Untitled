
using UnityEngine;
using MyPlayer;
public class AttackState : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<PlayerStatus>().IsAttack = false;
    }
}
