# GitHub Actions CI/CD Workflow for CityInfo API

This directory contains GitHub Actions workflows for the CityInfo API project.

## Workflows

### Build and Deploy to ACR (`build-and-deploy.yml`)

This workflow automates building, testing, and deploying the CityInfo API to Azure Container Registry.

**Trigger Events:**
- Push to `main` branch
- Pull requests to `main` branch

**Jobs:**

1. **build-and-test**
   - Runs on every push and pull request
   - Sets up .NET 10
   - Restores dependencies
   - Builds the project in Release mode
   - Runs all tests

2. **build-and-push-docker**
   - Runs only after `build-and-test` succeeds
   - Runs only on pushes to the `main` branch (not on PRs)
   - Sets up Docker Buildx for efficient multi-platform builds
   - Authenticates with Azure Container Registry
   - Builds the Docker image using the Dockerfile in `CityInfo.Api/`
   - Pushes the image with two tags:
	 - Git commit SHA (for version tracking)
	 - `latest` (for quick reference)

## Prerequisites

### Azure Container Registry Setup

1. **Create an Azure Container Registry** (if you don't have one):
   ```bash
   az acr create --resource-group <resource-group-name> \
				 --name <registry-name> \
				 --sku Basic
   ```

2. **Create a service principal** for GitHub Actions authentication:
   ```bash
   az ad sp create-for-rbac \
	 --name github-actions-sp \
	 --role acrpush \
	 --scopes /subscriptions/<subscription-id>/resourceGroups/<resource-group>/providers/Microsoft.ContainerRegistry/registries/<registry-name>
   ```

   This will output JSON with `appId`, `password`, and `tenant` values.

### GitHub Repository Secrets

Add the following secrets to your GitHub repository (Settings → Secrets and variables → Actions):

1. **REGISTRY_LOGIN_SERVER**
   - Value: `<registry-name>.azurecr.io`
   - This is your Azure Container Registry login server

2. **REGISTRY_USERNAME**
   - Value: `<appId>` from the service principal creation
   - The service principal's application ID

3. **REGISTRY_PASSWORD**
   - Value: `<password>` from the service principal creation
   - The service principal's password/secret

## Configuration

### Modifying Environment Variables

In `.github/workflows/build-and-deploy.yml`, you can customize:

- `REGISTRY`: Set to `secrets.REGISTRY_LOGIN_SERVER`
- `IMAGE_NAME`: Change from `cityinfo-api` to your preferred image name
- `IMAGE_TAG`: Uses git commit SHA by default; can be customized

### Changing Trigger Events

To run the workflow on different events, modify the `on:` section:

```yaml
on:
  push:
	branches:
	  - main
	  - develop  # Add other branches
	tags:
	  - 'v*'     # Add tag-based triggers
  pull_request:
	branches:
	  - main
```

### Filtering Deployment to Production

To deploy only on version tags:

```yaml
if: startsWith(github.ref, 'refs/tags/v')
```

## How It Works

### Build and Test (runs on all events)

```
1. Check out code
2. Set up .NET 10
3. Run: dotnet restore
4. Run: dotnet build --configuration Release
5. Run: dotnet test --configuration Release
```

### Build and Push to ACR (runs only on main push after tests pass)

```
1. Check out code
2. Set up Docker Buildx
3. Authenticate with ACR using service principal
4. Build Docker image from Dockerfile
5. Push with tags:
   - <registry>/<image-name>:<commit-sha>
   - <registry>/<image-name>:latest
```

## Deployment to Azure Container Instances or AKS

After the image is pushed to ACR, you can deploy it using:

### Azure Container Instances (ACI)

```bash
az container create \
  --resource-group <resource-group-name> \
  --name cityinfo-api \
  --image <registry-name>.azurecr.io/cityinfo-api:latest \
  --registry-login-server <registry-name>.azurecr.io \
  --registry-username <username> \
  --registry-password <password> \
  --dns-name-label cityinfo-api \
  --ports 8080
```

### Azure Kubernetes Service (AKS)

```bash
kubectl set image deployment/cityinfo-api \
  cityinfo-api=<registry-name>.azurecr.io/cityinfo-api:latest \
  --namespace default
```

## Viewing Workflow Results

1. Go to your GitHub repository
2. Click on the "Actions" tab
3. Select the workflow run
4. View logs for each job/step
5. In case of failure, check the logs for error details

## Troubleshooting

### Docker Build Fails

- Ensure the Dockerfile path is correct: `./CityInfo.Api/Dockerfile`
- Check that all project references in the Dockerfile exist
- Verify .NET version compatibility (currently using .NET 10)

### ACR Authentication Fails

- Verify that the service principal credentials are correct
- Ensure the service principal has `acrpush` role on the registry
- Check that secrets are properly set in GitHub (no extra whitespace)

### Tests Fail

- Run tests locally: `dotnet test`
- Check the workflow logs for detailed error messages
- Ensure all dependencies are properly restored

## Additional Resources

- [GitHub Actions Documentation](https://docs.github.com/en/actions)
- [Docker Build and Push Action](https://github.com/docker/build-push-action)
- [Azure Container Registry Documentation](https://learn.microsoft.com/en-us/azure/container-registry/)
- [.NET Build Action](https://github.com/actions/setup-dotnet)

## Next Steps

1. Set up Azure Container Registry (if not already done)
2. Create service principal credentials
3. Add the three secrets to your GitHub repository
4. Push code to the `main` branch to trigger the workflow
5. Monitor the workflow execution in the Actions tab
