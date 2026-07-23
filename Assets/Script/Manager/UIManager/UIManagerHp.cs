using UnityEngine;
using System.Collections.Generic;
public partial class UIManager : MonoBehaviour
{ 
    int HPcount=3;
    int activeHP=0;
    List<GameObject>HPList;

    public GameObject HPprefab;
    public void InitialHP()
    {
        HPList = new List<GameObject>();
        Vector3 beginPos = new Vector3(350,-100f,-10f);
        for(int i = 0; i <= 8; i++)
        {
            GameObject set = Instantiate(HPprefab);
            set.transform.SetParent(gameObject.transform,false);
            var rect = set.GetComponent<RectTransform>();
            rect.anchoredPosition3D = beginPos + i*new Vector3(110,0,0);
            set.SetActive(false);
            HPList.Add(set);
        }
    }
    public void SetHP(int x)
    {
        HPcount = x;
        UpdateHP();
    }

    public void UpdateHP()
    {
        // Debug.Log(activeHP+" : "+HPcount+":"+HPList.Count);
        if(activeHP==HPcount)return;
        if(activeHP < HPcount)
        {
            for(int i = activeHP; i < HPcount; i++)
            {
                HPList[i].SetActive(true);
            }
            activeHP = HPcount;
        }
        else
        {
            for(int i = HPcount; i < activeHP; i++)
            {
                HPList[i].SetActive(false);
            }
            activeHP = HPcount;
        }
        
    }

}
