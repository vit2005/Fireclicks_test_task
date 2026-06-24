using UnityEngine;
using UnityEngine.SceneManagement;

public class AppController : MonoBehaviour
{
    [SerializeField] private Tower tower;
    [SerializeField] private GameObject initialScreenPanel;
    [SerializeField] private GameObject resultScreenPanel;

    private IGameState _currentState;
    private InitialGameState _initialState;
    private GameplayGameState _gameplayState;
    private ResultGameState _resultState;

    public Tower Tower => tower;
    public GameObject InitialScreenPanel => initialScreenPanel;
    public GameObject ResultScreenPanel => resultScreenPanel;

    private void Start()
    {
        _initialState = new InitialGameState(this);
        _gameplayState = new GameplayGameState(this);
        _resultState = new ResultGameState(this);

        ChangeState(_initialState);
    }

    public void ChangeState(IGameState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter();
    }

    public void StartGame() => ChangeState(_gameplayState);

    public void EndGame(float survivalTime)
    {
        _resultState.SetSurvivalTime(survivalTime);
        ChangeState(_resultState);
    }

    public void RestartGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}
