-- update Topic 

create Proc update_Topic @Topic_Id int , @Topic_Name nvarchar(50)
as
begin try
	if exists (select Top_id from Topic where Top_id=@Topic_Id )
		begin
			update Topic
			set Top_Name = @Topic_Name
			 where Top_id=@Topic_Id
	    end
    else
        select 'Topic Id not found'
end try
begin catch
    Select ERROR_MESSAGE()
end catch

-- update_Topic 5 , vuiu

---------------------------------------------------------------------------------------------------------
-- update Question

create Proc update_Question  @Q_Id int , @Q_Bady nvarchar(max) , @Q_Type nvarchar(20) ,
@Model_Ans nvarchar(250) , @Point int , @Crs_ID int
with encryption
as
    begin try
    if  exists (select Q_ID from Question where Q_ID = @Q_Id )
        begin
			update Question
			set [Q_Type] = @Q_Type , [Question_Bady] = @Q_Bady , [Model_Answer] = @Model_Ans ,
				[Point] = @Point, [Crs_ID] = @Crs_ID
			where Q_ID = @Q_Id
        end
    else
        select 'Question ID Not Found'
    end try
begin catch
	Select ERROR_MESSAGE()
end catch

---------------------------------------------------------------------------------------------------------
-- update Student
 
alter Proc update_Student @st_id int , @st_Fname nvarchar(50) , @st_Lname nvarchar(50) , @st_Email varchar(100),
@st_password int , @dept_id int , @St_Address nvarchar(50)
as
begin try
	if  exists (select st_id from Student where St_id = @st_id )
		begin
			update Student
			 set St_FName = @st_Fname , St_Lname = @st_Lname , St_Email = @st_Email
				, St_Password = @st_password , dept_id = @dept_id , St_Address = @St_Address
			 where St_id = @st_id
		end
    else
        select 'Student Id is not found'
end try
begin catch
    Select ERROR_MESSAGE()
end catch

-- update_Student 111 , Mostafa , Khairy , 'Mostafakhairy109@gmail.com' , 151 , 2 , Sohag

---------------------------------------------------------------------------------------------------------

-- update instructor
 
create Proc update_instructor @ins_id int , @ins_Fname nvarchar(50) , @ins_Lname nvarchar(50) , @ins_Email varchar(100),
@ins_password int , @dept_id int , @ins_Address nvarchar(50)
as
begin try
	if  exists (select Ins_id from Instractor where Ins_id = @ins_id )
		begin
			update Instractor
			 set Ins_FName = @ins_Fname , ins_Lname = @ins_Lname , ins_Email = @ins_Email
				, ins_Password = @ins_password , dept_id = @dept_id , ins_Address = @ins_Address
			 where ins_id = @ins_id
		end
    else
        select 'instuctor Id is not found'
end try
begin catch
    Select ERROR_MESSAGE()
end catch

---------------------------------------------------------------------------------------------------------






















































































