using UnityEngine;
using UnityEngine.UI;
public partial class UIManager : MonoBehaviour
{ 
    float ctTime;
    public GameObject CdCirclePrefab;

    GameObject ctGameObject;
    bool ctIsBegin=false;
    public void BeginCountDown(Vector3 center, float r ,float time)
    {
        ctIsBegin = true;
        ctTime = time;
        ctGameObject = Instantiate(CdCirclePrefab);
        ctGameObject.transform.SetParent(gameObject.transform,false);
        RectTransform rect= ctGameObject.GetComponent<RectTransform>();
        rect.localPosition = GetUIPosition(center);
        rect.sizeDelta=new Vector2(2*r,2*r);

    }

    public void UpdateCountDown(float delta)
    {
        if(!ctIsBegin)return ;
        float ratio = delta/ctTime;
        if(ratio>1)ratio=1;
        ctGameObject.GetComponent<Image>().fillAmount=1-ratio;
    }

    public void FinishCoundDown()
    {
        Destroy(ctGameObject);
        ctIsBegin = false;
    }

}
