using UnityEngine;

public class ColliderInformer : MonoBehaviour
{
    public bool WasCombinedIn { get; set; }
    private bool _hasCollided;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_hasCollided && !WasCombinedIn)
        {
            _hasCollided = true;
            ThrowCatController.instance.CanThrow = true;
            ThrowCatController.instance.SpawnACat(CatSelector.instance.NextCat);
            CatSelector.instance.PickNextCat();
            Destroy(this);
        }
    }
}
