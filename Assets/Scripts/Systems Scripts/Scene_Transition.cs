using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Transition : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Game_Manager gameManager = Singleton.instance.GetComponent<Game_Manager>();

        if (other.gameObject.CompareTag("PlayerBody"))
        {
            Scene currentScene = SceneManager.GetActiveScene();
            Debug.Log(currentScene.ToString());

            if (currentScene.name == "Level_1")
            {
                gameManager.GameWinTrigger();
            }
        }
    }
}