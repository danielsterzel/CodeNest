# CodeNest

CodeNest to aplikacja internetowa wspomagająca zarządzanie kursami edukacyjnymi. System umożliwia studentom zapisy na zajęcia, a nauczycielom – przeglądanie przypisanych kursów i list uczestników. Aplikacja została stworzona w oparciu o ASP.NET Core MVC oraz Entity Framework Core.
## Funkcjonalności
    **Role użytkowników**:

        Studenci i Nauczyciele z odpowiednim poziomem dostępu (autoryzacja ról).

    Zarządzanie kursami:

        Studenci mogą wyszukiwać kursy wg przedmiotu (np. Matematyka, Fizyka) i zapisywać się na nie.

        Nauczyciele mogą przeglądać przypisane kursy oraz listy studentów.

    Kalendarz zajęć:

        Automatyczne generowanie terminów zajęć w odstępach tygodniowych.

        Widok kalendarza dla studentów i nauczycieli.

    Rezerwacje:

        Obsługa rezerwacji miejsc na kursach z automatycznym powiązaniem użytkownika i kursu.

    Bezpieczeństwo:

        Logowanie oparte na sesji oraz filtrowanie dostępu według roli.

## Technologie

    Backend:

        ASP.NET Core MVC

        Entity Framework Core

    Frontend:

        Razor Views (cshtml)

        HTML, CSS

    Baza danych:

        SQL Server (via EF Core)

Uruchomienie aplikacji

    Sklonuj repozytorium:

git clone https://github.com/danielsterzel/CodeNest.git

Przejdź do katalogu projektu:

``` bash
cd CodeNest
```

Przywróć zależności:

``` bash
dotnet restore

```

Zastosuj migracje i zaktualizuj bazę danych:


``` bash
dotnet ef database update
```

Uruchom aplikację:

``` bash
dotnet run

```

Wejdź na stronę w przeglądarce:

https://localhost:5001

lub adres wyświetlony w konsoli.

## Autorzy:
 - Dagmara Krenich
 - Daniel Sterzel
