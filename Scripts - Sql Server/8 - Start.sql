
insert into TB_TaskStatus
select 'pendente'
union select 'em produção' 
union select 'suspensa' 
union select 'finalizada'

insert into TB_Roles
select 0,'Master','Master'
union 
select 1,'Admin','Admin'

insert into dbo.TB_User
values( 'master','master@gmail.com','12345')

insert into dbo.TB_UserRoles
select 1,0
