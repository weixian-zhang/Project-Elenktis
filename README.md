# Project-Elenktis
A series of microservicelized components that keep your Azure subscriptions secure and compliant

## Prerequisites - Beta

* Create Elenktis Azure service principal - this is shared across all controller manager services and Flex Volume. Service Principal for Elenktis is recommended to grant access at management group in order to gain assert all subscriptions under the management group.

* Setup AKV FlexVolume with service principal.
  [instructions on AKV FlexVolume](https://samcogan.com/access-azure-key-vault-from-your-kubernetes-pods/)
  
