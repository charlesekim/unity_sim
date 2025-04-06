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

// --- Placeholder Systems ---
public static class PlayerDecisionSystem
{
    public static void Evaluate()
    {
        Debug.Log("Evaluating Player Decisions...");
        // TODO: Determine what each champion should do
    }
}

public static class ChampionManager
{
    public static void TickChampions()
    {
        Debug.Log("Ticking Champion States...");
        // TODO: Update champion abilities, cooldowns, HP regen, etc.
    }
}

public static class WaveManager
{
    public static void TickWaves()
    {
        Debug.Log("Advancing Minion Waves...");
        // TODO: Move minions along lanes, resolve collisions or combat
    }
}

public static class ObjectiveManager
{
    public static void TickObjectives()
    {
        Debug.Log("Handling Objectives...");
        // TODO: Control turrets, neutral monsters, vision etc.
    }
}

public static class EventLogger
{
    public static void FlushTickLogs()
    {
        // Flush logs for UI or debugging
    }
}
