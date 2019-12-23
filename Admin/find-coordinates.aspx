<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin-master.master" AutoEventWireup="true" CodeFile="find-coordinates.aspx.cs" Inherits="Admin_find_coordinates" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .row {
            padding-left: 10px;
            padding-right: 10px;
            padding: 5px;
        }

        /*#map {
             height: 400px;
             width:600px;    
             box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);         
         }*/
    </style>
    <script type="text/javascript">
        function calculateCoordinates() {
            debugger;
            var txtAddress1 = document.getElementById('<%= txtLocation.ClientID%>');
            var txtLatitude = document.getElementById('<%= txtLatitude.ClientID%>');
            var txtLongitude = document.getElementById('<%= txtLongitude.ClientID%>');
            var address = txtAddress1.value;
            var geocoder;
            geocoder = new google.maps.Geocoder();
            geocoder.geocode({ 'address': address }, function (results, status) {
                debugger;
                if (status == google.maps.GeocoderStatus.OK) {
                    debugger;
                    var location = results[0].geometry.location;
                    //txtLatitude.value = location.lat();
                    //txtLongitude.value = location.lng();
                    $("[id*=txtLatitude]").val(location.lat());
                    $("[id*=txtLongitude]").val(location.lng());
                    //  alert(txtLatitude.value);
                    //  alert(txtLongitude.value);
                }
                else
                    alert(' - not found');

            });

        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form runat="server">
        <div class="x_panel">
            <div class="x_title">
                <h2>Latitude and Longitude of User</h2>
                <ul class="nav navbar-right panel_toolbox">
                    <li><a href="#" data-toggle="modal" title="Help" data-target="#myModalHelpRequirements"><i class="fa fa-question"></i></a>
                    </li>
                    <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                    </li>
                    <li><a class="close-link"><i class="fa fa-close"></i></a>
                    </li>
                </ul>
                <div class="clearfix"></div>
            </div>

            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="x_content">
                        <br />
                        <asp:TextBox ID="txtLocation" CssClass="form-control" runat="server" Text=""></asp:TextBox>
                        <br />
                        <asp:ScriptManager ID="ScriptManager1" runat="server" />
                        <%-- <asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="ClientClick" />
                    </Triggers>
                    <ContentTemplate>--%>
                        <asp:Button ID="btnSearch" CssClass="btn btn-success btn-sm pull-left" runat="server" Text="Search" OnClientClick="return calculateCoordinates(this)" EnableTheming="False" />
                        <%--OnClick="FindCoordinates"--%>
                        <%-- </ContentTemplate>
                </asp:UpdatePanel>--%>
                        <br />
                        <br />
                        <asp:GridView ID="GridView1" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White"
                            runat="server" AutoGenerateColumns="false" Visible="false">
                            <Columns>
                                <asp:BoundField DataField="Id" HeaderText="Id" />
                                <asp:BoundField DataField="Address" HeaderText="Address" />
                                <asp:BoundField DataField="Latitude" HeaderText="Latitude" />
                                <asp:BoundField DataField="Longitude" HeaderText="Longitude" />
                            </Columns>
                        </asp:GridView>
                        <br />
                        <div class="row">
                            <div class="col-md-6">
                                <label style="text-align: right;" class=" control-label">
                                    Latitude:
                                </label>
                                <%--<asp:UpdatePanel ID="UpdateAmount" runat="server" UpdateMode="Conditional">
                                              <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                                              </Triggers>
                                              <ContentTemplate>--%>
                                <asp:TextBox ID="txtLatitude" CssClass="form-control" runat="server"></asp:TextBox>
                                <%--  </ContentTemplate>
                                         </asp:UpdatePanel>--%>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label style="text-align: right;" class=" control-label">
                                    Longitude:
                                </label>
                                <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                              <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                                              </Triggers>
                                              <ContentTemplate>--%>
                                <asp:TextBox ID="txtLongitude" CssClass="form-control" runat="server"></asp:TextBox>
                                <%-- </ContentTemplate>
                                         </asp:UpdatePanel>--%>
                            </div>
                        </div>


                        <div id="divErr" runat="server" class="" style="text-align: center; margin-top: 10px;">
                        </div>
                        <%--    <button id="btn" onclick="initMap() ">Find</button>--%>
                        <%-- <div class="row">
                    <div class="col-md-4 col-md-offset-2">
                        <div id="map"></div>
                    </div>
                </div>--%>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>

    <script>

        <%--        function initMap() {
            debugger;
          var myLatLng = { lat: <%=txtLatitude.ClientID %>, lng: <%=txtLongitude.ClientID %> };

        var map = new google.maps.Map(document.getElementById('map'), {
          zoom: 15,
          center: myLatLng
        });

        var marker = new google.maps.Marker({
          position: myLatLng,
          map: map,          
        });
      }--%>
    </script>
    <%--<script async defer
            src="https://maps.googleapis.com/maps/api/js?key=AIzaSyA4XKn81b2VaTmEdhrUOpkn-0eYz2-7Y6E&callback=initMap">
    </script>--%>

    <script src="https://maps.google.com/maps/api/js?key=AIzaSyCeTTcNZZRlIGUNCyJr4x3Y1bq-CK3hUCg" async="async" defer="defer"></script>

    <!-- Custom Theme Scripts -->
    <script src="../js/custom.min.js"></script>
</asp:Content>

