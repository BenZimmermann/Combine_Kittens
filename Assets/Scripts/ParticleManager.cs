using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager instance;

    [SerializeField] private ParticleSystem _mainParticleSystem;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    /// <summary>
    /// Spielt den Effekt an einer Position ab
    /// </summary>
    public void PlayEffect(Vector3 position)
    {
        if (_mainParticleSystem == null) return;

        // Particle System an Position setzen
        _mainParticleSystem.transform.position = position;

        // Effekt spielen
        _mainParticleSystem.Play();
    }
}
