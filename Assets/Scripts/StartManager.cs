using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    [SerializeField] private GameObject _easterEgg;
    public void StartPressed()
    {
        AudioManager.instance.PlaySFX("click");
        SceneManager.LoadScene(1);
    }
    public void QuitPressed()
    {
        AudioManager.instance.PlaySFX("click");
        Application.Quit();
    }
    public void OnCatPressed()
    {
        AudioManager.instance.PlaySFX("click");
       _easterEgg.SetActive(true);
        StartCoroutine(DisableEasterEgg());
    }

    public IEnumerator DisableEasterEgg()
    {
        yield return new WaitForSeconds(5f);
        _easterEgg.gameObject.SetActive(false);
    }
}

