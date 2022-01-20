using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

using Core;
using Inventories;

namespace Quest
{
    public class QuestList : MonoBehaviour, IPredicateEvaluator
    {
        [SerializeField] Quest firstQuest = null;

        List<QuestStatus> statuses = new List<QuestStatus>();

        public event Action onUpdate;

        void Start()
        {
            if (firstQuest != null)
                AddQuest(firstQuest);
        }

        public void AddQuest(Quest quest)
        {
            if (HadQuest(quest))
                return;

            QuestStatus newStatus = new QuestStatus(quest);
            statuses.Add(newStatus);

            if (onUpdate != null)
                onUpdate();
        }

        public void CompleteObjective(Quest quest, string objective)
        {
            QuestStatus status = GetQuestStatus(quest);

            if (status == null || status.IsObjectiveComplete(objective))
                return;

            status.CompleteObjective(objective);

            if (status.IsComplete())
                GiveReward(quest);

            if (onUpdate != null)
                onUpdate();
        }

        private bool HadQuest(Quest quest)
        {
            return GetQuestStatus(quest) != null;
        }

        public IEnumerable<QuestStatus> GetStatuses()
        {
            return statuses;
        }

        private void GiveReward(Quest quest)
        {
            foreach (Quest.Reward reward in quest.GetAllRewards())
                if (reward.rewardType == "Coin")
                    FindObjectOfType<Purse>().UpdateBalance((float)reward.number);
                else if (reward.rewardType == "Life")
                    FindObjectOfType<GameSession>().RecieveLives(reward.number);
        }

        private QuestStatus GetQuestStatus(Quest quest)
        {
            foreach (QuestStatus status in statuses)
                if (status.GetQuest() == quest)
                    return status;
            return null;
        }

        public bool? Evaluate(string predicate, string[] parameters)
        {
            switch (predicate)
            {
                case "Has quest":
                    return HadQuest(Quest.GetByName(parameters[0]));
                case "Completed quest":
                    return GetQuestStatus(Quest.GetByName(parameters[0])).IsComplete();
            }

            return null;
        }
    }
}