using UnityEngine;
using UnityEngine.SceneManagement;

public class AppController : MonoBehaviour
{
    [SerializeField] private Tower tower;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private SpellCaster spellCaster;
    [SerializeField] private KillTracker killTracker;
    [SerializeField] private UpgradeScreen upgradeScreen;
    [SerializeField] private UpgradeConfig[] upgradePool;
    [SerializeField] private GameObject initialScreenPanel;
    [SerializeField] private GameObject resultScreenPanel;

    private IGameState _currentState;
    private InitialGameState _initialState;
    private GameplayGameState _gameplayState;
    private ResultGameState _resultState;
    private GameContext _gameContext;

    public Tower Tower => tower;
    public EnemySpawner EnemySpawner => enemySpawner;
    public SpellCaster SpellCaster => spellCaster;
    public KillTracker KillTracker => killTracker;
    public UpgradeScreen UpgradeScreen => upgradeScreen;
    public GameObject InitialScreenPanel => initialScreenPanel;
    public GameObject ResultScreenPanel => resultScreenPanel;
    public GameContext GameContext => _gameContext;

    private void Start()
    {
        _gameContext = new GameContext(spellCaster, tower, enemySpawner);

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

    public UpgradeConfig[] GetRandomUpgrades(int count)
    {
        var pool = new System.Collections.Generic.List<UpgradeConfig>(upgradePool);
        var result = new UpgradeConfig[Mathf.Min(count, pool.Count)];

        for (int i = 0; i < result.Length; i++)
        {
            int index = Random.Range(0, pool.Count);
            result[i] = pool[index];
            pool.RemoveAt(index);
        }

        return result;
    }
}
