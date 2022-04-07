using TimeForChange.EnumHelpers;
using TimeForChange.Interfaces;
using UnityEngine;

namespace TimeForChange.Quests
{
    public class QuestBehaviour : MonoBehaviour
    {
        private QuestState _questState;
        private IQuest _currentQuest;

        public QuestState GetState() => _questState;
        public void SetQuest(IQuest quest) => _currentQuest = quest;

        public void NextState()
        {
            switch (_questState)
            {
                case QuestState.StartQuest:
                    _currentQuest.StartQuest();
                    _questState++;
                    break;
                case QuestState.PassingQuest:
                    _currentQuest.PassingQuest();
                    _questState++;
                    break;
                case QuestState.EndQuest:
                    _currentQuest.EndQuest();
                    break;
            }
        }
    }
}