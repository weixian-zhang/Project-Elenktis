# Project-Elenktis
A series of microservicelized components that keep your Azure subscriptions secure and compliant

## Prerequisites

* Create Elenktis Azure service principal - this is shared across all controller manager services and Flex Volume. Service Principal for Elenktis is recommended to grant access at management group in order to gain assert all subscriptions under the management group.

* Enable AAD Pod Identity
