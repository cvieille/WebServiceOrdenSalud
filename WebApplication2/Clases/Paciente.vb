Public Class Paciente
    Inherits Persona
    '---------------------------------------------------------------------------------------------------------------------------------------------
    'CAMPOS DE LA TABLA GEN-PACIENTE
    '---------------------------------------------------------------------------------------------------------------------------------------------
    Dim GEN_idPaciente As Integer = Nothing
    Dim GEN_idIdentificacion As Integer
    Dim GEN_dir_ruralidadPaciente As String = Nothing
    Dim GEN_idPrevision As Integer = Nothing
    Dim GEN_idPrevisionTramo As Integer = Nothing
    Dim GEN_nuiPaciente As Integer = 0
    Dim GEN_pac_pac_numeroPaciente As Integer = 0
    Dim GEN_otros_fonosPaciente As String = Nothing
    Dim GEN_emailPaciente As String = Nothing
    Dim GEN_PraisPaciente As String = Nothing
    Dim GEN_fec_nacimientoPaciente As Date
    Dim GEN_fec_actualizacionPaciente As DateTime


#Region "CRUD"
    Dim con As New Conexion

    Public Sub Pacientes(ByVal idpaciente As String)
        Dim consulta As String = ""
        consulta = "SELECT * " _
            & "FROM GEN_Paciente " _
            & "where GEN_idPaciente = " & idpaciente

        Dim tablas As Data.DataTable = con.consulta_sql_datatable(consulta)

        If tablas.Rows.Count > 0 Then
            Dim filas As Data.DataRow = tablas.Rows(0)

            Me.GEN_idPaciente = idpaciente
            If filas.Item("GEN_idIdentificacion").ToString() <> "" Then
                Me.GEN_idIdentificacion = filas.Item("GEN_idIdentificacion").ToString()
            Else
                Me.GEN_idIdentificacion = Nothing
            End If
            set_GEN_numero_documento(Trim(filas.Item("GEN_numero_documentoPaciente").ToString()))
            set_GEN_digito(Trim(filas.Item("GEN_digitoPaciente").ToString()))
            set_GEN_Nombre(Trim(filas.Item("GEN_nombrePaciente").ToString()))
            set_GEN_ApePaterno(Trim(filas.Item("GEN_ape_paternoPaciente").ToString()))

            If filas.Item("GEN_ape_maternoPaciente").ToString() <> "" Then
                set_GEN_ApeMaterno(Trim(filas.Item("GEN_ape_maternoPaciente").ToString()))
            Else
                set_GEN_ApeMaterno(Nothing)
            End If

            If filas.Item("GEN_dir_callePaciente").ToString() <> "" Then
                set_GEN_dir_calle(Trim(filas.Item("GEN_dir_callePaciente").ToString()))
            Else
                set_GEN_dir_calle(Nothing)
            End If

            If filas.Item("GEN_dir_numeroPaciente").ToString() <> "" Then
                set_Gen_Dir_Numero(Trim(filas.Item("GEN_dir_numeroPaciente").ToString()))
            Else
                set_Gen_Dir_Numero(Nothing)
            End If

            If filas.Item("GEN_dir_ruralidadPaciente").ToString() <> "" Then
                Me.GEN_dir_ruralidadPaciente = Trim(filas.Item("GEN_dir_ruralidadPaciente").ToString())
            Else
                Me.GEN_dir_ruralidadPaciente = Nothing
            End If

            If filas.Item("GEN_idCiudad").ToString() <> "" Then
                set_GEN_idCiudad(filas.Item("GEN_idCiudad"))
            Else
                set_GEN_idCiudad(Nothing)
            End If

            If filas.Item("GEN_idComuna").ToString() <> "" Then
                set_Gen_IdComuna(filas.Item("GEN_idComuna"))
            Else
                set_Gen_IdComuna(Nothing)
            End If

            If filas.Item("GEN_idRegion").ToString() <> "" Then
                MyBase.set_Gen_IdRegion(filas.Item("GEN_idRegion"))
            Else
                MyBase.set_Gen_IdRegion(Nothing)
            End If

            If filas.Item("GEN_idPais").ToString() <> "" Then
                set_Gen_IdPais(filas.Item("GEN_idPais"))
            Else
                set_Gen_IdPais(Nothing)
            End If

            If filas.Item("GEN_idPrevision").ToString() <> "" Then
                Me.GEN_idPrevision = filas.Item("GEN_idPrevision")
            Else
                Me.GEN_idPrevision = Nothing
            End If

            If filas.Item("GEN_idPrevision_Tramo").ToString() <> "" Then
                Me.GEN_idPrevisionTramo = filas.Item("GEN_idPrevision_Tramo")
            Else
                Me.GEN_idPrevisionTramo = Nothing
            End If

            If filas.Item("GEN_idSexo").ToString() <> "" Then
                set_GEN_IdSexo(CInt(filas.Item("GEN_idSexo").ToString()))
            Else
                set_GEN_IdSexo(Nothing)
            End If

            If filas.Item("GEN_telefonoPaciente").ToString() <> "" Then
                set_GEN_telefono(Trim(filas.Item("GEN_telefonoPaciente").ToString()))
            Else
                set_GEN_telefono(Nothing)
            End If

            If filas.Item("GEN_otros_fonosPaciente").ToString() <> "" Then
                Me.GEN_otros_fonosPaciente = Trim(filas.Item("GEN_otros_fonosPaciente").ToString())
            Else
                Me.GEN_otros_fonosPaciente = Nothing
            End If

            If filas.Item("GEN_emailPaciente").ToString() <> "" Then
                Me.GEN_emailPaciente = Trim(filas.Item("GEN_emailPaciente").ToString())
            Else
                Me.GEN_emailPaciente = Nothing
            End If

            Me.GEN_fec_nacimientoPaciente = filas.Item("GEN_fec_nacimientoPaciente").ToString()


            Me.GEN_nuiPaciente = Trim(Trim(filas.Item("GEN_nuiPaciente").ToString()))
            If filas.Item("GEN_pac_pac_numeroPaciente").ToString() <> "" Then
                Me.GEN_pac_pac_numeroPaciente = Trim(filas.Item("GEN_pac_pac_numeroPaciente").ToString())
            Else
                Me.GEN_pac_pac_numeroPaciente = Nothing
            End If
            DateTime.TryParse(Trim(filas.Item("GEN_fec_actualizacionPaciente").ToString), Me.GEN_fec_actualizacionPaciente)
            Me.GEN_PraisPaciente = Trim(filas.Item("GEN_PraisPaciente").ToString())

        End If
    End Sub

    '---------------------------------------------------------------------------------------------------------------------------------------------
    ' INSERTA EN TABLA PACIENTES
    '---------------------------------------------------------------------------------------------------------------------------------------------
    Public Function set_Crea_Pacientes()
        Dim AgregaNull As String = " NULL, "

        Dim Consulta As String = "INSERT INTO GEN_Paciente(" _
            & "GEN_numero_documentoPaciente, " _
            & "GEN_digitoPaciente, " _
            & "GEN_nombrePaciente, " _
            & "GEN_ape_paternoPaciente, " _
            & "GEN_ape_maternoPaciente, " _
            & "GEN_fec_nacimientoPaciente, " _
            & "GEN_nuiPaciente, " _
            & "GEN_idIdentificacion, " _
            & "GEN_PraisPaciente, " _
            & "GEN_idSexo, " _
            & "GEN_idPrevision, " _
            & "GEN_dir_ruralidadPaciente, " _
            & "GEN_idRegion, " _
            & "GEN_idComuna, " _
            & "GEN_idCiudad, " _
            & "GEN_idPais, " _
            & "GEN_dir_callePaciente, " _
            & "GEN_dir_numeroPaciente, " _
            & "GEN_telefonoPaciente, " _
            & "GEN_otros_fonosPaciente, " _
            & "GEN_emailPaciente, " _
            & "GEN_fec_actualizacionPaciente, " _
            & "GEN_idPrevision_Tramo, " _
            & "GEN_pac_pac_numeroPaciente, " _
            & "GEN_estadoPaciente) " _
            & "VALUES ("

        'NUMERO DE DOCUMENTO
        If get_GEN_numero_documento() <> Nothing Then
            Consulta &= " '" & get_GEN_numero_documento() & "', "
        Else
            Consulta &= AgregaNull
        End If

        'DIGITO VERIFICADOR
        If get_GEN_digito() <> Nothing Then
            Consulta &= "'" & get_GEN_digito() & "', "
        Else
            Consulta &= AgregaNull
        End If

        'NOMBRE
        If get_GEN_Nombre() <> Nothing Then
            Consulta &= "UPPER('" & get_GEN_Nombre() & "'), "
        Else
            Consulta &= AgregaNull
        End If

        'APELLIDO PATERNO
        If get_GEN_ApePaterno() <> Nothing Then
            Consulta &= "UPPER('" & get_GEN_ApePaterno() & "'), "
        Else
            Consulta &= AgregaNull
        End If

        'APELLIDO MATERNO
        If Trim(get_GEN_ApeMaterno()) <> "" Then
            Consulta &= "UPPER('" & Trim(get_GEN_ApeMaterno()) & "'), "
        Else
            Consulta &= AgregaNull
        End If

        'FECHA DE NACIMIENTO
        If Me.GEN_fec_nacimientoPaciente <> Nothing Then
            Consulta &= "'" & Me.GEN_fec_nacimientoPaciente & "', "
        Else
            Consulta &= AgregaNull
        End If

        'N° DE UBICACION INTERNA
        If Me.GEN_nuiPaciente <> Nothing Then
            Consulta &= Me.GEN_nuiPaciente & ", "
        Else
            Consulta &= AgregaNull
        End If

        'IDENTIFICACION
        If Me.GEN_idIdentificacion <> 0 And Me.GEN_idIdentificacion <> Nothing Then
            Consulta = Consulta & Me.GEN_idIdentificacion & ", "
        Else
            Consulta = Consulta & AgregaNull
        End If

        'PRAIS PACIENTE
        If Trim(Me.GEN_PraisPaciente) <> "" Then
            Consulta = Consulta & "'" & Trim(Me.GEN_PraisPaciente) & "', "
        Else
            Consulta = Consulta & AgregaNull
        End If

        'SEXO
        If get_GEN_Sexo() <> Nothing And get_GEN_Sexo() <> 0 Then
            Consulta = Consulta & get_GEN_Sexo() & ", "
        Else
            Consulta = Consulta & AgregaNull
        End If

        'PREVISIÓN
        If Me.GEN_idPrevision <> Nothing And Me.GEN_idPrevision <> 0 Then
            Consulta = Consulta & Me.GEN_idPrevision & ", "
        Else
            Consulta = Consulta & AgregaNull
        End If

        'RURALIDAD
        If (Me.GEN_dir_ruralidadPaciente) <> "" Then
            Consulta = Consulta & "'" & Trim(Me.GEN_dir_ruralidadPaciente) & "', "
        Else
            Consulta = Consulta & AgregaNull
        End If

        'REGION
        If get_Gen_Idregion() <> Nothing And get_Gen_Idregion() <> 0 Then
            Consulta = Consulta & get_Gen_Idregion() & ", "
        Else
            Consulta = Consulta & AgregaNull
        End If

        'COMUNA
        If get_Gen_Idcomuna() <> Nothing And get_Gen_Idcomuna() <> 0 Then
            Consulta = Consulta & get_Gen_Idcomuna() & ", "
        Else
            Consulta = Consulta & AgregaNull
        End If

        'CIUDAD
        If get_GEN_IdCiudad() <> Nothing And get_GEN_IdCiudad() <> 0 Then
            Consulta = Consulta & get_GEN_IdCiudad() & ", "
        Else
            Consulta = Consulta & AgregaNull
        End If

        'PAIS
        If get_Gen_Idpais() <> Nothing And get_Gen_Idpais() <> 0 Then
            Consulta = Consulta & get_Gen_Idpais() & ", "
        Else
            Consulta = Consulta & AgregaNull
        End If

        'DIRECCION DE CALLE
        If Trim(get_GEN_dir_calle()) <> "" Then
            Consulta = Consulta & "UPPER('" & Trim(get_GEN_dir_calle()) & "'), "
        Else
            Consulta = Consulta & AgregaNull
        End If

        'N° DE DIRECCION
        If Trim(get_Gen_Dir_Numero()) <> "" Then
            Consulta = Consulta & "UPPER('" & Trim(get_Gen_Dir_Numero()) & "'), "
        Else
            Consulta = Consulta & AgregaNull
        End If

        'NUMERO DE TELEFONO
        If Trim(get_GEN_telefono()) <> "" Then
            Consulta = Consulta & "'" & Trim(get_GEN_telefono()) & "', "
        Else
            Consulta = Consulta & AgregaNull
        End If

        'OTROS TELEFONOS
        If Trim(Me.GEN_otros_fonosPaciente) <> "" Then
            Consulta = Consulta & "'" & Trim(Me.GEN_otros_fonosPaciente) & "', "
        Else
            Consulta = Consulta & AgregaNull
        End If

        'EMAIL PACIENTE
        If Trim(Me.GEN_emailPaciente) <> "" Then
            Consulta = Consulta & "'" & Trim(Me.GEN_emailPaciente) & "', "
        Else
            Consulta = Consulta & AgregaNull
        End If

        'FECHA DE ACTUALIZACIÓN PACIENTE
        Consulta = Consulta & "'" & Me.GEN_fec_actualizacionPaciente & "', "

        'TRAMO DE LA PREVISION
        If Me.GEN_idPrevisionTramo <> Nothing And Me.GEN_idPrevisionTramo <> 0 Then
            Consulta = Consulta & Me.GEN_idPrevisionTramo & ", "
        Else
            Consulta = Consulta & AgregaNull
        End If

        'NUMERO PACIENTE
        If (Me.GEN_pac_pac_numeroPaciente) <> 0 Then
            Consulta = Consulta & "'" & Trim(Me.GEN_pac_pac_numeroPaciente) & "', "
        Else
            Consulta = Consulta & AgregaNull
        End If

        Consulta &= "'Activo')"

        Me.GEN_idPaciente = con.ejecuta_sql_devuelve_identidad(Consulta)
        Return Me.GEN_idPaciente

    End Function

    '---------------------------------------------------------------------------------------------------------------------------------------------
    ' UPDTE EN TABLA PACIENTES 
    '---------------------------------------------------------------------------------------------------------------------------------------------
    Public Sub set_Update_pacientes()
        Dim sql As String = "UPDATE GEN_Paciente SET "
        If get_GEN_numero_documento() <> Nothing Then
            sql = sql & "GEN_numero_documentoPaciente = '" & get_GEN_numero_documento() & "', "
        Else
            sql = sql & "GEN_numero_documentoPaciente = NULL, "
        End If
        If get_GEN_digito() <> Nothing Then
            sql = sql & "GEN_digitoPaciente = '" & get_GEN_digito() & "', "
        Else
            sql = sql & "GEN_digitoPaciente = NULL, "
        End If
        If get_GEN_Nombre() <> Nothing Then
            sql = sql & "GEN_nombrePaciente = UPPER('" & get_GEN_Nombre() & "'), "
        Else
            sql = sql & "GEN_nombrePaciente = NULL, "
        End If
        If get_GEN_ApePaterno() <> Nothing Then
            sql = sql & "GEN_ape_paternoPaciente = UPPER('" & get_GEN_ApePaterno() & "'), "
        Else
            sql = sql & "GEN_ape_paternoPaciente = NULL, "
        End If
        If get_GEN_ApeMaterno() <> Nothing And get_GEN_ApeMaterno() <> "" Then
            sql = sql & "GEN_ape_maternoPaciente = UPPER('" & get_GEN_ApeMaterno() & "'), "
        Else
            sql = sql & "GEN_ape_maternoPaciente = NULL, "
        End If
        If Me.GEN_fec_nacimientoPaciente <> Nothing Then
            sql = sql & "GEN_fec_nacimientoPaciente = '" & Me.GEN_fec_nacimientoPaciente & "', "
        Else
            sql = sql & "GEN_fec_nacimientoPaciente = NULL, "
        End If
        If Me.GEN_nuiPaciente <> Nothing Then
            sql = sql & "GEN_nuiPaciente = " & Me.GEN_nuiPaciente & ", "
        Else
            sql = sql & "GEN_nuiPaciente = NULL, "
        End If
        If Me.GEN_PraisPaciente <> Nothing And Me.GEN_PraisPaciente <> "" Then
            sql = sql & "GEN_PraisPaciente = '" & Me.GEN_PraisPaciente & "', "
        Else
            sql = sql & "GEN_PraisPaciente = NULL, "
        End If
        If get_GEN_Sexo() <> Nothing And get_GEN_Sexo() <> 0 Then
            sql = sql & "GEN_idSexo = " & get_GEN_Sexo() & ", "
        Else
            sql = sql & "GEN_idSexo = NULL, "
        End If
        If Me.GEN_idIdentificacion <> Nothing And Me.GEN_idIdentificacion <> 0 Then
            sql = sql & "GEN_idIdentificacion = " & Me.GEN_idIdentificacion & ", "
        Else
            sql = sql & "GEN_idIdentificacion = NULL, "
        End If
        If Me.GEN_idPrevision <> Nothing And Me.GEN_idPrevision <> 0 Then
            sql = sql & "GEN_idPrevision = " & Me.GEN_idPrevision & ", "
        Else
            sql = sql & "GEN_idPrevision = NULL, "
        End If
        If Me.GEN_idPrevisionTramo <> Nothing And Me.GEN_idPrevisionTramo <> 0 Then
            sql = sql & "GEN_idPrevision_Tramo = " & Me.GEN_idPrevisionTramo & ", "
        Else
            sql = sql & "GEN_idPrevision_Tramo = NULL, "
        End If
        If get_Gen_Idregion() <> Nothing And get_Gen_Idregion() <> 0 Then
            sql = sql & "GEN_idRegion = " & get_Gen_Idregion() & ", "
        Else
            sql = sql & "GEN_idRegion = NULL, "
        End If
        If get_Gen_Idcomuna() <> Nothing And get_Gen_Idcomuna() <> 0 Then
            sql = sql & "GEN_idComuna = " & get_Gen_Idcomuna() & ", "
        Else
            sql = sql & "GEN_idComuna = NULL, "
        End If
        If get_GEN_IdCiudad() <> Nothing And get_GEN_IdCiudad() <> 0 Then
            sql = sql & "GEN_idCiudad = " & get_GEN_IdCiudad() & ", "
        Else
            sql = sql & "GEN_idCiudad = NULL, "
        End If
        If get_Gen_Idpais() <> Nothing And get_Gen_Idpais() <> 0 Then
            sql = sql & "GEN_idPais = " & get_Gen_Idpais() & ", "
        Else
            sql = sql & "GEN_idPais = NULL, "
        End If
        If Me.GEN_dir_ruralidadPaciente <> Nothing And Me.GEN_dir_ruralidadPaciente <> 0 Then
            sql = sql & "GEN_dir_ruralidadPaciente = '" & Me.GEN_dir_ruralidadPaciente & "', "
        Else
            sql = sql & "GEN_dir_ruralidadPaciente = NULL, "
        End If
        If get_GEN_dir_calle() <> Nothing And get_GEN_dir_calle() <> "" Then
            sql = sql & "GEN_dir_callePaciente = UPPER('" & get_GEN_dir_calle() & "'), "
        Else
            sql = sql & "GEN_dir_callePaciente = NULL, "
        End If
        If get_Gen_Dir_Numero() <> Nothing And get_Gen_Dir_Numero() <> "" Then
            sql = sql & "GEN_dir_numeroPaciente = UPPER('" & get_Gen_Dir_Numero() & "'), "
        Else
            sql = sql & "GEN_dir_numeroPaciente = NULL, "
        End If
        If get_GEN_telefono() <> Nothing And get_GEN_telefono() <> "" Then
            sql = sql & "GEN_telefonoPaciente = '" & get_GEN_telefono() & "', "
        Else
            sql = sql & "GEN_telefonoPaciente = NULL, "
        End If
        If Me.GEN_otros_fonosPaciente <> Nothing And Me.GEN_otros_fonosPaciente <> "" Then
            sql = sql & "GEN_otros_fonosPaciente = '" & Me.GEN_otros_fonosPaciente & "', "
        Else
            sql = sql & "GEN_otros_fonosPaciente = NULL, "
        End If
        If Me.GEN_emailPaciente <> Nothing And Me.GEN_emailPaciente <> "" Then
            sql = sql & "GEN_emailPaciente = '" & Me.GEN_emailPaciente & "', "
        Else
            sql = sql & "GEN_emailPaciente = NULL, "
        End If
        If Me.GEN_fec_actualizacionPaciente <> Nothing Then
            sql = sql & "GEN_fec_actualizacionPaciente = '" & Me.GEN_fec_actualizacionPaciente & "', "
        Else
            sql = sql & "GEN_fec_actualizacionPaciente = NULL, "
        End If
        If Me.GEN_pac_pac_numeroPaciente <> Nothing And Me.GEN_pac_pac_numeroPaciente <> 0 Then
            sql = sql & "GEN_pac_pac_numeroPaciente = " & Me.GEN_pac_pac_numeroPaciente & " "
        Else
            sql = sql & "GEN_pac_pac_numeroPaciente = NULL "
        End If
        sql = sql & " WHERE GEN_idPaciente = " & Me.GEN_idPaciente
        con.ejecuta_sql(sql)
    End Sub

