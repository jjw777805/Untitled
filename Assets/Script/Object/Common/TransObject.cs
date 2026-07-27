using MyEnemy;
using MyPlayer;
using UnityEngine;

namespace MyObject
{
    public class TransObject : MonoBehaviour
    {
        public string toScene;
        public Vector3 position;
        void OnTriggerEnter2D(Collider2D collision)
        {
            
            if (collision.CompareTag("Player"))
            {
                GameManager.instance.LoadScene(toScene,position);
            }
        }
    }
}