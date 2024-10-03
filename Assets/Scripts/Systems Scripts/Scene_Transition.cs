using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Transition : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        Game_Manager gameManager = Singleton.instance.GetComponent<Game_Manager>();
        if (other.CompareTag("Player"))
        {
            Scene currentScene = SceneManager.GetActiveScene();
            if (currentScene.name == "GamePlay1")
            {
                //Debug.Log("Active scene is " + currentScene.name);
                //gameManager.GamePlay2();
            }
        }
    }
}