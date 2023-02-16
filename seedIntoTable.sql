insert into Book
(BookId, BookTitle,Author,Publisher)
values ('9b0896fa-3880-4c2e-bfd6-925c87f22878','CQRS for Dummies','James Richards','Penguin');


insert into Book
(BookId, BookTitle,Author,Publisher)
values ('0550818d-36ad-4a8d-9c3a-a715bf15de76','Visual Studio Tips','Louis Johnston','Elsiver');


insert into Book
(BookId, BookTitle,Author,Publisher)
values ('8e0f11f1-be5c-4dbc-8012-c19ce8cbe8e1','NHibernate Cookbook','Sam Thomas','Puffer Books');

insert into BookStore
(BookStoreAddress, BookStorePostCode, BookStoreName)
values('12 Brindalee Place Footscray', 3043, 'Gregs Book');


insert into BookStore
(BookStoreAddress, BookStorePostCode, BookStoreName)
values('20 Cardigan Street Carlton', 3003, 'Carlton Book Corner');


insert into BookStore
(BookStoreAddress, BookStorePostCode, BookStoreName)
values('40 Grove Street Avondale Heights', 3042, 'The Book Heights');


insert into BookCopy
(BookId, BookStoreID, isReserved)
values (3,2,0);


insert into BookCopy
(BookId, BookStoreID, isReserved)
values (3,2,1);

insert into BookCopy
(BookId, BookStoreID, isReserved)
values (3,3,0);

insert into BookCopy
(BookId, BookStoreID, isReserved)
values (3,3,1);

insert into BookCopy
(BookId, BookStoreID, isReserved)
values (3,2,0);

insert into BookCopy
(BookId, BookStoreID, isReserved)
values (3,3,1);


insert into BookCopy
(BookId, BookStoreID, isReserved)
values (3,4,0);


insert into BookCopy
(BookId, BookStoreID, isReserved)
values (1,4,1);

insert into BookCopy
(BookId, BookStoreID, isReserved)
values (1,3,0);

insert into BookCopy
(BookId, BookStoreID, isReserved)
values (1,3,1);

insert into BookCopy
(BookId, BookStoreID, isReserved)
values (1,2,0);

insert into BookCopy
(BookId, BookStoreID, isReserved)
values (1,3,0);


insert into BookCopy
(BookId, BookStoreID, isReserved)
values (2,4,1);

insert into BookCopy
(BookId, BookStoreID, isReserved)
values (2,2,0);

insert into BookCopy
(BookId, BookStoreID, isReserved)
values (2,3,1);

insert into BookCopy
(BookId, BookStoreID, isReserved)
values (2,2,0);

insert into BookCopy
(BookId, BookStoreID, isReserved)
values (2,3,1);


Select *
from BookCopy;


insert into Reservation
(BookCopyID, DateReserved, DateReturned)
values(5, '2023-01-04','2023-01-07');


Select * from Book;

Select B.BookTitle, BS.BookStoreName, BC.TotalCopies, BC.CopiesAvailable from Book B 
join BookCopy BC on BC.BookId = B.BookKeyId
join BookStore BS on BS.BookStoreID =BC.BookStoreID;


Select * from Reservation;





Select * 
from BookCopy;

delete from Reservation;

delete from BookCopy;



ALTER TABLE BookCopy
Add TotalCopies integer

Alter Table BookCopy
Drop column isReserved;


'80f6233b-44fd-422e-8c82-da8db1c6548c'

'83a070d6-165c-4862-9c31-058650fd3427'



insert into Reservation 
(BookCopyID, DateReserved, DateReturned, UserId)
values(19, '2023-02-18', '2023-02-23','80f6233b-44fd-422e-8c82-da8db1c6548c');

insert into Reservation 
(BookCopyID, DateReserved, DateReturned, UserId)
values(18, '2023-02-16', '2023-02-24','80f6233b-44fd-422e-8c82-da8db1c6548c');


insert into Reservation
(BookCopyID, DateReserved, DateReturned, UserId)
values(20, '2023-03-01', '2023-03-10','83a070d6-165c-4862-9c31-058650fd3427');


insert into Reservation
(BookCopyID, DateReserved, DateReturned, UserId)
values(19, '2023-03-02', '2023-03-09','83a070d6-165c-4862-9c31-058650fd3427');


Select *
from Reservation;

Select U.username, B.BookTitle, R.DateReserved, R.DateReturned
from AspNetUsers U join Reservation R on R.UserId =U.Id
join BookCopy BC on R.BookCopyID =BC.BookCopyID
join Book B on BC.BookId =B.BookKeyId;


alter table Reservation 
add DueDate date;

update Reservation
set DueDate='2023-02-23'
where ReservationID=2;


update Reservation
set DueDate='2023-02-24'
where ReservationID=3;


update Reservation
set DateReturned=null
where ReservationID=3;

update Reservation
set DateReturned=null
where ReservationID=5;


update Reservation
set DueDate='2023-03-09'
where ReservationID=5;



