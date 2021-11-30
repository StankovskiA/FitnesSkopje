alter table Reviews
add constraint FK_GymIdInReviews foreign key (gymId) references Gyms(Id);