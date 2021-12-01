alter table Favourites 
add constraint FK_GymId foreign key (gymId) references Gyms (Id);