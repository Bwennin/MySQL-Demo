Imports MySql.Data.MySqlClient

Module Program

    Sub Main()
        Dim strUser As String
        Dim strPassword As String
        Dim strConnection As String

        Console.Write("User: ")
        strUser = Console.ReadLine()
        Console.Write("Password: ")
        strPassword = Console.ReadLine()

        strConnection = $"server=localhost;user={strUser};database=sakila;port=3306;password={strPassword}"

        Using oConnection As New MySqlConnection(strConnection)
            Try
                Console.WriteLine("Connecting to MySQL..." & vbNewLine)
                oConnection.Open()

                Dim strSQL As String = "SELECT * FROM sakila.actor LIMIT 0,10;"
                Dim cmd As New MySqlCommand(strSQL, oConnection)
                Dim rdr As MySqlDataReader = cmd.ExecuteReader()

                While rdr.Read()
                    Console.WriteLine($"{rdr(0):00} - {rdr(1)} {rdr(2)}")
                End While

                Console.WriteLine()
            Catch ex As InvalidOperationException
                Console.WriteLine("Unable to load data. Make sure your database connection is running.")
            Catch ex As Exception
                Console.WriteLine(ex.ToString())

                If ex.InnerException IsNot Nothing Then
                    Console.WriteLine(ex.InnerException.ToString())
                End If
            Finally
                oConnection.Close()
                Console.WriteLine("Connection closed. Press any key to exit...")
                Console.ReadLine()
            End Try
        End Using
    End Sub

End Module
