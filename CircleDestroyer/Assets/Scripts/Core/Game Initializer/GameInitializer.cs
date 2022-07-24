using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.GameInitializer
{
    public class GameInitializer : MonoBehaviour
    {
        private const string GameSceneName = "GameScene";
        
        private void Start()
        {
            SceneManager.LoadScene(GameSceneName);
        }
    }
}
