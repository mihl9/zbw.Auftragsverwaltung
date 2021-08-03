# Overview

## Requirements
### Functional
Number | Area | Description
;----------- | ------------- | ------------
1 | Verwaltung | In der Datenbankapplikation können Kunden verwaltet werden
2 | Verwaltung | In der Datenbankapplikation können Adressen verwaltet werden
3 | Verwaltung | In der Datenbankapplikation können Artikel verwaltet werden
4 | Verwaltung | In der Datenbankapplikation können Artikelgruppen verwaltet werden
5 | Verwaltung | In der Datenbankapplikation können Aufträge verwaltet werden
6 | Verwaltung | In der Datenbankapplikation können Auftragspositionen verwaltet werden
7 | Allgemein | Eine eigenständige Windows-Desktop-Applikation muss entwickelt werden.
8 | Technologie-Stack | Es muss in der Programmiersprache C# entwickelt werden
9 | Technologie-Stack | Der Presentation-Layer erfolgt auf Windows Forms oder WPF
10 | Technologie-Stack | Der Data-Layer erfolgt mit Entity Framework und CodeFirsts
11 | Technologie-Stack | Als Datenbanksystem muss MS SQL Server verwenden werden
12 | Verwaltung | In der Applikation muss in der Verwaltung das Suchen möglich sein
13 | Verwaltung | In der Applikation muss in der Verwaltung das Erstellen möglich sein
14 | Verwaltung | In der Applikation muss in der Verwaltung das Bearbeiten möglich sein
15 | Verwaltung | In der Applikation muss in der Verwaltung das Löschen möglich sein
16 | Attribute | "Für die Entität ""Kunden"" sollen mindestens die Attribute KundenNr, Name, E-Mail-Adresse, Website, Passwort verwaltet werden können"
17 | Attribute | "Für die Entität ""Adressen"" sollen mindestens die Attribute Strasse, PLZ, Ort verwaltet werden können"
18 | Attribute | "Für die Entität ""Artikelgruppe"" sollen mindestens die Attribute Name, übergeordnete Artikelgruppe (Hierarchie) verwaltet werden können"
19 | Attribute | "Für die Entität ""Artikel"" sollen mindestens die Attribute Artikelnummer, Bezeichnung, Preis, Artikelgruppe verwaltet werden können"
20 | Attribute | "Für die Entität ""Aufträge"" sollen mindestens die Attribute Auftragsnummer, Datum, Kunde verwaltet werden können"
21 | Attribute | "Für die Entität ""Positionen"" sollen mindestens die Attribute Nummer, Artikel, Anzahl verwaltet werden können"
22 | Artikelgruppen | Artikelgruppen müssen hierarchisch gegliedert werden können
23 | Artikelgruppen | Artikelgruppen sowie deren Hierarchie müssen ausgegeben werden können
24 | Artikelgruppen | Die Ausgabe muss in Form einer Treeview in der Applikation zur Verfügung gestellt werden
25 | Jahresvergleich | Ein Jahresvergleich für die vergangenen 3 Jahre pro Quartal muss abgefragt werden können
26 | Jahresvergleich | "Es müssen folgende Zahlen (Kategorien) aufgelistet werden können:
- Anzahl Aufträge
- Anzahl verwaltete Artikel
- Durchschnittliche Anzahl Artikel pro Auftrag
- Umsatz pro Kunde
- Gesamtumsatz"
27 | Jahresvergleich | Für die Abfrage muss zwingend Window Functions verwendet werden.
28 | Jahresvergleich | "Die Struktur der Darstellung muss folgendermassen aussehen:"
29 | Jahresvergleich | Die Ausgabe muss in einem GridView erfolgen
30 | Fakturen | "Eine Abfrage, die alle Fakturen mit folgenden Feldern auflistet muss erstellt werden:
- Kundennummer
- Name
- Strasse
- PLZ
- Ort
- Land
- Rechnungsdatum
- Rechnungsnummer
- Rechnungsbetrag netto
- Rechnungsbetrag brutto"
31 | Fakturen | Die Ausgabe muss in einem GridView erfolgen
32 | Fakturen | Der Benutzer muss die Möglichkeit haben, Filter zu setzen, die in der Abfrage berücksichtigt werden.
33 | Fakturen | Bei der Abfrage muss für jede Rechnung die am Rechnungsdatum gültige Adresse ausgegeben werden. Für diese Anforderung muss eine temporale Datenstruktur verwendet werden.
34 | Validierung | Eingaben bei der Kundenerfassung müssen validiert werden und fehlerhafte Eingaben im UI als solche erkennbar sein.
35 | Validierung | "Die Adressnummer der Kunden muss zwingend mit dem Präfix ""CU"" beginnen (CU=Customer) und anschliessend eine 5stellige Nummer folgen."
36 | Validierung | Die Validierung für E-Mail Adressen muss alle Fälle gemäss RFC 5322 abdecken. Ist dies nicht möglich, muss dokumentiert werden, welche Fälle nicht abgedeckt sind.
37 | Validierung | "Die Validierung für die Website muss folgende Formate zulassen:
www.google.com
http://www.google.com
https://www.google.com
google.com

Die Adressen dürfen Subdomains, Pfade und Parameter enthalten bspw.
https://policies.google.com/technologies/voice?hl=de&gl=ch"
38 | Validierung | "Bei Passwörtern gelten folgende Regeln:
- Min. 8 Zeichen
- Zwingend einen Gross- sowie einen Kleinbuchstaben
- Zwingend eine Zahl
- usw."
39 | Export / Import | Kundendaten müssen importiert und exportiert werden können.
40 | Export / Import | Im UI muss für beide Funktionen eine Schaltfläche zur Verfügung stehen.
41 | Export / Import | "Die Daten müssen im json und xml Format exportiert werden können (Aufbau siehe Arbeitsblatt ""Projektarbeit Auftragsverwaltung"")."
42 | Export / Import | Das Passwort muss sicher exportiert werden können. Es darf unter keinen Umständen möglich sein, das Passwort im Klartext auslesen zu können.

### Not Functional
Number | Area | Description
;----------- | ------------- | ------------
43 | Datenbank | Alle Datenbankzugriffe müssen gekapselt werden mit einem DataAccess-Layer.
44 | Datenbank | Im ViewModel darf es keine direkte Interaktion mit der Datenbank geben.
45 | Datenbank | Die Datenbankanbindung muss mit ADO.NET oder mit EF erfolgen.
46 | Datenbank | Die Anwendung sollte konsequent sein, sodass die Art der technologischen Anbindung egal ist.


## Quality Goals

## Stakeholders

## Demo