# Контейнеризованное приложение ASP.NET Core Minimal API

Собрать и запустить:

```sh
docker-compose up -d
```

Или сначала собрать:

```sh
dotnet publish --os linux --arch x64 /t:PublishContainer
```

а потом запустить:

```sh
docker run -d --rm --name super-app -p 8080:8080 amironov73/containerized
```
