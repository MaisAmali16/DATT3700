using UnityEngine;

public enum PlayerProfile { Paranoid, Cautious, Bold }
public enum PlayerZone { Zone1, Zone2, Zone3, Zone4 }

public class Room2Manager : MonoBehaviour
{
    [Header("DEBUG")]
    public Transform xrRig;                 // assign your XR Rig
    public Vector3 startPositionRoom1;      // origin Room 1
    public Vector3 startPositionRoom2;      // origin Room 2
    public bool debugMode = false;          
    public PlayerProfile testProfile = PlayerProfile.Paranoid;
    public PlayerZone testZone = PlayerZone.Zone1;

    [Header("Settings")]
    public Transform playerCamera;
    public Collider roomBounds;  
    public float cubeSpawnDistanceMin = 0.5f; // min distance from player
    public float spawnInterval = 5f;
    public float lookAngle = 60f;
    public float boldMoveDelay = 4f;

    // internal
    private float timer = 0f;
    private bool timerRunning = false;
    private string playerProfile;
    private ZoneData strongestZone;

    private float spawnTimer = 0f;
    private bool playerLookingAtCube = false;
    private float lookAwayTimer = 0f;
    private int cautiousLookCount = 0;
    private bool playerHasSeenCube = false;

    // models
    public GameObject zone1Model;
    public GameObject zone2Model;
    public GameObject zone3Model;
    public GameObject zone4Model;
    private GameObject activeCube;

    public void StartRoom()
    {
        timerRunning = true;

        // deactivate all models
        zone1Model.SetActive(false);
        zone2Model.SetActive(false);
        zone3Model.SetActive(false);
        zone4Model.SetActive(false);

        // debug or normal profile
        if(debugMode)
        {
            playerProfile = testProfile.ToString();
            strongestZone = new ZoneData { zoneName = testZone.ToString() };
            //if(xrRig != null) xrRig.position = startPositionRoom2;
        }
        else
        {
            //if(xrRig != null) xrRig.position = startPositionRoom1;
             
            (string profile, ZoneData zone) = PlayerDataManager.Instance.GetPlayerProfile();
            playerProfile = profile;
            strongestZone = zone;
            Debug.Log("profile " + playerProfile + " zone " + strongestZone);
        }

        // select the correct cube
        switch(strongestZone.zoneName)
        {
            case "Zone1": activeCube = zone1Model; break;
            case "Zone2": activeCube = zone2Model; break;
            case "Zone3": activeCube = zone3Model; break;
            default: activeCube = zone4Model; break;
        }

        Debug.Log("Using cube: " + activeCube.name);
    }



    void TrySpawnCube()
    {
        //if cube null or is already activated
        if(activeCube == null || activeCube.activeSelf) return;

        Debug.Log(" Chance Success!");
        // move cube somewhere in the room, away from the player
        activeCube.transform.position = GetRandomSpawnPosition();

        Vector3 dirToCube = activeCube.transform.position - playerCamera.position;
        float angle = Vector3.Angle(playerCamera.forward, dirToCube);

        // spawn chance by profile
        float spawnChance = 0.3f;
        switch(playerProfile)
        {
            case "Paranoid": spawnChance = 0.48f; break; //0.2
            case "Cautious": spawnChance = 0.48f; break; //0.37
            case "Bold": spawnChance = 0.48f; break;
        }

        //player looking away
        if(angle > lookAngle && Random.value < spawnChance)
        {
            activeCube.SetActive(true);
            Vector3 lookPos = playerCamera.position;
            lookPos.y = activeCube.transform.position.y; // keep cube upright
            activeCube.transform.LookAt(lookPos);
            cautiousLookCount = 0;
            lookAwayTimer = 0f;
            Debug.Log("Cube spawned at " + activeCube.transform.position);
        }
    }

        //get random spawn pos
        Vector3 GetRandomSpawnPosition()
    {
        if(roomBounds == null)
        {
            Debug.LogWarning("Room bounds not assigned!");
            return playerCamera.position + playerCamera.forward * 2f; // fallback
        }

        Bounds b = roomBounds.bounds;
        Vector3 spawnPos;
        int attempts = 0;

        do
        {
            float x = Random.Range(b.min.x, b.max.x);
            float z = Random.Range(b.min.z, b.max.z);
            float y = (b.min.y) + 1f; // ground level
            spawnPos = new Vector3(x, y, z);
            attempts++;
        }
        //ensures not TOO close to player
        while (Vector3.Distance(spawnPos, playerCamera.position) < cubeSpawnDistanceMin && attempts < 50);

        return spawnPos;
    }

    void MoveCubeCloser()
    {
        if (activeCube == null || playerCamera == null) return;

        float moveDistance = 10f; // how far the cube moves closer

        // direction from cube to player
        Vector3 directionToPlayer = (playerCamera.position - activeCube.transform.position).normalized;

        // move the cube
        activeCube.transform.position += directionToPlayer * moveDistance;

        // make it face the player
        Vector3 lookPos = playerCamera.position;
        lookPos.y = activeCube.transform.position.y; // keep cube upright
        activeCube.transform.LookAt(lookPos);

        Debug.Log("Cube moved closer!");
    }

    void Update()
    {
        Debug.DrawRay(playerCamera.position, playerCamera.forward * 50f, Color.green);

        //timer
        if(timerRunning) timer += Time.deltaTime;

        //every 5 seconds, try spawning
        spawnTimer += Time.deltaTime;
        if(spawnTimer >= spawnInterval)
        {
            spawnTimer = 0f;
            TrySpawnCube();
            Debug.Log("trying spawn!");
        }

        //cube is null and not active
        if(activeCube == null || !activeCube.activeSelf) return;

        Vector3 dirToCube = activeCube.transform.position - playerCamera.position;
        float angle = Vector3.Angle(playerCamera.forward, dirToCube);

        //player looking 
        if(angle < lookAngle)
        {
            playerHasSeenCube = true;
            if(playerProfile == "Cautious") cautiousLookCount++; //player looked!
            else if(playerProfile == "Bold") lookAwayTimer = 0f; //reset timer
        }
        //player not looking
        else
        {
            //but has seen cube before
            if(playerProfile == "Paranoid" && playerHasSeenCube) 
            {
                activeCube.SetActive(false);
                playerHasSeenCube = false;
                Debug.Log("Paranoid cube disappears");
            }
            else if(playerProfile == "Cautious" && playerHasSeenCube)
            {
                if(cautiousLookCount >= 2)
                {
                    activeCube.SetActive(false);
                    cautiousLookCount = 0;
                    playerHasSeenCube = false;
                    Debug.Log("Cautious cube disappears");
                }
            }
            else if(playerProfile == "Bold" && playerHasSeenCube)
            {
                lookAwayTimer += Time.deltaTime; //count how long not looking
                if(lookAwayTimer >= boldMoveDelay && Random.value < 0.3f) //30% chance cube moves closer
                {
                    MoveCubeCloser();
                    lookAwayTimer = 0f;
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        if(playerCamera == null) return;

        Gizmos.color = Color.red;
        Vector3 left = Quaternion.Euler(0, -lookAngle, 0) * playerCamera.forward;
        Vector3 right = Quaternion.Euler(0, lookAngle, 0) * playerCamera.forward;

        Gizmos.DrawRay(playerCamera.position, playerCamera.forward * 5f);
        Gizmos.DrawRay(playerCamera.position, left * 5f);
        Gizmos.DrawRay(playerCamera.position, right * 5f);
    }
}