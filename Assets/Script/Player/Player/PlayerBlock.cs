
using MyUtils;
using UnityEngine;

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
        TimeCount blkCT =new TimeCount();
        public string blkShiverImage = "p_blk.prefab";
        float blkShiverBeginTime=0.0f;
        public enum BlkType
        {
            Hurt,Trap
        }
        
        BlkType blkType;
        void BlockInitial()
        {
            // Debug.Log(blkShiverTime);
            blkCT.SetTime(blkShiverTime);
            blkCT.On_End.AddListener(
                ()=>{
                        UIManager.instance.CloseImage(blkShiverImage);
                        Time.timeScale=1;
                    });
        }
        void BlockStart(float time,Vector3 delta)
        {
            Time.timeScale=0;
            blkDeltaPos = delta;
            if(time-blockBeginTime <= blkCD)blkCnt++;
            else blkCnt = 1;
            blockBeginTime = time;
            blkLimitTime = blkDuration/blkCnt;
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
                
            
            if (!ps.IsBlock())return ;
            float delta = Time.realtimeSinceStartup - blockBeginTime;
            // Debug.Log("delta:"+delta);
            if (!inputs.Player.Block.WasPressedThisFrame())
            {
                UIManager.instance.UpdateCountDown(delta);
                if (delta > blkLimitTime)
                {
                    ps.BlockEnd();
                    UIManager.instance.FinishCoundDown();
                    if(blkType==BlkType.Hurt)Hurt(hurtDamage);
                    else Trap(hurtDamage);
                    blkCnt = 0;
                    Time.timeScale=1;
                    return ;
                }else return;
            }

            // Debug.Log("ssuccess!");
            ps.BlockEnd();
            ps.GetGround();
            UIManager.instance.FinishCoundDown();
            Vector3 way = blkDeltaPos.normalized*blkRadius;
            UIManager.instance.ShowImage(
                    blkShiverImage,
                    transform.position+way,
                    way,
                    new Vector2(150,150));
            blkShiverBeginTime = Time.realtimeSinceStartup;
            blkCT.Reset();
        }
    }
}