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

