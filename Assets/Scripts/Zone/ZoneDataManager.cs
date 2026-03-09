using UnityEngine;
using System.Collections.Generic;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance;

    public List<ZoneData> zoneDataList = new List<ZoneData>();

    private void Awake()
    {
        // Singleton so all zones can send data here
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StoreZoneData(ZoneData data)
    {
        zoneDataList.Add(data);
        Debug.Log("Stored data for " + data.zoneName);
    }
}