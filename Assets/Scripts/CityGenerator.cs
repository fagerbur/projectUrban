using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityGenerator : MonoBehaviour
{
    public RectTransform foundation;
    private Object[] buildingsArray;
    private bool isCityFull = false;

    void Awake()
    {
        //TODO: swap resources.load to assetbundle
        //TODO: Fix building collision so it's not sticky...
        buildingsArray = Resources.LoadAll("Buildings", typeof(GameObject));

        while(!isCityFull)
        {
            int x = (int)Random.Range(0,18);
            
            GameObject building = (GameObject)buildingsArray[x];
            Vector3 newLocation = new Vector3(Mathf.Round(Random.Range(-foundation.rect.width * 2, foundation.rect.width * 2) / 10) * 10f, 0, Mathf.Round(Random.Range(-foundation.rect.width * 2, foundation.rect.width * 2) / 10) * 10f);
            Vector3 buildingBounds = building.GetComponent<MeshFilter>().sharedMesh.bounds.max;
            Collider[] isColliding = Physics.OverlapBox(newLocation, buildingBounds);
            int collisionCount = 0;

            while(isColliding.Length > 1)
            {   
                newLocation = new Vector3(Mathf.Round(Random.Range(-foundation.rect.width * 2, foundation.rect.width * 2) / 10) * 10f, 0, Mathf.Round(Random.Range(-foundation.rect.width * 2, foundation.rect.width * 2) / 10) * 10f);
                isColliding = Physics.OverlapBox(newLocation, buildingBounds);
                collisionCount++;

                if(collisionCount > 15)
                {
                    isCityFull = true;
                    break;
                }
            }

            if(!isCityFull)
            {
                Instantiate(building, newLocation, Quaternion.identity);
            }
        }
    }
}
