using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Gwent.Services;

namespace Game.Management
{
    public class MissionManager : BaseGameManager
    {
        public int currentLevel { get; private set; }
        public int maxLevel { get; private set; }

        public override void Startup(NetworkService networkService)
        {
            this.status = ManagerStatus.Initializing;
		
            Debug.Log("Mission manager starting...");

            this.UpdateData(0, 3);
		
            base.Startup(networkService);
        }

        public void UpdateData(int currentLevel, int maxLevel)
        {
            this.currentLevel = currentLevel;
            this.maxLevel = maxLevel;
        }

        public void ReachObjective()
        {
            // could have logic to handle multiple objectives
            Messenger.Broadcast(GameEvent.LEVEL_COMPLETE);
        }

        public void GoToNext()
        {
            if (this.currentLevel < this.maxLevel) {
                this.currentLevel++;
                string name = $"Level{this.currentLevel}";
                Debug.Log($"Loading {name}");
                SceneManager.LoadScene(name);
            } else {
                Debug.Log("Last level");
                Messenger.Broadcast(GameEvent.GAME_COMPLETE);
            }
        }

        public void RestartCurrent()
        {
            string name = $"Level{this.currentLevel}";
            Debug.Log($"Loading {name}");
            SceneManager.LoadScene(name);
        }
    }
}
