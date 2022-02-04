FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PdfFlow/PdfFlow.csproj", "PdfFlow/"]
RUN dotnet restore "PdfFlow/PdfFlow.csproj"
COPY . .
WORKDIR "/src/PdfFlow"
RUN dotnet build "PdfFlow.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PdfFlow.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PdfFlow.dll"]
