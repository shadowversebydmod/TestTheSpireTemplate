using BaseLib.Patches.Content;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace CharMod.CharModCode.CardKeywords;

public partial class CharModKeywords
{
    [CustomEnum("ENHANCE")]
    [KeywordProperties(AutoKeywordPosition.None)]
    public static CardKeyword Enhance;
}
