| Module   | PRE – Programming Expert       |
|----------|--------------------------------|
| Opdracht | PE 2                           |
| Type     | Individuele Opdracht           |
| Deadline | zie Leho                       |
| Max score| zie Leho                       |


# PE2 - JobApplicationSystem
## Algemeen
Dit is een individuele opdracht. Jouw ingave is een zelfstandig geproduceerd werk waarmee je bewijst dat je over 
de vereiste vaardigheden beschikt. Gebruik code van derden enkel als studiemateriaal. Deel je oplossingen nooit 
met anderen. Gekopieerde en gedeelde code resulteert in een nulscore voor de volledige opdracht. Mocht je toch 
code gebruiken van online bronnen, maak dan gebruik van referenties.

### AI gebruik
Het gebruik van AI-tools zoals ChatGPT, Copilot, Bard, etc. is niet toegestaan. Het gebruik van deze tools kan tot verdere acties leiden, zoals het opstarten van een fraudedossier.
![Kader gebruik AI](Opgave/image.png)

### GIT en code conventies
Je werk wordt geëvalueerd op de gebruikelijke Git- en codeconventies. 

Hanteer een consistente programmeerstijl en maak passende commits bij het bouwen, wijziging of verwijderen 
van een onderdeel in het project. Er worden minstens **13 commits** verwacht. Voor elke klasse moet je dus een commit maken. Per niet gemaakte commit wordt er 0,5 punt afgetrokken van het eindtotaal.
We gaan rekening houden als je niet alle klassen kan implementeren.

### Indienen
- Dien jouw oplossing in via **GitHub Classroom** voor het einde van de afgesproken deadline. 
- Maak jouw online repository aan via de Classroom link op Leho.

## Opgave
### Start
Maak twee nieuwe projecten aan genaamd "Pre.PEJobApplicationSystem.Cons" en "Pre.PEJobApplicationSystem.Core". 

### Klassendiagram (/15)
Maak het onderstaande klassendiagram volgens de geziene leerstof na in C#.
![Klassendiagram](Opgave/UML.svg)

Zorg ervoor dat alle elementen correct geïmplementeerd zijn volgens de geziene leerstof.

### Uitwerking
Hieronder vind je een beschrijving van de verschillende klassen. Implementeer deze beschrijving op een correcte en logische manier zoals gezien in de lessen.

Alle properties moeten op een logische manier uitgewerkt worden.

#### Person
- **Email**: Zorg ervoor dat de string een "@"-teken bevat. Indien dit niet het geval is, moet je een Exception gooien met daarin de volgende tekst: "This is not a valid email address.".
- **GetInfo**: Deze methode geeft de volgende string terug: "{Type zoals Candidate/Recruiter} - Name: {FullName}, Email: {Email}".

#### Recruiter
- **PostJob**: Voeg een job toe aan de lijst van jobs (company). Je hoeft niet te controleren of de job al bestaat.
- **ReviewApplication**: Deze methode maakt een nieuw Interview-object aan en hij voegt de volgende feedback toe aan het interview: "Top!". Tot slot moet je dit interview toevoegen aan het object van "JobApplication".

#### Candidate
- **ApplyForJob**: Voeg een job toe aan de lijst van sollicitaties (jobApplications). Je hoeft niet te controleren of de job al bestaat.

#### Resume
- **AddExperience**: Voeg een ervaring toe aan de lijst van ervaringen (experiences). Controleer hierbij of de ervaring al in de lijst voorkomt (=.Contains). Indien het al bestaat, moet je de volgende Exception teruggeven: "Experience already exists";
- **AddSkill**: Voeg een skill toe aan de lijst van skills (skills). Hierbij moet je niet controleren of de skill al in de lijst voorkomt.

#### WorkExperience
- **GetExperienceInYears**: Hierbij moet het programma het aantal jaren teruggeven op basis van de start- en einddatum (enkel het jaartal is voldoende).

