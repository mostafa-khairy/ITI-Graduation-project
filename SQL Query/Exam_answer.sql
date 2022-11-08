create proc Exam_St_answer @St_id int , @Ex_id int , @Q_id int , @St_Answer varchar(50)
as 
begin
	if exists (select St_id from Student where St_id = @St_id) 
		begin
			if exists(select Ex_id from Exam where Ex_id=@Ex_id)
				begin
					(select [Ex_id] , [Q_ID] , [Question_Bady] 
					from Exam_Question e , Question q
					where e.Ques_id = q.Q_ID and e.Ex_id = @Ex_id) 

					insert into Student_Exam ([St_id],[Ex_id],[Ques_id],[Date],[St_Answer])
					values (@St_id , @Ex_id , @Q_id ,GETDATE() , @St_Answer ) 
				end	
			else 
				select'this Exam does not exist'
		end
	else 
		select 'this student ID does not exist'
end



























































