FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["EFCoreCodeFirst/EFCoreCodeFirst.csproj", "EFCoreCodeFirst/"]
RUN dotnet restore "EFCoreCodeFirst/EFCoreCodeFirst.csproj"
COPY . .
WORKDIR "/src/EFCoreCodeFirst"
RUN dotnet build "EFCoreCodeFirst.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EFCoreCodeFirst.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EFCoreCodeFirst.dll"]
