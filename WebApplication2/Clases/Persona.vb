Public Class Persona

#Region "VARIABLES DE CLASE"

    ' VARIABLES DE CLASE
    '=====================================================================
    Dim GEN_Nombre As String = Nothing
    Dim GEN_ApePaterno As String = Nothing
    Dim GEN_ApeMaterno As String = Nothing
    Dim GEN_dir_calle As String = Nothing
    Dim GEN_dir_numero As String = Nothing
    Dim GEN_telefono As String = Nothing
    Dim GEN_digito As String = Nothing
    Dim GEN_numero_documento As String
    Dim GEN_idCiudad As Integer = 284, GEN_idComuna As Integer = 45
    Dim GEN_idPais As Integer = 1, GEN_IdRegion As Integer = 14
    Dim GEN_IdSexo As Integer = 1, GEN_EstadoP As String = "Activo"

#End Region

#Region "MÉTODOS"

    Public Function get_GEN_NombreCompleto()
        Return Me.get_GEN_Nombre() & " " & Me.get_GEN_ApePaterno() & " " & Me.get_GEN_ApeMaterno()
    End Function

    Public Function get_Id_ciudad_por_Codigo(ByVal codigoCiudad)
        Dim con As New Conexion
        Dim consulta As String = "SELECT " _
        & "GEN_idCiudad " _
        & "FROM " _
        & "GEN_Ciudad " _
        & "WHERE " _
        & "GEN_codigoCiudad = " & codigoCiudad
        Return CInt(con.consulta_sql_devuelve_string(consulta))
    End Function

#End Region

#Region "SET"

    Public Sub set_GEN_digito(ByVal GEN_digito)
        Me.GEN_digito = GEN_digito
    End Sub

    Public Sub set_GEN_numero_documento(ByVal GEN_rut)
        Me.GEN_numero_documento = GEN_rut
    End Sub

    Public Sub set_GEN_IdSexo(ByVal GEN_IdSexo As Integer)
        Me.GEN_IdSexo = GEN_IdSexo
    End Sub

    Public Sub set_GEN_Nombre(ByVal GEN_Nombre)
        Me.GEN_Nombre = GEN_Nombre
    End Sub

    Public Sub set_GEN_ApePaterno(ByVal GEN_ApePaterno)
        Me.GEN_ApePaterno = GEN_ApePaterno
    End Sub

    Public Sub set_GEN_ApeMaterno(ByVal GEN_ApeMaterno)
        Me.GEN_ApeMaterno = GEN_ApeMaterno
    End Sub

    Public Sub set_GEN_dir_calle(ByVal GEN_dir_calle)
        Me.GEN_dir_calle = GEN_dir_calle
    End Sub

    Public Sub set_GEN_telefono(ByVal GEN_telefono)
        Me.GEN_telefono = GEN_telefono
    End Sub

    Public Sub set_GEN_idCiudad(ByVal GEN_idCiudad As Integer)
        Me.GEN_idCiudad = GEN_idCiudad
    End Sub

    Public Sub set_Gen_IdComuna(ByVal GEN_idComuna As Integer)
        Me.GEN_idComuna = GEN_idComuna
    End Sub

    Public Sub set_Gen_IdPais(ByVal GEN_idPais As Integer)
        Me.GEN_idPais = GEN_idPais
    End Sub

    Public Sub set_Gen_IdRegion(ByVal GEN_idRegion As Integer)
        Me.GEN_IdRegion = GEN_idRegion
    End Sub

    Public Sub set_Gen_Dir_Numero(ByVal GEN_dir_numero)
        Me.GEN_dir_numero = GEN_dir_numero
    End Sub

    Public Sub set_GEN_EstadoP(ByVal GEN_estadoP)
        Me.GEN_EstadoP = GEN_estadoP
    End Sub

#End Region

#Region "GET"

    Public Function get_GEN_Nombre()
        Return Me.GEN_Nombre
    End Function

    Public Function get_GEN_ApePaterno()
        Return Me.GEN_ApePaterno
    End Function

    Public Function get_GEN_ApeMaterno()
        Return Me.GEN_ApeMaterno
    End Function

    Public Function get_GEN_Sexo()
        Return Me.GEN_IdSexo
    End Function

    Public Function get_GEN_dir_calle()
        Return Me.GEN_dir_calle
    End Function

    Public Function get_GEN_telefono()
        Return Me.GEN_telefono
    End Function

    Public Function get_Gen_Idcomuna()
        Return Me.GEN_idComuna
    End Function

    Public Function get_Gen_Idpais()
        Return Me.GEN_idPais
    End Function

    Public Function get_Gen_Idregion()
        Return Me.GEN_IdRegion
    End Function

    Public Function get_GEN_IdSexo()
        Return Me.GEN_IdSexo
    End Function

    Public Function get_GEN_digito()
        Return Me.GEN_digito
    End Function

    Public Function get_GEN_numero_documento()
        Return Me.GEN_numero_documento
    End Function

    Public Function get_Gen_Dir_Numero()
        Return Trim(Me.GEN_dir_numero)
    End Function

    Public Function get_GEN_EstadoP()
        Return Me.GEN_EstadoP
    End Function

    Public Function get_GEN_IdCiudad()
        Return Me.GEN_idCiudad
    End Function

#End Region
    '---------------------------------------------------
    ' FUNCION QUE ELIMINA ACENTOS Y CARACTERES EN UN TEXTO
    '---------------------------------------------------
    Public Shared Function AcentosASCII(ByVal texto) As String
        Dim ConAcento() As String = {"&#225;", "&#233;", "&#237;", "&#243;", "&#250;", "&#193;", "&#201;", "&#205;", "&#211;", "&#218;", "&#209;", "&#241;", "&#196;", "&#203;", "&#207;", "&#214;", "&#220;", "&#228;", "&#235;", "&#239;", "&#246;", "&#252;", "&#160;", "&#176;"}
        Dim SinAcento() As String = {"á", "é", "í", "ó", "ú", "Á", "É", "Í", "Ó", "Ú", "Ñ", "ñ", "Ä", "Ë", "Ï", "Ö", "Ü", "ä", "ë", "ï", "ö", "ü", " ", "°"}
        Dim i, numCaracteres As Integer
        numCaracteres = RTrim(ConAcento.Length)
        For i = 0 To (numCaracteres - 1)
            texto = Replace(texto, ConAcento(i), SinAcento(i))
        Next
        Return texto
    End Function
End Class
