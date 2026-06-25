using UnityEngine;
using UnityEngine.Pool;

public class DefaultObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int initialPoolSize = 10;
    [SerializeField] private int maxPoolSize = 20;
    [SerializeField] private Transform spawnParent;

    private IObjectPool<GameObject> instancesPool;

    private void Awake()
    {
        instancesPool = new ObjectPool<GameObject>(CreateInstance, OnGetInstance, OnReleaseInstance, OnDestroyInstance, false, initialPoolSize, maxPoolSize);
    }

    private GameObject CreateInstance()
    {
        GameObject instance = Instantiate(prefab, spawnParent);
        instance.SetActive(false);
        return instance;
    }

    private void OnGetInstance(GameObject instance) => instance.SetActive(true);
    private void OnReleaseInstance(GameObject instance) => instance.SetActive(false);
    private void OnDestroyInstance(GameObject instance) => Destroy(instance);

    public GameObject GetInstance() => instancesPool.Get();
    public void ReleaseInstance(GameObject instance) => instancesPool.Release(instance);
}
