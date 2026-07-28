using MyDiaLog;
using MyUI;
using MyUtils;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.ResourceManagement.AsyncOperations;
using static MyDiaLog.DiaLogChoiceSO;

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
            currentPanel.personName.SetText(currentDL.personName);
            currentPanel.content.SetText(currentDL.content);
            await sprite.LoadAsync(
                currentDL.image.RuntimeKey.ToString(),
                currentDL.image.RuntimeKey.ToString()
            );
            currentPanel.image.sprite = sprite.Get(currentDL.image.RuntimeKey.ToString());
            currentPanel.Open();

            if(currentDL.type == MyEnum.DialogType.Choice)SetChoice();

            
        }

        void SetChoiceButton(DialogButton bt,DialogChoiceData data)
        {
            // Debug.Log(data.choiceText);
            bt.SetText(data.choiceText);
            bt.onClick=data.onClick;
        }
        void SetChoice()
        {
            var cs = currentDL.choice.choices;
            currentPanel.SetActiveNumber(cs.Count);
            for(int i = 0; i < cs.Count; i++)
            {
                var t =currentPanel.GetDialogButton(i);
                SetChoiceButton(t,cs[i]);
            }
            currentPanel.GetDialogButton(0).Select();
        }

        public void Open(DiaLogSO dlSO)
        {
            if(!IsInstance()){
                instance.Open(dlSO);
                return ;
            }          
            Stop();
            Debug.Log(EventSystem.current.currentSelectedGameObject.name);
            if (EventSystem.current != null)
            {
                left.SetFrontSelected(EventSystem.current.currentSelectedGameObject);
                right.SetFrontSelected(EventSystem.current.currentSelectedGameObject);
            }
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
        

        public void NextDialog(DiaLogSO nxt)
        {
            if (!IsInstance())
            {
                instance.NextDialog(nxt);
                return ;
            }

            if(currentDL.type == MyEnum.DialogType.Choice)currentPanel.Clear();
            currentDL = nxt;
            if(currentDL.type == MyEnum.DialogType.End)
            {
                Close();
                return ;
            }
            SetCurrentPanel();
            SetCurrentDialog();
        }

        public void NextDialog()
        {
            if (!IsInstance())
            {
                instance.NextDialog();
                return ;
            }
            NextDialog(currentDL.next);
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