using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quest.UI
{
    public class QuestListUI : MonoBehaviour
    {
        [SerializeField] QuestItemUI questPrefab;

        QuestList questList;

        void Start()
        {
            questList = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();
            questList.onUpdate += Redraw;

            Redraw();
        }

        private void Redraw()
        {
            foreach (Transform child in transform)
                Destroy(child.gameObject);

            foreach (QuestStatus status in questList.GetStatuses())
            {
                QuestItemUI uIInstance = Instantiate<QuestItemUI>(questPrefab, transform);
                uIInstance.Setup(status);
            }
        }
    }
}