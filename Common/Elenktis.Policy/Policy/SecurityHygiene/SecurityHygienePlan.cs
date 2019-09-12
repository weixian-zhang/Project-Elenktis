using System;

namespace Elenktis.Policy.SecurityHygiene
{
    public class SecurityHygienePlan : AssessmentPlan
    {
        public SecurityHygienePlan(string subscriptionId) : base(subscriptionId) {}

        public CheckAKVImportedCertExpiryPolicy CheckAKVImportedCertExpiryPolicy { get; set; }

        public CheckDDoSStandardEnableOnVNetWithPIPPolicy CheckDDoSStandardEnableOnVNetWithPIPPolicy { get; set; }

        public CheckRemoteDebugEnableOnAppServicePolicy CheckRemoteDebugEnableOnAppServicePolicy { get; set; }
   
        public CheckRemoteDebugEnableOnAzFuncPolicy CheckRemoteDebugEnableOnAzFuncPolicy { get; set; }
   
        public CheckSubnetWithMissingNSGPolicy CheckSubnetWithMissingNSGPolicy { get; set; }
   
        public EnableAuditOnAzSQLPolicy EnableAuditOnAzSQLPolicy { get; set; }
   
        public EnableAzPostgreSQLATPPolicy EnableAzPostgreSQLATPPolicy { get; set; }

        public EnableAzSQLATPPolicy EnableAzSQLATPPolicy { get; set; }

        public EnableAzSQLTDEPolicy EnableAzSQLTDEPolicy { get; set; }

        public EnableAzStorageATPPolicy EnableAzStorageATPPolicy { get; set; }

        public EnableHttpsOnlyOnAppServicePolicy EnableHttpsOnlyOnAppServicePolicy { get; set; }
   
        public EnableMariaDbATPPolicy EnableMariaDbATPPolicy { get; set; }

        public EnableMySQLATPPolicy EnableMySQLATPPolicy { get; set; }

        public EnableResourceLockAtSubscriptionPolicy EnableResourceLockAtSubscriptionPolicy { get; set; }
   
        public EnableSecureTransferOnAzStoragePolicy EnableSecureTransferOnAzStoragePolicy { get; set; }
   
        public EnableVAScanOnAzSQLPolicy EnableVAScanOnAzSQLPolicy { get; set; }

        public EnableVMDiskEncryptionPolicy EnableVMDiskEncryptionPolicy { get; set; }

        public InstallMissingWinDefenderOnVMPolicy InstallMissingWinDefenderOnVMPolicy { get; set; }

        public EnableVMConnectedDefaultLAWorkspacePolicy EnableVMConnectedDefaultLAWorkspacePolicy { get; set; }
   }
}