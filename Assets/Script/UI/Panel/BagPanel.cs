using System.Collections.Generic;
using MyObject;
using UnityEngine;

namespace MyUI
{
    public class BagPanel : Panel
    {

        List<CollectionCommonData>itemData;
        List<Sprite>itemImage;

        public CltScrollList itemList;

    
        public void SetData(List<CollectionCommonData> it)
        {
            itemData =it;
        }

        public void SetImage(List<Sprite> it)
        {
            itemImage = it;
        }
    
        public void Create()
        {
            itemList.SetCount(itemData.Count);
            itemList.SetData(itemData);
            itemList.SetImage(itemImage);
            itemList.Initialized();
        }
    }
}