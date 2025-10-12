using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class CatSelector : MonoBehaviour
{
    public static CatSelector instance;

    public GameObject[] Cats;
    public GameObject[] NoPhysicsCats;
    public int HighestStartingIndex = 3;

    [SerializeField] private Image _nextCatImage;
    [SerializeField] private Sprite[] _catSprites;

    public GameObject NextCat { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        PickNextCat();
    }

    public GameObject PickRandomCatForThrow()
    {
        int randomIndex = Random.Range(0, HighestStartingIndex + 1);

        if (randomIndex < NoPhysicsCats.Length)
        {
            GameObject randomCat = NoPhysicsCats[randomIndex];
            return randomCat;

        }
        return null;
    }
    
    public void PickNextCat()
    {
        int randomIndex = Random.Range(0, HighestStartingIndex + 1);

        if (randomIndex < Cats.Length)
        {
            GameObject nextCat = NoPhysicsCats[randomIndex];
            NextCat = nextCat;

            _nextCatImage.sprite = _catSprites[randomIndex];
        }
    }
}

