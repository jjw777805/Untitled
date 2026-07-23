using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;
public partial class UIManager : MonoBehaviour
{ 
    Vector2 GetUIPosition(Vector3 pos)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(pos);
 
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform, 
            screenPos, 
            UIcamera,
            out Vector2 localPos
        );
        return localPos;
    }

    #region  Image
    Dictionary<string,GameObject>ImageList = new Dictionary<string, GameObject>();
    Dictionary<string,AsyncOperationHandle<GameObject>>ImageHandleList
        = new Dictionary<string,AsyncOperationHandle<GameObject>>();

    async public void ShowImage(string name , Vector3 position, Vector3 way , Vector2 size)
    {
        GameObject Image ;
        if(ImageList.ContainsKey(name))Image = ImageList[name];
        else
        {
            var handle = Addressables.InstantiateAsync(name);
            Image = await handle.Task;
            ImageList.Add(name,Image);
            ImageHandleList.Add(name,handle);
            // Addressables.Release(handle);
        }
        // Debug.Log("checkDict:\n"+string.Join(", ", ImageList.Keys));
        Image.transform.SetParent(canvas.transform);
        RectTransform rect= Image.GetComponent<RectTransform>();
        rect.localPosition = GetUIPosition(position);
        rect.right=way;
        rect.sizeDelta = size;
    }

    public void CloseImage(string name)
    {
        // Debug.Log("Here!: check "+name+ "\n"+string.Join(", ", ImageList.Keys));
        if (ImageList.ContainsKey(name))
        {
            // Debug.Log("In!");
            Addressables.ReleaseInstance(ImageHandleList[name]);
            ImageList.Remove(name);
            ImageHandleList.Remove(name);
        }
    }

    #endregion
}
