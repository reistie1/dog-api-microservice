FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine3.17 AS base
ARG USER
ARG GROUP
ENV HOME /home/$USER
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

RUN apk add --update sudo bash
RUN apk add --no-cache icu-libs

# add new user
# RUN addgroup -S $GROUP
# RUN adduser -S $USER -G $GROUP

# USER $USER

WORKDIR /app
EXPOSE 5000
FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine3.17 AS build
WORKDIR /src
COPY ["./Dog.Api/Dog.Api.csproj", "Dog.Api/"]
COPY ["../Dog.Api.Core/Dog.Api.Core.csproj", "Dog.Api.Core/"]
COPY ["../Dog.Api.Infrastructure/Dog.Api.Infrastructure.csproj", "Dog.Api.Infrastructure/"]
RUN dotnet restore "Dog.Api/Dog.Api.csproj"
COPY . .
WORKDIR "/src/Dog.Api"
RUN dotnet build "Dog.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dog.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish ./
ENTRYPOINT ["dotnet", "Dog.Api.dll"]