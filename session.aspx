<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CDFMaster.master" CodeFile="session.aspx.cs" Inherits="session" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">  
   
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
    <script src="js/bootstrap.min.js"></script>

    <script type="text/javascript">
        //$(document).ready(function () {
        //    //debugger;
        //    //$('[id*=hlView]').show();
        //    //$('[id*=report_btn]').hide();
        //});
      
        //$('[id*=hlView]').click(function () {
        //    debugger;
        //    alert('hiiiii');
        //   // $('[id*=report_btn]').show();
        //})
       

        //debugger;
        //$('[id*=hlView]').on('click', function () {
        //    alert("The paragraph was clicked.");
        //});

        function myloader()
        {            
            setTimeout(function () {
                $('#loaderdiv').fadeToggle('fast');
                window.location.reload(true);
                
            }, 20000); // <-- time in milliseconds
          
        }

    
         
        function openpopup()
        {
            $("#myModalPopupUserFeedback").modal('show');
        }
       
    
       
       
        function OpenModal() {
          
            $("#myModal").modal('show');
        }
        function CloseModal() {
            $("#myModal").modal('hide');
        } 

    </script>

    <style type="text/css">
        .btn-success, .btn-danger {
            margin: 10px;
        }
        .dtl td
        {
            padding: 5px;
        }
        .dtl th
        {
            padding: 5px;
        }     
       


     

        .btnOTP {
            margin-top: 3px;
            margin-left: 4px;
        }

    </style>
         <link rel="stylesheet" href="css/bootstrap.min.css" integrity="sha384-GJzZqFGwb1QTTN6wy59ffF1BuGJpLSa9DkKMp0DgiMDm4iYMj70gZWKYbI706tWS" crossorigin="anonymous"/>
        <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.2.1/js/bootstrap.min.js" integrity="sha384-B0UglyR+jN6CkvvICOB2joaf5I4l3gm9GU6Hc1og6Ls7i6U/mkkaduKaBhlAXv9k" crossorigin="anonymous"></script>
       


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="script1" runat="server"></asp:ScriptManager>

           <asp:UpdateProgress ID="UpdateProgress1"  runat="server">
                <ProgressTemplate>
               
              <div id="loaderdiv" style="position: fixed; text-align: center; height: 100%; width: 100%; top:0; right: 0; left: 0; z-index: 9999999; background-color:#000000bf; opacity:1;"> <br /><br /><br /><br /><br /><br />  
             <br /><br /><br /><br /><br /><br /> <img src="images/ajax-loader.gif" style="height: 158px; text-align:center; width: 211px;" alt="" />         
               </div>     
                    
                </ProgressTemplate>
            </asp:UpdateProgress>

       <%--  <div id="loaderDiv" class="loading" style="text-align:center">
                  Loading. Please wait.<br />
          <img src="images/ajax-loader.gif" style="height:71px;width:108px" alt="" />       
          </div>--%>
      
     

        <div class="col-md-12 col-sm-10 col-xs-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Upcoming Sessions                       
                    </h2>
                    <ul class="nav navbar-right panel_toolbox">
                        <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                        </li>
                        <li><a href="javascript:location.reload()"><i class="fa fa-refresh" aria-hidden="true"></i></a>
                        </li>                      
                    </ul>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">                 
                     <asp:Label ID="NoSesFound" runat="server" Text=""></asp:Label>  
                    <div style="overflow-x:scroll;">                  
                    <asp:GridView ID="Gridmysession" runat="server" AutoGenerateColumns="False" CellPadding="4"                                               
                        CssClass="table"  AllowPaging="True" PageSize="10"  ForeColor="#333333" GridLines="None" 
                          OnRowDataBound="Gridmysession_RowDataBound" OnRowCommand="Gridmysession_RowCommand" >
                        <AlternatingRowStyle HorizontalAlign="Center" BackColor="White" ForeColor="#284775" />
                        <EditRowStyle BackColor="#999999" />                        
                        <FooterStyle BackColor="#35578a" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#35578a" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="black" HorizontalAlign="Center" />
                        <RowStyle HorizontalAlign="Center" BackColor="#F7F6F3" ForeColor="#333333" />
                        <Columns>
                             <asp:BoundField DataField="LeadType" HeaderText="Lead Type" SortExpression="LeadType" />

                            <asp:BoundField DataField="YourRole" HeaderText="Your Role" SortExpression="YourRole" />

                            <asp:BoundField DataField="ShadowCDF" HeaderText="Shadow CDF" SortExpression="ShadowCDF" />

                             <asp:BoundField DataField="ConductingCDF" HeaderText="Conducting CDF " SortExpression="ConductingCDF" />

                            <asp:BoundField DataField="SesId" HeaderText="Session_Id"
                                ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" SortExpression="SesId" />

                            <asp:BoundField DataField="StudId" HeaderText="StudId" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />

                              <asp:BoundField DataField="productId" HeaderText="productId" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                            <asp:BoundField DataField="CandName" HeaderText="Candidate Name"
                                SortExpression="CandName" />

                            <asp:BoundField DataField="CandGender" HeaderText="Gender"
                                SortExpression="CandGender" />

                            <asp:BoundField DataField="CandContact" HeaderText="Contact"
                                SortExpression="CandContact" />

                            <asp:BoundField DataField="CandEmail" HeaderText="Email"
                                SortExpression="CandEmail" />

                            <asp:BoundField DataField="SesDate" ItemStyle-Width="50px" ItemStyle-Wrap="False"  HeaderText="Session Date"
                                SortExpression="SesDate" />

                            <asp:BoundField DataField="SesAddress" HeaderText="Session Address" ItemStyle-Width="212px" ItemStyle-Wrap="true"
                                SortExpression="SesAddress" />

                            <asp:BoundField DataField="SesCity" HeaderText="Session City"
                                SortExpression="SesCity" />

                            <asp:BoundField DataField="SesCityId" HeaderText="SesCityId"
                                SortExpression="SesCityId" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />

                            <asp:BoundField DataField="SesTime" HeaderText="Session Time"
                                SortExpression="SesTime" />


                            <asp:BoundField DataField="SesStatus" HeaderText="Session Status"
                                 ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" SortExpression="SesStatus" />

                            <asp:BoundField DataField="CdfAcceptance" HeaderText="CDF Acceptance"
                                SortExpression="CdfAcceptance" />

                            <%--<asp:TemplateField HeaderText="Requset For Session Completion OTP">
                                <ItemTemplate>
                                    <asp:Button ID="btnRequestForOTP" runat="server" Text="Requset For OTP"/>
                                </ItemTemplate>
                            </asp:TemplateField>--%>

                            
                            <asp:TemplateField HeaderText="Requset For Session Completion OTP">
                                <ItemTemplate>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <%--data-toggle="modal" data-target="#myModal"--%>
                                            <asp:Button ID="btnOTP" CssClass="btn btn-success btnOTP" 
                                                runat="server" data-toggle="modal" data-target="#myModal" Text="Requset For OTP" Visible='<%#  (Eval("CdfAcceptance").ToString() == "Accepted" && Convert.ToBoolean(Eval("MatchDateTime"))==true )%>'
                                                CommandArgument='<%# Eval("CdfAcceptance") + ","+ Eval("MatchDateTime")+","+Eval("StudId") %>' CommandName="RequsetOTP" />
                                        
                                           
                                           
                                            <%--<asp:Button ID="Button1" CssClass="btn btn-danger btn-sm btn-block" 
                                                           runat="server" Text="Requset For OTP" OnClientClick="if ( ! UserActive()) return false;"
                                                           CommandArgument='<%# Eval("CdfAcceptance") + ","+ Eval("MatchDateTime") %>' CommandName="RequsetOTP" />--%>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </ItemTemplate>
                            </asp:TemplateField>

                          <%--   <asp:TemplateField  HeaderText="Session OTP" >
                                <ItemTemplate>
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>                            
                                              <asp:TextBox ID="Txtsubmit" runat="server"  placeholder="Enter OTP Here" Visible="false" ></asp:TextBox>  <br/>
                                             <asp:Label ID="OTP_Erro" runat="server" Text=""  ForeColor="Red"></asp:Label>  <br/>                                         
                                            <asp:Button ID="btnsumit" runat="server"  OnClick="btn_sendOTP_Click" Text="Submit" Visible="false"  />
                                             </ContentTemplate>
                                    </asp:UpdatePanel>
                                </ItemTemplate>
                            </asp:TemplateField>--%>


                            <%--<asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="btn_active" CssClass="btn btn-danger btn-sm btn-block" Visible='<%#  (Eval("userStatus").ToString()) == "DEACTIVE"  %>'
                                                runat="server" Text="DEACTIVATED" OnClientClick="if ( ! UserActive()) return false;"
                                                CommandArgument='<%# Eval("candID") + "," + "ACTIVE" %>' CommandName="Status" />
                                            <asp:Button ID="Button2" CssClass="btn btn-success btn-sm btn-block" Visible='<%#  (Eval("userStatus").ToString()) == "ACTIVE" %>'
                                                runat="server" Text="ACTIVE" OnClientClick="if ( ! UserDeactive()) return false;"
                                                CommandArgument='<%# Eval("candID") + "," + "DEACTIVE" %>' CommandName="Status" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </ItemTemplate>
                            </asp:TemplateField>--%>

                           <asp:TemplateField HeaderText="Graph">
                            <ItemTemplate>
                           <%--  <asp:HyperLink ID="hlView"  ToolTip="View Graph"  OnClick="myfunction"  ForeColor="DarkBlue"  runat="server" NavigateUrl='<%# Eval("StudId", "~/Graph_Report/ViewGrah_Scheduling.aspx?StudId={0}") %>'>View</asp:HyperLink>--%>
                             
                              <%--<asp:LinkButton ID="LinkEditLine" ToolTip="View Graph"  ForeColor="DarkBlue" runat="server" Text="View" NavigateUrl='<%# Eval("StudId", "~/Graph_Report/ViewGrah_Scheduling.aspx?StudId={0}") %>'/>--%>                               
                        
                                      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                   <ContentTemplate>    
                              <asp:Button ID="graph_btn" ToolTip="View Graph"  runat="server" Text="View Graph" CommandArgument='<%# Eval("StudId")%>' CommandName="graph" />
                             </ContentTemplate>
                                   <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="graph_btn" EventName="Click" />
                                    </Triggers>
                                 </asp:UpdatePanel>
                          <%--<asp:LinkButton ID="hyperlinkGraph" runat="server" Text="view" CommandName="graph"  CommandArgument='<%# Eval("StudId") %>'></asp:LinkButton>--%>                          

                            <%--    <asp:HyperLink ID="graph_download" ForeColor="DarkBlue" runat="server" >Download</asp:HyperLink>--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                      <asp:TemplateField HeaderText="Report">
                            <ItemTemplate>                               
                                <%--<asp:HyperLink ID="HyperLink1" target="_blank"  runat="server" ForeColor="DarkBlue" NavigateUrl='<%# Eval("StudId", "~/Graph_Report/Half_Report_By_RAPD1_ForCDF.aspx?StudId={0}") %>'
                                    CssClass="bodytext" ToolTip="Download Report" ImageUrl="~/images/pdf.jpg">View_Report</asp:HyperLink>--%>
                                  <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                   <ContentTemplate>    
                                  <asp:Button ID="report_btn" ToolTip="Download Report" runat="server"  Text="Download Report" CommandArgument='<%# Eval("productId") + ","+Eval("StudId") %>' CommandName="report" OnClientClick="myloader()"  />
                                </ContentTemplate>
                                   <Triggers>
                                         <asp:AsyncPostBackTrigger ControlID="report_btn" EventName="Click" />
                                    </Triggers>
                                 </asp:UpdatePanel>
                                <%--<asp:HyperLink ID="Full_Report"  runat="server" NavigateUrl='<%# Eval("StudId", "~/Graph_Report/New_Report_Download.aspx?StudId={0}") %>'
                                    CssClass="bodytext"></asp:HyperLink>--%>  


                                <%--<asp:LinkButton ID="hyperlinkGraph" runat="server" CommandName="graph" CommandArgument='<%# Eval("StudId", "~/Graph_Report/ViewGrah_Scheduling.aspx?StudId={0}") %>'></asp:LinkButton>--%>                          
                                              
                                              
                            </ItemTemplate>
                        </asp:TemplateField>
                             <asp:BoundField DataField="shadowAcceptance" HeaderText="shadowAcceptance"
                                ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" SortExpression="shadowAcceptance" />
                     
                        </Columns>                        
                        <PagerStyle BackColor="#35578a" HorizontalAlign="Center"
                            Font-Bold="True" CssClass="pagination-ys" Wrap="True" />
                    </asp:GridView>   
           
                </div>
               </div>
            </div>
        </div>

        <div class="col-md-12 col-sm-10 col-xs-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>History</h2>
                    <ul class="nav navbar-right panel_toolbox">
                        <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                        </li>
                        <li><a href="javascript:location.reload()"><i class="fa fa-refresh" aria-hidden="true"></i></a>
                        </li>                       
                    </ul>
                    <div class="clearfix"></div>
                </div>
               <div style="overflow-x:scroll;">
                    <asp:GridView ID="dtl" runat="server" AutoGenerateColumns="False"  HeaderStyle-CssClass="header"
                        CssClass="dtl"  AllowPaging="True" PageSize="10" CellPadding="4" ForeColor="#333333" GridLines="None"
                         OnRowDataBound="dtl_RowDataBound"  OnRowCommand="dtl_RowCommand"  OnPageIndexChanging="dtl_PageIndexChanging">
                        <AlternatingRowStyle HorizontalAlign="Center" BackColor="White" ForeColor="#284775" />
                        <EditRowStyle BackColor="#999999"/>
                        <FooterStyle BackColor="#35578a" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#35578a" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="black" HorizontalAlign="Center" />
                        <RowStyle HorizontalAlign="Center" BackColor="#F7F6F3" ForeColor="#333333" />
                        <Columns>
                            <asp:BoundField DataField="LeadType" HeaderText="Lead Type" SortExpression="LeadType" />

                              <asp:BoundField DataField="YourRole" HeaderText="Your Role" SortExpression="YourRole" />

                              <asp:BoundField DataField="ShadowCDF"  HeaderText="Shadow CDF" SortExpression="ShadowCDF" />

                             <asp:BoundField DataField="ConductingCDF" HeaderText="Conducting CDF " SortExpression="ConductingCDF" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />

                            <asp:BoundField DataField="SesId" HeaderText="Session_Id"
                                ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" SortExpression="SesId" />

                            <asp:BoundField DataField="CandName" ItemStyle-HorizontalAlign="Center" HeaderText="Candidate Name"
                                SortExpression="CandName">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="CandGender" HeaderText="Gender"
                                SortExpression="CandGender" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />

                            <asp:BoundField DataField="CandContact" HeaderText="Contact"
                                SortExpression="CandContact" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />

                            <asp:BoundField DataField="CandEmail" ItemStyle-HorizontalAlign="Center" HeaderText="Email"
                                HeaderStyle-HorizontalAlign="Center"
                                SortExpression="CandEmail">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="SesDate" ItemStyle-Width="50px" ItemStyle-Wrap="False" HeaderText="Session Date"
                                SortExpression="SesDate" />


                            <asp:BoundField DataField="SesAddress" HeaderText="Session Address" SortExpression="SesAddress" />


                            <asp:BoundField DataField="SesCity" HeaderText="Session City" SortExpression="SesCity" />

                            <asp:BoundField DataField="SesCityId" HeaderText="SesCityId"
                                SortExpression="SesCityId" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />

                            <asp:BoundField DataField="SesTime" HeaderText="Session Time" SortExpression="SesTime" />

                            <asp:BoundField DataField="SesStatus" HeaderText="Session Status" SortExpression="SesStatus" />

                            <asp:BoundField DataField="CdfAcceptance"  HeaderText="CDF Acceptance" SortExpression="CdfAcceptance" />

                            <asp:BoundField DataField="productId" HeaderText="productId" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />

                            <asp:TemplateField HeaderText="Graph">
                            <ItemTemplate> 
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                             <ContentTemplate> 
                              <asp:Button ID="graph_btn1"  ToolTip="View Graph"  runat="server" Text="View Graph" CommandArgument='<%# Eval("StudId")%>' CommandName="graph" />
                             </ContentTemplate>
                                   <Triggers>
                             <asp:AsyncPostBackTrigger ControlID="graph_btn1" EventName="Click" />
                                    </Triggers>
                             </asp:UpdatePanel> 
                            </ItemTemplate>
                        </asp:TemplateField>
                      <asp:TemplateField HeaderText="Report">
                            <ItemTemplate> 
                                 <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                             <ContentTemplate> 
                                 <%-- <asp:Button ID="report_btn1" ToolTip="Download Report" runat="server"  Text="Download Report" CommandArgument='<%# Eval("productId") + ","+Eval("StudId") %>' CommandName="report" OnClientClick="myloader()" />--%>
                                 <asp:ImageButton ID="report_btn1" ToolTip="Download Report" runat="server"  CommandArgument='<%# Eval("productId") + ","+Eval("StudId") %>' CommandName="report" Height="38px" OnClientClick="myloader()" ImageUrl="~/images/pdf.jpg"  />
                            </ContentTemplate>
                                   <Triggers>
                             <asp:AsyncPostBackTrigger ControlID="report_btn1" EventName="Click" />
                                    </Triggers>
                             </asp:UpdatePanel> 
                                  </ItemTemplate>
                        </asp:TemplateField>

                           <asp:TemplateField HeaderText="FeedBack">
                            <ItemTemplate> 
                                <asp:UpdatePanel ID="UpdatePanel" runat="server">
                             <ContentTemplate> 
                              <asp:Button ID="btnfeedback" ToolTip="View Feedback" CommandArgument='<%# Eval("StudId")%>' CommandName="feedback" runat="server" Text="FeedBack"/>
                             </ContentTemplate>                                  
                             </asp:UpdatePanel> 
                            </ItemTemplate>
                        </asp:TemplateField>                                          

                        </Columns>
                          <PagerStyle BackColor="#35578a" HorizontalAlign="Center"
                            Font-Bold="True" CssClass="pagination-ys" Wrap="True" />
                    </asp:GridView>
                </div>
            </div>
        </div> 

        <div class="modal fade" id="myModal"   role="dialog" data-backdrop="static">
            <div class="modal-dialog modal-sm">
                <!-- Modal content-->
                <div class="modal-content" style="width:388px;">
                    <div class="modal-header">
                        <button type="button" class="close" onclick="window.location.reload()" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title" style="text-align: center">Session Completion OTP</h4>
                    </div>
                 
                    <div class="modal-body" style="height: 83px; width: 388px;">                                           
                         <asp:TextBox ID="txtReqOTP" runat="server" Placeholder="Enter OTP">
                         </asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;                    
                         <asp:Button ID="btn_sendOTP" runat="server" Text="Submit"  OnClick="btn_sendOTP_Click"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;
                         <asp:Button ID="btn_Resend" runat="server" Text="Resend OTP" OnClick="btn_Resend_Click"></asp:Button>
                        <asp:Label ID="OTP_Error" runat="server" Text=""  ForeColor="Red"></asp:Label> 
                    </div>                 
                    <%--  <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>--%>
                </div>

            </div>
        </div>   


         <%--  FeedBackModal--%>
      <div class="modal fade" id="myModalPopupUserFeedback" role="dialog" data-backdrop="static">
                                    <div class="modal-dialog modal-lg" style="margin-left: 6%; width: 200px">
                                        <div class="widget-bg">
                                            <div class="row">
                                                <div class="col-sm-2">
                                                    <div class="panel panel-success" style="text-align: center;display:table-row; height: 450px">
                                                        <div class="modal-header">
                                                            <button type="button" class="close" data-dismiss="modal" style="font-size: 35px;">
                                                                &times;
                                                            </button>
                                                            <h3 class="modal-title">Student Feedback</h3>
                                                        </div>   
                                                        <div    class="modal-body" style="width:1501px;" >  
                                                          
                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
				                                            	<ContentTemplate> 
                                                            <asp:Label ID="feedbacklbl" style="color:red;font-size:medium" runat="server" ></asp:Label> 
                                                                      <div id="feedbackdiv" runat="server">
                                                            <table id="feedbacktbl" class="table"  > 
                                                                 <tr>
                                                                        <th>Ques No.</th>
                                                                        <th style="text-align:center;">Question</th>
                                                                        <th>Answer</th>
                                                                </tr>
                                                                 <tr>
                                                                        <td> <asp:Label ID="lblQno" runat="server"></asp:Label></td>
                                                                        <td><asp:Label ID="lblQ" runat="server" ></asp:Label></td>
                                                                        <td><asp:Label ID="lblAns" runat="server"></asp:Label></td>
                                                                 </tr> 
                                                                <tr>
                                                                        <td> <asp:Label ID="lblQno1" runat="server"></asp:Label></td>
                                                                        <td><asp:Label ID="lblQ1" runat="server" ></asp:Label></td>
                                                                        <td><asp:Label ID="lblAns1" runat="server"></asp:Label></td>
                                                                 </tr> 
                                                                 <tr>
                                                                        <td> <asp:Label ID="lblQno2" runat="server"></asp:Label></td>
                                                                        <td><asp:Label ID="lblQ2" runat="server" ></asp:Label></td>
                                                                        <td><asp:Label ID="lblAns2" runat="server"></asp:Label></td>
                                                                 </tr> 
                                                                 <tr>
                                                                        <td> <asp:Label ID="lblQno3" runat="server"></asp:Label></td>
                                                                        <td><asp:Label ID="lblQ3" runat="server" ></asp:Label></td>
                                                                        <td><asp:Label ID="lblAns3" runat="server"></asp:Label></td>
                                                                 </tr> 
                                                                  <tr>
                                                                        <td> <asp:Label ID="lblQno4" runat="server"></asp:Label></td>
                                                                        <td><asp:Label ID="lblQ4" runat="server" ></asp:Label></td>
                                                                        <td><asp:Label ID="lblAns4" runat="server"></asp:Label></td>
                                                                       

                                                                 </tr> 
                                                                 <tr>
                                                                        <td> <asp:Label ID="lblQno5" runat="server"></asp:Label></td>
                                                                        <td><asp:Label ID="lblQ5" runat="server" ></asp:Label></td>
                                                                        <td><asp:Label ID="lblAns5" runat="server"></asp:Label></td>
                                                                 </tr> 
                                                             </table>
                                                                           </div>
                                                                    </ContentTemplate>                       
				                                                </asp:UpdatePanel>
                                                               
                                                               
                                                                                                    
                                                              <%--  <div class="modal-body">
                                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="ID">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblID" runat="server" Text='<%#Eval("ID") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblname" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="btlSel" runat="server" Text="Select" CssClass="btn btn-primary" OnClick="btlSel_Click" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>  --%>      
                                                    </div>
                                                    </div>
                                               </div>
                                           </div>
                                        </div>
                                    </div>                                   
                                </div>      
                          
                                   
                                

      <%--  EndFeedBackModel--%>
        
        
        
        
          
    </form>
    
    <script src="js/custom.min.js"></script>
    <script type='text/javascript' src='https://www.google.com/jsapi'></script>

</asp:Content>


