using UnityEngine;

public class BaseGameManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; protected set; }
    
    protected NetworkService networkService;

    public BaseGameManager()
    {
        this.status = ManagerStatus.Shutdown;
    }

    public virtual void Startup(NetworkService networkService)
    {
        this.networkService = networkService;

        // Any long-running startup tasks go here (in children classes),
        // and set status to 'Initializing' until those tasks are complete.
        
        this.status = ManagerStatus.Started;
    }
}
