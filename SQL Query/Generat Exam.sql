alter proc Generat_Exam @Exam_id int , @Cource_name nvarchar(50) , @No_TF int , @No_MCQ int 
as 
begin 
	if exists(select Ex_id from Exam where Ex_id = @Exam_id) 
		select 'this exam id already exast '	
	else 
		begin 
			if exists (select Crs_Name from Course where Crs_Name = @Cource_name)
				begin 
					declare @Crs_id int  = (select Crs_id from Course where Crs_Name = @Cource_name)

					insert into Exam ([Ex_id],[No_TF],[No_MCQ],[Crs_id])
					values (@Exam_id , @No_TF , @No_MCQ , @Crs_id )

					insert into Exam_Question (Ex_id , Ques_id )
					select top(@No_TF) @Exam_id , Q_id
					from Question
					where Q_Type = 'TF' and Crs_ID = @Crs_id 
					order by newid()

					insert into Exam_Question (Ex_id , Ques_id )
					select top(@No_MCQ) @Exam_id , Q_id
					from Question   
					where Q_Type = 'MCQ' and Crs_ID = @Crs_id 
					order by newid()
				end
			else 
				select 'this course is not exist'
		end
end
