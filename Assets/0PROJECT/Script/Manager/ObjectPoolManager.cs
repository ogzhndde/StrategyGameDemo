using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
using System.ComponentModel;
using System;

public class ObjectPoolManager : MonoBehaviour
{
    public static List<PooledObjectInfo> ObjectPools = new List<PooledObjectInfo>();
    public static PoolType PoolTypeEnum;

    private GameObject _objectPoolEmptyHolder;

    private static GameObject _gameobjectsEmpty;
    private static GameObject _particleSystemEmpty;



    void Awake()
    {
        SetupEmpties();
    }

    private void SetupEmpties()
    {
        // CREATE GAMEOBJECTS IN SCENE TO KEEP POOL OBJECTS
        _objectPoolEmptyHolder = new GameObject("Pooled Objects");

        _particleSystemEmpty = new GameObject("Particle Effects");
        _particleSystemEmpty.transform.SetParent(_objectPoolEmptyHolder.transform);

        _gameobjectsEmpty = new GameObject("Gameobjects");
        _gameobjectsEmpty.transform.SetParent(_objectPoolEmptyHolder.transform);
    }

    public static GameObject SpawnObjects(GameObject objectToSpawn, Vector3 spawnPosition, Quaternion spawnRotation, PoolType poolType = PoolType.None)
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
            Debug.Log("yeni spawnladim");
            //SET PARENT
            GameObject parentObject = SetParentObjects(poolType);

            //IF THERE IS NO INACTIVE OBJECTS, SPAWN A NEW ONE
            spawnableObj = Instantiate(objectToSpawn, spawnPosition, spawnRotation);

            if (parentObject != null)
            {
                spawnableObj.transform.SetParent(parentObject.transform);
            }
        }
        else
        {
            Debug.Log("kapaliyi actim");
            //IF THERE IS AN INACTIVE OBJECT, REACTIVE IT
            spawnableObj.transform.position = spawnPosition;
            spawnableObj.transform.rotation = spawnRotation;
            pool.InactiveObjects.Remove(spawnableObj);
            spawnableObj.SetActive(true);
        }

        return spawnableObj;
    }

    public static GameObject SpawnObjects(GameObject objectToSpawn, Transform parentTransform)
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
            //IF THERE IS NO INACTIVE OBJECTS, SPAWN A NEW ONE
            spawnableObj = Instantiate(objectToSpawn, parentTransform);
        }
        else
        {
            //IF THERE IS AN INACTIVE OBJECT, REACTIVE IT
            pool.InactiveObjects.Remove(spawnableObj);
            spawnableObj.SetActive(true);
        }

        return spawnableObj;
    }

    public static void ReturnObjectToPool(GameObject obj)
    {
        string goName = obj.name.Substring(0, obj.name.Length - 7); //CUT THE (CLONE) PART FROM NAME

        PooledObjectInfo pool = ObjectPools.Find(p => p.LookupString == goName);

        if (pool == null)
        {
            Debug.LogWarning("There is something wrong about pool! " + obj.name);
        }
        else
        {
            obj.SetActive(false);
            pool.InactiveObjects.Add(obj);
        }
    }

    private static GameObject SetParentObjects(PoolType poolType)
    {
        switch (poolType)
        {
            case PoolType.ParticleSystem:
                return _particleSystemEmpty;

            case PoolType.Gameobject:
                return _gameobjectsEmpty;
                
            case PoolType.None:
                return null;

            default:
                return null;
        }
    }
}

public class PooledObjectInfo
{
    public string LookupString;
    public List<GameObject> InactiveObjects = new List<GameObject>();
}
