using MyEnemy;
using MyPlayer;
using UnityEngine;
using UnityEngine.Events;

namespace MyObject
{
    public class InteractObject : MonoBehaviour
    {
        [SerializeField]
        UnityEvent onEntry=new UnityEvent();
        [SerializeField]
        UnityEvent onExit=new UnityEvent();

        public UnityEvent OnEntry
        {
            get
            {
                return onEntry;
            }
            set
            {
                onEntry = value;
            }
        }

        public UnityEvent OnExit
        {
            get
            {
                return onExit;
            }
            set
            {
                onExit = value;
            }
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if (GameManager.instance != null)
                {
                    if(GameManager.instance.IsStop())return ;
                }
                onEntry?.Invoke();
            }
        }

        public void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                onExit?.Invoke();
            }
        }
    }
}