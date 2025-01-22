## 📝 ToDoApp
ToDoApp to prosta aplikacja do zarządzania zadaniami stworzona w ramach projektu na uczelnię. Aplikacja składa się z trzech głównych komponentów:

* Frontend – aplikacja webowa serwowana przez Nginx
* Backend – aplikacja napisana w .NET Core
* Baza danych – PostgreSQL do przechowywania danych

## 🚀 Jak uruchomić aplikację za pomocą Dockera
### Wymagania
Aby uruchomić aplikację lokalnie, upewnij się, że masz zainstalowane:

* Docker 
* Docker Compose (jeśli nie jest wbudowany w Dockera)
### Uruchomienia aplikacji
* Sklonuj repozytorium oraz przejdź do lokalizacji

```
git clone https://github.com/Wajkt0r/ToDoApp.git
cd ToDoApp
```

* Uruchom aplikację za pomocą Dockera:

```
docker-compose up --build
```

* Otwórz aplikację w przeglądarce:
```
 http://localhost:8080
```

### Zatrzymanie aplikacji
Aby zatrzymać działanie aplikacji, użyj:

```
docker-compose down
```

### Jeśli chcesz usunąć kontenery oraz przechowywane dane (np. w bazie danych), użyj:

```
docker-compose down -v
```
