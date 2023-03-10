using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using Gwent.Services;

namespace Game.Management
{
    public class DataManager : BaseGameManager
    {
        private string filename;

        public override void Startup(NetworkService networkService)
        {
            this.status = ManagerStatus.Initializing;
		
            Debug.Log("Data manager starting...");

            this.filename = Path.Combine(Application.persistentDataPath, "game.dat");
		
            base.Startup(networkService);
        }

        public void SaveGameState()
        {
            Dictionary<string, object> gamestate = new Dictionary<string, object>();
		
            gamestate.Add("curLevel", Managers.Mission.currentLevel);
            gamestate.Add("maxLevel", Managers.Mission.maxLevel);

            using FileStream stream = File.Create(this.filename);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, gamestate);
        }

        public void LoadGameState()
        {
            if (!File.Exists(this.filename)) {
                Debug.Log("No saved game");
                return;
            }

            Dictionary<string, object> gamestate;

            using (FileStream stream = File.Open(this.filename, FileMode.Open)) {
                BinaryFormatter formatter = new BinaryFormatter();
                gamestate = formatter.Deserialize(stream) as Dictionary<string, object>;
            }
		
            Managers.Mission.UpdateData((int)gamestate["curLevel"], (int)gamestate["maxLevel"]);
            Managers.Mission.RestartCurrent();
        }
    }
}