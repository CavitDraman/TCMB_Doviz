Imports System.Net.Http
Imports Newtonsoft.Json

Public Class Form1
    Public JSON_Content As String

    Private Sub Doviz()
        Try
            Dim client As New HttpClient()
            Dim Tarih1 As String = DateTimePicker1.Value.ToString("dd-MM-yyyy")
            Dim Tarih2 As String = DateTimePicker2.Value.ToString("dd-MM-yyyy")

            Dim Key = "Key Giriniz"

            Dim DataUrl As String = "https://evds2.tcmb.gov.tr/service/evds/series=TP.DK.USD.S.YTL-TP.DK.EUR.S.YTL&startDate=" & Tarih1 & "&endDate=" & Tarih2 & "&type=json&key=" & Key
            Dim httpReqMsg As New HttpRequestMessage(HttpMethod.Get, DataUrl)
            Dim response As HttpResponseMessage = client.SendAsync(httpReqMsg).Result

            If response.IsSuccessStatusCode Then
                Dim content As String = response.Content.ReadAsStringAsync().Result
                Dim Kur As DovizResp = JsonConvert.DeserializeObject(Of DovizResp)(content)

                DataGridView1.Rows.Clear()

                For Each r As Item In Kur.items
                    DataGridView1.Rows.Add(r.Tarih, r.TP_DK_USD_S_YTL, r.TP_DK_EUR_S_YTL)
                Next
            Else
                MsgBox(response.StatusCode & "-" & response.ReasonPhrase)
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

#Region "Class"
    Public Class DovizResp
        Public Property items As List(Of Item)
        Public Property totalcount As Integer

    End Class
    Public Class Item
        Public Property Tarih As String
        Public Property TP_DK_USD_S_YTL As String
        Public Property TP_DK_EUR_S_YTL As String
    End Class

#End Region

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Doviz()
    End Sub
End Class
