using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Quest.UI
{
    public class QuestItemUI : MonoBehaviour
    {
        [SerializeField] Text title;
        [SerializeField] Text progress;

        QuestStatus status;

        public void Setup(QuestStatus status)
        {
            this.status = status;

            Quest quest = status.GetQuest();
            title.text = quest.GetTitle();
            progress.text = status.GetCompletedObjectivesCount().ToString()
                + "/" + quest.GetObjectiveCount().ToString();
        }

        public QuestStatus GetQuestStatus()
        {
            return status;
        }
    }
}