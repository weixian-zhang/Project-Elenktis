namespace Elenktis.Policy.SecurityHygiene
{
    [Plan(typeof(SecurityHygienePlan))]
    public class CheckAppServiceImportedCertExpiry : Policy {}
}