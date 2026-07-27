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

        public void SetData(List<CollectionCommonData> it)
        {
            itemData =it;
        }

        public void SetImage(List<Sprite> it)
        {
            itemImage = it;
        }
        public override void Initialized()
        {
            base.Initialized();
            for(int i=0;i<count;i++)
            {
                var it = item[i];
                var text = it.GetComponentInChildren<TMP_Text>();
                var bt = it.GetComponent<Button>();
                // Debug.Log(((text)==null) +" "+ (bt==null));
                text.SetText(itemData[i].objName);

                int index = i;

                bt.onClick.AddListener(
                    () =>
                    {
                        UIManager.instance.OpenMsgBox(
                            GetInstanceID()+"_"+index,
                            itemData[index].detailDiscription,
                            new Vector2(1600,800)
                        );
                    }
                );
                bt.onSelect.AddListener(
                    () =>
                    {
                        cDiscription.SetImage(itemImage[index]);
                        cDiscription.SetBriefText(itemData[index].shortDiscription);
                    }
                );
                bt.OnExit.AddListener(
                    () =>
                    {
                        navigationButton.Select();
                        navigationButton.SetIsIn(false);
                    }
                );
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
    }
}