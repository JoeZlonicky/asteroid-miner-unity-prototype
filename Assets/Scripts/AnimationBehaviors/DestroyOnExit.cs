using UnityEngine;

namespace AnimationBehaviors
{
    public class DestroyOnExit : StateMachineBehaviour
    {
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Destroy(animator.gameObject);
        }
    }
}
