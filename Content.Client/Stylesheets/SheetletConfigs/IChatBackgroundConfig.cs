using Robust.Shared.Utility;

namespace Content.Client.Stylesheets.SheetletConfigs;

public interface IChatBackgroundConfig : ISheetletConfig
{
    public ResPath ChatBackgroundPath { get; }
}
