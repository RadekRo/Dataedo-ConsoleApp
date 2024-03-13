### Opis b��d�w w pierwotnej aplikacji wraz z naniesionymi zmianami:

## Program.cs

B��dna nazwa przekazywanego pliku (dataa zmiast data). Zmiana nazwy pliku na data.

## DataReader.cs

* Usuni�to nieu�ywane usingi na pocz�tku bloku kodu.

* ImportedObjects - tu raczej chcemy zainicjowa� jedynie pust� list�, do kt�rej nast�pnie b�dziemy dodawa� obiekty. Nie ma raczej wi�kszego sensu inicjowa� j� z jednym pustym obiektem na starcie.

* Przy odczytywaniu pliku warto skorzysta� z bloku "using". Eliminuje to konieczno�� pami�tania o zamkni�ciu pliku na koniec.

* W bloku using - przy tworzeniu nowego obiektu do listy od razu mo�emy dokona� czyszczenia danych. Eliminuje to do�� rozbudowany w kolejnej p�tli foreach fragment kodu s�u��cego do czyszczenia danych ju� wprowadzonych. Przy du�ej ilo�ci obiekt�w b�dzie to mia�o bardzo du�y wp�yw na tempo dzia�ania programu. Spowolni go do�� znacz�co.

* W p�tli for "for (int i = 0; i <= importedLines.Count; i++)" zaczynamy iteracj� od "0", czyli b��dnie wstawiony jest znak "<=", b�dzie to prowadzi�o do b��du IndexOutOfRangeException, 
gdy� iteracja odb�dzie si� o jeden raz za du�o. 

* W przypadku kalkulacji ilo�ci dzieci (number of children) bardziej efektywne by�oby skorzystanie z LINQ w miejsce zagnie�d�onych p�tli.

* Pomimo przekazania warto�ci printData (domy�lnie = true) nie wykorzystujemy jej w bloku kodu. Nie b�dzie wi�c mo�liwo�ci manipulacji pojawianiem si� danych przekazuj�c do funkcji "false". Poprawione.

* Property - poprawione double na int w NumberOfChildren, gdzie zamierzono liczy� obiekty i wydaje si� oczywiste, �e nie b�dzie tu do czynienia z warto�ciami po przecinkach, a raczej liczbami ca�kowitymi.

* W klasie ImportedObject ponownie ustanowiono propert� Name, tymczasem jest ona dziedziczona z klasy ImportedObjectBaseClass. Je�li nie jest to b��d i rzeczywi�cie chciano nadpisa� warto�� nale�a�o by skorzysta� ze s�owa kluczowego "new". Ja zak�adam b��d, wi�c usuwam propert�.

* Ka�dej propercie dodano getter i setter;

-------------------
_Aktualnie program kompiluje si� bez b��d�w i dzia�a prawid�owo._