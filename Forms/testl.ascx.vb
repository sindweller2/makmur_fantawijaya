
Partial Class Forms_testl
    Inherits System.Web.UI.UserControl

    Dim at As New KWATerbilang.cKWATerbilang
    Dim x As String = "6,831,000"

    Public Property terbilang() As String
        Get
            Dim o As Object = ViewState("terbilang")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
        Set(ByVal value As String)
            ViewState("terbilang") = value
        End Set
    End Property

    Sub test()

        Dim x As String = "6,831,255.129"
        Dim nilai As Decimal = Decimal.ToDouble(x)
        Dim bulat As Decimal = Math.Truncate(Decimal.ToDouble(x))
        Dim selisih_angka As Decimal = nilai - bulat
        Dim selisih_string As String = FormatNumber(nilai - bulat, 3)

        terbilang = at.KwaTerbilangDecimalSpecial(bulat)

        terbilang = Replace(terbilang, "Seribu", "Satu Ribu")

        If selisih_angka = 0 Then
            terbilang = terbilang
        Else
            If selisih_string.Substring(2, 1) = "0" And selisih_string.Substring(3, 1) = "0" Then
                terbilang = terbilang + " Dan Nol Nol "
                terbilang = terbilang + at.KwaTerbilangDecimalSpecial(selisih_string.Substring(4, 1))

            ElseIf selisih_string.Substring(2, 1) = "0" And selisih_string.Substring(3, 1) <> "0" And selisih_string.Substring(4, 1) = "0" Then
                terbilang = terbilang + " Dan Nol "
                terbilang = terbilang + at.KwaTerbilangDecimalSpecial(selisih_string.Substring(3, 1))
                terbilang = terbilang + " Nol"

            ElseIf selisih_string.Substring(2, 1) = "0" And selisih_string.Substring(3, 1) <> "0" And selisih_string.Substring(4, 1) <> "0" Then
                terbilang = terbilang + " Dan Nol "
                terbilang = terbilang + at.KwaTerbilangDecimalSpecial(selisih_string.Substring(3, 1)) & " "
                terbilang = terbilang + at.KwaTerbilangDecimalSpecial(selisih_string.Substring(4, 1))

            ElseIf selisih_string.Substring(2, 1) <> "0" And selisih_string.Substring(3, 1) = "0" And selisih_string.Substring(4, 1) = "0" Then
                terbilang = terbilang + " Dan "
                terbilang = terbilang + at.KwaTerbilangDecimalSpecial(selisih_string.Substring(2, 1)) & " "
                terbilang = terbilang + "Nol Nol"

            ElseIf selisih_string.Substring(2, 1) <> "0" And selisih_string.Substring(3, 1) <> "0" And selisih_string.Substring(4, 1) = "0" Then
                terbilang = terbilang + " Dan "
                terbilang = terbilang + at.KwaTerbilangDecimalSpecial(selisih_string.Substring(2, 1)) & " "
                terbilang = terbilang + at.KwaTerbilangDecimalSpecial(selisih_string.Substring(3, 1)) & " "
                terbilang = terbilang + "Nol"
            ElseIf selisih_string.Substring(2, 1) <> "0" And selisih_string.Substring(3, 1) <> "0" And selisih_string.Substring(4, 1) <> "0" Then
                terbilang = terbilang + " Dan "
                terbilang = terbilang + at.KwaTerbilangDecimalSpecial(selisih_string.Substring(2, 1)) & " "
                terbilang = terbilang + at.KwaTerbilangDecimalSpecial(selisih_string.Substring(3, 1)) & " "
                terbilang = terbilang + at.KwaTerbilangDecimalSpecial(selisih_string.Substring(4, 1))
            End If
            terbilang = terbilang + " Sen"
        End If

        Me.lbl_msg.Text = terbilang
    End Sub
    
    Protected Sub btn_test_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_test.Click
        Me.test()
    End Sub
End Class
