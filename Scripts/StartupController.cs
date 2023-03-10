using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Gwent.Management;
using Gwent.Events;

namespace Game
{
    public class StartupController : MonoBehaviour
    {
        [SerializeField] private Slider progressBar;

        private void OnEnable()
        {
            Messenger<int, int>.AddListener(StartupEvent.MANAGERS_PROGRESS, this.OnManagersProgress);
            Messenger.AddListener(StartupEvent.MANAGERS_STARTED, OnManagersStarted);
        }

        private void OnDisable()
        {
            Messenger<int, int>.RemoveListener(StartupEvent.MANAGERS_PROGRESS, this.OnManagersProgress);
            Messenger.RemoveListener(StartupEvent.MANAGERS_STARTED, OnManagersStarted);
        }

        private void OnManagersProgress(int numReady, int numModules)
        {
            float progress = (float)numReady / numModules;
            this.progressBar.value = progress;
        }

        private static void OnManagersStarted()
        {
            Managers.Mission.GoToNext();
        }
    }	
}
