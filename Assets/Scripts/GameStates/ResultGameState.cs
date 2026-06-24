using TMPro;
using UnityEngine;

public class ResultGameState : IGameState
{
    private readonly AppController _app;
    private float _survivalTime;

    public ResultGameState(AppController app) => _app = app;

    public void SetSurvivalTime(float time) => _survivalTime = time;

    public void Enter()
    {
        _app.ResultScreenPanel?.SetActive(true);

        var label = _app.ResultScreenPanel?.GetComponentInChildren<TextMeshProUGUI>();
        if (label != null)
            label.text = $"Game Over\nSurvived: {FormatTime(_survivalTime)}";
    }

    public void Exit()
    {
        _app.ResultScreenPanel?.SetActive(false);
    }

    private static string FormatTime(float seconds)
    {
        int m = (int)(seconds / 60);
        int s = (int)(seconds % 60);
        return m > 0 ? $"{m}m {s}s" : $"{s}s";
    }
}
