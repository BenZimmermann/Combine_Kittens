using UnityEngine;

public class CatInfo : MonoBehaviour
{
    public int CatIndex = 0;
    public int PointsWhenAnnihilated = 1;
    public float CatMass = 1f;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.mass = CatMass;
    }
}
