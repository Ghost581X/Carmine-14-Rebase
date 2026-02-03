using System.Numerics;
using Content.Client.Stylesheets.Palette;
using Content.Client.Stylesheets.SheetletConfigs;
using Content.Client.Stylesheets.Stylesheets;
using Robust.Client.Graphics;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.Controls;
using static Content.Client.Stylesheets.StylesheetHelpers;

namespace Content.Client.Stylesheets.Sheetlets;

//[CommonSheetlet]
public static class StylePanelHelpers
{
    // TODO: Figure out a nicer way to store/represent these hardcoded margins. This is icky.
    public static StyleBoxTexture BasePanel<T>(T sheet) where T : PalettedStylesheet, IPanelConfig
    {
        var basePanel = new StyleBoxTexture
        {
            Texture = sheet.GetTextureOr(sheet.PanelBorderPath, NanotrasenStylesheet.TextureRoot),
            Mode = StyleBoxTexture.StretchMode.Tile
        };
        //basePanel.SetPatchMargin(StyleBox.Margin.All, 10); // THIS DECIDES WHAT PART IS THE "BORDER", AKA THE SPACE LEFT INSIDE IS STRETCHED ACROSS THE MIDDLE AND SHOULD BE PURE WHITE
        basePanel.SetPatchMargin(StyleBox.Margin.All, 48); //test high fleet ui
        basePanel.SetPadding(StyleBox.Margin.All, 1);
        basePanel.SetContentMarginOverride(StyleBox.Margin.Vertical, 5); //these decide how far the "inner" content is offset from the border.
        basePanel.SetContentMarginOverride(StyleBox.Margin.Horizontal, 10); //you have to marco-polo this because its not offset like youd expect (equally top/bottom)
        return basePanel;
    }

    public static StyleBoxTexture OpenLeftPanel<T>(T sheet) where T : PalettedStylesheet, IPanelConfig
    {
        var openLeftPanel = new StyleBoxTexture(BasePanel(sheet))
        {
            Texture = new AtlasTexture(sheet.GetTextureOr(sheet.OpenLeftPanelBorderPath, NanotrasenStylesheet.TextureRoot),
                UIBox2.FromDimensions(new Vector2(10, 0), new Vector2(14, 24))),
        };
        openLeftPanel.SetPatchMargin(StyleBox.Margin.Left, 0);
        openLeftPanel.SetContentMarginOverride(StyleBox.Margin.Left, 8);
        // openLeftBox.SetPadding(StyleBox.Margin.Left, 1);
        return openLeftPanel;
    }

    public static StyleBoxTexture OpenRightPanel<T>(T sheet) where T : PalettedStylesheet, IPanelConfig
    {
        var openRightPanel = new StyleBoxTexture(BasePanel(sheet))
        {
            Texture = new AtlasTexture(sheet.GetTextureOr(sheet.OpenRightPanelBorderPath, NanotrasenStylesheet.TextureRoot),
                UIBox2.FromDimensions(new Vector2(0, 0), new Vector2(14, 24))),
        };
        openRightPanel.SetPatchMargin(StyleBox.Margin.Right, 0);
        openRightPanel.SetContentMarginOverride(StyleBox.Margin.Right, 8);
        openRightPanel.SetPadding(StyleBox.Margin.Right, 1);
        return openRightPanel;
    }
    public static StyleBoxTexture GargoylePanelLeft<T>(T sheet) where T : PalettedStylesheet, IPanelConfig
    {
        var gargoylePanelLeft = new StyleBoxTexture
        {
            Texture = sheet.GetTextureOr(sheet.GargoyleLeftPanelPath, NanotrasenStylesheet.TextureRoot),
            Mode = StyleBoxTexture.StretchMode.Tile
        };
        gargoylePanelLeft.SetPatchMargin(StyleBox.Margin.All, 1); //test high fleet ui
        gargoylePanelLeft.SetPadding(StyleBox.Margin.All, 1);
        gargoylePanelLeft.SetContentMarginOverride(StyleBox.Margin.Vertical, 1); //these decide how far the "inner" content is offset from the border.
        gargoylePanelLeft.SetContentMarginOverride(StyleBox.Margin.Horizontal, 1); //you have to marco-polo this because its not offset like youd expect (equally top/bottom)
        return gargoylePanelLeft;
    }

    public static StyleBoxTexture GargoylePanelRight<T>(T sheet) where T : PalettedStylesheet, IPanelConfig
    {
        var gargoylePanelRight = new StyleBoxTexture
        {
            Texture = sheet.GetTextureOr(sheet.GargoyleRightPanelPath, NanotrasenStylesheet.TextureRoot),
            Mode = StyleBoxTexture.StretchMode.Tile
        };
        gargoylePanelRight.SetPatchMargin(StyleBox.Margin.All, 48); //test high fleet ui
        gargoylePanelRight.SetPadding(StyleBox.Margin.All, 1);
        gargoylePanelRight.SetContentMarginOverride(StyleBox.Margin.Vertical, 5); //these decide how far the "inner" content is offset from the border.
        gargoylePanelRight.SetContentMarginOverride(StyleBox.Margin.Horizontal, 10); //you have to marco-polo this because its not offset like youd expect (equally top/bottom)
        return gargoylePanelRight;
    }
}
