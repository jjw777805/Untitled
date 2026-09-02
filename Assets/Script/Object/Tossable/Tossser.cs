using System.Collections.Generic;
using System.Linq;
using MyUtils;
using UnityEngine;

namespace MyObject
{
    public class Tosser :CrashObject
    {

        public List<TossDataSo>bullets = new List<TossDataSo>();
        public List<float>beginTime = new List<float>();
        List<TimeCount>tc = new List<TimeCount>();
        List<GameObject>GO = new List<GameObject>();
        List<bool>hasBegin = new List<bool>();


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
                hasBegin.Add(false);
                tc.Add(GetTimeCount(i));
            }

        }
        float deltaTime =0;
        public override void Start()
        {
            base.Start();
            Initialize();
            deltaTime = 0;
        }

        public void Update()
        {
            if(Time.timeScale==0)return;
            float tk;
            if(beginTime!=null)tk = beginTime.Max();
            else tk = 0;
            
            if(deltaTime<tk)deltaTime += Time.deltaTime;
            // Debug.Log(deltaTime +"&" + tk);
            for(int i = 0; i < tc.Count; i++)
            {
                if( deltaTime < beginTime[i])continue;
                else
                {
                    if (!hasBegin[i])
                    {
                        hasBegin[i]=true;
                        Create(bullets[i]);
                    }
                    else
                    {
                        tc[i].Update(Time.deltaTime);
                        if(tc[i].isStop())tc[i].Reset();
                    }
                }
                
            }
        }
    }
}