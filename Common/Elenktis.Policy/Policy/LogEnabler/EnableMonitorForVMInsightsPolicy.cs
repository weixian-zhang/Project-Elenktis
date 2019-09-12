namespace Elenktis.Policy.LogEnabler
{
    [Plan(typeof(LogEnablerPlan))]
    public class EnableMonitorForVMInsightsPolicy : Policy {}
}

//https://docs.microsoft.com/en-us/azure/azure-monitor/insights/vminsights-enable-at-scale-powershell#enable-with-powershell

//install Azure Monitor For VM through PS
//this should install dependency agent, mma and install InfrastureInsights & Service Map solutions
//https://docs.microsoft.com/en-us/azure/azure-monitor/insights/vminsights-enable-at-scale-powershell#enable-with-powershell