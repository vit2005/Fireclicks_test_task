public class InitialGameState : IGameState
{
    private readonly AppController _app;

    public InitialGameState(AppController app) => _app = app;

    public void Enter()
    {
        _app.InitialScreenPanel?.SetActive(true);
        _app.ResultScreenPanel?.SetActive(false);
    }

    public void Exit()
    {
        _app.InitialScreenPanel?.SetActive(false);
    }
}
