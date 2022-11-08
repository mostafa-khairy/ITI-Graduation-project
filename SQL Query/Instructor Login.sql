create proc Instructor_Login @Email nvarchar(max) , @Pass int
as 
	if exists ( select Ins_Email , Ins_Password from Instractor where Ins_Email = @Email and Ins_Password = @Pass )
		begin
			select 1
		end
	else 
		select 0