#End Region


#Region "METODOS SET"

    Public Sub set_GEN_idPaciente(ByVal GEN_idPaciente)
        If GEN_idPaciente = 0 Then Me.GEN_idPaciente = Nothing Else Me.GEN_idPaciente = GEN_idPaciente
    End Sub
    Public Sub set_dir_ruralidad(ByVal GEN_dir_ruralidadPaciente)
        Me.GEN_dir_ruralidadPaciente = GEN_dir_ruralidadPaciente
    End Sub
    Public Sub set_GEN_otros_fonosPaciente(ByVal GEN_otros_fonosPaciente)
        Me.GEN_otros_fonosPaciente = GEN_otros_fonosPaciente
    End Sub
    Public Sub set_GEN_emailPaciente(ByVal GEN_emailPaciente)
        Me.GEN_emailPaciente = GEN_emailPaciente
    End Sub

    Public Sub set_fec_nacimiento(ByVal GEN_fec_nacimientoPaciente)
        Date.TryParse(GEN_fec_nacimientoPaciente, Me.GEN_fec_nacimientoPaciente)
    End Sub
    Public Sub set_GEN_idPrevision(ByVal GEN_idPrevision)
        If GEN_idPrevision = 0 Then Me.GEN_idPrevision = Nothing Else Me.GEN_idPrevision = GEN_idPrevision
    End Sub
    Public Sub set_GEN_idPrevisionTramo(ByVal GEN_idPrevisionTramo)
        If GEN_idPrevisionTramo = 0 Then Me.GEN_idPrevisionTramo = Nothing Else Me.GEN_idPrevisionTramo = GEN_idPrevisionTramo
    End Sub
    Public Sub set_GEN_nuiPaciente(ByVal GEN_nuiPaciente)
        Me.GEN_nuiPaciente = GEN_nuiPaciente
    End Sub
    Public Sub set_GEN_pac_pac_numeroPaciente(ByVal GEN_pac_pac_numeroPaciente)
        Me.GEN_pac_pac_numeroPaciente = GEN_pac_pac_numeroPaciente
    End Sub
    Public Sub set_GEN_fec_actualizacionPaciente(ByVal GEN_fec_actualizacionPaciente)
        Date.TryParse(GEN_fec_actualizacionPaciente, Me.GEN_fec_actualizacionPaciente)
    End Sub
    Public Sub set_GEN_idIdentificacion(ByVal GEN_idIdentificacion)
        If GEN_idIdentificacion = 0 Then Me.GEN_idIdentificacion = Nothing Else Me.GEN_idIdentificacion = GEN_idIdentificacion
    End Sub
    Public Sub set_GEN_PraisPaciente(ByVal GEN_PraisPaciente)
        Me.GEN_PraisPaciente = GEN_PraisPaciente
    End Sub
