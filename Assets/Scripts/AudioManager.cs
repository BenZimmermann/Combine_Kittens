using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header(" Soundeffekte")]
    [SerializeField] private AudioClip mergeSound;
    [SerializeField] private AudioClip dropSound;
    [SerializeField] private AudioClip clickSound;

    private AudioSource _audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySFX(string name)
    {
        AudioClip clip = null;

        switch (name)
        {
            case "merge":
                clip = mergeSound;
                break;
            case "drop":
                clip = dropSound;
                break;
            case "click":
                clip = clickSound;
                break;
        }

        if (clip != null)

            _audioSource.PlayOneShot(clip);
        _audioSource.pitch = Random.Range(0.95f, 1.05f);
    }
}
