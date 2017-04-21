Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Data
Imports System.Data.Odbc

' Para permitir que se llame a este servicio Web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class LeeryGrabaraOrden
    Inherits System.Web.Services.WebService
    '---------------------------------------------------------------------------------------------------------------------------------------------
    'OTROS CAMPOS
    '---------------------------------------------------------------------------------------------------------------------------------------------
    Public connectionStringODBC As String = "Dsn=salud;uid=cvieille;pwd=cv2014"

    <WebMethod()> _
    Public Function CargaDatosdesdeOrdenporRut(ByVal rut As String, ByVal digito As String, ByVal tipo_doc As Integer) As Boolean
        Dim bool As Boolean = False
        Try
            Dim sql_Str As String = ""
            Dim rut_completo As String = ""

            Dim p As New Paciente

            sql_Str = "SELECT p.PAC_PAC_Nombre, " _
                & "p.PAC_PAC_ApellPater, " _
                & "p.PAC_PAC_ApellMater, " _
                & "p.PAC_PAC_FechaNacim, " _
                & "c.PAC_CAR_NumerFicha, " _
                & "p.PAC_PAC_DireccionGralHabit, " _
                & "p.PAC_PAC_NumerHabit, " _
                & "p.PAC_PAC_Fono, " _
                & "p.PAC_PAC_Sexo, " _
                & "p.PAC_PAC_ComunHabit, " _
                & "p.PAC_PAC_CiudaHabit, " _
                & "p.PAC_PAC_RegioHabit, " _
                & "p.PAC_PAC_FechaModif, " _
                & "p.PAC_PAC_TelefonoMovil, " _
                & "p.PAC_PAC_Numero, " _
                & "p.PAC_PAC_Prevision, " _
                & "p.PAC_PAC_TipoBenef " _
            & "FROM PAC_Paciente p, PAC_Carpeta c where p.PAC_PAC_Numero *= c.PAC_PAC_Numero "

            If tipo_doc = 1 Then

                rut_completo = Trim(rut) & "-" & digito

                If Len(rut) = 7 Then rut_completo = "0" & rut_completo
                If Len(rut) = 6 Then rut_completo = "00" & rut_completo
                If Len(rut) = 5 Then rut_completo = "000" & rut_completo
                If Len(rut) = 4 Then rut_completo = "0000" & rut_completo
                If Len(rut) = 3 Then rut_completo = "00000" & rut_completo

                sql_Str = sql_Str & " and p.PAC_PAC_Rut='" & rut_completo & "'"
            Else
                sql_Str = sql_Str & " and p.PAC_PAC_NroPasaporte='" & rut & "'"
            End If

            Dim conn As Odbc.OdbcConnection = New OdbcConnection(connectionStringODBC)
            conn.Open()
            Dim comm As Odbc.OdbcCommand = New OdbcCommand(sql_Str, conn)

            Dim dr As Odbc.OdbcDataReader = comm.ExecuteReader()


            While dr.Read()
                bool = True


                Dim consulta As String = ""
                consulta = "SELECT GEN_idPaciente FROM GEN_Paciente where GEN_numero_documentoPaciente ='" & rut & "' AND GEN_idIdentificacion=" & tipo_doc
                Dim idpac As String = ""
                Dim con As New Conexion
                idpac = con.consulta_sql_devuelve_string(consulta)
                p.Pacientes(idpac)


                Dim FechaUltimaModificacionOrden As DateTime = dr(12).ToString()

                With p

                    If FechaUltimaModificacionOrden > .get_GEN_fec_actualizacionPaciente() Or idpac = "" Then
                        CargadeOrdenaSql(p, dr)
                    Else
                        CargadeSqlaOrden(p, rut_completo)

                    End If


                End With
            End While
            conn.Close()
        Catch ex As Exception
            Dim msg As String = ex.Message
        End Try
        Return bool
    End Function

    Private Sub CargadeOrdenaSql(ByVal gen_paciente As Object, ByVal dr As OdbcDataReader)
        Dim paciente As New Paciente
        Dim con As New Conexion
        Dim consulta As String
        With gen_paciente


            If Trim(dr(0).ToString) <> "" Then .set_GEN_Nombre(Trim(dr(0).ToString))
            If Trim(dr(1).ToString) <> "" Then .set_GEN_ApePaterno(Trim(dr(1).ToString))
            If Trim(dr(2).ToString) <> "" Then .set_GEN_ApeMaterno(Trim(dr(2).ToString))
            If Trim(dr(5).ToString) <> "" Then .set_GEN_dir_calle(Trim(dr(5).ToString))
            If Trim(dr(6).ToString) <> "" Then .set_Gen_Dir_Numero(Trim(dr(6).ToString))
            If Trim(dr(9).ToString) <> "" Then
                consulta = "select GEN_idCiudad from GEN_Ciudad where GEN_codigoCiudad = " & Trim(dr(9).ToString)
                Dim idciudad As String = con.consulta_sql_devuelve_string(consulta)
                If idciudad <> "" Then .set_GEN_idCiudad(CInt(idciudad))
            End If
            If Trim(dr(10).ToString) <> "" Then
                consulta = "select GEN_idcomuna from GEN_Comuna where GEN_codigoComuna = " & Trim(dr(10).ToString)
                Dim idcomuna As String = con.consulta_sql_devuelve_string(consulta)
                If idcomuna <> "" Then .set_Gen_IdComuna(CInt(idcomuna))
            End If
            If Trim(dr(11).ToString) <> "" Then
                consulta = "select GEN_idregion from GEN_region where GEN_codRegionOrden = '" & Trim(dr(11).ToString) & "'"
                Dim idregion As String = con.consulta_sql_devuelve_string(consulta)

                If idregion <> "" Then
                    .get_Gen_Idpais(1)
                    .set_Gen_IdRegion(CInt(idregion))
                End If

            End If
            If Trim(dr(7).ToString) <> "" Then .set_GEN_telefono(Trim(dr(7).ToString))
            If Trim(dr(13).ToString) <> "" Then .set_GEN_otros_fonosPaciente(Trim(dr(13).ToString))
            If Trim(dr(8).ToString) <> "" Then
                consulta = "select GEN_idSexo from GEN_Sexo where GEN_codSexoOrden = '" & Trim(dr(8).ToString) & "'"
                Dim idsexo As String = con.consulta_sql_devuelve_string(consulta)
                If idsexo <> "" Then .set_GEN_IdSexo(idsexo)
            End If
            If Trim(dr(3).ToString) <> "" Then Date.TryParse(Trim(dr(3).ToString), .get_GEN_fec_nacimientoPaciente)
            If IsNumeric(Trim(dr(4).ToString)) Then .set_GEN_nuiPaciente(Trim(dr(4).ToString))
            If Trim(dr(14).ToString) <> "" Then .set_GEN_pac_pac_numeroPaciente(Trim(dr(14).ToString))
            If Trim(dr(15).ToString) = "F" Then
                .set_GEN_idPrevision(1)
                If Trim(dr(16).ToString) = "A" Then
                    .set_GEN_idPrevisionTramo(1)
                ElseIf Trim(dr(16).ToString) = "B" Then
                    .set_GEN_idPrevisionTramo(2)
                ElseIf Trim(dr(16).ToString) = "C" Then
                    .set_GEN_idPrevisionTramo(3)
                ElseIf Trim(dr(16).ToString) = "D" Then
                    .set_GEN_idPrevisionTramo(4)
                End If
            ElseIf Trim(dr(15).ToString) = "I" Then
                .set_GEN_idPrevision(2)
            End If


            If dr(12).ToString() <> "" Then .set_GEN_fec_actualizacionPaciente(dr(12).ToString())

            If .get_GEN_idPaciente = 0 Or .get_GEN_idPaciente = Nothing Then
                .set_Crea_Pacientes()
            Else
                .set_Update_pacientes()
            End If
        End With
    End Sub

    Private Sub CargadeSqlaOrden(ByVal gen_paciente As Object, ByVal RutCompletoPaciente As String)
        Dim consulta As String = ""
        Dim consultaId As String = ""
        Dim paciente As New Paciente
        Dim con As New Conexion

        Try
            With gen_paciente
                consulta = "UPDATE PAC_Paciente SET " _
                        & "PAC_PAC_Nombre ='" & .get_GEN_Nombre() & "', " _
                        & "PAC_PAC_ApellPater='" & .get_GEN_ApePaterno() & "', " _
                        & "PAC_PAC_ApellMater='" & .get_GEN_ApeMaterno() & "', " _
                        & "PAC_PAC_FechaNacim='" & CDate(.get_GEN_fec_nacimientoPacienteOriginal()).ToString("yyyy-MM-dd") & "', " _
                        & "PAC_PAC_Fono='" & .get_GEN_telefono() & "', "

                If .get_GEN_idPrevision() = 1 Then
                    consulta = consulta & "PAC_PAC_Prevision='F', "

                    If .get_GEN_idPrevisionTramo() = 1 Then
                        consulta = consulta & "PAC_PAC_TipoBenef='A', "
                    ElseIf .get_GEN_idPrevisionTramo() = 2 Then
                        consulta = consulta & "PAC_PAC_TipoBenef='B', "
                    ElseIf .get_GEN_idPrevisionTramo() = 3 Then
                        consulta = consulta & "PAC_PAC_TipoBenef='C', "
                    ElseIf .get_GEN_idPrevisionTramo() = 4 Then
                        consulta = consulta & "PAC_PAC_TipoBenef='D', "
                    End If

                ElseIf .get_GEN_idPrevision() = 2 Then
                    consulta = consulta & "PAC_PAC_Prevision='I', "
                End If


                Dim direccion As String = Nothing
                If .get_GEN_dir_calle() <> Nothing Then
                    direccion = Trim(.get_GEN_dir_calle())
                End If
                'If .get_Gen_Dir_Numero() <> Nothing And .get_Gen_Dir_Numero() <> " " Then
                '    direccion = direccion & " " & Trim(.get_Gen_Dir_Numero())
                'End If
                If direccion <> Nothing Then
                    consulta = consulta & "PAC_PAC_DireccionGralHabit='" & Trim(direccion) & "', "
                End If


                If .get_GEN_IdSexo() Then
                    consultaId = "select GEN_codSexoOrden GEN_idSexo from GEN_Sexo where GEN_idSexo = " & .get_GEN_IdSexo()
                    Dim GEN_codSexoOrden As String = con.consulta_sql_devuelve_string(consultaId)
                    If GEN_codSexoOrden <> "" Then consulta = consulta & "PAC_PAC_Sexo='" & GEN_codSexoOrden & "', "
                End If

                consulta = consulta & "PAC_PAC_FechaModif='" & CDate(.get_GEN_fec_actualizacionPaciente()).ToString("yyyy-MM-dd H:mm:ss") & "' "
                If .get_GEN_otros_fonosPaciente() <> Nothing Then consulta = consulta & ", PAC_PAC_TelefonoMovil='" & .get_GEN_otros_fonosPaciente() & "' "

             
               


                consulta = consulta & "WHERE PAC_PAC_Rut='" & RutCompletoPaciente & "'"

            End With

            Dim conexion As New OdbcConnection(connectionStringODBC)
            Dim command As New OdbcCommand(consulta, conexion)
            conexion.Open()
            command.ExecuteNonQuery()
            conexion.Close()


        Catch ex As Exception

        End Try

    End Sub

End Class