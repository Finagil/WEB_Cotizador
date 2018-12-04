Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Partial Public Class WebFormSimple
    Inherits System.Web.UI.Page
    Dim ta As New CotizaDSTableAdapters.TasasAplicablesTableAdapter
    Const TasaIva As Double = 0.16
    Dim TasaVidaMes As Double = 1
    Dim TasaVidaDia As Double = TasaVidaMes / 30.4
    Dim TasaVidaAnual As Double = TasaVidaMes * 12
    Dim TasaAnual As Double = 0
    Dim Bandera As Boolean = False
    Dim ContRecur As Double = 0
    Dim CapitaT As Double = 0
    Dim PagoT As Double = 0
    Dim InteresT As Double = 0
    Dim IvaT As Double = 0
    Dim SegT As Double = 0
    Dim TotalT As Double = 0
    Dim Diferencia As Double = 0
    Dim DiasT As Double = 0
    Dim PagoS() As Double

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        TxtTasa.Text = ta.TasaAplicacble(1, ta.SacaPeriodoMAX, "CS")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Master.Titulo = "Cotizador Credito Simple"
    End Sub

    Protected Sub BotonEnviar1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BotonEnviar1.Click
        BotonImp.Visible = False
        GridAmortizaciones.Visible = False
        If IsNumeric(TxtMonto.Text) = False Then
            LbError.Text = "Monto finanaciado no válido."
            LbError.Visible = True
            Exit Sub
        Else
            If CDec(TxtMonto.Text) <= 0 Then
                LbError.Text = "el Monto no puede ser Negativo."
                LbError.Visible = True
                Exit Sub
            ElseIf CDec(TxtMonto.Text) < 1000000 Then
                LbError.Text = "el Monto minimo es de 1,000,000 de pesos."
                LbError.Visible = True
                Exit Sub
            ElseIf CDec(TxtMonto.Text) > 10000000 Then
                LbError.Text = "el Monto maximo es de 10,000,000 de pesos."
                LbError.Visible = True
                Exit Sub
            End If
        End If
        LbError.Visible = False
        CalculaTabla()
    End Sub

    Sub CalculaTabla()
        Dim TasaIvaX As Decimal = TasaIva
        Dim TAmortizaciones As New CotizaDS.TablaAmortizacionDataTable
        Dim NoPagos As Integer = 0
        Dim NoPagosAnual As Integer = 0
        Dim Capital As Double = CDbl(TxtMonto.Text).ToString("N2")
        Dim CapitalSEG As Double = CDbl(TxtMonto.Text).ToString("N2")
        Dim MesSeguro As String = ""
        Dim FechaAux As Date = Date.Now.AddMonths(1)
        Dim FechaFin As Date = Date.Now.AddMonths(1)
        Dim SegVidaX As Double = TasaVidaDia
        Dim ErrorEnero As Date = Date.Now.AddMonths(1)
        Dim Meses As Integer = CmbPlazo.SelectedValue

        If FechaAux.Day > 28 Then
            FechaAux = FechaAux.AddMonths(1).AddDays((FechaAux.AddMonths(1).Day - 1) * -1)
        End If

        If CmbTipoPers.SelectedValue = "M" Then
            SegVidaX = 0
        End If

        If CmbTipoPers.SelectedValue <> "F" Then
            TasaIvaX = 0
        End If

        TasaAnual = CDbl(TxtTasa.Text) / 100
        FechaFin = FechaAux.AddMonths(Meses)
        NoPagos = Meses
        NoPagosAnual = 12


        Dim FechaAnt As Date = Date.Now
        Dim Cont As Integer = 0
        Dim Dias As Double = 0
        Dim DiasX As Double = 0
        Dim Interes As Double = 0

        Dim PagoX As Double = 0
        Dim PagoY As Double = 0
        Dim Extra As Double = 0
        Dim Aux As Double = 0
        Dim rr As CotizaDS.TablaAmortizacionRow
        Dim rrr As CotizaDS.TablaAmortizacionRow

        TAmortizaciones.Rows.Clear()
        MesSeguro = FechaAux.ToString("yyyyMM")

        While FechaAux < FechaFin.ToShortDateString And Capital > 0
            Cont += 1
            Dias = DateDiff(DateInterval.Day, FechaAnt, FechaAux)
            Interes = (Capital * (TasaAnual / 360) * Dias).ToString("N2")
            rr = TAmortizaciones.NewRow
            rr.No_Pago = Cont
            rr.Fecha_Vencimiento = FechaAux.ToShortDateString
            rr.Dias = Dias 'DIAS
            If Cont = 1 Then
                DiasX = DateDiff(DateInterval.Day, FechaAnt, FechaAnt.AddMonths(CmbPlazo.SelectedValue))
                PagoX = Pmt((TasaAnual / 360) * Dias, NoPagos, Capital * -1, 0, DueDate.EndOfPeriod)
                PagoY = Pmt((TasaAnual / 360) * Dias, NoPagos, Capital * -1, 0, DueDate.EndOfPeriod)
            Else
                PagoX = Pmt((TasaAnual / 360) * Dias, NoPagos, Capital * -1, 0, DueDate.EndOfPeriod)
            End If

            rr.Saldo_Insoluto = Capital.ToString("N2")
            rr.Interes = Interes.ToString("N2") ' INTERES
            If NoPagos = Cont Then
                rr.Capital = Capital.ToString("N2") ' CAPITAL
                rr.Pago = (Capital + Interes).ToString("N2")
                PagoX = (Capital + Interes)
            Else
                rr.Capital = (PagoX - Interes).ToString("N2") ' CAPITAL
                rr.Pago = PagoX.ToString("N2")
            End If


            If MesSeguro <> FechaAux.ToString("yyyyMM") Then
                CapitalSEG = Capital
            End If
            MesSeguro = FechaAux.ToString("yyyyMM")



            rr.Iva_Interes = (Interes * TasaIvaX).ToString("N2")
            rr.Seguro_de_Vida = (((Capital + Interes) / 1000) * SegVidaX * Dias).ToString("N2")
            rr.Pago_Total = ((((Capital + Interes) / 1000) * SegVidaX * Dias) + (Interes * TasaIvaX) + PagoX).ToString("N2")

            Capital = Capital.ToString("N2") - (PagoX.ToString("N2") - Interes.ToString("N2"))

            FechaAnt = FechaAux
            FechaAux = FechaAnt.AddMonths(1)
            If Cont = 1 Then
                If rr.Capital < 0 Then
                    Response.Write("Primera amortizacion Menor a cero, reconsidere las fecha de contratacion.")
                    TAmortizaciones.Rows.Clear()
                    Exit Sub
                End If
            End If
            TAmortizaciones.AddTablaAmortizacionRow(rr)
        End While


        CapitaT = 0
        PagoT = 0
        InteresT = 0
        IvaT = 0
        SegT = 0
        TotalT = 0
        Diferencia = 0
        DiasT = 0
        ReDim PagoS(NoPagos)
        Cont = 0
        PagoS(0) = CDbl(TxtMonto.Text) * -1
        For Each rr In TAmortizaciones.Rows
            Cont += 1
            PagoS(Cont) = CDbl(rr.Pago_Total) - CDbl(rr.Iva_Interes)
            Capital = CDbl(rr.Capital)
            CapitaT += Capital
            PagoT = PagoT + CDbl(rr.Pago)
            DiasT = DiasT + CDbl(rr.Dias)
            InteresT = InteresT + CDbl(rr.Interes)
            IvaT = IvaT + CDbl(rr.Iva_Interes)
            SegT = SegT + CDbl(rr.Seguro_de_Vida)
            TotalT = TotalT + CDbl(rr.Pago_Total)
        Next
        GridAmortizaciones.DataSource = TAmortizaciones
        GridAmortizaciones.DataBind()

        If GridAmortizaciones.Rows.Count > 0 Then
            BotonImp.Visible = True
            GridAmortizaciones.Visible = True
            Dim TIR As Double = IRR(PagoS, 0.01)
            Session.Item("CAT") = Math.Round((((1 + (TIR)) ^ NoPagosAnual) - 1), 3).ToString("p1")
            'LbAjuste.Text = "CAT: " & Cat & "  Ajuste: " & Diferencia.ToString
        End If

    End Sub

    Private Sub GridAmortizaciones_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridAmortizaciones.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(1).Text = CDate(e.Row.Cells(1).Text).ToShortDateString()
            e.Row.Cells(3).Text = CDec(e.Row.Cells(3).Text).ToString("n2")
            e.Row.Cells(4).Text = CDec(e.Row.Cells(4).Text).ToString("n2")
            e.Row.Cells(5).Text = CDec(e.Row.Cells(5).Text).ToString("n2")
            e.Row.Cells(6).Text = CDec(e.Row.Cells(6).Text).ToString("n2")
            e.Row.Cells(7).Text = CDec(e.Row.Cells(7).Text).ToString("n2")
            e.Row.Cells(8).Text = CDec(e.Row.Cells(8).Text).ToString("n2")
            e.Row.Cells(9).Text = CDec(e.Row.Cells(9).Text).ToString("n2")
            e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(5).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(6).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(7).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(8).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(9).HorizontalAlign = HorizontalAlign.Right

        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(2).Text = DiasT
            e.Row.Cells(4).Text = CapitaT.ToString("n2")
            e.Row.Cells(5).Text = InteresT.ToString("n2")
            e.Row.Cells(6).Text = PagoT.ToString("n2")
            e.Row.Cells(7).Text = IvaT.ToString("n2")
            e.Row.Cells(8).Text = SegT.ToString("n2")
            e.Row.Cells(9).Text = TotalT.ToString("n2")
            e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(5).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(6).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(7).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(8).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(9).HorizontalAlign = HorizontalAlign.Right
        End If

    End Sub

    Protected Sub CmbPlazo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbPlazo.SelectedIndexChanged
        TxtTasa.Text = ta.TasaAplicacble(CmbPlazo.SelectedValue, ta.SacaPeriodoMAX, "CS")
    End Sub

    Protected Sub BotonImp_Click(sender As Object, e As EventArgs) Handles BotonImp.Click
        Dim rep As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        Dim DS As New CotizaDS
        Dim R As CotizaDS.ReporteRow
        For Each rr As GridViewRow In GridAmortizaciones.Rows
            R = DS.Tables("Reporte").NewRow
            R.NoPago = rr.Cells.Item(0).Text
            R.FecCon = Now.Date
            R.FecVen = rr.Cells.Item(1).Text
            R.Dias = rr.Cells.Item(2).Text
            R.Saldo = rr.Cells.Item(3).Text
            R.Extra = 0
            R.Capital = rr.Cells.Item(4).Text
            R.Interes = rr.Cells.Item(5).Text
            R.Pago = rr.Cells.Item(6).Text
            R.Iva = rr.Cells.Item(7).Text
            R.Seguro = rr.Cells.Item(8).Text
            R.PagoT = rr.Cells.Item(9).Text
            R.Tasa = TxtTasa.Text
            R.Seg = TasaVidaMes
            DS.Tables("Reporte").Rows.Add(R)
        Next
        Dim newrptRepSalCli As New ReportDocument()
        newrptRepSalCli.Load(Server.MapPath("~/Cotizadores/CotizacionCS.rpt"))
        newrptRepSalCli.SetDataSource(DS)
        If CmbTipoPers.SelectedValue = "M" Then
            newrptRepSalCli.SetParameterValue("TipoPersona", "PERSONA MORAL")
        Else
            newrptRepSalCli.SetParameterValue("TipoPersona", "PERSONA FISICA CON ACTIVIDAD EMPRESARIAL")
        End If

        newrptRepSalCli.SetParameterValue("CAT", Session.Item("CAT"))
        Dim Comision As Decimal = CDec(TxtMonto.Text) * CDec(TxtComision.Text) / 100
        newrptRepSalCli.SetParameterValue("Comision", Comision)
        newrptRepSalCli.SetParameterValue("IvaComision", Comision * 0.16)
        newrptRepSalCli.SetParameterValue("Ratific", 1724.14)
        newrptRepSalCli.SetParameterValue("RatificIVA", 275.86)

        Dim cad As String = "~\tmp\" & Date.Now.ToString("yyyyMMddmmss") & ".pdf"
        newrptRepSalCli.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath(cad))

        Response.Write("<script>")
        cad = cad.Replace("\", "/")
        cad = cad.Replace("~", "..")
        Response.Write("window.open('" & cad & "','_blank')")
        Response.Write("</script>")
        'Response.Write("")

        'CrystalReportViewer1.ReportSource = newrptRepSalCli
        'CrystalReportViewer1.Visible = True
        'GridAmortizaciones.Visible = False
        'BtPrint.Enabled = False
    End Sub
End Class