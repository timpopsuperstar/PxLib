using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class OpHandler<T,E> where E : System.Enum
{
    private static Dictionary<string, AsyncOperationHandle<T>> _opHandles = new Dictionary<string, AsyncOperationHandle<T>>();
    protected static string GetKeyStringAddress(E e) => e.ToString();

    public static T Get(E e)
    {
        var key = GetKeyStringAddress(e);
        if (_opHandles.ContainsKey(key))
        {
            return _opHandles[key].Result;
        }
        return default;
    }

    public static IEnumerator Load(E e)
    {
        var key = GetKeyStringAddress(e);
        if (_opHandles.ContainsKey(key))
        {
            yield break;
        }
        var handle = Addressables.LoadAssetAsync<T>(key);
        yield return handle;
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            _opHandles.Add(key, handle);
        }
    }
    public static IEnumerator LoadAll()
    {
        var enumNames = System.Enum.GetNames(typeof(E));        
        for (int i = 0; i < enumNames.Length; i++)
        {
            var key = enumNames[i];
            if (_opHandles.ContainsKey(key))
            {
                continue;
            }
            var handle = Addressables.LoadAssetAsync<T>(key);
            yield return handle;
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                _opHandles.Add(key, handle);
            }
        }
    }
    public static void Release(E e)
    {
        var key = GetKeyStringAddress(e);
        if (_opHandles.ContainsKey(key))
        {
            var handle = _opHandles[key];
            Addressables.Release(handle);
            _opHandles.Remove(key);
        }
    }
    public static void ReleaseAll()
    {
        foreach (AsyncOperationHandle<T> handle in _opHandles.Values)
        {
            Addressables.Release(handle);
        }
    }
}
