##DESARROLLO
#Imagen base
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

#Directorio dentro de contenedor
WORKDIR /src
#Copia archivo
COPY . ./
#Compila aplicación en modo release y coloca archivos en esa ruta
WORKDIR /src/API
RUN dotnet publish API.csproj -c Release -o /app/out



##PRODUCCION
#Imagen que utiliza para ejecutar la app
FROM mcr.microsoft.com/dotnet/aspnet:8.0
##Directorio de trabajo
WORKDIR /app
#Copia ficheros del build a ese directorio
COPY --from=build /app/out .

#Declaración de path para la escritura de logs
ENV LOG_PATH=/app/logs
#Ruta del volumen persistente de logs
VOLUME ["/app/logs"]
#Puerto donde escucha la app
EXPOSE 7877
#Comando para ejecutar la app
ENTRYPOINT ["dotnet", "API.dll"]
