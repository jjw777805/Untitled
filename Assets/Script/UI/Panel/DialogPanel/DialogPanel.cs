using System.Collections.Generic;
using MyObject;
using TMPro;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

namespace MyUI
{
    public class DiaLogPanel : Panel
    {
        public Button button;
        public TMP_Text personName;
        public TMP_Text content;
        public Image image;
        public GameObject choiceFather;
        public GameObject buttonSample;

        private List<GameObject>choice = new List<GameObject>();
        private int count = 0;


        protected void CreateDialogButton(int num)
        {
            buttonSample.SetActive(false);
            for(int i = 0; i < num; i++)
            {
                // Debug.Log(i+" : "+buttonSample.name);
                GameObject t =Instantiate(buttonSample);
                t.transform.SetParent(choiceFather.transform,false);
                t.SetActive(true);
                choice.Add(t);
                
            }
            for(int i = 0; i < num; i++)
            {
                var now = choice[i].GetComponent<Navigation>();
                if (count + i != 0)
                {
                    now.up = choice[count + i - 1].GetComponent<DialogButton>();
                }
                if (i != num - 1)
                {
                    now.down = choice[count + i+1].GetComponent<DialogButton>();
                }
            }
            count+=num;
        }
        public DialogButton GetDialogButton(int index)
        {
            if(count <= index)CreateDialogButton(index - count + 1);
            return choice[index].GetComponent<DialogButton>();
        }

        public void SetActiveNumber(int num)
        {
            if(count <= num - 1)CreateDialogButton(num - count);
            for(int i = 0; i < num; i++)
            {
                choice[i].SetActive(true);
            }
            for(int i=num; i < count; i++)
            {
                choice[i].SetActive(false);
            }
        }

        public void Clear()
        {
            for(int i = 0; i < count; i++)
            {
                choice[i].SetActive(false);
            }
        }

        public override void Close()
        {
            Clear();
            base.Close();
        }
    }
}