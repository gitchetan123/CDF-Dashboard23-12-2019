<%@ Page Title="" Language="C#" MasterPageFile="~/CDFMaster.master" AutoEventWireup="true" CodeFile="ViewGraph.aspx.cs" Inherits="Candidate_ViewGraph" %>
<%@ Register TagPrefix="web" Namespace="WebChart" Assembly="WebChart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .panel
        {
            padding-bottom: 10px;
            padding-left: 10px;
            padding-right: 10px;
            padding-top: 10px;
            text-align: center;
            max-width: 900px;
            margin: 0 auto;
        }
        
        .row
        {
            padding: 2px;          
        
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="panel panel-primary">
        <div class="row">
            <div class="">
                <div class="col-sm-3">
                </div>
                <h3><asp:Label ID="lbl_name" class="control-label text-primary col-sm-6" runat="server"
                    Text=""></asp:Label></h3>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-2">
            </div>
            <div class="col-sm-8  text-info">
                <h3>
                    Personality Test
                </h3>
            </div>
        </div>
        <div class="row">
        <div class="col-sm-2">
            </div>
            <div class="col-sm-8">
                <h4>
                    Following is the result of Personality Test</h4>
            </div>
        </div>
        <div class="row">
         <div class="col-sm-3"></div>
            <table class="table-bordered text-info col-sm-6 ">
                <thead>
                    <tr>
                        <td width=25%>
                        </td>
                        <td width=25%>
                            M
                        </td>
                        <td width=25%>
                            L
                        </td>
                        <td width=25%>
                            DIFF
                        </td>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td width=25%>
                            R
                        </td>
                        <td width=25%>
                            <asp:Label ID="lblBM" class="control-label" runat="server"></asp:Label>
                        </td>
                        <td width=25%>
                            <asp:Label ID="lblBL" class="control-label" runat="server"></asp:Label>
                        </td>
                        <td width=25%>
                            <asp:Label ID="lblDiffB" class="control-label" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width=25%>
                            A
                        </td>
                        <td width=25%>
                            <asp:Label ID="lblRM" class="control-label" runat="server"></asp:Label>
                        </td>
                        <td width=25%>
                            <asp:Label ID="lblRL" class="control-label" runat="server"></asp:Label>
                        </td>
                        <td width=25%>
                            <asp:Label ID="lblDiffR" class="control-label" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width=25%>
                            P
                        </td>
                        <td width=25%>
                            <asp:Label ID="lblBlM" class="control-label" runat="server"></asp:Label>
                        </td>
                        <td width=25%>
                            <asp:Label ID="lblBlL" class="control-label" runat="server"></asp:Label>
                        </td>
                        <td width=25%>
                            <asp:Label ID="lblDiffBl" class="control-label" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width=25%>
                            D
                        </td>
                        <td width=25%>
                            <asp:Label ID="lblGM" class="control-label" runat="server"></asp:Label>
                        </td>
                        <td width=25%>
                            <asp:Label ID="lblGL" class="control-label" runat="server"></asp:Label>
                        </td>
                        <td width=25%>
                            <asp:Label ID="lblDiffG" class="control-label" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr align="center">
                        <td width=25%>
                        </td>
                        <td width=25%>
                            TOTAL
                        </td>
                        <td width=25%>
                            <asp:Label ID="lblTotal" class="control-label" runat="server"></asp:Label>
                        </td>
                        <td width=25%>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    <div class="row">
    <div class="col-sm-2"></div>
            <div class="col-sm-8">
                <h4>
                    Following are the resultant graphs of Personality Test
                </h4>
            </div>
        </div>
        <div class="row">
         <table class="table-responsive col-sm-12 ">
            <tbody>
                <tr>
                    <td height="20">
                        <web:ChartControl runat="server" ID="ChartControl1" ChartPadding="30" BottomChartPadding="20"
                            TopPadding="20" Padding="20" BorderStyle="outset" GridLines="Both" Legend-Position="Bottom"
                            Legend-Width="30" Width="250px" YCustomEnd="19" YCustomStart="-19" YValuesInterval="1"
                            Height="550px">
                            <Background Type="LinearGradient" Color="#CCCCFF" EndPoint="900, 900" />
                            <Border Color="102, 153, 255" />
                            <ChartTitle Text="RAPD Graph1" Font="Tahoma, 12pt, style=Bold" ForeColor="White"
                                StringFormat="Center,Near,Character,LineLimit" />
                            <XAxisFont StringFormat="Near,Center,Character,DirectionVertical" />
                            <XTitle Font="Tahoma, 8pt, style=Bold" ForeColor="White" StringFormat="Center,Near,Character,LineLimit" />
                            <YTitle Font="Tahoma, 8pt, style=Bold" StringFormat="Near,Near,Character,DirectionVertical" />
                            <YAxisFont StringFormat="Far,Near,Character,LineLimit" />
                            <Legend Position="Bottom" Width="30"></Legend>
                        </web:ChartControl>
                        <web:ChartControl runat="server" ID="ChartControl2" ChartPadding="30" BottomChartPadding="20"
                            TopPadding="20" Padding="20" BorderStyle="outset" GridLines="Both" Legend-Position="Bottom"
                            Legend-Width="30" Width="250px" Height="550px" YCustomEnd="19" YCustomStart="-19"
                            YValuesInterval="1">
                            <Background Type="LinearGradient" Color="#CCCCFF" EndPoint="900, 900" />
                            <Border Color="102, 153, 255" />
                            <ChartTitle Text="RAPD Graph2" Font="Tahoma, 12pt, style=Bold" ForeColor="White"
                                StringFormat="Center,Near,Character,LineLimit" />
                            <XAxisFont StringFormat="Near,Center,Character,DirectionVertical" />
                            <XTitle Font="Tahoma, 8pt, style=Bold" ForeColor="White" StringFormat="Center,Near,Character,LineLimit" />
                            <YTitle Font="Tahoma, 8pt, style=Bold" StringFormat="Near,Near,Character,DirectionVertical" />
                            <YAxisFont StringFormat="Far,Near,Character,LineLimit" />
                            <Legend Position="Bottom" Width="30"></Legend>
                        </web:ChartControl>
                        <web:ChartControl runat="server" ID="ChartControl3" ChartPadding="30" BottomChartPadding="20"
                            TopPadding="20" Padding="20" BorderStyle="outset" GridLines="Both" Legend-Position="Bottom"
                            Legend-Width="30" Width="250px" Height="550px" YCustomEnd="19" YCustomStart="-19"
                            YValuesInterval="1">
                            <Background Type="LinearGradient" Color="#CCCCFF" EndPoint="900, 900" />
                            <Border Color="102, 153, 255" />
                            <ChartTitle Text="RAPD Graph3" Font="Tahoma, 12pt, style=Bold" ForeColor="White"
                                StringFormat="Center,Near,Character,LineLimit" />
                            <XAxisFont StringFormat="Near,Center,Character,DirectionVertical" />
                            <XTitle Font="Tahoma, 8pt, style=Bold" ForeColor="White" StringFormat="Center,Near,Character,LineLimit" />
                            <YTitle Font="Tahoma, 8pt, style=Bold" StringFormat="Near,Near,Character,DirectionVertical" />
                            <YAxisFont StringFormat="Far,Near,Character,LineLimit" />
                            <Legend Position="Bottom" Width="30"></Legend>
                        </web:ChartControl>
                    </td>
                </tr>
            </tbody>
        </table>
  
      </div>       
         <a href=javascript:history.back()>Back</a> 
    </div>   
   
</asp:Content>


