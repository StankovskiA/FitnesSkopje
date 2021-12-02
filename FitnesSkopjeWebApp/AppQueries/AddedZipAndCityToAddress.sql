use [aspnet-FitnesSkopjeWebApp-20211109102850];

select Address from Gyms;

--NE CITA MK 
update Gyms
set Address = concat(Address, ', 1000 —копЉе');


update Gyms
set Address = replace(Address, '??????', 'Skopje');



