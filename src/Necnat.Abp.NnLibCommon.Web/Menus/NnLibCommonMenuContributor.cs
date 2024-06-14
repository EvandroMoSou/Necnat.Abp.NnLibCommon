using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace Necnat.Abp.NnLibCommon.Web.Menus;

public class NnLibCommonMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        //Add main menu items.
        context.Menu.AddItem(new ApplicationMenuItem(NnLibCommonMenus.Prefix, displayName: "NnLibCommon", "~/NnLibCommon", icon: "fa fa-globe"));

        return Task.CompletedTask;
    }
}
