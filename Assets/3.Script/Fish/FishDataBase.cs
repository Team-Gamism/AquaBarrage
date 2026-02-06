using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/FishDataBase")]
public class FishDataBase : ScriptableObject
{
    public List<FishData> fishes;

    private Dictionary<string, FishData> lookup;

    public void Init()
    {
        lookup = new Dictionary<string, FishData>();
        foreach (var m in fishes)
            lookup[m.id.ToString()] = m;
    }

    public FishData Get(string id)
    {
        if (lookup == null)
            Init();

        return lookup.TryGetValue(id, out var data) ? data : null;
    }
}
