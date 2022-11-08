create proc Student_Login @Email nvarchar(max) , @Pass int
as 
	if exists ( select St_Email , St_Password from Student where St_Email = @Email and St_Password = @Pass )
		begin
			select 1
		end
	else 
		select 0


 Student_Login 'mostafakahiey@gmail.com'  , 151
  Student_Login 'dfassaafsadfsadfs'  , 151