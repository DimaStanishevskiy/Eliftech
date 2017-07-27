create table Companies(
	Id int primary key identity,
	Name nvarchar(50) not null,
	EstimatedEarnings int not null,
	FatherCompany_Id int foreign key references Companies(Id)
)