using System.Collections.Generic;

public class ChampionProfile
{
    // --- Tactical Profile ---
    public TacticalProfile tacticalProfile;          // Encapsulates positioning, initiative, awareness, etc.

    // --- Identifiers ---
    public string backendId;                         // Internal ID used for lookups/refs (e.g., "champ_riftpunch")

    // --- Runtime Combat State ---
    public bool isAlive = true;                      // Whether the champion is currently alive
    public float deathTimer = 0f;                    // Time remaining until respawn
    public bool isCrowdControlled = false;           // Flag for whether the champion is CC'd
    public float ccTimer = 0f;                       // Duration remaining on current CC
    public string ccType = null;                     // Type of CC applied (e.g., stun, root)
    public Dictionary<string, float> activeCooldowns = new(); // Cooldowns for each ability or action
    public string currentAction;                     // Description of current action (e.g., moving, casting)
    public float actionCooldown;                     // Delay between actions or decisions
    public bool isChanneling;                        // Whether the champion is mid-channel on an ability
    public float channelTimer;                       // Time left in the channel
    public float lastActionTime;                     // Timestamp of last action taken
    public bool isUnderTower;                        // Whether the champion is currently targeted by a turret
    public bool isRecalling;                         // Whether the champion is channeling a recall

    // --- Positioning and Location ---
    public string currentZone;                       // Current map zone the champion is in
    public string spawnZone;                         // Starting zone for spawning or laning
    public string assignedPosition;                  // Assigned role/lane this match (Top, Jungle, etc.)

    // --- Level and Identity ---
    public int currentLevel = 1;                     // Current level of the champion
    public int maxLevel = 18;                        // Level cap
    public string name;                              // Champion name (e.g., "Riftpunch")
    public string[] archetypes;                      // Champion class types (e.g., ["Juggernaut", "Diver"])
    public string preferredLane;                     // Lane this champion is best suited for
    public string playerId;                          // ID linking champion to a PlayerProfile
    public string teamId;                            // ID of the team this champion belongs to

    // --- Stats ---
    public float clutchFactor;                     // Performance under pressure (e.g., low HP, high-stakes fight)
    public float vulnerabilityRating;                // Likelihood of being caught or punished in poor position
    public float positioningRating;                  // Abstract representation of average positioning quality
    public float awarenessRating;                    // Optional: perception of threats/objectives
    public float initiativeRating;                   // How likely they are to initiate or follow up

    public CombatProfile baseStats;                  // Static sim-based stats (e.g., tradePower, CC, scaling)
    public CombatProfile currentStats;               // Modified sim stats during a match
    public BaseStats baseAttributes;                 // Core stats like HP, armor, move speed, etc.
    public BaseStats currentAttributes;              // Runtime stats after item/modifier effects
    public List<Modifier> activeModifiers;           // Temporary or permanent stat changes (buffs, debuffs)

    // --- Abilities ---
    public List<AbilityProfile> abilities;           // Ability list with active/passive descriptions
    //public AbilityComboProfile comboLogic;           // Logic for how abilities chain or synergize

    // --- Tags, Metadata, and Tracking ---
    public MatchStats matchStats;                    // Per-match performance tracker
    public AIBehaviorProfile aiProfile;              // AI control preferences and logic tuning
    //public Sprite portrait;                          // Portrait art asset for UI
    public string[] tags;                            // Behavior tags (e.g., "engage", "poke", "peel")
    public string[] synergyWith;                     // Recommended archetypes for team synergy
    public string[] counteredBy;                     // Archetypes or tactics that counter this champion

    // --- Targeting & Behavior ---
    public ChampionProfile currentTarget;            // Who this champion is currently targeting
    public bool isTargetable = true;                 // Whether the champion can currently be targeted
} 

public class CombatProfile
{
    public float tradePower;
    public float flankPower;
    public float burstWindow;
    public float aoeDamage;
    public float ccPower;
    public float zoningControl;
    public float teamfightImpact;
    public float macroImpact;
    public float roamPower;
    public float peelPower;
    public float scaling;
}

public class BaseStats
{
    public float maxHP;
    public float baseHPRegen;
    public float maxMana;
    public float baseManaRegen;
    public float armor;
    public float magicResist;
    public float moveSpeed;
    public float attackRange;
    public float attackSpeed;
    public float baseAD;
    public float abilityPower;
}

public class MatchStats
{
    public int kills;             // Total kills secured
    public int deaths;            // Times died
    public int assists;           // Kill assists
    public float damageDealt;     // Total damage dealt
    public float damageTaken;     // Total damage received
    public float healingDone;     // Total healing provided
    public float shieldingDone;   // Total shielding provided
    public float ccTimeApplied;   // Total crowd control time applied
    public float goldEarned;      // Total gold generated during the match
    public int wardsPlaced;       // Number of wards placed
    public int wardsDestroyed;    // Number of enemy wards cleared
}

public class TacticalProfile
{
    public float vulnerabilityRating;   // Likelihood of being caught or punished in poor position
    public float positioningRating;     // Map awareness and spacing during fights
    public float awarenessRating;       // Threat/objective/map awareness
    public float initiativeRating;      // Willingness to engage or lead teamfight
    public float clutchFactor;          // Performance under pressure (low HP or critical moment)
    public float reactionSpeed;         // Time before taking an action (e.g. dodging, flashing)
    public float backupDiscipline;      // Willingness to peel or collapse with team
    public float tiltResistance;        // How performance changes after repeated deaths or setbacks
    public float bushCheckFrequency;    // Likelihood to scout or ward bushes before entering
    public float safeZoneBias;          // Preference for staying near turret/safe zones
}

public class AIBehaviorProfile
{
    public float aggressionRating;    // Tendency to engage in risky trades or fights (0.0–1.0)
    public float roamFrequency;       // Likelihood of roaming to other zones
    public float comboAggressiveness; // How likely this AI is to use a full combo
    public float retreatThreshold;    // HP percentage to trigger a disengage
    public float mapAwareness;        // General map tracking ability and objective evaluation
    public float decisionDelay;       // Time (in seconds) between decisions (reaction time)

    // --- Tactical Tendencies ---
    public float lanePressureBias;     // Preference for early pushing or passive laning
    public float objectiveFocus;       // How likely to prioritize neutral objectives
    public float turretDiveRisk;       // Likelihood of diving under enemy towers
    public float followUpDiscipline;   // How reliably this AI follows teammate engages
    public float soloPlayTendency;     // How often this AI looks for 1v1 or isolation plays
    public float fallbackDiscipline;   // How disciplined they are when retreating from fights
}

public class Modifier
{
    public string source; // e.g., "Morale Boost", "Patch 1.1"
    public Dictionary<string, float> statChanges; // e.g., {"burstWindow": +0.1f}
    public float duration; // -1 for permanent
}