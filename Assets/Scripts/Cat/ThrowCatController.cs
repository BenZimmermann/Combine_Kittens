using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ThrowCatController : MonoBehaviour
{
       public static ThrowCatController instance;
       public GameObject CurrentCat { get; set; }
       [SerializeField] private Transform _catTransform;
       [SerializeField] private Transform _parentAfterThrow;
       [SerializeField] private CatSelector _selector;

    private PlayerController _playerController;

    private Rigidbody2D _rb;
    private CircleCollider2D _circleCollider;

    public Bounds Bounds { get; private set; }

    private const float EXTRA_WIDTH = 0.02f;

    public bool CanThrow { get; set; } = true;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
        SpawnACat(_selector.PickRandomCatForThrow());

    }
    private void Update()
    {
        if (UserInput.IsThrowPressed && CanThrow)
        {
            AudioManager.instance.PlaySFX("drop");

            SpriteIndex index = CurrentCat.GetComponent<SpriteIndex>();
            Quaternion rot = CurrentCat.transform.rotation;

            GameObject go = Instantiate(CatSelector.instance.Cats[index.Index], CurrentCat.transform.position, rot);
            go.transform.SetParent(_parentAfterThrow);
            Destroy(CurrentCat);
            CanThrow = false;
        }
    }
    public void SpawnACat(GameObject cat)
    {
        GameObject go = Instantiate(cat, _catTransform);
        CurrentCat = go;
        _circleCollider = CurrentCat.GetComponent<CircleCollider2D>();
        Bounds = _circleCollider.bounds;

        _playerController.ChangeBoundary(EXTRA_WIDTH);
    }
}
