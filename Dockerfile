# Stage 1: Build stage using .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project file 
COPY *.csproj ./

# Copy source code
COPY . ./

# Publish for Linux (will automatically restore for correct platform)
RUN dotnet publish -c Release -o /app/publish --runtime linux-x64 --self-contained false

# Stage 2: Runtime stage using AWS Lambda base image
FROM public.ecr.aws/lambda/dotnet:8

# Copy the published application from build stage
COPY --from=build /app/publish ${LAMBDA_TASK_ROOT}/

# Set the Lambda handler
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