#End Region

#Region "METODOS GET"
    Public Function get_GEN_idPaciente()
        If Me.GEN_idPaciente = Nothing Then Return 0 Else Return Me.GEN_idPaciente
    End Function
    Public Function get_GEN_dir_ruralidadPaciente()
        Return Me.GEN_dir_ruralidadPaciente
    End Function
    Public Function get_GEN_otros_fonosPaciente()
        Return Me.GEN_otros_fonosPaciente
    End Function
    Public Function get_GEN_emailPaciente()
        Return Me.GEN_emailPaciente
    End Function
    Public Function get_GEN_fec_nacimientoPaciente() As Date
        Return Me.GEN_fec_nacimientoPaciente
    End Function

    Public Function get_GEN_fec_nacimientoPacienteOriginal() As Date
        Return Me.GEN_fec_nacimientoPaciente
    End Function

    Public Function get_GEN_idPrevision()
        If Me.GEN_idPrevision = Nothing Then Return 0 Else Return Me.GEN_idPrevision
    End Function
    Public Function get_GEN_idPrevisionTramo()
        If Me.GEN_idPrevisionTramo = Nothing Then Return 0 Else Return Me.GEN_idPrevisionTramo
    End Function
    Public Function get_GEN_nuiPaciente()
        Return Me.GEN_nuiPaciente
    End Function
    Public Function get_GEN_pac_pac_numeroPaciente()
        Return Me.GEN_pac_pac_numeroPaciente
    End Function
    Public Function get_GEN_fec_actualizacionPaciente()
        If Me.GEN_fec_actualizacionPaciente = Nothing Then Return Nothing Else Return Me.GEN_fec_actualizacionPaciente
    End Function

    Public Function get_GEN_idIdentificacion()
        If Me.GEN_idIdentificacion = Nothing Then Return 0 Else Return Me.GEN_idIdentificacion
    End Function
    Public Function get_GEN_PraisPaciente()
        Return Me.GEN_PraisPaciente
    End Function

#End Region
End Class
