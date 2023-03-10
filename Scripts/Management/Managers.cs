using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gwent.Services;
using Gwent.Events;

namespace Game.Management
{
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

        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            
            Managers.Data = this.GetComponent<DataManager>();
            // Managers.Player = GetComponent<PlayerManager>();
            // Managers.Inventory = GetComponent<InventoryManager>();
            Managers.Mission = this.GetComponent<MissionManager>();

            this.startSequence = new List<IGameManager>();
            // this.startSequence.Add(Managers.Player);
            // this.startSequence.Add(Managers.Inventory);
            this.startSequence.Add(Managers.Mission);

		    // Must be booted last
            this.startSequence.Add(Managers.Data);

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
}
