using Content.Client.Stylesheets.SheetletConfigs;
using Content.Client.Stylesheets.Stylesheets;
using Content.Client.UserInterface.Controls;
using Robust.Client.Graphics;
using Robust.Client.UserInterface;
using static Content.Client.Stylesheets.StylesheetHelpers;

namespace Content.Client.Stylesheets.Sheetlets;

[CommonSheetlet]
public sealed class ChatBackgroundSheetlet<T> : Sheetlet<T> where T : PalettedStylesheet, IChatBackgroundConfig
{
    public override StyleRule[] GetRules(T sheet, object config)
    {
        IChatBackgroundConfig chatBackgroundCfg = sheet;

        var chatBackground = new StyleBoxTexture
        {
            Texture = sheet.GetTextureOr(chatBackgroundCfg.ChatBackgroundPath, NanotrasenStylesheet.TextureRoot),
            Mode = StyleBoxTexture.StretchMode.Tile,
        };

        return
        [
            E<ChatBackground>()
                .Prop(ChatBackground.StylePropertyBackground, chatBackground),
        ];
    }
}
