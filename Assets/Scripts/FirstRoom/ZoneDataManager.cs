using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance;

    public List<ZoneData> zoneDataList = new List<ZoneData>();

    private void Awake()
    {
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

    public void PrintAllZones()
    {
        foreach (var z in zoneDataList)
        {
            Debug.Log($"Zone: {z.zoneName}, GazeTime: {z.gazeTime}, GazeCount: {z.gazeCount}, AvgDistance: {z.averageDistance}");
        }
    }
// Compute player profile and strongest zone
public (string profile, ZoneData strongestZone) GetPlayerProfile()
{
    if (zoneDataList.Count == 0)
        return ("No data", null);

    string finalProfile = "Unknown";
    ZoneData strongestZone = null;
    int highestZoneScore = int.MinValue;

    foreach (var z in zoneDataList)
    {
        int paranoidScore = 0;
        int cautiousScore = 0;
        int boldScore = 0;

        // -------- PARANOID --------
        // distance matters most
        if (z.averageDistance > 18) paranoidScore += 3;
        else if (z.averageDistance > 11) paranoidScore += 2;

        // lots of turning
        if (z.gazeCount > 6) paranoidScore += 3;
        else if (z.gazeCount >= 3) paranoidScore += 2;

        // doesn't stare long
        if (z.gazeTime < 1f) paranoidScore += 2;


        // -------- CAUTIOUS --------
        // careful observation
        if (z.gazeTime > 3f) cautiousScore += 3;
        else if (z.gazeTime >= 1f) cautiousScore += 2;

        // moderate distance
        if (z.averageDistance >= 10 && z.averageDistance <= 18) cautiousScore += 2;

        // moderate turning
        if (z.gazeCount >= 2 && z.gazeCount <= 5) cautiousScore += 2;


        // -------- BOLD --------
        // gets close
        if (z.averageDistance < 10) boldScore += 3;

        // doesn't turn much
        if (z.gazeCount < 3) boldScore += 2;

        // confident gaze
        if (z.gazeTime > 2f) boldScore += 2;


        // determine zone profile
        int zoneBestScore = Mathf.Max(paranoidScore, cautiousScore, boldScore);
        string zoneProfile = "Unknown";

        if (zoneBestScore == paranoidScore)
            zoneProfile = "Paranoid";
        else if (zoneBestScore == cautiousScore)
            zoneProfile = "Cautious";
        else if (zoneBestScore == boldScore)
            zoneProfile = "Bold";


        Debug.Log($"Zone {z.zoneName} → Paranoid:{paranoidScore}  Cautious:{cautiousScore}  Bold:{boldScore}  => {zoneProfile}");

        // check if this is the strongest zone
        if (zoneBestScore > highestZoneScore)
        {
            highestZoneScore = zoneBestScore;
            finalProfile = zoneProfile;
            strongestZone = z;
        }
    }

    return (finalProfile, strongestZone);
}
    
}