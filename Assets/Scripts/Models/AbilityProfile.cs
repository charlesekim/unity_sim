using System.Collections.Generic;

public class AbilityProfile
{
    public string name;                     // Name of the ability (e.g. "Crushing Blow")
    public string type;                     // Type: "Active", "Passive", "Ultimate"
    public float cooldown;                  // Cooldown duration in seconds
    public bool isDash;                    // Whether the ability includes a movement component
    public bool isBlink;                    // Whether the ability includes a movement component
    public bool isInterruptible;           // Can it be stopped mid-cast?
    public float executionWeight;          // Likelihood this ability is used in a combo/situation
    public string synergyTag;              // For emergent interactions (e.g. "AOECC", "Zone", "Knockup")
    public float priorityWeight;           // Used to determine ability priority in a fight
    public string description;             // UI or developer-facing description
    public int rank = 1;                    // Current rank of the ability
    public int maxRank = 5;                 // Maximum allowed rank (typically 5 or 3 for ults)
    public List<float> damageByRank;       // List of damage values per rank (index = rank - 1)
    public List<float> cooldownByRank;     // List of cooldowns per rank (index = rank - 1)
    //public AbilityIntent intent;           // AI usage logic and contextual priorities
    //public AbilityRange rangeData;         // Data for cast range, projectile speed, etc.
    public AbilityScaling scalingData;      // Data for scaling (AD, AP, target-specific)
    public float castRange;                // Maximum range of ability use
    public float windupDelay;              // Time before the ability takes effect after cast starts
    public float projectileSpeed;          // Speed of projectiles (0 = instant)
    public string activationCondition;     // Trigger condition (e.g., "onAuto", "onDamageTaken")
    public float percentTargetMaxHpDamage; // Percent of target's max HP as bonus damage
    public float percentTargetMissingHpScaling; // Bonus scaling based on target's missing HP
    public bool isToggle;                  // Whether this ability is toggled on/off (e.g., Karthus E, Jinx Q)
    public float toggleTickInterval;       // How frequently the effect applies while toggled on (in seconds)
    public float toggleManaCostPerSecond;  // Mana drain while toggled on (if any)
    public float manaCost;                  // Mana required to cast the ability
    public float energyCost;                  // Energy required to cast the ability
    //public AbilityIntent intent;                // AI usage logic and contextual priorities
    public float castIntentWeight;       // AI likelihood to cast this ability off cooldown (0–1)
    public bool isChanneled;                // Whether this ability has a channeling time before completion
    public float channelDuration;           // Duration of the channel if the ability is channeled
    public bool isUltimate;                // Whether this ability is an ultimate or not
    public string targetingType;            // e.g., "Targeted", "Skillshot", "Untargeted", "Area", "Passive"
    //public CrowdControlEffect ccEffect;            // Crowd control effect applied by this ability, if any

    public List<AbilityEffect> additionalEffects; // Optional: list of non-damage effects (e.g., healing, shields, stealth, stat changes)
}

public class AbilityExecution
{
    public float castTime;                  // Time it takes to complete the cast
    public float castRange;                // Maximum range of ability use
    public float windupDelay;              // Time before the ability takes effect after cast starts
    public float projectileSpeed;          // Speed of projectiles (0 = instant)
    public float effectRadius;              // Range or radius for AoE abilities
}

public class AbilityScaling
{
    public float adScaling;                 // Scaling with attack damage
    public float apScaling;                 // Scaling with ability power
    public float percentTargetMaxHpDamage; // Percent of target's max HP as bonus damage
    public float armorScaling;              // Additional damage based on target's armor
    public float magicResistScaling;        // Additional damage based on target's MR
    public float percentTargetMissingHpScaling; // Bonus scaling based on target's missing HP
}
public class ccEffect
{
    public string type;                 // e.g., "Stun", "Root", "Silence"
    public float duration;             // Duration in seconds
    public bool isHardCC;              // True if hard CC (stun, knockup), false for slows, etc.
    public bool breaksChannels;        // If it cancels channels
    public bool displaces;             // If it moves or knocks back the target
    public string appliesTo;           // "Target", "Area", etc.
    public string condition;           // Optional: only applies in some conditions
}

public class StatEffect
{
    public string type;               // "Heal", "Shield", "ManaRegen", "HPRegen", etc.
    public float value;              // Magnitude of the effect
    public float duration;           // Duration in seconds (0 = instant)
    public string targetType;        // "Self", "Ally", "Enemy", "AlliesInRadius", etc.
    public bool appliesToMultiple;   // Whether the effect hits multiple targets
    public string condition;         // Optional condition, e.g., "target below 50% HP"
    public string description;       // UI-facing description
}

public class AbilityEffect
{
    public string type;                 // e.g., "Heal", "Shield", "Execute", "VisionReveal"
    public float value;                // Strength or magnitude of the effect
    public float duration;             // Duration of the effect, if applicable
    public string targetType;          // e.g., "Self", "Ally", "Enemy", "Area"
    public bool appliesToMultiple;     // True if AoE or chainable effect
    public string effectCondition;     // Optional: only applies under certain conditions (e.g., "if below 30% HP")
    public string effectDescription;   // UI-friendly description of the effect
}