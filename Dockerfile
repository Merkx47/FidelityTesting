# Use the official AWS Lambda .NET 8 base image
# This includes the Lambda runtime and .NET 8
FROM public.ecr.aws/lambda/dotnet:8

# Set the working directory to the Lambda task root
WORKDIR ${LAMBDA_TASK_ROOT}

# Copy the project file and restore dependencies
# This layer is cached if dependencies don't change
COPY *.csproj ./
RUN dotnet restore

# Copy the entire project source code
COPY . ./

# Build and publish the application in Release mode
RUN dotnet publish -c Release -o out

# Copy the published application to the Lambda task root
RUN cp -r /var/task/out/* ${LAMBDA_TASK_ROOT}/

# Set the Lambda handler
# Format: AssemblyName::Namespace.ClassName::MethodName
CMD [ "FidelityTesting::FidelityTesting.LambdaEntryPoint::FunctionHandlerAsync" ]













#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
# USER app
# WORKDIR /app
# EXPOSE 8080
# EXPOSE 8081

# FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# ARG BUILD_CONFIGURATION=Release
# WORKDIR /src
# COPY ["FidelityTesting/FidelityTesting.csproj", "FidelityTesting/"]
# RUN dotnet restore "./FidelityTesting/FidelityTesting.csproj"
# COPY . .
# WORKDIR "/src/FidelityTesting"
# RUN dotnet build "./FidelityTesting.csproj" -c $BUILD_CONFIGURATION -o /app/build

# FROM build AS publish
# ARG BUILD_CONFIGURATION=Release
# RUN dotnet publish "./FidelityTesting.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "FidelityTesting.dll"]