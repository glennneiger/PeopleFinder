$search = Read-Host 'Please enter a name to search for'

Add-Type -Path ".\System.Data.SQLite.dll"
$con = New-Object -TypeName System.Data.SQLite.SQLiteConnection
$con.ConnectionString = "Data Source=" + $PSScriptRoot + "\people.db"
$con.Open()

$sql = $con.CreateCommand()
$sql.CommandText = "
SELECT *
FROM Persons as p
WHERE FirstName = '" + $search + "'
	OR LastName = '" + $search + "';"

$adapter = New-Object -TypeName System.Data.SQLite.SQLiteDataAdapter $sql
$data = New-Object System.Data.DataSet
$adapter.Fill($data)

foreach($t in $data.Tables){
	Write-Output $t
}