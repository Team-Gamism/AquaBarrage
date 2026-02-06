using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableManager : MonoBehaviour
{
    public static AddressableManager Instance { get; private set; }

    private Dictionary<string, AsyncOperationHandle> handleMap
        = new Dictionary<string, AsyncOperationHandle>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Address 기반 로드
    /// </summary>
    public void Load<T>(string address, System.Action<T> onCompleted = null)
        where T : Object
    {
        if (string.IsNullOrEmpty(address))
        {
            Debug.LogError("Address is null or empty");
            return;
        }

        // 이미 로드됨
        if (handleMap.TryGetValue(address, out var existingHandle))
        {
            if (existingHandle.IsDone)
                onCompleted?.Invoke(existingHandle.Result as T);

            return;
        }

        var handle = Addressables.LoadAssetAsync<T>(address);
        handleMap.Add(address, handle);

        handle.Completed += op =>
        {
            if (op.Status == AsyncOperationStatus.Succeeded)
            {
                onCompleted?.Invoke(op.Result);
            }
            else
            {
                Debug.LogError($"Failed to load address: {address}");
                handleMap.Remove(address);
            }
        };
    }

    public AsyncOperationHandle LoadAsync<T>(string address)
    where T : Object
    {
        if (handleMap.TryGetValue(address, out var existing))
            return existing;

        var handle = Addressables.LoadAssetAsync<T>(address);
        handleMap[address] = handle;
        return handle;
    }

    /// <summary>
    /// 이미 로드된 에셋 가져오기
    /// </summary>
    public T Get<T>(string address) where T : Object
    {
        if (string.IsNullOrEmpty(address))
            return null;

        if (handleMap.TryGetValue(address, out var handle))
        {
            if (handle.IsValid() && handle.IsDone)
                return handle.Result as T;
        }

        return null;
    }

    /// <summary>
    /// Address 기반 Release
    /// </summary>
    public void Release(string address)
    {
        if (string.IsNullOrEmpty(address))
            return;

        if (!handleMap.TryGetValue(address, out var handle))
            return;

        if (handle.IsValid())
            Addressables.Release(handle);

        handleMap.Remove(address);
    }

    /// <summary>
    /// 전체 해제
    /// </summary>
    public void ReleaseAll()
    {
        foreach (var handle in handleMap.Values)
        {
            if (handle.IsValid())
                Addressables.Release(handle);
        }

        handleMap.Clear();
    }
}
