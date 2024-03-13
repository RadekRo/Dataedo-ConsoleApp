### Opis błędów w pierwotnej aplikacji wraz z naniesionymi zmianami:

## Program.cs

Błędna nazwa przekazywanego pliku (dataa zmiast data). Zmiana nazwy pliku na data.

## DataReader.cs

* Usunięto nieużywane usingi na początku bloku kodu.

* ImportedObjects - tu raczej chcemy zainicjować jedynie pustą listę, do której następnie będziemy dodawać obiekty. Nie ma raczej większego sensu inicjować ją z jednym pustym obiektem na starcie.

* Przy odczytywaniu pliku warto skorzystać z bloku "using". Eliminuje to konieczność pamiętania o zamknięciu pliku na koniec.

* W bloku using - przy tworzeniu nowego obiektu do listy od razu możemy dokonać czyszczenia danych. Eliminuje to dość rozbudowany w kolejnej pętli foreach fragment kodu służącego do czyszczenia danych już wprowadzonych. Przy dużej ilości obiektów będzie to miało bardzo duży wpływ na tempo działania programu. Spowolni go dość znacząco.

* W pętli for "for (int i = 0; i <= importedLines.Count; i++)" zaczynamy iterację od "0", czyli błędnie wstawiony jest znak "<=", będzie to prowadziło do błędu IndexOutOfRangeException, 
gdyż iteracja odbędzie się o jeden raz za dużo. 

* W przypadku kalkulacji ilości dzieci (number of children) bardziej efektywne byłoby skorzystanie z LINQ w miejsce zagnieżdżonych pętli.

* Pomimo przekazania wartości printData (domyślnie = true) nie wykorzystujemy jej w bloku kodu. Nie będzie więc możliwości manipulacji pojawianiem się danych przekazując do funkcji "false". Poprawione.

* Property - poprawione double na int w NumberOfChildren, gdzie zamierzono liczyć obiekty i wydaje się oczywiste, że nie będzie tu do czynienia z wartościami po przecinkach, a raczej liczbami całkowitymi.

* W klasie ImportedObject ponownie ustanowiono propertę Name, tymczasem jest ona dziedziczona z klasy ImportedObjectBaseClass. Jeśli nie jest to błąd i rzeczywiście chciano nadpisać wartość należało by skorzystać ze słowa kluczowego "new". Ja zakładam błąd, więc usuwam propertę.

* Każdej propercie dodano getter i setter;

-------------------
_Aktualnie program kompiluje się bez błędów i działa prawidłowo._
