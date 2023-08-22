using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] private const float Time_To_Despawn = 1f;

    private Coroutine _timerCoroutine;

    void OnEnable()
    {
        _timerCoroutine = StartCoroutine(ReturnToPoolAfterTime());
    }

    private IEnumerator ReturnToPoolAfterTime()
    {
        float elapsedTime = 0f;

        while (elapsedTime < Time_To_Despawn)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }
}
