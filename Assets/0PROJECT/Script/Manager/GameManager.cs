using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : SingletonManager<GameManager>
{
    public GameData data;

    public GameObject denemePrfeab;
    public GameObject denemePrfeab2;


    void Awake()
    {
#if !UNITY_EDITOR
        SaveManager.LoadData(data);
#endif
    }

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ObjectPoolManager.SpawnObjects(denemePrfeab, new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f),0), Quaternion.identity, PoolType.Gameobject);
            Debug.Log("Yesil spawn");
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            ObjectPoolManager.SpawnObjects(denemePrfeab2, new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f),0), Quaternion.identity, PoolType.Gameobject);
            Debug.Log("kirmizi spawn");
        }
    }



    //########################################    EVENTS    ###################################################################

    private void OnEnable()
    {
        // EventManager.AddHandler(GameEvent.OnStart, OnStart);
    }

    private void OnDisable()
    {
        // EventManager.RemoveHandler(GameEvent.OnStart, OnStart);
    }

}
