// File: CrowdControlEffect.cs
// Location: Assets/Scripts/Models/Combat/
using System.Collections.Generic;
using System;

[Serializable]
public class CrowdControlEffect
{
    public string source;            // "Ashen Arrow"
    public string type;              // "Stun", "Root", etc.
    public float duration;           // Remaining time
    public float originalDuration;   // For display/debugging
    public bool isHardCC;            // Fully disables or not

    public CrowdControlEffect(string source, string type, float duration, bool isHardCC)
    {
        this.source = source;
        this.type = type;
        this.duration = duration;
        this.originalDuration = duration;
        this.isHardCC = isHardCC;
    }
}
