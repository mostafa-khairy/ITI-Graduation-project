alter proc Exam_corr @St_id int , @Ex_id int 
as 
begin
	declare @No_Q int = (select count(Ques_id) from Student_Exam WHERE St_id =1 and Ex_id = 1 )

	declare @Right_Q float = (select count(s.Ques_id) from Student_Exam s join Question q on s.Ques_id=q.Q_ID
	where s.St_id = 1 and s.Ex_id = 1 and s.St_Answer = q.Model_Answer )

	declare @Grade float 
	select @Grade = (@Right_Q/@No_Q) *100
 
	update Stud_Course 
	set [Crs_Grade] = ( str(@Grade) + '%' )
	where St_id= @St_id  and Crs_id = (select Crs_id from Exam where Ex_id = @Ex_id) 
 
end








































































