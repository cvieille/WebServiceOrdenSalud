Public Class Conexion
    '-----------------------------------------------------------------------------------------------------------------------------------------
    'CADENA DE CONEXION A LA BASE DE DATOS
    '-----------------------------------------------------------------------------------------------------------------------------------------
    'Public cadena_conexion As String = "Data Source = 10.6.180.176\SQLEXPRESS; Initial Catalog = anatomia_patologica; Persist Security Info=True;User ID=sa;Password=Anatomia2016;Connect Timeout=30"

    Public cadena_conexion As String = "Data Source = 10.6.180.176\SQLEXPRESS; Initial Catalog = anatomia_patologica; Persist Security Info=True;User ID=sa;Password=Anatomia2016;Connect Timeout=30"



#Region "CRUD"
    '-----------------------------------------------------------------------------------------------------------------------------------------
    ' FUNCION QUE EJECUTA LAS CONSULTAS A LA BASE DE DATOS Y DEVUELVE UN DATATABLE 
    '-----------------------------------------------------------------------------------------------------------------------------------------
    Public Function consulta_sql_datatable(ByVal consulta_sql3 As String)
        Dim cnnBaseDat As SqlClient.SqlConnection
        Dim comBaseDat As SqlClient.SqlCommand
        Dim adpBaseDat As SqlClient.SqlDataAdapter

        Dim dstTablas As DataSet
        cnnBaseDat = New SqlClient.SqlConnection With {
            .ConnectionString = cadena_conexion
        }
        cnnBaseDat.Open()

        comBaseDat = New SqlClient.SqlCommand(consulta_sql3, cnnBaseDat)
        adpBaseDat = New SqlClient.SqlDataAdapter(comBaseDat)
        dstTablas = New DataSet
        adpBaseDat.Fill(dstTablas, "MiTabla")

        Dim tablas As Data.DataTable = dstTablas.Tables("MiTabla")

        cnnBaseDat.Close()
        comBaseDat.Connection.Close()

        Return tablas

    End Function

    '-----------------------------------------------------------------------------------------------------------------------------------------
    ' FUNCION QUE EJECUTA LAS CONSULTAS A LA BASE DE DATOS Y DEVUELVE UN DATAADAPTER
    '-----------------------------------------------------------------------------------------------------------------------------------------
    Public Function consulta_sql_dataAdapter(ByVal consulta_sql3 As String)
        Dim cnnBaseDat As SqlClient.SqlConnection
        Dim comBaseDat As SqlClient.SqlCommand
        Dim adpBaseDat As SqlClient.SqlDataAdapter

        Dim dstTablas As DataSet
        cnnBaseDat = New SqlClient.SqlConnection With {
            .ConnectionString = cadena_conexion
        }
        cnnBaseDat.Open()

        comBaseDat = New SqlClient.SqlCommand(consulta_sql3, cnnBaseDat)
        adpBaseDat = New SqlClient.SqlDataAdapter(comBaseDat)
        dstTablas = New DataSet
        adpBaseDat.Fill(dstTablas, "MiTabla")

        cnnBaseDat.Close()
        comBaseDat.Connection.Close()

        Return adpBaseDat

    End Function

    '-----------------------------------------------------------------------------------------------------------------------------------------
    ' FUNCION QUE EJECUTA LAS CONSULTAS A LA BASE DE DATOS Y DEVUELVE UN STRING 
    '-----------------------------------------------------------------------------------------------------------------------------------------
    Public Function consulta_sql_devuelve_string(ByVal consulta_sql2 As String)
        Dim cnnBaseDat As SqlClient.SqlConnection
        Dim comBaseDat As SqlClient.SqlCommand
        Try

            cnnBaseDat = New SqlClient.SqlConnection With {
                .ConnectionString = cadena_conexion
            }
            cnnBaseDat.Open()

            comBaseDat = New SqlClient.SqlCommand(consulta_sql2, cnnBaseDat)

            Dim sdato As String
            sdato = CStr(comBaseDat.ExecuteScalar())

            cnnBaseDat.Close()
            comBaseDat.Connection.Close()

            Return sdato

        Catch ex As Exception
            Dim fail As String = ex.Message & ex.Source & ex.StackTrace
        End Try

    End Function

    '-----------------------------------------------------------------------------------------------------------------------------------------
    ' FUNCION QUE EJECUTA LAS CONSULTAS A LA BASE DE DATOS
    '-----------------------------------------------------------------------------------------------------------------------------------------
    Public Sub ejecuta_sql(ByVal consulta_sql As String)
        Dim bool As String = "True"
        Try
            Dim cnnBaseDat As SqlClient.SqlConnection
            Dim comBaseDat As SqlClient.SqlCommand

            cnnBaseDat = New SqlClient.SqlConnection With {
                .ConnectionString = cadena_conexion
            }
            cnnBaseDat.Open()

            comBaseDat = New SqlClient.SqlCommand(consulta_sql, cnnBaseDat)
            comBaseDat.ExecuteNonQuery()

            cnnBaseDat.Close()
            comBaseDat.Connection.Close()
        Catch ex As Exception
            bool = ex.Message
        End Try
    End Sub

    '-----------------------------------------------------------------------------------------------------------------------------------------
    ' FUNCION QUE EJECUTA LAS CONSULTAS A LA BASE DE DATOS Y DEVUELVE SI ID 
    '-----------------------------------------------------------------------------------------------------------------------------------------
    Public Function ejecuta_sql_devuelve_identidad(ByVal consulta_sql2 As String)
        Dim cnnBaseDat As SqlClient.SqlConnection
        Dim comBaseDat As SqlClient.SqlCommand

        cnnBaseDat = New SqlClient.SqlConnection With {
            .ConnectionString = cadena_conexion
        }
        cnnBaseDat.Open()

        comBaseDat = New SqlClient.SqlCommand(consulta_sql2, cnnBaseDat)
        Dim n As String
        n = CStr(comBaseDat.ExecuteScalar())

        comBaseDat = New SqlClient.SqlCommand("SELECT @@IDENTITY ", cnnBaseDat)
        n = CStr(comBaseDat.ExecuteScalar())

        cnnBaseDat.Close()
        comBaseDat.Connection.Close()

        Return n
    End Function
#End Region

End Class