#### Skill
- **SetLevel**: Hierbij moet je controleren of het level tussen 1 en 5 ligt. Indien dit niet het geval is, moet je een ArgumentOutOfRangeException (The number must be between 1 and 5.) gooien.

#### Job
- **SetMin-/SetMaxSalary**: Het salaris moet altijd groter zijn dan 100 en kleiner dan 1.000.000. Indien dit niet het geval is, moet je een ArgumentOutOfRangeException gooien.
- **IsCandidateEligible**: Een kandidaat is geschikt als de persoon minstens tien JobApplications heeft.

#### Company
- **SetIndustry**: Hierbij moet je controleren of de industry één van de volgende waarden is: "IT" of "Other". Indien dit niet het geval is, moet je een ArgumentException gooien.
- **AddJob**: Voeg een job toe aan de lijst van jobs (jobs). Je hoeft niet te controleren of de job al bestaat.

#### JobApplication
- **SetApplicationDate**: De datum van de sollicitatie mag niet in de toekomst liggen. Indien dit wel het geval is, moet je een ArgumentOutOfRangeException (Application date cannot be in the future) gooien.
- **AddInterview**: Voeg een interview toe aan interview. Je hoeft niet te controleren of het interview al bestaat.

#### ApplicationManager
- **AddCandidate**: Voeg een kandidaat toe aan de lijst van kandidaten (candidates). Je hoeft niet te controleren of de kandidaat al bestaat.
- **AddRecruiter**: Voeg een recruiter toe aan de lijst van recruiters (recruiters). Je hoeft niet te controleren of de recruiter al bestaat.
- **AddJobApplication**: Voeg een jobapplicatie toe aan de lijst van jobapplicaties (jobApplications). Je hoeft niet te controleren of de jobapplicatie al bestaat.
- **GetApplicationForJob**: Het programma moet de eerste jobApplication teruggeven uit de lijst van jobApplications. Je moet dus geen LINQ-query gebruiken.

#### IApplicationManager
- **RegisterCandidate**: Voeg een kandidaat toe aan de lijst van kandidaten (candidates). Je hoeft niet te controleren of de kandidaat al bestaat.
- **RegisterCompany**: Voeg een bedrijf toe aan de lijst van bedrijven (companies). Je hoeft niet te controleren of het bedrijf al bestaat.
- **Apply**: Deze methode voegt de JobApplication toe aan de lijst van jobApplications (jobApplications). Je hoeft niet te controleren of de jobApplication al bestaat.
- **MatchCandidateToJobs**: Deze methode wordt in het onderdeel "Extension methods" verder uitgewerkt.
Maak in deze methode al een lijst aan van jobs. Gebruik de jobs van het eerste bedrijf om de lijst op te vullen.

#### Interview
- **AddFeedback**: Deze methode voegt feedback toe aan "feedback". De vorige waarde moet overschreven worden.


### Extension Methods (/5)
Implementeer de volgende extension methods in een aparte klasse genaamd "ApplicationManagerExtensions".
- **CalculateMatchScore**: Deze methode geeft een getal terug op basis van de volgende berekening: het aantal karakters van de volledige naam van de kandidaat + aantal skills van de kandidaat * 10. Deze methode moet toegepast worden op de klasse Candidate, samen met een extra parameter "Job". CalculateMatchScore(Candidate, Job)
- **FindBestMatches**: Deze methode krijgt twee parameters binnen, namelijk candidate en een lijst van beschikbare jobs en het moet op een lijst van jobs toegepast worden. De methode geeft de eerste drie jobs terug uit de lijst van jobs. Gebruik deze methode in de methode "MatchCandidateToJobs" uit de interface IApplicationManager. 


### Testen (/1)
De volgende zaken **moeten** getest worden in de console-applicatie:


- Maak een nieuwe instantie aan van ApplicationManager. Voeg hierin minstens één kandidaat en één recruiter toe.



Denk na over de juiste volgorde van de stappen.
