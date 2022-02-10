Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

Public Module koneksi
    Public con As Data.SqlClient.SqlConnection
    Public sqlcom As Data.SqlClient.SqlCommand
    Public sqldr As Data.SqlClient.SqlDataReader

    'membuka koneksi ke database
    Public Sub Openkoneksi()
        con = New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("trading").ConnectionString.ToString)
        con.Open()
    End Sub


    'mematikan koneksi ke database
    Public Sub CloseKoneksi()
        con.Dispose()
        con.Close()
        'con.Container.Remove(con)
        'con = Nothing
    End Sub

    'memasukkan record ke database
    Public Sub InsertRecord(ByVal sqlcommand As String)
        Openkoneksi()
        sqlcom = New Data.SqlClient.SqlCommand(sqlcommand, con)
        sqlcom.ExecuteNonQuery()
        CloseKoneksi()
    End Sub

    'mengambil record dari database
    Public Function SelectRecord(ByVal sqlcommand As String)
        Openkoneksi()
        sqlcom = New Data.SqlClient.SqlCommand(sqlcommand, con)
        'sqlcom.CommandType = CommandType.StoredProcedure
        sqldr = sqlcom.ExecuteReader()
        Return sqldr
        CloseKoneksi()
    End Function

    'meng-update
    Public Sub UpdateRecord(ByVal sqlcommand As String)
        Openkoneksi()
        sqlcom = New Data.SqlClient.SqlCommand(sqlcommand, con)
        sqlcom.ExecuteNonQuery()
        CloseKoneksi()
    End Sub

    'men-delete
    Public Sub DeleteRecord(ByVal sqlcommand As String)
        Openkoneksi()
        sqlcom = New Data.SqlClient.SqlCommand(sqlcommand, con)
        sqlcom.ExecuteNonQuery()
        CloseKoneksi()
    End Sub
End Module
