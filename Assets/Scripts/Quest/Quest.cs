using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quest
{
    [CreateAssetMenu(fileName = "New Quest", menuName = "Quest", order = 0)]
    public class Quest : ScriptableObject
    {
        [SerializeField] List<Objective> objectives = new List<Objective>();
        [SerializeField] List<Reward> rewards = new List<Reward>();

        [System.Serializable]
        public class Objective
        {
            public string reference;
            public string description;
        }

        [System.Serializable]
        public class Reward
        {
            [Min(1)] public int number;
            public string rewardType;

            public string GetText()
            {
                if (number == 1)
                    return "1 " + rewardType;
                return number.ToString() + " " + rewardType + "s";
            }
        }

        public string GetTitle()
        {
            return name;
        }

        public int GetObjectiveCount()
        {
            return objectives.Count;
        }

        public IEnumerable<Objective> GetAllObjectives()
        {
            return objectives;
        }

        public IEnumerable<Reward> GetAllRewards()
        {
            return rewards;
        }

        public bool HasObjective(string objectiveReference)
        {
            foreach (Objective objective in objectives)
                if (objective.reference == objectiveReference)
                    return true;
            return false;
        }

        public static Quest GetByName(string questName)
        {
            foreach (Quest quest in Resources.LoadAll<Quest>(""))
                if (quest.name == questName)
                    return quest;
            return null;
        }
    }
}