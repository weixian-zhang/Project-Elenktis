using System;
using System.Collections.Generic;
using System.Text;

namespace Elenktis.Fixer.MandateServiceFixer
{
    public class MandateServiceCreator
    {
        //private static void CreateLAWorkspaceIfNotExist(TenantSubscription subscription)
        //{
        //    var omsClient = new OperationalInsightsManagementClient(_sdkCred);
        //    omsClient.SubscriptionId = subscription.SubscriptionId;

        //    if (!subscription.IsResourceGroupExist)
        //    {
        //        string rgNameIfNotExist =
        //            _execContext.InformerConfig.ResourceGroupNameIfNotExist + subscription.DisplayName;

        //        var rmClient = new ResourceManagementClient(_sdkCred);

        //        var createdRG = rmClient.ResourceGroups.CreateOrUpdate
        //            (rgNameIfNotExist, new ResourceGroup() { Location = "Southeast Asia" });

        //        _execContext.ResourceGroup = createdRG;
        //    }
        //    else
        //        _execContext.ResourceGroup = subscription.ResourceGroups.FirstOrDefault();

        //    if (omsClient.Workspaces.List().Count() == 0)
        //    {
        //        string workspaceName =
        //            _execContext.InformerConfig.WorkspaceNameIfNotExist + subscription.DisplayName;

        //        //create workspace on first resource group found
        //        Workspace workspace = omsClient.Workspaces.BeginCreateOrUpdate
        //            (subscription.ResourceGroups.FirstOrDefault().Name,
        //                workspaceName, new Workspace("Southeast Asia"));

        //        _execContext.Workspace = workspace;
        //    }
        //    else
        //        _execContext.Workspace = omsClient.Workspaces.List().FirstOrDefault();
        //}

        //private static void UpgradeSecurityCenterToStandardIfFree(TenantSubscription subscription)
        //{
        //    ISecurityCenterClient ascClient = new SecurityCenterClient(_sdkCred);
        //    ascClient.SubscriptionId = subscription.SubscriptionId;

        //    var ascPricing = ascClient.Pricings.Get("Free");

        //    //upgrade price plan to standard
        //    if (ascPricing.PricingTier == "Free")
        //    {
        //        var pricing =
        //            ascClient.Pricings.UpdateAsync("Standard", "Standard").GetAwaiter().GetResult();
        //    }

        //    SetASCWorkspace(ascClient);

        //    //https://docs.microsoft.com/en-us/rest/api/securitycenter/
        //}

        //private static void SetASCWorkspace(ISecurityCenterClient ascClient)
        //{
        //    WorkspaceSetting ws =
        //        ascClient.WorkspaceSettings.Update
        //        ("controllermanager-laworkspacesettings", _execContext.Workspace.Id, "subscription");

        //    AutoProvisioningSetting aps =
        //        ascClient.AutoProvisioningSettings.Create("controllermanager-autoprosettings", "On");

        //    _execContext.WorkspaceSetting = ws;
        //    _execContext.AutoProvisioningSetting = aps;
        //}

        //private static void InstallMSAntimalware(TenantSubscription subscription)
        //{
        //    IComputeManagementClient cc = new ComputeManagementClient(_execContext.AzSDKCredentials);
        //    cc.SubscriptionId = subscription.SubscriptionId;

        //    var vms = cc.VirtualMachines.ListAll();

        //    foreach (var vm in vms)
        //    {
        //        //cc.VirtualMachineExtensions.CreateOrUpdate(vm, "vmname", "IaaSAntimalware");
        //    }


        //}
    }
}
