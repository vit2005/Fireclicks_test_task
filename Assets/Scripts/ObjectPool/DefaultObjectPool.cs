using UnityEngine;
using UnityEngine.Pool;

public class DefaultObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int initialPoolSize = 10;
    [SerializeField] private int maxPoolSize = 20;

    private IObjectPool<GameObject> instancesPool;

    private void Awake()
    {
        instancesPool = new ObjectPool<GameObject>(CreateInstance, OnGetInstance, OnReleaseInstance, OnDestroyInstance, false, initialPoolSize, maxPoolSize);
    }

#region Private Methods
    private GameObject CreateInstance()
    {
        GameObject instance = Instantiate(prefab);
        instance.SetActive(false);
        return instance;
    }

    private void OnGetInstance(GameObject instance)
    {
        instance.SetActive(true);
    }

    private void OnReleaseInstance(GameObject instance)
    {
        instance.SetActive(false);
    }

    private void OnDestroyInstance(GameObject instance)
    {
        Destroy(instance);
    }
#endregion

    public GameObject GetInstance()
    {
        return instancesPool.Get();
    }

    public void ReleaseInstance(GameObject instance)
    {
        instancesPool.Release(instance);
    }
}
