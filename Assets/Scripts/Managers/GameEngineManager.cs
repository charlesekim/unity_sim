// --- GameEngineManager.cs ---
using UnityEngine;

public class GameEngineManager : MonoBehaviour
{
    public float tickRate = 0.25f; // time between each tick in seconds
    private float tickTimer = 0f;
    private bool isRunning = true;

    private void Update()
    {
        if (!isRunning) return;

        tickTimer += Time.deltaTime;
        if (tickTimer >= tickRate)
        {
            tickTimer = 0f;
            Tick();
        }
    }

    private void Tick()
    {
        Debug.Log("--- New Tick ---");

        PlayerDecisionSystem.Evaluate();
        ChampionManager.TickChampions();
        WaveManager.TickWaves();
        ObjectiveManager.TickObjectives(); // placeholder for future systems
        EventLogger.FlushTickLogs();
    }

    public void PauseSimulation() => isRunning = false;
    public void ResumeSimulation() => isRunning = true;
}








