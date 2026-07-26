using System.Collections.Generic;
using System.Threading.Tasks;
using MyObject;
using TMPro;
using UnityEditor.Build.Pipeline;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

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
                bt.onClick.AddListener(()=>{});
                
                int index = i;
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