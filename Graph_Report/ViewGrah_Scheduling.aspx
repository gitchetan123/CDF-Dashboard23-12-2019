<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CDFMaster.master" CodeFile="ViewGrah_Scheduling.aspx.cs" Inherits="MobileAppReports_ViewGrah_Scheduling" %>


<%@ Register TagPrefix="web" Namespace="WebChart" Assembly="WebChart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
            padding: 1px;          
        
        }
    </style>
    
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
    <script type="text/javascript">
        function closeloader() {
            debugger;
            $("#myModal").modal('hide');
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="Form1" role="form" runat="server">
    <div class="panel panel-primary">

    <%--<div class="modal fade" id="myModal" role="dialog" data-backdrop="static"  style="display:none;">
    <div class="modal-dialog modal-sm">         
      <div class="modal-content">       
          <img src="../images/ajax-loader.gif" style="width:192px" alt=""/>
      </div>
      </div>
  </div>--%>
         
        <div class="row">
            <div class="col-sm-offset-2 col-sm-8  text-info">
                <h3><asp:Label ID="lbl_name" class="control-label text-primary" runat="server"
                    Text=""></asp:Label></h3>

            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <h3><asp:Label ID="lbl_username" class="control-label" runat="server"
                    Text=""></asp:Label></h3>
            </div>
        </div> 
         <div class="row">
            <div class="col-sm-12">
                <h3><asp:Label ID="lbl_age" class="control-label" runat="server"
                    Text=""></asp:Label></h3>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-2">
            </div>
            <div class="col-sm-8 text-info">
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
        &nbsp;
         &nbsp;
         &nbsp;
       
         
        <div>
            <asp:Button id="back" runat="server"  style="font-size:20px;color:black;font-weight:bold" PostBackUrl ="javascript:history.back()" Text="Back" Width="118px" /> 
        </div>
    </div>   

      
   
        
    </form>
     
    
</asp:Content>
