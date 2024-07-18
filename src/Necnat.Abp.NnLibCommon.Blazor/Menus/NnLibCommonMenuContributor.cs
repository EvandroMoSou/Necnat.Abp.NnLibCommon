using Necnat.Abp.NnLibCommon.Localization;
using Necnat.Abp.NnLibCommon.Permissions;
using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace Necnat.Abp.NnLibCommon.Blazor.Menus;

public class NnLibCommonMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<NnLibCommonResource>();

        bool displaybillingMenu = false;
        var billingMenu = new ApplicationMenuItem(
            NnLibCommonMenus.Prefix,
            l["Menu:NnLibCommon"],
            icon: "far fa-star"
        );

        bool displayBillingConfiguracaoMenu = false;
        var billingConfiguracaoMenu = new ApplicationMenuItem(
            NnLibCommonMenus.Configuration,
            l["Menu:NnLibCommon:Configuration"],
            order: 1
        );

        if (await context.IsGrantedAsync(NnLibCommonPermissions.PrmDistributedService.Default))
        {
            billingConfiguracaoMenu.AddItem(new ApplicationMenuItem(
                NnLibCommonMenus.Configuration_NecnatEndpoint,
                l["Menu:NnLibCommon:Configuration:NecnatEndpoint"],
                url: "/NnLibCommon/Configuration/NecnatEndpoints",
                order: 1
            ));
            displayBillingConfiguracaoMenu = true;
        }

        if (displayBillingConfiguracaoMenu)
            billingMenu.AddItem(billingConfiguracaoMenu);

        if (displaybillingMenu || displayBillingConfiguracaoMenu)
            context.Menu.AddItem(billingMenu);
    }
}
