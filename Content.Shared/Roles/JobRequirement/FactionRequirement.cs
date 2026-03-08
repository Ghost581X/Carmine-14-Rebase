using System.Diagnostics.CodeAnalysis;
using Content.Shared.Preferences;
using JetBrains.Annotations;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;
using Robust.Shared.Utility;

namespace Content.Shared.Roles;

/// <summary>
/// CARMINE: requires the character to be of a certain faction
/// </summary>
[UsedImplicitly]
[Serializable, NetSerializable]
public sealed partial class FactionRequirement : JobRequirement
{
    [DataField(required: true)]
    public string RequiredFaction;

    public override bool Check(IEntityManager entManager,
        IPrototypeManager protoManager,
        HumanoidCharacterProfile? profile,
        IReadOnlyDictionary<string, TimeSpan> playTimes,
        [NotNullWhen(false)] out FormattedMessage? reason)
    {
        reason = new FormattedMessage();

        if (profile is null) //the profile could be null if the player is a ghost. In this case we don't need to block the role selection for ghostrole
            return true;

        if (!Inverted)
        {
            reason = FormattedMessage.FromMarkupPermissive(Loc.GetString("role-timer-faction-must-equal",
                ("faction", RequiredFaction)));

            if (profile.Faction != RequiredFaction)
                return false;
        }
        else
        {
            reason = FormattedMessage.FromMarkupPermissive(Loc.GetString("role-timer-faction-must-not-equal",
                ("faction", RequiredFaction)));

            if (profile.Faction == RequiredFaction)
                return false;
        }

        return true;
    }
}
