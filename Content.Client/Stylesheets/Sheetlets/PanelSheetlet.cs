using Content.Client.Stylesheets.SheetletConfigs;
using Robust.Client.Graphics;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.Controls;
using static Content.Client.Stylesheets.StylesheetHelpers;

namespace Content.Client.Stylesheets.Sheetlets;

[CommonSheetlet]
public sealed class PanelSheetlet<T> : Sheetlet<T> where T : PalettedStylesheet, IPanelConfig
{
    public override StyleRule[] GetRules(T sheet, object config)
    {
        IPanelConfig buttonCfg = sheet;

        var boxLight = new StyleBoxFlat()
        {
            BackgroundColor = sheet.SecondaryPalette.BackgroundLight,
            // Texture = sheet.GetTextureOr(stripebackCfg.StripebackPath, NanotrasenStylesheet.TextureRoot), //this does not work
            // Mode = StyleBoxTexture.StretchMode.Tile,
        };
        var boxDark = new StyleBoxFlat()
        {
            BackgroundColor = sheet.SecondaryPalette.BackgroundDark,
        };
        var boxPositive = new StyleBoxFlat { BackgroundColor = sheet.PositivePalette.Background };
        var boxNegative = new StyleBoxFlat { BackgroundColor = sheet.NegativePalette.Background };
        var boxHighlight = new StyleBoxFlat { BackgroundColor = sheet.HighlightPalette.Background };

        return
        [
            E<PanelContainer>().Class(StyleClass.PanelLight).Panel(boxLight),
            E<PanelContainer>().Class(StyleClass.PanelDark).Panel(boxDark),

            E<PanelContainer>().Class(StyleClass.Positive).Panel(boxPositive),
            E<PanelContainer>().Class(StyleClass.Negative).Panel(boxNegative),
            E<PanelContainer>().Class(StyleClass.Highlight).Panel(boxHighlight),

            E<PanelContainer>()
                .Class("BackgroundDark")
                .Prop(PanelContainer.StylePropertyPanel, new StyleBoxFlat(Color.FromHex("#25252A"))),

            // panels that have the same corner bezels as buttons //carmine edit: no the fuck they dont anymore
            E()
                .Class(StyleClass.BackgroundPanel)
                .Prop(PanelContainer.StylePropertyPanel, StylePanelHelpers.BasePanel(sheet)),
                //.Modulate(sheet.SecondaryPalette.Background), //CARMINE EDIT: remove modulation to keep sprites using their original colors
            E()
                .Class(StyleClass.BackgroundPanelOpenLeft)
                .Prop(PanelContainer.StylePropertyPanel, StylePanelHelpers.OpenLeftPanel(sheet)),
                //.Modulate(sheet.SecondaryPalette.Background),
            E()
                .Class(StyleClass.BackgroundPanelOpenRight)
                .Prop(PanelContainer.StylePropertyPanel, StylePanelHelpers.OpenRightPanel(sheet)),
                //.Modulate(sheet.SecondaryPalette.Background),
            E()
                .Class(StyleClass.GargoylePanelLeft)
                .Prop(PanelContainer.StylePropertyPanel, StylePanelHelpers.GargoylePanelLeft(sheet)),
                //.Modulate(sheet.SecondaryPalette.Background),
            E()
                .Class(StyleClass.GargoylePanelRight)
                .Prop(PanelContainer.StylePropertyPanel, StylePanelHelpers.GargoylePanelRight(sheet)),
                //.Modulate(sheet.SecondaryPalette.Background),
        ];
    }
}
