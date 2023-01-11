using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DataManager))]
// [RequireComponent(typeof(PlayerManager))]
// [RequireComponent(typeof(InventoryManager))]
[RequireComponent(typeof(MissionManager))]
public class Managers : MonoBehaviour
{
    public static DataManager Data { get; private set; }
    // public static PlayerManager Player {get; private set;}
    // public static InventoryManager Inventory {get; private set;}
    public static MissionManager Mission { get; private set; }

    private List<IGameManager> startSequence;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        
        Data = GetComponent<DataManager>();
        // Player = GetComponent<PlayerManager>();
        // Inventory = GetComponent<InventoryManager>();
        Mission = GetComponent<MissionManager>();

        this.startSequence = new List<IGameManager>();
        // this.startSequence.Add(Player);
        // this.startSequence.Add(Inventory);
        this.startSequence.Add(Mission);

		// Must be booted last
        this.startSequence.Add(Data);

        this.StartCoroutine(this.StartupManagers());
    }

    private IEnumerator StartupManagers()
    {
        NetworkService networkService = new NetworkService();

        foreach (IGameManager manager in this.startSequence) {
            manager.Startup(networkService);
        }

        yield return null;

        int numModules = this.startSequence.Count;
        int numReady = 0;

        while (numReady < numModules) {
            int lastReady = numReady;
            numReady = 0;

            foreach (IGameManager manager in this.startSequence) {
                if (manager.status == ManagerStatus.Started) {
                    numReady++;
                }
            }

            if (numReady > lastReady) {
                Debug.Log($"Progress: {numReady}/{numModules}");
                Messenger<int, int>.Broadcast(StartupEvent.MANAGERS_PROGRESS, numReady, numModules);
            }

            yield return null;
        }

        Debug.Log("All managers started up");
        Messenger.Broadcast(StartupEvent.MANAGERS_STARTED);
    }
}
