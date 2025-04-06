using UnityEngine;
using System.Collections.Generic;

// --- Placeholder Systems ---
public static class ChampionManager
{
    // Main entry per tick: handles all champion-related time-based logic
    public static void TickChampions(List<ChampionProfile> champions, float tickRate)
    {
        foreach (var champ in champions)
        {
            TickRespawn(champ, tickRate);       // Handle respawn logic
            TickCrowdControl(champ, tickRate);  // Update CC effects
            TickChanneling(champ, tickRate);    // Resolve channeling
            TickActionCooldown(champ, tickRate);// Action throttle
            TickAbilityCooldowns(champ, tickRate);// Abilities on cooldown
            ApplyRegeneration(champ, tickRate); // Health/mana regen
            TickModifiers(champ, tickRate);     // Buffs/debuffs
            TryCastAbility(champ);              // Attempt spell cast
            LogChampionSummary(champ);          // Debug summary
        }
    }

    private static void TickRespawn(ChampionProfile champ, float tickRate)
    {
        if (!champ.isAlive)
        {
            champ.deathTimer -= tickRate;
            if (champ.deathTimer <= 0f)
            {
                champ.isAlive = true;
                champ.currentAttributes.maxHP = champ.baseAttributes.maxHP;
                champ.currentZone = champ.spawnZone;
                Debug.Log($"{champ.name} has respawned at {champ.spawnZone}.");
            }
        }
    }

    private static void TickCrowdControl(ChampionProfile champ, float tickRate)
    {
        if (champ.activeCCs == null || champ.activeCCs.Count == 0)
        {
            champ.isCrowdControlled = false;
            champ.ccType = null;
            return;
        }

        for (int i = champ.activeCCs.Count - 1; i >= 0; i--)
        {
            var cc = champ.activeCCs[i];
            cc.duration -= tickRate;
            if (cc.duration <= 0f)
            {
                champ.activeCCs.RemoveAt(i);
                Debug.Log($"{champ.name}'s {cc.type} from {cc.source} has ended.");
            }
        }

        champ.isCrowdControlled = champ.activeCCs.Exists(c => c.isHardCC);
        champ.ccType = champ.isCrowdControlled ? champ.activeCCs[0].type : null;
    }

    private static void TickChanneling(ChampionProfile champ, float tickRate)
    {
        if (champ.isChanneling)
        {
            champ.channelTimer -= tickRate;
            if (champ.channelTimer <= 0f)
            {
                champ.isChanneling = false;
                Debug.Log($"{champ.name} has finished channeling.");
            }
        }
    }

    private static void TickActionCooldown(ChampionProfile champ, float tickRate)
    {
        if (champ.actionCooldown > 0f)
        {
            champ.actionCooldown -= tickRate;
        }
    }

    private static void TickAbilityCooldowns(ChampionProfile champ, float tickRate)
    {
        var keys = new List<string>(champ.activeCooldowns.Keys);
        foreach (var key in keys)
        {
            champ.activeCooldowns[key] -= tickRate;
            if (champ.activeCooldowns[key] < 0f)
                champ.activeCooldowns[key] = 0f;
        }
    }

    private static void ApplyRegeneration(ChampionProfile champ, float tickRate)
    {
        champ.currentAttributes.maxHP += champ.baseAttributes.baseHPRegen * tickRate;
        champ.currentAttributes.maxHP = Mathf.Min(champ.currentAttributes.maxHP, champ.baseAttributes.maxHP);

        champ.currentAttributes.maxMana += champ.baseAttributes.baseManaRegen * tickRate;
        champ.currentAttributes.maxMana = Mathf.Min(champ.currentAttributes.maxMana, champ.baseAttributes.maxMana);
    }

    private static void TickModifiers(ChampionProfile champ, float tickRate)
    {
        if (champ.activeModifiers != null)
        {
            for (int i = champ.activeModifiers.Count - 1; i >= 0; i--)
            {
                var mod = champ.activeModifiers[i];
                if (mod.duration > 0f)
                {
                    mod.duration -= tickRate;
                    if (mod.duration <= 0f)
                    {
                        champ.activeModifiers.RemoveAt(i);
                        Debug.Log($"{champ.name} had a modifier expire: {mod.source}");
                    }
                }
            }
        }
    }

    private static void TryCastAbility(ChampionProfile champ)
    {
        if (!champ.isAlive || champ.isCrowdControlled || champ.isChanneling || champ.actionCooldown > 0f)
            return;

        foreach (var ability in champ.abilities)
        {
            if (champ.activeCooldowns.TryGetValue(ability.name, out float cooldown) && cooldown <= 0f)
            {
                if (Random.value <= ability.castIntentWeight && champ.currentTarget != null && champ.currentTarget.isAlive)
                {
                    float baseDamage = ability.damageByRank[ability.rank - 1];
                    float scaling = ability.scalingData.adScaling * champ.currentAttributes.baseAD +
                                    ability.scalingData.apScaling * champ.currentAttributes.abilityPower;
                    float totalDamage = baseDamage + scaling;

                    string type = ability.type;
                    float resistance = type == "Magic" ? champ.currentTarget.currentAttributes.magicResist : champ.currentTarget.currentAttributes.armor;
                    float reducedDamage = totalDamage * (100f / (100f + resistance));

                    champ.currentTarget.currentAttributes.maxHP -= reducedDamage;
                    champ.matchStats.damageDealt += reducedDamage;
                    champ.currentTarget.matchStats.damageTaken += reducedDamage;

                    champ.activeCooldowns[ability.name] = ability.cooldown;
                    champ.actionCooldown = 1f; // simple 1s delay between casts for now

                    Debug.Log($"{champ.name} casts {ability.name} on {champ.currentTarget.name} for {reducedDamage:F1} damage.");
                    break;
                }
            }
        }
    }

    private static void LogChampionSummary(ChampionProfile champ)
    {
        Debug.Log($"Champion: {champ.name} | HP: {champ.currentAttributes.maxHP:F0} | CC: {champ.isCrowdControlled} | Zone: {champ.currentZone}");
    }
}