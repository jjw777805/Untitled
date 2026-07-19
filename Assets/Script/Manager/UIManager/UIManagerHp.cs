using MyUI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;
using System.IO;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Threading;
public partial class UIManager : MonoBehaviour
{ 
    int HPcount=3;
    int activeHP=0;
    List<GameObject>HPList;

    public GameObject HPprefab;
    public void InitialHP()
    {
        HPList = new List<GameObject>();
        Vector3 beginPos = new Vector3(-60,39.6f,-841.7767f);
        for(int i = 0; i <= 8; i++)
        {
            GameObject set = Instantiate(HPprefab);
            set.transform.SetParent(gameObject.transform,false);
            set.transform.localPosition = beginPos + i*new Vector3(11,0,0);
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
        Debug.Log(activeHP+" : "+HPcount+":"+HPList.Count);
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
