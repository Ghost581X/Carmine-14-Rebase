using System.Numerics;
using Content.Client.Stylesheets.Palette;
using Content.Client.Stylesheets.SheetletConfigs;
using Content.Client.Stylesheets.Stylesheets;
using Robust.Client.Graphics;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.Controls;
using static Content.Client.Stylesheets.StylesheetHelpers;

namespace Content.Client.Stylesheets.Sheetlets;

// [CommonSheetlet]
public static class StyleBoxHelpers
{
    // TODO: Figure out a nicer way to store/represent these hardcoded margins. This is icky.
    public static StyleBoxTexture BaseStyleBox<T>(T sheet) where T : PalettedStylesheet, IButtonConfig
    {
        var baseBox = new StyleBoxTexture
        {
            Texture = sheet.GetTextureOr(sheet.BaseButtonPath, NanotrasenStylesheet.TextureRoot),
            Mode = StyleBoxTexture.StretchMode.Tile
        };
        baseBox.SetPatchMargin(StyleBox.Margin.All, 16);
        baseBox.SetPadding(StyleBox.Margin.All, 1);
        baseBox.SetContentMarginOverride(StyleBox.Margin.Vertical, 10);
        baseBox.SetContentMarginOverride(StyleBox.Margin.Horizontal, 10);
        return baseBox;
    }

    public static StyleBoxTexture OpenLeftStyleBox<T>(T sheet) where T : PalettedStylesheet, IButtonConfig
    {
        var openLeftBox = new StyleBoxTexture(BaseStyleBox(sheet))
        {
            Texture = new AtlasTexture(sheet.GetTextureOr(sheet.OpenLeftButtonPath, NanotrasenStylesheet.TextureRoot),
                UIBox2.FromDimensions(new Vector2(10, 0), new Vector2(14, 24))),
        };
        openLeftBox.SetPatchMargin(StyleBox.Margin.Left, 0);
        openLeftBox.SetContentMarginOverride(StyleBox.Margin.Left, 8);
        // openLeftBox.SetPadding(StyleBox.Margin.Left, 1);
        return openLeftBox;
    }

    public static StyleBoxTexture OpenRightStyleBox<T>(T sheet) where T : PalettedStylesheet, IButtonConfig
    {
        var openRightBox = new StyleBoxTexture(BaseStyleBox(sheet))
        {
            Texture = new AtlasTexture(sheet.GetTextureOr(sheet.OpenRightButtonPath, NanotrasenStylesheet.TextureRoot),
                UIBox2.FromDimensions(new Vector2(0, 0), new Vector2(14, 24))),
        };
        openRightBox.SetPatchMargin(StyleBox.Margin.Right, 0);
        openRightBox.SetContentMarginOverride(StyleBox.Margin.Right, 8);
        openRightBox.SetPadding(StyleBox.Margin.Right, 1);
        return openRightBox;
    }

    public static StyleBoxTexture SquareStyleBox<T>(T sheet) where T : PalettedStylesheet, IButtonConfig
    {
        var openBothBox = new StyleBoxTexture(BaseStyleBox(sheet))
        {
            Texture = new AtlasTexture(sheet.GetTextureOr(sheet.OpenBothButtonPath, NanotrasenStylesheet.TextureRoot),
                UIBox2.FromDimensions(new Vector2(10, 0), new Vector2(3, 24))),
        };
        openBothBox.SetPatchMargin(StyleBox.Margin.Horizontal, 0);
        openBothBox.SetContentMarginOverride(StyleBox.Margin.Horizontal, 8);
        openBothBox.SetPadding(StyleBox.Margin.Horizontal, 1);
        return openBothBox;
    }

    public static StyleBoxTexture SmallStyleBox<T>(T sheet) where T : PalettedStylesheet, IButtonConfig
    {
        var smallBox = new StyleBoxTexture
        {
            Texture = sheet.GetTextureOr(sheet.SmallButtonPath, NanotrasenStylesheet.TextureRoot),
        };
        return smallBox;
    }
}
