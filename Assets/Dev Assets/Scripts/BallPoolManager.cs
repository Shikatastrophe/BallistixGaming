using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BallPoolManager : MonoBehaviour
{
    //Usé una forma diferente de programar, gracias a un video, bntw este comentario probablemente no tenga muy biena redaccion, ando chambiando a las 2 de la mañana ouuuuu hyeah. https://www.youtube.com/watch?v=9O7uqbEe-xc

    public static List<PooledObjectInfo> ObjectPools = new List<PooledObjectInfo>();

    public static GameObject SpawnObject(GameObject objectToSpawn, Vector3 spawnPosition, Quaternion spawnRotation)
    {
        PooledObjectInfo pool = ObjectPools.Find(p => p.LookupString == objectToSpawn.name);

        if (pool == null)
        {
            pool = new PooledObjectInfo() { LookupString = objectToSpawn.name };
            ObjectPools.Add(pool);
        }

        GameObject spawnableObj = pool.InactiveObjects.FirstOrDefault();

        if (spawnableObj == null)
        {
            spawnableObj = Instantiate(objectToSpawn, spawnPosition, spawnRotation);
        }
        else
        {
            spawnableObj.transform.position = spawnPosition;
            spawnableObj.transform.rotation = spawnRotation;
            pool.InactiveObjects.Remove(spawnableObj);
            spawnableObj.SetActive(true);
        }

        return spawnableObj;
    }

    public static void ReturnObject(GameObject obj)
    {
        string objName = obj.name.Substring(0, obj.name.Length - 7); //para quitar el (Clone) y asi si sirva

        PooledObjectInfo pool = ObjectPools.Find(p => p.LookupString == objName);

        if (pool == null)
        {
            Debug.LogWarning("Object not pooled: "+ obj.name);
        }

        else
        {
            obj.SetActive(false);
            pool.InactiveObjects.Add(obj);
        }
    }
}

public class PooledObjectInfo
{
    public string LookupString;
    public List<GameObject> InactiveObjects = new List<GameObject>();
}
