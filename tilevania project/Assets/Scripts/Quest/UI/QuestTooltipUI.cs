using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Quest.UI
{
    public class QuestTooltipUI : MonoBehaviour
    {
        [SerializeField] Text title;
        [SerializeField] Transform objectiveContainer;
        [SerializeField] GameObject objectivePrefab;
        [SerializeField] GameObject incompleteObjectivePrefab;
        [SerializeField] Transform rewardContainer;
        [SerializeField] GameObject rewardPrefab;

        public void Setup(QuestStatus status)
        {
            Quest quest = status.GetQuest();

            title.text = quest.GetTitle();

            foreach (Transform child in objectiveContainer)
                Destroy(child.gameObject);

            foreach (Quest.Objective objective in quest.GetAllObjectives())
            {
                GameObject prefab = status.IsObjectiveComplete(objective.reference) ? objectivePrefab : incompleteObjectivePrefab;
                GameObject objectiveInstance = Instantiate(prefab, objectiveContainer);
                objectiveInstance.GetComponentInChildren<Text>().text = objective.description;
            }

            foreach (Transform child in rewardContainer)
                Destroy(child.gameObject);

            if (quest.GetAllRewards().Count() == 0)
            {
                GameObject rewardInstance = Instantiate(rewardPrefab, rewardContainer);
                rewardInstance.GetComponentInChildren<Text>().text = "No reward";
            }
            else
                foreach (Quest.Reward reward in quest.GetAllRewards())
                {
                    GameObject rewardInstance = Instantiate(rewardPrefab, rewardContainer);
                    rewardInstance.GetComponentInChildren<Text>().text = reward.GetText();
                }
        }
    }
}