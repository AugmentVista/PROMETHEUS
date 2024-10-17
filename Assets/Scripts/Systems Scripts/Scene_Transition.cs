using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Transition : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) // This is a shortcut, properly connect to LevelManager when there is time
    {
        Debug.Log("Are we getting anything?");
        Game_Manager gameManager = Singleton.instance.GetComponent<Game_Manager>();
        Debug.Log(gameManager.ToString());

        if (other.gameObject.tag == "PlayerBody")
        {
            Scene currentScene = SceneManager.GetActiveScene();
            Debug.Log(currentScene.ToString());

            if (currentScene.name == "GamePlay1")
            {
                gameManager.GameWinTrigger(); // test if this itself will change scene
                //SceneManager.LoadScene("GameWin");
            }
        }
    }
}