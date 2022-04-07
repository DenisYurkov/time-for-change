using UnityEngine.SceneManagement;

namespace TimeForChange.Extensions
{
    public static class Ending
    {
        public static int Score = 0;

        public static void ChoiceEnd(int goodScene, int badScene) => 
            SceneManager.LoadScene(Score >= 5 ? goodScene : badScene);
    }
}