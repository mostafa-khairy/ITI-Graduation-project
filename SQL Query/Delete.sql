-- Delete Student Course


alter proc Delete_St_Crs @St_Id int ,  @Crs_id int
as
begin try
	if exists(select St_id , Crs_id from Stud_Course where St_id = @St_Id and Crs_id =@Crs_id) 
		begin
			delete from Stud_Course
			where  St_id = @St_Id and Crs_id =@Crs_id
		end
	else 
	select 'this course & this student doe not exist'
end try
begin catch
	select 'error'
end catch


---------------------------------------------------------------------------------------------------------------------
-- Delete Department

create proc Delete_Department @Dept_Id int 
as
begin try
	if exists(select Dept_id from Department where Dept_id = @Dept_Id ) 
		begin
			delete from Department
			where  Dept_id = @Dept_Id 
		end
	else 
		select 'this Department doe not exist'
end try
begin catch
	select ERROR_MESSAGE()
end catch

--Delete_Department 1
--Delete_Department 8

---------------------------------------------------------------------------------------------------------------------

-- Delete Question and Choice

create proc Delete_Question @Q_Id int 
as
begin try
	if exists(select Q_ID from Question where Q_ID = @Q_Id ) 
		begin
			delete from Choices
			where  Q_ID = @Q_Id 


			delete from Question
			where  Q_ID = @Q_Id 
		end
	else 
		select 'this Question doe not exist'
end try
begin catch
	select ERROR_MESSAGE()
end catch


-- Delete_Question 118

---------------------------------------------------------------------------------------------------------------------



















---------------------------------------------------------------------------------------------------------------------















---------------------------------------------------------------------------------------------------------------------



















---------------------------------------------------------------------------------------------------------------------











































