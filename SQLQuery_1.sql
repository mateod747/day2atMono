create table Peppers (
    PepperID int not null primary key,
    PepperName varchar(255)
);

insert into Peppers values(1,'California Reaper');
insert into Peppers values(2,'Jalapeno');
insert into Peppers values(3,'Moruga Scorpion');

select PepperName from Peppers;

create table PepperShops (
    ShopID int not null primary key,
    ShopName varchar(255)
);

insert into PepperShops values(1,'Pevec');
insert into PepperShops values(2,'Kaufland');
insert into PepperShops values(3,'Spar');

select * from PepperShops;

create table Pepper_Shops_Junction (
    PepperID int,
    ShopID int,
    constraint pepper_sho_pk primary key (PepperID, ShopID),
    foreign key (PepperID) references Peppers(PepperID),
    foreign key (ShopID) references PepperShops(ShopID)
);

insert into Pepper_Shops_Junction values(1,1);
insert into Pepper_Shops_Junction values(1,2);

insert into Pepper_Shops_Junction values(2,1);
insert into Pepper_Shops_Junction values(2,3);

insert into Pepper_Shops_Junction values(3,2);
insert into Pepper_Shops_Junction values(3,3);

select PepperName, ShopName
from Pepper_Shops_Junction
join Peppers on Peppers.PepperID = Pepper_Shops_Junction.PepperID
join PepperShops on PepperShops.ShopID = Pepper_Shops_Junction.ShopID;

select ShopName, PepperName
from Pepper_Shops_Junction 
join PepperShops on PepperShops.ShopID = Pepper_Shops_Junction.ShopID
join Peppers on Peppers.PepperID = Pepper_Shops_Junction.PepperID
where Peppers.PepperID = 1
