using UnityEngine;

namespace TimeForChange.Data
{
   [CreateAssetMenu(fileName = "QuestDate", menuName = "Quest/QuestDate")]
   public class QuestData : ScriptableObject
   {
      [TextArea(3, 3)] public string QuestPurpose;
      [TextArea(3, 3)] public string QuestDescription;
   }
}