using UnityEngine;
using MyEnemy;
public class LEAttackState : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<LittleEnemy>().isAtk = false;
    }
}
