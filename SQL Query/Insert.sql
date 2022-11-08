-- insert student 
alter proc Insert_St @id int , @fname nvarchar(50) , @lname nvarchar(50) , @dept_id int ,@email nvarchar(50) , @password int 
as 
	begin try 
		insert into Student ([St_id],[St_FName],[St_Lname],[Dept_id],[St_Email],[St_Password])
		values (@id , @fname , @lname , @dept_id ,@email, @password)
	end try
	begin catch
		select 'error'
	end catch

--Insert_St 111 , 'mostafa' , 'khairy' , 1 , 'mostafakahiey@gmail.com' , 151


----------------------------------------------------------------------------------------------

--insert instractor 

alter proc Insert_ins @id int , @fname nvarchar(50) , @lname nvarchar(50) , @dept_id int ,@email nvarchar(50) , @password int 
as 
	begin try 
		insert into Instractor ([Ins_id],[Ins_FName],[Ins_LName],[Dept_id],[Ins_Email],[Ins_Password])
		values (@id , @fname , @lname , @dept_id , @email , @password)
	end try
	begin catch
		select 'error'
	end catch

--Insert_ins 22 , 'mohamed' , 'ahmed' , 2 , 'mohamedahmed@gmail.com' , 365

--------------------------------------------------------------------------------------------------------

-- insert student cource
create proc insert_St_Crs @St_Id int ,  @Crs_id int
as
begin try
	if exists(select St_id , Crs_id from Stud_Course where St_id = @St_Id and Crs_id =@Crs_id) 
		begin
			select 'this course is already assigned to this student '
		end	
	else 
	insert into Stud_Course (St_id , Crs_id)
	values(@St_Id ,  @Crs_id)
end try
begin catch
	select 'error'
end catch



--------------------------------------------------------------------------------------------------------

-- insert instructor cource
create proc insert_ins_Crs @ins_Id int ,  @Crs_id int
as
begin try
	if exists(select ins_id , Crs_id from Ins_course where ins_id = @ins_Id and Crs_id =@Crs_id) 
		begin
			select 'this course is already assigned to this instructor '
		end	
	else 
		insert into ins_Course (ins_id , Crs_id)
		values(@ins_Id ,  @Crs_id)
end try
begin catch
	select 'error'
end catch



-----------------------------------------------------------------------------------------------------------------
/*
create proc insert_Question @Q_id int ,@Q_Type nvarchar(50) , @Q_point int =  5 , @Q_body nvarchar(max) , 
@Crs_id int , @Model_Answer nvarchar(max)

as
begin 
	if exists (select Crs_id from Course where Crs_id = @Crs_id )
		begin
			if not exists (select Q_ID from Question where Q_ID = @Q_id)
				begin	
					insert into Question ([Q_ID],[Question_Bady],[Q_Type],[Point] , [Model_Answer],[Crs_ID])
					values(@Q_id , @Q_body ,@Q_Type , @Q_point , @Model_Answer , @Crs_id )
				end
			else
				select 'this Question id already exist' 
		end
	else 
		select 'this course doen not exist'
end		


--insert_Question 116 , MCQ , 5 ,'which of the following is not a keyword?' , 700 , Eval


-----------------------------------------------------------------------------------------------------------------

alter proc insert_Question @Q_id int ,@Q_Type nvarchar(50)  , @Q_body nvarchar(max) , 
@Crs_id int , @Model_Answer nvarchar(max) , @choice_1 nvarchar(max), @choice_2 nvarchar(max), @choice_3 nvarchar(max),@choice_4 nvarchar(max)

as
begin 
	if exists (select Crs_id from Course where Crs_id = @Crs_id )
		begin
			if not exists (select Q_ID from Question where Q_ID = @Q_id)
				begin	
					insert into Question ([Q_ID],[Question_Bady],[Q_Type] , [Model_Answer],[Crs_ID])
					values(@Q_id , @Q_body ,@Q_Type  , @Model_Answer , @Crs_id )

					insert into Choices ([Q_ID],[Choice])
					values (@Q_id,@choice_1),(@Q_id,@choice_2),(@Q_id,@choice_3),(@Q_id,@choice_4)
				end
			else
				select 'this Question id already exist' 
		end
	else 
		select 'this course doen not exist'
end		


insert_Question 116 , MCQ  ,'which of the following is not a keyword?' , 700 , 'Eval' ,Eval ,Assert ,Nonlocal , Pass
*/

-----------------------------------------------------------------------------------------------------------------


-- insert question and choices 

alter proc insert_Question @Q_id int ,@Q_Type nvarchar(50)  , @Q_body nvarchar(max) , 
@Crs_id int , @Model_Answer nvarchar(max) , @choice_1 nvarchar(max), @choice_2 nvarchar(max), @choice_3 nvarchar(max),@choice_4 nvarchar(max)

as
begin 
	if exists (select Crs_id from Course where Crs_id = @Crs_id )
		begin
			if not exists (select Q_ID from Question where Q_ID = @Q_id)
				begin	
					insert into Question ([Q_ID],[Question_Bady],[Q_Type] , [Model_Answer],[Crs_ID])
					values(@Q_id , @Q_body ,@Q_Type  , @Model_Answer , @Crs_id )

					if @Q_Type = 'TF'
						begin 
							insert into Choices ([Q_ID],[Choice])
							values (@Q_id,@choice_1),(@Q_id,@choice_2)
						end
					else
						begin
							insert into Choices ([Q_ID],[Choice])
							values (@Q_id,@choice_1),(@Q_id,@choice_2),(@Q_id,@choice_3),(@Q_id,@choice_4)
						
						end
				end
			else
				select 'this Question id already exist' 
		end
	else 
		select 'this course doen not exist'
end		

--insert_Question 116 , MCQ  ,'which of the following is not a keyword?' , 700 , 'Eval' ,Eval ,Assert ,Nonlocal , Pass


--insert_Question 117 , TF  ,'Mathematical operations can be performed on a string?' , 700 , 'FALSE' , TRUE ,FALSE ,Nonlocal , Pass


-----------------------------------------------------------------------------------------------------------------
-- insert topic 
alter proc Insert_topic @Topic_id int , @Topic_Name nvarchar(50) 
as 
	begin  
		if not exists (select Top_id from Topic where Top_id = @Topic_id )
			begin 
				insert into topic ([Top_id],[Top_Name])
				values (@Topic_id , @Topic_Name)
			end
		else 
			select 'this topic already exists'
	end 

-- Insert_topic 1 , 6526

-----------------------------------------------------------------------------------------------------------------

-- insert department  
create proc Insert_department @dept_id int , @dept_Name nvarchar(50) 
as 
	begin  
		if not exists (select dept_id from Department where dept_id = @dept_id )
			begin 
				insert into Department([dept_id],[dept_Name])
				values (@dept_id , @dept_Name)
			end
		else 
			select 'this Department already exists'
	end 

-- Insert_department 1 , 6526

-----------------------------------------------------------------------------------------------------------------


















