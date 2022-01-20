using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quest
{
    public class QuestStatus
    {
        Quest quest;
        List<string> completedObjectives = new List<string>();

        public QuestStatus(Quest quest)
        {
            this.quest = quest;
        }

        public Quest GetQuest()
        {
            return quest;
        }

        public int GetCompletedObjectivesCount()
        {
            return completedObjectives.Count;
        }

        public bool IsObjectiveComplete(string objective)
        {
            return completedObjectives.Contains(objective);
        }

        public bool IsComplete()
        {
            foreach (Quest.Objective objective in quest.GetAllObjectives())
                if (!completedObjectives.Contains(objective.reference))
                    return false;
            return true;
        }

        public void CompleteObjective(string objective)
        {
            if (quest.HasObjective(objective) && !completedObjectives.Contains(objective))
                completedObjectives.Add(objective);
        }
    }
}