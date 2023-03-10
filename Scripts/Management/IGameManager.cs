using Gwent.Services;

namespace Game.Management
{
    public interface IGameManager
    {
        ManagerStatus status { get; }
        void Startup(NetworkService networkService);
    }
}
