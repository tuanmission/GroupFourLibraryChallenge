Create Table Book(
BookKeyId integer identity(1,1) primary key,
BookId varchar(50),
BookTitle varchar(50),
Author varchar(50),
Publisher varchar(50)
);

Create Table BookStore(
BookStoreID integer identity(1,1) primary key,
BookStoreAddress varchar(50),
BookStorePostCode integer,
BookStoreName varchar(75)
);

Create Table BookCopy(
BookCopyID integer identity(1,1) primary key,
BookId integer,
BookStoreID integer,
isReserved bit
constraint fk_column
foreign key (BookId) references Book(BookKeyId)
foreign key (BookStoreID) references BookStore(BookStoreID)
);

Create Table Reservation(
ReservationID integer identity(1,1) primary key,
BookCopyID integer,
DateReserved date,
DateReturned date
foreign key (BookCopyID) references BookCopy(BookCopyID)
)