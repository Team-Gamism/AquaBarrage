using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class FishPreloadController : MonoBehaviour
{
    [SerializeField] private FishDataBase monsterDB;

    private HashSet<string> loadedAddresses = new HashSet<string>();

    public static FishPreloadController Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    /// <summary>
    /// 씬 진입 전 호출
    /// </summary>
    public IEnumerator Preload(List<FishType> stage)
    {
        HashSet<string> requiredAddresses = new HashSet<string>();
        List<AsyncOperationHandle> handles = new List<AsyncOperationHandle>();

        foreach (var id in stage)
        {
            var data = monsterDB.Get(id.ToString());
            if (data == null)
                continue;

            requiredAddresses.Add(data.prefabAddress);

            if (!loadedAddresses.Contains(data.prefabAddress))
            {
                var handle = AddressableManager.Instance
                    .LoadAsync<GameObject>(data.prefabAddress);

                handles.Add(handle);
                loadedAddresses.Add(data.prefabAddress);
            }
        }

        ReleaseUnused(requiredAddresses);

        // 모든 preload 완료 대기
        foreach (var handle in handles)
            yield return handle;
    }


    private void ReleaseUnused(HashSet<string> required)
    {
        var toRelease = new List<string>();

        foreach (var addr in loadedAddresses)
        {
            if (!required.Contains(addr))
                toRelease.Add(addr);
        }

        foreach (var addr in toRelease)
        {
            AddressableManager.Instance.Release(addr);
            loadedAddresses.Remove(addr);
        }
    }
}
