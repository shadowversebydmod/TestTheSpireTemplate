using BaseLib.Abstracts;
using CharMod.CharModCode.Cards;
using Godot;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Relics;

namespace CharMod.CharModCode.Character;

public class CharMod : PlaceholderCharacterModel
{
    public const string CharacterId = "CharMod";

    public static readonly Color Color = new("ffffff");

    public override Color NameColor => Color;
    public override CharacterGender Gender => CharacterGender.Neutral;
    public override int StartingHp => 70;

    public override IEnumerable<CardModel> StartingDeck => [
        ModelDb.Card<SampleEnhanceStrike>(),
        ModelDb.Card<StrikeIronclad>(),
        ModelDb.Card<StrikeIronclad>(),
        ModelDb.Card<StrikeIronclad>(),
        ModelDb.Card<StrikeIronclad>(),
        ModelDb.Card<DefendIronclad>(),
        ModelDb.Card<DefendIronclad>(),
        ModelDb.Card<DefendIronclad>(),
        ModelDb.Card<DefendIronclad>(),
        ModelDb.Card<DefendIronclad>()
    ];

    public override IReadOnlyList<RelicModel> StartingRelics =>
    [
        ModelDb.Relic<BurningBlood>()
    ];

    public override CardPoolModel CardPool => ModelDb.CardPool<CharModCardPool>();
    public override RelicPoolModel RelicPool => ModelDb.RelicPool<CharModRelicPool>();
    public override PotionPoolModel PotionPool => ModelDb.PotionPool<CharModPotionPool>();

    /*  PlaceholderCharacterModel will use basegame placeholder UI assets until a real mod package provides custom
        character art. Keeping these defaults avoids headless test failures from unloaded template PNG resources. */
}
