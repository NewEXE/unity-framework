using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private TMP_Text healthLabel;
    [SerializeField] private TMP_Text levelEnding;
    [SerializeField] private InventoryPopup popup;

    private void OnEnable()
    {
        Messenger.AddListener(GameEvent.HEALTH_UPDATED, this.OnHealthUpdated);
        Messenger.AddListener(GameEvent.LEVEL_FAILED, this.OnLevelFailed);
        Messenger.AddListener(GameEvent.LEVEL_COMPLETE, this.OnLevelComplete);
        Messenger.AddListener(GameEvent.GAME_COMPLETE, this.OnGameComplete);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(GameEvent.HEALTH_UPDATED, this.OnHealthUpdated);
        Messenger.RemoveListener(GameEvent.LEVEL_FAILED, this.OnLevelFailed);
        Messenger.RemoveListener(GameEvent.LEVEL_COMPLETE, this.OnLevelComplete);
        Messenger.RemoveListener(GameEvent.GAME_COMPLETE, this.OnGameComplete);
    }

    private void Start()
    {
        this.OnHealthUpdated();

        this.levelEnding.gameObject.SetActive(false);
        this.popup.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.I)) {
            return;
        }

        bool isShowing = this.popup.gameObject.activeSelf;
        this.popup.gameObject.SetActive(!isShowing);
        this.popup.Refresh();
    }

    private void OnHealthUpdated()
    {
        // string message = $"Health: {Managers.Player.health}/{Managers.Player.maxHealth}";
        // this.healthLabel.text = message;
    }

    private void OnLevelFailed()
    {
        this.StartCoroutine(this.FailLevel());
    }

    private IEnumerator FailLevel()
    {
        this.levelEnding.gameObject.SetActive(true);
        this.levelEnding.text = "Level Failed";

        yield return new WaitForSeconds(2);

        // Managers.Player.Respawn();
        Managers.Mission.RestartCurrent();
    }

    private void OnLevelComplete()
    {
        this.StartCoroutine(this.CompleteLevel());
    }

    private IEnumerator CompleteLevel()
    {
        this.levelEnding.gameObject.SetActive(true);
        this.levelEnding.text = "Level Complete!";

        yield return new WaitForSeconds(2);

        Managers.Mission.GoToNext();
    }

    private void OnGameComplete()
    {
        this.levelEnding.gameObject.SetActive(true);
        this.levelEnding.text = "You Finished the Game!";
    }

    public void SaveGame()
    {
        Managers.Data.SaveGameState();
    }

    public void LoadGame()
    {
        Managers.Data.LoadGameState();
    }
}
