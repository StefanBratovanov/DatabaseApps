IF OBJECT_ID('usp_GetProjectsByEmployee') IS NOT NULL
DROP PROC usp_GetProjectsByEmployee
GO

CREATE PROC usp_GetProjectsByEmployee(@firstName nvarchar(50), @lastName nvarchar(50))
AS
SELECT p.Name, p.Description, p.StartDate
FROM Employees e 
join EmployeesProjects ep on ep.EmployeeID = e.EmployeeID
join Projects p on p.ProjectID = ep.ProjectID
WHERE e.FirstName = @firstName and e.LastName = @lastName
GO

--exec usp_GetProjectsByEmployee 'ruth', 'ellerbrock' 
--go