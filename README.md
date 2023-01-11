# unity-framework
> Boilerplate code for Unity projects.
> 
> Uses MVC concept. For details read "[Unity in action](https://www.manning.com/books/unity-in-action-third-edition)" book, chapter 9.

## Workflow

1. Some UI action assigned on some controller.
2. UI action calls mutation methods in any manager(s).
3. Manager(s) broadcast event about mutation.
4. Any controller(s) can be subscribed to event and perform re-render or (and) call another method in manager(s).

Note. Manager's methods can be called from any place - trigger, controller or any other.

Example:

Let's say that we need to have game restarts counter.

1. We have "Restart game" button and "Restarts count: 0" counter label.
2. `OnClick` of this button assigned to `UIController::OnRestartLevelClicked()`.
3. `UIController::OnRestartLevelClicked()` perform `Managers.Mission.Restart()` call.
4. In `Managers.Mission.Restart()`:
   - validate is action authorized;
   - reload scene;
   - increase restarts counter (`Managers.Mission.restartsCount++`);
   - broadcast `GameEvent.LEVEL_RESTARTED` event.
5. `UIController` was subscribed to this `GameEvent.LEVEL_RESTARTED` event. Let's say handler is `UIController::onLevelRestarted()`.
6. Handler `UIController::onLevelRestarted()` can set `$"Restarts count: {Managers.Mission.restartsCount}"` to counter label.
7. Optionally handler can perform any other Manager's methods calls - update inventory, change player's health, ... 

## Credits

Used fragments from https://github.com/jhocking/uia-3e.

Thanks to Joseph Hocking for great book!
