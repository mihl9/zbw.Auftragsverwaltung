# Overview

## Requirements
### Functional
Number | Area | Description
:----------- | ------------- | ------------
REQ-001 | Verwaltung | In der Datenbankapplikation können Kunden verwaltet werden
REQ-002 | Verwaltung | In der Datenbankapplikation können Adressen verwaltet werden
REQ-003 | Verwaltung | In der Datenbankapplikation können Artikel verwaltet werden
REQ-004 | Verwaltung | In der Datenbankapplikation können Artikelgruppen verwaltet werden
REQ-005 | Verwaltung | In der Datenbankapplikation können Aufträge verwaltet werden
REQ-006 | Verwaltung | In der Datenbankapplikation können Auftragspositionen verwaltet werden
REQ-007 | Allgemein | Eine eigenständige Windows-Desktop-Applikation muss entwickelt werden.
REQ-008 | Technologie-Stack | Es muss in der Programmiersprache C# entwickelt werden
REQ-009 | Technologie-Stack | Der Presentation-Layer erfolgt auf Windows Forms oder WPF
REQ-010 | Technologie-Stack | Der Data-Layer erfolgt mit Entity Framework und CodeFirsts
REQ-011 | Technologie-Stack | Als Datenbanksystem muss MS SQL Server verwenden werden
REQ-012 | Verwaltung | In der Applikation muss in der Verwaltung das Suchen möglich sein
REQ-013 | Verwaltung | In der Applikation muss in der Verwaltung das Erstellen möglich sein
REQ-014 | Verwaltung | In der Applikation muss in der Verwaltung das Bearbeiten möglich sein
REQ-015 | Verwaltung | In der Applikation muss in der Verwaltung das Löschen möglich sein
REQ-016 | Attribute | Für die Entität "Kunden" sollen mindestens die Attribute KundenNr, Name, E-Mail-Adresse, Website, Passwort verwaltet werden können
REQ-017 | Attribute | Für die Entität "Adressen" sollen mindestens die Attribute Strasse, PLZ, Ort verwaltet werden können
REQ-018 | Attribute | Für die Entität "Artikelgruppe" sollen mindestens die Attribute Name, übergeordnete Artikelgruppe (Hierarchie) verwaltet werden können
REQ-019 | Attribute | Für die Entität "Artikel" sollen mindestens die Attribute Artikelnummer, Bezeichnung, Preis, Artikelgruppe verwaltet werden können
REQ-020 | Attribute | Für die Entität "Aufträge" sollen mindestens die Attribute Auftragsnummer, Datum, Kunde verwaltet werden können
REQ-021 | Attribute | Für die Entität "Positionen" sollen mindestens die Attribute Nummer, Artikel, Anzahl verwaltet werden können
REQ-022 | Artikelgruppen | Artikelgruppen müssen hierarchisch gegliedert werden können
REQ-023 | Artikelgruppen | Artikelgruppen sowie deren Hierarchie müssen ausgegeben werden können
REQ-024 | Artikelgruppen | Die Ausgabe muss in Form einer Treeview in der Applikation zur Verfügung gestellt werden
REQ-025 | Jahresvergleich | Ein Jahresvergleich für die vergangenen 3 Jahre pro Quartal muss abgefragt werden können
REQ-026 | Jahresvergleich | Es müssen folgende Zahlen (Kategorien) aufgelistet werden können:<ul><li>Anzahl Aufträge</li><li>Anzahl verwaltete Artikel</li><li>Durchschnittliche Anzahl Artikel pro Auftrag</li><li>Umsatz pro Kunde</li><li>Gesamtumsatz</li></ul>
REQ-027 | Jahresvergleich | Für die Abfrage muss zwingend Window Functions verwendet werden.
REQ-028 | Jahresvergleich | "Die Struktur der Darstellung muss folgendermassen aussehen:"
REQ-029 | Jahresvergleich | Die Ausgabe muss in einem GridView erfolgen
REQ-030 | Fakturen | Eine Abfrage, die alle Fakturen mit folgenden Feldern auflistet muss erstellt werden:<ul><li>Kundennummer</li><li>Name</li><li>Strasse</li><li>PLZ</li><li>Ort</li><li>Land</li><li>Rechnungsdatum</li><li>Rechnungsnummer</li><li>Rechnungsbetrag netto</li><li>Rechnungsbetrag brutto</li></ul>
REQ-031 | Fakturen | Die Ausgabe muss in einem GridView erfolgen
REQ-032 | Fakturen | Der Benutzer muss die Möglichkeit haben, Filter zu setzen, die in der Abfrage berücksichtigt werden.
REQ-033 | Fakturen | Bei der Abfrage muss für jede Rechnung die am Rechnungsdatum gültige Adresse ausgegeben werden. Für diese Anforderung muss eine temporale Datenstruktur verwendet werden.
REQ-034 | Validierung | Eingaben bei der Kundenerfassung müssen validiert werden und fehlerhafte Eingaben im UI als solche erkennbar sein.
REQ-035 | Validierung | Die Adressnummer der Kunden muss zwingend mit dem Präfix "CU" beginnen (CU=Customer) und anschliessend eine 5stellige Nummer folgen.
REQ-036 | Validierung | Die Validierung für E-Mail Adressen muss alle Fälle gemäss RFC 5322 abdecken. Ist dies nicht möglich, muss dokumentiert werden, welche Fälle nicht abgedeckt sind.
REQ-037 | Validierung | Die Validierung für die Website muss folgende Formate zulassen: <ul><li>www.google.com</li><li>http://www.google.com</li><li>https://www.google.com</li><li>google.com</li><li>https://policies.google.com/technologies/voice?hl=de&gl=ch"</li></ul>
REQ-038 | Validierung | Bei Passwörtern gelten folgende Regeln: <ul><li>Min. 8 Zeichen</li><li>Zwingend einen Gross- sowie einen Kleinbuchstaben</li><li>Zwingend eine Zahl</li><li>usw.</li></ul>
REQ-039 | Export / Import | Kundendaten müssen importiert und exportiert werden können.
REQ-040 | Export / Import | Im UI muss für beide Funktionen eine Schaltfläche zur Verfügung stehen.
REQ-041 | Export / Import | Die Daten müssen im json und xml Format exportiert werden können (Aufbau siehe Arbeitsblatt "Projektarbeit Auftragsverwaltung").
REQ-042 | Export / Import | Das Passwort muss sicher exportiert werden können. Es darf unter keinen Umständen möglich sein, das Passwort im Klartext auslesen zu können.

### Not Functional
Number | Area | Description
:----------- | ------------- | ------------
REQ-043 | Datenbank | Alle Datenbankzugriffe müssen gekapselt werden mit einem DataAccess-Layer.
REQ-044 | Datenbank | Im ViewModel darf es keine direkte Interaktion mit der Datenbank geben.
REQ-045 | Datenbank | Die Datenbankanbindung muss mit ADO.NET oder mit EF erfolgen.
REQ-046 | Datenbank | Die Anwendung sollte konsequent sein, sodass die Art der technologischen Anbindung egal ist.


## Quality Goals

## Stakeholders

## Demo