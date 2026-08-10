using System.Collections.Generic;
using MyObject;
using TMPro;
using UnityEngine;

namespace MyUI
{
    public class CltScrollList : ScrollList
    {
        List<CollectionCommonData>itemData;
        List<Sprite>itemImage;

        public CltDiscription cDiscription;
        public NavigateButton navigationButton;
        bool isSelect;
        int currentSelectedID = -1;

        public void SetData(List<CollectionCommonData> it)
        {
            itemData =it;
        }

        public void SetImage(List<Sprite> it)
        {
            itemImage = it;
        }

        int head = 0 , tail;
        void InitailList()
        {
            head=0;
            tail = existCount;

        }

        void DealWithUp()
        {
            //如果能够显示的数量大于需要缓冲的，直接忽略
            // Debug.Log("before:"+head+","+tail+","+currentSelectedID);
            if(maxCount>existCount || currentSelectedID == 0)return;
            currentSelectedID --;
            // Debug.Log("now:"+head+","+tail+","+currentSelectedID);
            int index = currentSelectedID;
            if(index == head - 1)
            {
                if(maxCount == existCount)
                {
                    rect.anchoredPosition = new Vector2(0,0);
                }
                else TailToHead();
                
            }
            else if(index == head)
            {
                rect.anchoredPosition = new Vector2(0,0);
            }
            item[index%existCount].GetComponent<Selectable>().Select();
        }
        void DealWithDown()
        {
            //如果能够显示的数量大于需要缓冲的，直接忽略
            // Debug.Log("before:"+head+","+tail+","+currentSelectedID);
            if(currentSelectedID == count-1 )return ;
            if(maxCount>existCount)return;
            currentSelectedID ++;
            // Debug.Log("now:"+head+","+tail+","+currentSelectedID);
            int index = currentSelectedID;
            
            if(index == tail - 1)
            {
                if(maxCount == existCount)
                {
                    rect.anchoredPosition = new Vector2(0,upDelta);
                }
                else HeadToTail();
                
            }
            else if( maxCount != existCount && index == tail - 2)
            {
                //tail - 1是空出来做缓冲的
                rect.anchoredPosition = new Vector2(0,upDelta);
            }
            item[index%existCount].GetComponent<Selectable>().Select();
        }
        
        void HeadToTail()
        {
            int index = head%existCount;
            var it = item[index];
            ButtonInitial(index,tail);
            it.GetComponent<RectTransform>().SetAsLastSibling();
            head++;
            tail++;
        }

        void TailToHead()
        {
            int index = (tail-1)%existCount;
            var it = item[index];
            ButtonInitial(index,head - 1);
            it.GetComponent<RectTransform>().SetAsFirstSibling();
            head--;
            tail--;
        }
        void ButtonInitial(int itemIndex,int dataIndex)
        {
            itemIndex %= existCount;
            var it = item[itemIndex];
            var text = it.GetComponentInChildren<TMP_Text>();
            var bt = it.GetComponent<Button>();
            // Debug.Log(((text)==null) +" "+ (bt==null));
            int trueDataIndex = dataIndex % itemData.Count;

            text.SetText(itemData[trueDataIndex].objName);
            bt.onClick.RemoveAllListeners();
            bt.onClick.AddListener(
                () =>
                {
                    UIManager.instance.OpenMsgBox(
                        GetInstanceID()+"_"+trueDataIndex,
                        itemData[trueDataIndex].detailDiscription,
                        new Vector2(1600,800)
                    );
                }
            );
            bt.onSelect.RemoveAllListeners();
            bt.onSelect.AddListener(
                () =>
                {
                    isSelect = true;
                    currentSelectedID = dataIndex;
                    cDiscription.SetImage(itemImage[trueDataIndex]);
                    cDiscription.SetBriefText(itemData[trueDataIndex].shortDiscription);
                }
            );
            bt.OnExit.RemoveAllListeners();
            bt.OnExit.AddListener(
                () =>
                {
                    isSelect = false;
                    currentSelectedID = -1;
                    navigationButton.Select();
                    navigationButton.SetIsIn(false);
                }
            );
        }
        [ContextMenu("Initial")]
        public override void Initialized()
        {
            base.Initialized();
            InitailList();
            isSelect = false ;
            for(int i=0;i<existCount;i++)
            {
               ButtonInitial(i,i);
            }
        }

        public void Select()
        {
            if (count == 0)
            {
                Debug.Log("no item");
                return ;
            }
            var bt = item[0].GetComponent<Button>();
            bt.Select();
        }
        private MyInput inputs;
        static float beginTime;
        public void Start()
        {
            inputs=GameManager.instance.GetInputs();
            beginTime=Time.realtimeSinceStartup;
            
        }
        public void Update()
        {
            float delta = Time.realtimeSinceStartup - beginTime;
            if(delta < 0.2f )return ;
            if (inputs.Player.Move.IsPressed() && isSelect )
            {   
                Vector2 move = inputs.Player.Move.ReadValue<Vector2>();
                
                if (move.y > 0.1f) DealWithUp();
                else if (move.y < -0.1f) DealWithDown();

                beginTime = Time.realtimeSinceStartup;
            }
        }
    }
}