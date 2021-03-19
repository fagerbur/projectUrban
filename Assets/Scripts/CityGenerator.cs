using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityGenerator : MonoBehaviour
{
    public RectTransform foundation;
    public GameObject arenaBuildings;

    private Object[] buildingsArray;
    private bool isCityFull = false;

    void Awake()
    {
        SpawnAll();
    }

    public void SpawnAll()
    {
        //TODO: swap resources.load to assetbundle
        buildingsArray = Resources.LoadAll("Buildings", typeof(GameObject));

        while(!isCityFull)
        {
            int x = (int)Random.Range(0,18);
            
            GameObject building = (GameObject)buildingsArray[x];
            Vector3 newLocation = new Vector3(Mathf.Round(Random.Range(-foundation.rect.width * 3.5f, foundation.rect.width * 3.5f) / 10) * 10f, 0, Mathf.Round(Random.Range(-foundation.rect.width * 3.5f, foundation.rect.width * 3.5f) / 10) * 10f);
            Vector3 buildingBounds = building.GetComponent<MeshFilter>().sharedMesh.bounds.max * 1.2f;
            Collider[] isColliding = Physics.OverlapBox(newLocation, buildingBounds);
            int collisionCount = 0;

            while(isColliding.Length > 1)
            {   
                newLocation = new Vector3(Mathf.Round(Random.Range(-foundation.rect.width * 3.5f, foundation.rect.width * 3.5f) / 10) * 10f, 0, Mathf.Round(Random.Range(-foundation.rect.width * 3.5f, foundation.rect.width * 3.5f) / 10) * 10f);
                isColliding = Physics.OverlapBox(newLocation, buildingBounds);
                collisionCount++;

                if(collisionCount > 5)
                {
                    isCityFull = true;
                    break;
                }
            }

            if(!isCityFull)
            {
                Instantiate(building, newLocation, Quaternion.identity, arenaBuildings.transform);
            }
        }
    }

    public void DestroyAll()
    {
        foreach(Transform bldg in arenaBuildings.transform)
        {
            Destroy(bldg.transform.gameObject);
        }
        isCityFull = false;
    }
}
