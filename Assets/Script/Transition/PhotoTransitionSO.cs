
using UnityEngine;
using UnityEngine.UI;

namespace MyTransition
{
    [CreateAssetMenu(menuName ="Game/MyTrans/Photo")]
    public class PhotoTransitionSO : TransitionTemplateSO
    {
        
        public SpriteRenderer from,to;
        public float durationTime=1.0f;
        public override void SetIn()
        {
        }
        public override void SetOut()
        {
            
        }
    }
}