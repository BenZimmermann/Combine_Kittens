using UnityEngine;

public class CatCombinder : MonoBehaviour
{
    private int _layerIndex;
    private CatInfo _info;

    private void Awake()
    { 
        _info = GetComponent<CatInfo>();
        _layerIndex = gameObject.layer;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == _layerIndex)
        {
            CatInfo info = collision.gameObject.GetComponent<CatInfo>();
            if (info != null)
            {
                if (info.CatIndex == _info.CatIndex)
                {
                    int thisID = gameObject.GetInstanceID();
                    int otherID = collision.gameObject.GetInstanceID();

                    if (thisID > otherID)
                    {
                        if (_info.CatIndex == CatSelector.instance.cats.Length - 1)
                        {
                            Destroy(collision.gameObject);
                            Destroy(gameObject);
                        }
                        else
                        {
                            Vector3 middlePosition = (transform.position + collision.transform.position) / 2f;
                            GameObject go = Instantiate(SpawnCombinedCat(_info.CatIndex), GameManager.instance.transform);
                            go.transform.position = middlePosition;

                            ColliderInformer informer = go.GetComponent<ColliderInformer>();
                            if (informer != null)
                            { 
                                informer.WasCombinedIn = true;
                            }

                            Destroy(collision.gameObject);
                            Destroy(gameObject);
                        }
                    }
                }
            }
        }
    }
    private GameObject SpawnCombinedCat(int index)
    {
        GameObject go = CatSelector.instance.cats[index + 1];
        return go;
    }
}
