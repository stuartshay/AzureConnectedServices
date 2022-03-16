### Create Shell Variables

```bash
resourceGroup="AzureConnectedServices-RG"
location="eastus"
storageAccount="azureconnectedservices01"
shareName="connectedservices-db"
```

Turn on persisted parameter

```bash
az config param-persist on
```

### Storage Account

InfluxDB ${DATA_DIR}/influxdb:/var/lib/influxdb

```bash
az storage account create --resource-group $resourceGroup \
        --name $storageAccount \
        --location $location \
        --sku Standard_LRS
```

File Share

```bash
az storage share create \
  --name $shareName \
  --account-name $storageAccount
```

Get Storage Account Key

```bash
STORAGE_KEY=$(az storage account keys list --resource-group $resourceGroup \
--account-name $storageAccount --query "[0].value" --output tsv)

echo $STORAGE_KEY
```
