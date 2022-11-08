create proc all_St_in_Dep# @Dep_id int
as 
begin 
	if exists(select Dept_id from Department where Dept_id = @Dep_id)
		begin
			select * from Student where Dept_id =  @Dep_id
		end
	else
		select 'this department does not exist'
end

-- all_St_in_Dep# 2
-- all_St_in_Dep# 100

--------------------------------------------------------------------------------------------------------------------

alter proc All_St_Grade @St_id int
as 
begin 
	if exists(select St_id from Student where St_id = @St_id)
		begin
			select sc.St_id , s.St_FName, s.St_Lname ,c.Crs_Name ,sc.Crs_Grade from Stud_Course sc ,
			Student s ,Course c  where sc.St_id = s.St_id and c.Crs_id = sc.Crs_id  and sc.St_id =  @St_id
		end
	else
		select 'this Student does not exist'
end

-- All_St_Grade 1

-------------------------------------------------------------------------------------------------------------------

alter proc Ins_Crs_#St  @ins_id int
as 
begin 
	if exists(select Ins_id from Instractor where Ins_id = @ins_id)
		begin
			select c.Crs_Name , COUNT(sc.St_id)   from Course c , Stud_Course sc , Ins_course ic 
			where C.Crs_id = SC.Crs_id 
			and c.Crs_id=ic.Crs_id and  Ins_id = @ins_id
			group by c.Crs_Name
		end
	else
		select 'this instractor does not exist'
end

-- Ins_Crs_#St 4


-----------------------------------------------------------------------------------------------------------------

alter proc TopicForCrs  @Crs_id int
as 
begin 
	if exists(select Crs_id from Course where Crs_id = @Crs_id)
		begin
			select Top_Name from Topic t , Course c where t.Top_id = c.Top_id and Crs_id = @Crs_id
		end
	else
		select 'this Course does not exist'
end



-------------------------------------------------------------------------------------------------------------------


create proc Exam_Questions  @Ex_id int
as 
begin 
	if exists(select Ex_id from Exam where Ex_id = @Ex_id)
		begin
			select q.Question_Bady from Question q , Exam_Question eq where q.Q_ID = eq.Ques_id  and Ex_id = @Ex_id
		end
	else
		select 'this Exam does not exist'
end



---------------------------------------------------------------------------------------------------------------- 

alter proc St_Exam_Q  @Ex_id int , @St_id int 
as 
begin 
	if exists(select Ex_id , St_id from Student_Exam where Ex_id = @Ex_id and St_id=@St_id )
		begin
			select q.Question_Bady , Se.St_Answer from Student_Exam se , Question q where q.Q_ID = se.Ques_id  
			and Ex_id = @Ex_id and St_id = @St_id
		end
	else
		select 'Error'
end


--St_Exam_Q  2,1




































