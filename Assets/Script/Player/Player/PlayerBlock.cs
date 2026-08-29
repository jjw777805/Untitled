
using System;
using MyUtils;
using UnityEngine;
using UnityEngine.Rendering;

namespace MyPlayer
{
    public partial class Player:MonoBehaviour
    {
        float blockBeginTime=-100f;
        public float blkDuration=60f;
        public float blkCD = 0f;
        int blkCnt=1;
        float blkLimitTime;
        Vector3 blkDeltaPos;
        public float blkRadius=2.0f;
        public float blkShiverTime=0.1f;
        /// <summary>
        /// 用来控制格挡或闪避成功之后的静止时长
        /// </summary>
        TimeCount blkCT =new TimeCount();
        public string blkShiverImage = "p_blk.prefab";
        float blkShiverBeginTime=0.0f;
        Action blkS,blkF;
        public enum BlkType
        {
            Hurt,Trap
        }
        public AudioClip clipBlock;
        BlkType blkType;
        void BlockInitial()
        {
            // Debug.Log(blkShiverTime);
            blkCT.SetTime(blkShiverTime);
            blkCT.On_End.AddListener(
                ()=>{
                        UIManager.instance.CloseImage(blkShiverImage);
                        Time.timeScale=blkTimeScale;
                        ps.BlockEnd();
                        ps.GetGround();
                        TakeJump();
                    });
        }
        float blkTimeScale=0;
        bool blockSuccess = false;
        void BlockStart(float time,Vector3 delta)
        {
            blockSuccess = false;
            blkTimeScale = Time.timeScale;
            Time.timeScale=0;
            blkDeltaPos = delta;
            if(time-blockBeginTime <= blkCD)blkCnt++;
            else blkCnt = 1;
            blockBeginTime = time;
            blkLimitTime = blkDuration/blkCnt;
            rb.velocity = Vector2.zero;
            // Debug.Log("limit:"+blkLimitTime +"begin:"+blockBeginTime);
            UIManager.instance.BeginCountDown(transform.position,100,blkLimitTime);
        }
        void Block()
        {
            
            if (Time.timeScale == 0)
            {
                float tempt = Time.realtimeSinceStartup;
                blkCT.Update(tempt - blkShiverBeginTime);
                blkShiverBeginTime =  tempt;
            }
                
            
            if (!ps.IsBlock() || hasSlide || blockSuccess)return ;
            float delta = Time.realtimeSinceStartup - blockBeginTime;
            // Debug.Log("delta:"+delta);
            if (!inputs.Player.Block.WasPressedThisFrame())
            {
                UIManager.instance.UpdateCountDown(delta);
                if (delta > blkLimitTime)
                {
                    // Debug.Log("hurt?");
                    ps.BlockEnd();
                    UIManager.instance.FinishCoundDown();
                    if(blkType==BlkType.Hurt)Hurt(hurtDamage);
                    else Trap(hurtDamage);
                    blkCnt = 0;
                    Time.timeScale=blkTimeScale;
                    blkF?.Invoke();
                    return ;
                }else return;
            }

            // Debug.Log("ssuccess!");
            UIManager.instance.FinishCoundDown();
            Vector3 way = blkDeltaPos.normalized*blkRadius;
            // Debug.Log(blkDeltaPos.normalized+":"+way);
            UIManager.instance.ShowImage(
                    blkShiverImage,
                    transform.position+way,
                    way,
                    new Vector2(150,150));
            blkShiverBeginTime = Time.realtimeSinceStartup;
            rb.velocity = Vector2.zero;
            // Debug.Log("sus1");
            blkS.Invoke();
            ps.Slide();
            audioOneshot.PlayOneShot(clipBlock);
            blockSuccess = true;
            // Debug.Log("sus2");
            blkCT.Reset();
        }
    }
}