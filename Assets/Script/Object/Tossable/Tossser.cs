using System.Collections.Generic;
using MyUtils;
using UnityEngine;

namespace MyObject
{
    public class Tosser :CrashObject
    {

        public List<TossDataSo>bullets = new List<TossDataSo>();
        List<TimeCount>tc = new List<TimeCount>();
        List<GameObject>GO = new List<GameObject>();


        void Create(TossDataSo bullet)
        {
            GameObject tGO = Instantiate(bullet.TossObject,null);
            tGO.SetActive(true);
            tGO.transform.position = 
                transform.TransformPoint(bullet.beginPos);
            // Debug.Log(transform.TransformPoint(transform.position));
            // Debug.Log(transform.TransformPoint(transform.position+bullet.beginPos));
            Tossable toss = tGO.GetComponent<Tossable>();
            toss.SetAliveTime(bullet.AliveTime);
            toss.SetCrashTime(bullet.CrashTime);
            toss.SetGravity(bullet.gravity);
            toss.SetInitialVelocity(bullet.beginVelocity);
            toss.SetIsConstant(bullet.isConstant);
            GO.Add(tGO);
            toss.Begin();
        }

        TimeCount GetTimeCount(int index)
        {
            var bullet = bullets[index];
            TimeCount tc = new TimeCount(bullet.TossSeq);
            tc.On_End.AddListener(
                () =>
                {
                    if(!bullets[index].shotOnce)Create(bullets[index]);
                }
               
            );
            tc.Reset();
            return tc;

        }
        void Initialize()
        {
            for(int i = 0; i < bullets.Count; i++)
            {
                Create(bullets[i]);
                tc.Add(GetTimeCount(i));
            }

        }
        public override void Start()
        {
            base.Start();
            Initialize();
        }

        public void Update()
        {
            for(int i = 0; i < tc.Count; i++)
            {
                tc[i].Update(Time.deltaTime);
                if(tc[i].isStop())tc[i].Reset();
            }
        }
    }
}