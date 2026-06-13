using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;
using TMPro;
using Unity.Collections;
using UnityEngine;

namespace MyPlayer
{
    public partial class PlayerStatus
    {
        private float attackSeq=1.0f;
        private bool canAttack = true;  
        private bool isAttack = false;
        public bool CanAttack()
        {
            return canAttack;
        }

        public bool IsAttack()
        {
            return isAttack;
        }

        public void SetAttackSeq(float k)
        {
            attackSeq = k;
        }

        private float attackBeginTime = 0;
        public void Attacked()
        {
            canAttack = false;
            isAttack = true;
            attackBeginTime = 0.0f;
        }

        void AttackUpdate()
        {
            attackBeginTime += Time.deltaTime;
            if(attackBeginTime > attackSeq)
            {
                isAttack = false;
                canAttack = true;
            }
        }
    }
}