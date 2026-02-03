using Robust.Shared.Utility;

namespace Content.Client.Stylesheets.SheetletConfigs;

public interface IPanelConfig : ISheetletConfig
{
    public ResPath GeometricPanelBorderPath { get; }
    public ResPath BlackPanelDarkThinBorderPath { get; }
    public ResPath PanelBorderPath { get; }
    public ResPath OpenLeftPanelBorderPath { get; }
    public ResPath OpenRightPanelBorderPath { get; }
    public ResPath PanelInnerTexturePath { get; }
    public ResPath GargoyleLeftPanelPath { get; }
    public ResPath GargoyleRightPanelPath { get; }
}
