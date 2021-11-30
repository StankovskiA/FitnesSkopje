use [aspnet-FitnesSkopjeWebApp-20211109102850]

create table Users(
Id int identity(1,1) primary key(id),
firstName nvarchar(100),
lastName nvarchar(100),
email nvarchar(100) unique,
phoneNumber nvarchar(50),
roleId nvarchar(128) foreign key (roleId) references AspNetRoles(Id)
);


create table Favourites(
userId int not null,
gymId int not null
);

alter table Favourites
add constraint FK_Users foreign key(userId) references Users(Id);

alter table Favourites
add constraint FK_Gyms foreign key(gymId) references Gyms(Id);