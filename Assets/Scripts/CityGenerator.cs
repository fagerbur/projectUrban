using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityGenerator : MonoBehaviour
{
    private Object[] buildingsArray;
    
    public RectTransform foundation;
    // private bounds cityLimits;

    void Awake()
    {
        //This will eventually be an asset bundle
        Debug.Log("Do we have a city?");
        buildingsArray = Resources.LoadAll("Buildings", typeof(GameObject));

        //Need to check to make sure no buildings overlap
        //Need to better fill the entire level

        foreach (GameObject building in buildingsArray)
        {
            Instantiate(building, new Vector3(Random.Range(-foundation.rect.width * 2, foundation.rect.width * 2), 0, Random.Range(-foundation.rect.height * 2, foundation.rect.height * 2)), Quaternion.identity);
        }
    }

    void Update()
    {
        
    }
}
