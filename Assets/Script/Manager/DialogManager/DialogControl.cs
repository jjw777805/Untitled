using MyDiaLog;
using MyUI;
using MyUtils;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace MyManager
{
    public partial class DialogManager : MonoBehaviour
    {
        DiaLogPanel currentPanel;
        public DiaLogPanel left,right;
        DiaLogSO currentDL;

        LoadSource<Sprite> sprite = new LoadSource<Sprite>();
        async void SetCurrentDialog()
        {
            if(EventSystem.current != null)
            {
                currentPanel.SetFrontSelected(EventSystem.current?.currentSelectedGameObject);
            }
            currentPanel.personName.SetText(currentDL.personName);
            currentPanel.content.SetText(currentDL.content);
            await sprite.LoadAsync(
                currentDL.image.RuntimeKey.ToString(),
                currentDL.image.RuntimeKey.ToString()
            );
            currentPanel.image.sprite = sprite.Get(currentDL.image.RuntimeKey.ToString());
            currentPanel.Open();
        }

        public void Open(DiaLogSO dlSO)
        {
            if(!IsInstance()){
                instance.Open(dlSO);
                return ;
            }          
            Stop();
            currentDL = dlSO;
            SetCurrentPanel();
            SetCurrentDialog(); 
        }

        void SetCurrentPanel()
        {
            if(currentDL.pos == MyEnum.DialogPos.Left)
            {
                if(currentPanel == right)currentPanel.Close();
                currentPanel = left ;
            }          
            else
            {
                if(currentPanel == left)currentPanel.Close();
                currentPanel = right ;
            }
        }
        public void NextDialog()
        {
            if (!IsInstance())
            {
                instance.NextDialog();
                return ;
            }

            currentDL = currentDL.next;
            if(currentDL.type == MyEnum.DialogType.End)
            {
                Close();
                return ;
            }
            SetCurrentPanel();
            SetCurrentDialog();
        }
        public void Close()
        {
            if (!IsInstance())
            {
                instance.Close();
                return ;
            }

            currentPanel.Close();
            sprite.Clear();
            Stop();
        }

        float stopTimeScale=1;
        bool isStop=false;
        void Stop()
        {
            if (!isStop)
            {
                isStop = true;
                stopTimeScale = Time.timeScale;
                Time.timeScale = 0;
            }
            else
            {
                isStop = false;
                Time.timeScale = stopTimeScale;
            }
        }
    }
}