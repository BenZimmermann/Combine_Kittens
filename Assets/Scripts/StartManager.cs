using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    public void StartPressed()
    {
        // Setze die Zielbildrate auf 60 FPS
        SceneManager.LoadScene(1);
    }
}
