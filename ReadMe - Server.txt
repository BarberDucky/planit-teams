-------------------------------------------
                  MESSAGES
-------------------------------------------

Sve poruke su organizovane u sledecem formatu:

- MessageEntity MessageEntity - O kom entitetu se radi (Board, Card itd)
- MessageType MessageType - Koje vrsta akcije je uzorkovala promenu (Create, Update itd)
- NEKI PODACI Data - Odgovarajuci DTO koji nosi podatke

Generalno razlikujemo dve vrste poruka:
 1. Poruke koje nose u Data delu nose neki DTO
 2. Poruke koje u Data delu nose samo id odgovarajuceg objekta

Poruke 2. tipa su sve poruke koje podrazumevaju brisanje (MessageType je Delete)
i sve BoardNotifications poruke (MessageEntity je tipa Board, MessageType BoardNotification),
sve ostale poruke su tipa 1.

-------------------------------------------
          GDE NACI INFORMACIJE
-------------------------------------------

- Svi tipovi poruka, kao i enumi MessageEntity i MessageType
  nalaze se u planit-data -> RabbitMQ -> MQMessages
- Svi odgovarajuci DTO se nalaze u odgovarajucim DTO file-ovima,
  po zelji dodati dodatne atribute DTO-ovima

-------------------------------------------
          SPECIFICNE PORUKE
-------------------------------------------

1. Board kanal:
   Sve poruke koje nose promenu na datom board-u
   Moguci MessageEntity - Board, Card, CardList, Comment
   Moguci MessageType - Create, Update, Delete, Move(za Card)

2. User kanal:

   2.1 Pri dodavanju user-a na board: 
	MessageEntity - Board
	MessageType - Create
	Data - BasicBoardDTO

   2.2 Pri brisanju user-a sa board-a ili pri brisanju celog board-a: 
	MessageEntity - Board
	MessageType - Delete
	Data - idBoard

   2.3 Pri bilo kakvoj promeni na board-u: 
	MessageEntity - Board
	MessageType - BoardNotification
	Data - idBoard

   2.4 Pri pomeranju kartice (Move operacija): 
	MessageEntity - Notification
	MessageType - Move
	Data - ReadNotificationDTO

   2.4 Pri bilo kojoj operaciji na posmatranoj kartici (Change operacija): 
	MessageEntity - Notification
	MessageType - Change
	Data - ReadNotificationDTO

-------------------------------------------
          POST POVRATNI TIPOVI
-------------------------------------------

Pri kreiranju Card List, Card i Comment povratni tip je odgovarajuci BasicDTO
Pri kreiranju Board-a povratni tip je ShortBoardDTO - to je ono sto vam ide sa strane
Ukoliko kreiranje ne uspe vraca se null

-------------------------------------------
          KORISNE INFO
-------------------------------------------

- Ako se baci exception, dobija se 500 response
- Ne zaboraviti da promenite password u web.config file-ovima,
  promena se vrsi na 2 mesta: api i data
- Najbolje dropovati celu bazu i pokrenuti migraciju sa:
  update-database
  (pre toga postaviti projekat u konzoli na planit-data)
- Postoji sansa da nece da radi neka ruta (kaze nesto tipa GET metoda ne postoji), 
  ako ste sigurni da ruta postoji i da ste je lepo pozvali probajte da dodate
  [Route(nesto)] u api ako nije definisano
- Sve rute sem registracije su autorizovane, 
  saljete token pri svakom pozivu,
  nikada nece dobiti userId, 
  ako treba da dodate nekog usera na board napr koristite username njegov
- Ako nesto ne radi, baci ex ili se ponasa kako ne treba
  ili pokusajte da popravite (ali preporucujem da ne gubite vreme sa time)
  ili zapisite negde pa cu da popravim kad se vratim
  