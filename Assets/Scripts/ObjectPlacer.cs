using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    [SerializeField] GameObject placeObj;
    [SerializeField] float placeDistance = 30f;
    [SerializeField] float placeRadius = 15f;
    [SerializeField] int objsToPlace = 100;

    [SerializeField] List<GameObject> objs;

    public void Generate ()
    {
        foreach (GameObject obj in objs) DestroyImmediate(obj);
        objs.Clear();

        for (int i = 0; i < objsToPlace; i++) 
        {
            Vector3 pos = new Vector3(transform.position.x + Random.Range(-placeRadius, placeRadius), 
                                      transform.position.y, 
                                      transform.position.z + Random.Range(-placeRadius, placeRadius));

            RaycastHit hit;
            if (Physics.Raycast(pos, Vector3.down, out hit)) 
            {
                float dist = Vector3.Distance(hit.point, pos);
                print(dist);
                if (dist >= placeDistance) 
                {
                    GameObject obj = Instantiate(placeObj, hit.point, Quaternion.LookRotation(hit.normal));
                    obj.transform.localScale = RandomSize();
                    objs.Add(obj);
                }
            }
        }
    }

    Vector3 RandomSize () 
    {
        float x = Random.Range(0.9f, 2f);
        float y = Random.Range(0.7f, 1.5f);
        float z = Random.Range(0.9f, 2f);

        return new Vector3(x, z, y);
    }
}
