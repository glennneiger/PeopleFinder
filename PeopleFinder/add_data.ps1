$firstName = Read-Host 'Please enter the new person''s first name'
$lastName = Read-Host 'Please enter the new person''s last name'
$age = Read-Host 'Please enter the new person''s age'
$address = Read-Host 'Please enter the new person''s address'
$interests = Read-Host 'Please enter the new person''s interests (comma delimited)'

Add-Type -Path ".\System.Data.SQLite.dll"
$con = New-Object -TypeName System.Data.SQLite.SQLiteConnection
$con.ConnectionString = "Data Source=" + $PSScriptRoot + "\people.db"
$con.Open()

$sql = $con.CreateCommand()
$sql.CommandText = "
INSERT INTO Persons (FirstName, LastName, Age, Address, Interests)
VALUES (@firstName, @lastName, @age, @address, @interests)";
$sql.Parameters.AddWithValue("@firstName", $firstName)
$sql.Parameters.AddWithValue("@lastName", $lastName)
$sql.Parameters.AddWithValue("@age", $age)
$sql.Parameters.AddWithValue("@address", $address)
$sql.Parameters.AddWithValue("@interests", $interests)

$sql.ExecuteNonQuery()
Write-Output "Added new users"