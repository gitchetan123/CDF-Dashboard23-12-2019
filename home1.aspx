<%@ Page Title="" Language="C#" MasterPageFile="~/CDFMaster.master" AutoEventWireup="true" CodeFile="home1.aspx.cs" Inherits="home1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .img {
            border-radius: 100px;
            width: 160px;
            height: 160px;
        }

         .GridmysessionCss td
        {
            padding: 5px;
        }
        .GridmysessionCss th
        {
            padding: 5px;
        }

        .header
            {
                background-color: #646464;
                font-family: Arial;
                color: White;
                border: none 0px transparent;
                height: 25px;
                text-align: center;
                font-size: 16px;
            }
 
            .rows
            {
                background-color: #fff;
                font-family: Arial;
                font-size: 14px;
                color: #000;
                min-height: 25px;
                text-align: left;
                border: none 0px transparent;
            }


        .card_text {
            font-size: 18px;
            font-family: Poppins;
            font-weight: normal;
            color: darkslategrey;
        }

        .row {
            padding: 5px;
        }

        .panel {
            padding: 10px;
            text-align: center;
            max-width: 700px;
            padding: 5px;
            margin-top: 50px;
        }

        @media (min-width: 1200px) {
           form{

               width:auto;
               height:auto;
              /* margin:auto;*/
               
               /*zoom:80%;*/
           }
       }
         @media (min-width: 992px) {
           form{

               /* width:auto;
               height:auto;
              /* margin:auto;*/
               
              zoom:80%;
           }
       }
        
    </style>
    <!-- Bootstrap -->
    <link href="vendors/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Font Awesome -->
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/latest/css/font-awesome.min.css" rel="stylesheet" />
    <!-- NProgress -->
    <%--<link href="../../vendors/nprogress/nprogress.css" rel="stylesheet" />--%>
    <!-- not found -->
    <!-- bootstrap-progressbar -->
    <%--<link href="../../vendors/bootstrap-progressbar/css/bootstrap-progressbar-3.3.4.min.css" rel="stylesheet" />--%>
    <!-- Not found -->

    <!-- Custom Theme Style -->
    <link href="css/custom.min.css" rel="stylesheet" />
    <link href="css/pagination.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="https://code.jquery.com/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>




    <%--  <meta charset="utf-8"/>
  <meta name="viewport" content="width=device-width, initial-scale=1"/>
  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css"/>
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js"></script>--%>



    <!-- Tell the browser to be responsive to screen width -->     
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <!-- Bootstrap 3.3.7 -->
    <%--<link rel="stylesheet" href="<%=ResolveUrl("~") %>bower_components/bootstrap/dist/css/bootstrap.min.css" />--%>
    <!-- Font Awesome -->
    <%--<link rel="stylesheet" href="<%=ResolveUrl("~") %>bower_components/font-awesome/css/font-awesome.min.css" />--%>
    <!-- Ionicons -->
    <%--<link rel="stylesheet" href="<%=ResolveUrl("~") %>bower_components/Ionicons/css/ionicons.min.css" />--%>
    <!-- Theme style -->
    <%--<link rel="stylesheet" href="<%=ResolveUrl("~") %>dist/css/AdminLTE.css" />--%>
    <!-- AdminLTE Skins. Choose a skin from the css/skins
       folder instead of downloading all of them to reduce the load. -->
    <%--<link rel="stylesheet" href="<%=ResolveUrl("~") %>dist/css/skins/_all-skins.min.css" />--%>
    <!-- Morris chart -->
    <%--<link rel="stylesheet" href="<%=ResolveUrl("~") %>bower_components/morris.js/morris.css" />--%>
    <!-- jvectormap -->
    <%-- <link rel="stylesheet" href="<%=ResolveUrl("~") %>bower_components/jvectormap/jquery-jvectormap.css" />--%>
    <!-- Date Picker -->
    <%--   <link rel="stylesheet" href="<%=ResolveUrl("~") %>bower_components/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css">--%>
    <!-- Daterange picker -->
    <%--    <link rel="stylesheet" href="<%=ResolveUrl("~") %>bower_components/bootstrap-daterangepicker/daterangepicker.css">--%>
    <!-- Google Font -->
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic" />
    <%--<link href="<%=ResolveUrl("~") %>dist/css/pagination.css" rel="stylesheet" />--%>

    <style>
        .imgg {
            /* Start the shake animation and make the animation last for 0.5 seconds */
            animation: shake 0.5s;
            /* When the animation is finished, start again */
            animation-iteration-count: infinite;
        }
        /*.GridmysessionCss
        {
            height:auto;
            width:auto;         
            
        }*/

      .header{
          text-align:center;
      }
 
      
      
        .pager
        {
            background-color: #646464;
            font-family: Arial;
            color: White;
            height: 30px;
            text-align: left;
        }
 
      

      
      


        .zoom {
            padding: 3px;
            background-color: #f7f7f7;
            transition: transform .2s;
            width: 200px;
            height: 200px;
            margin: 0 auto;
        }

            .zoom:hover {
                -ms-transform: scale(1.5); /* IE 9 */
                -webkit-transform: scale(1.5); /* Safari 3-8 */
                transform: scale(1.5);
            }

        .curve-corner {
            border-radius: 4px;
        }

        @keyframes shake {
            0% {
                transform: translate(2px, -1px) rotate(1deg);
            }
            /*10% {
                transform: translate(-1px, -2px) rotate(-1deg);
            }

            20% {
                transform: translate(-3px, 0px) rotate(1deg);
            }

            30% {
                transform: translate(3px, 2px) rotate(0deg);
            }

            40% {
                transform: translate(1px, -1px) rotate(1deg);
            }

            50% {
                transform: translate(-1px, 2px) rotate(-1deg);
            }

            60% {
                transform: translate(-3px, 1px) rotate(0deg);
            }

            70% {
                transform: translate(3px, 1px) rotate(-1deg);
            }

            80% {
                transform: translate(-1px, -1px) rotate(1deg);
            }

            90% {
                transform: translate(1px, 2px) rotate(0deg);
            }*/
            100% {
                transform: translate(1px, -2px) rotate(-1deg);
            }
        }


        .cursor {
            cursor: pointer;
        }

        img.hover-shadow {
            transition: 0.3s;
        }

        .hover-shadow:hover {
            box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);
        }

        .filter {
            height: 36px;
            width: 100px;
            margin-left: 985px;
            border-radius: 4px;
        }

        .textbox {
            border: 0px;
            height: 0px;
            width: 0px;
        }

        .count {
            font-size: 8px;
        }

        .target {
            font-size: 14px;
            color: black;
        }

        .awesome {
            font-family: Cambria;
            font-style: normal;
            margin: 0 auto;
            color: #313131;
            font-weight: bold;
            -webkit-animation: colorchange 2s infinite alternate;
        }

        @-webkit-keyframes colorchange {
            0% {
                color: black;
            }

            10% {
                color: red;
            }

            20% {
                color: black;
            }

            30% {
                color: red;
            }

            40% {
                color: black;
            }

            50% {
                color: red;
            }

            60% {
                color: black;
            }

            70% {
                color: red;
            }

            80% {
                color: black;
            }

            90% {
                color: red;
            }

            100% {
                color: black;
            }
        }


        /*my change*/
        form {
  border: 3px solid #f1f1f1;
}

/* Full-width inputs */
input[type=text], input[type=password] {
  width: 100%;
  padding: 12px 20px;
  margin: 8px 0;
  display: inline-block;
  border: 1px solid #ccc;
  box-sizing: border-box;
}

/* Set a style for all buttons */
#button {
  background-color: #4CAF50;
  color: white;
  padding: 14px 20px;
  margin: 8px 0;
  border: none;
  cursor: pointer;
  width: 100%;
}

/* Add a hover effect for buttons */
#button:hover {
  opacity: 0.8;
}


#button1 {
  background-color: #4CAF50;
  color: white;
  padding: 14px 20px;
  margin: 8px 0;
  border: none;
  cursor: pointer;
  width: 100%;
}

/* Add a hover effect for buttons */
#button1:hover {
  opacity: 0.8;
}

#button2 {
  background-color: #4CAF50;
  color: white;
  padding: 14px 20px;
  margin: 8px 0;
  border: none;
  cursor: pointer;
  width: 100%;
}

/* Add a hover effect for buttons */
#button2:hover {
  opacity: 0.8;
}


#button3 {
  background-color: #4CAF50;
  color: white;
  padding: 14px 20px;
  margin: 8px 0;
  border: none;
  cursor: pointer;
  width: 100%;
}

/* Add a hover effect for buttons */
#button3:hover {
  opacity: 0.8;
}


/* Extra style for the cancel button (red) */
.cancelbtn {
  width: auto;
  padding: 10px 18px;
  background-color: #f44336;
}
    </style>
   
    <%-- Prevent cut , copy paste start code--%>
    <script
        src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js">       
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('body').bind('cut copy paste', function (e) {
                e.preventDefault();
            });

           
            //$(window).on('load', function () {
            //    $('#myModal').modal('show');
            //});
         
        });

      
    </script>
    <%-- Prevent cut , copy paste end code --%>
    <script type="text/javascript">
        var email;
        var Id;
        var DealId;
        var token;

     

        $(document).ready(function () {
            debugger;
            token = '<%= Session["TOKEN"] %>';
            email = '<%= Session["dheyaEmail"] %>';
            var status = ['NEW', '10', 'IN_PROCESS', '16', '11', '13', '12', '7', '14', 'PROCESSED', 'CONVERTED', 'JUNK', '4', '9', '15'];
            var i, Status_Id;
            for (i = 0; i < status.length; i++) {
                Status_Id = status[i];
                bitrixLeads(Status_Id);
            }
            bitrixDeals();
        });
        function bitrixLeads(Status_Id) {
            debugger;
            $.ajax({
                url: 'https://dheya.bitrix24.in/rest/crm.lead.list?auth=' + token + '&filter[STATUS_ID]=' + Status_Id + '&filter[UF_CRM_1548328428067]=' + email,
                type: 'GET',
                dataType: 'json',
                success: function (d) {
                    if (d.result.length == 0) {
                        $('[id*=lb_Referred]').html("0");
                        $('[id*=lb_Progress]').html("0");
                        $('[id*=lb_Converted]').html("0");
                        $('[id*=lb_Junk]').html("0");
                        //var obj = {};
                        //obj.Email = email;
                        //$.ajax({
                        //    url: "home1.aspx/DeleteAllLeadByEmail",
                        //    contentType: "application/json; charset=utf-8",
                        //    type: 'POST',
                        //    dataType: 'json',
                        //    data: JSON.stringify(obj),
                        //    success: function (data) {
                        //        debugger;

                        //        //    alert('done');
                        //    },
                        //    error: function () {
                        //        alert("1.Error please try again");
                        //    }
                        //});
                    }
                    else
                    {
                        console.dir(d.result);
                        for (var i = 0; i < d.result.length; i++) {
                            Id = d.result[i].ID;
                            //$('[id*=hfid]').val(d.result[i].ID);
                            getdatabyid();
                        }
                    }
                },

                error: function () {
                    // alert("2.Wait...processing...Refresh again");
                }
            });
        }

        function myFunction()
        {
            window.location.href = 'CDF/bank-details.aspx';
        }

        function bitrixDeals() {
            $.ajax({
                url: 'https://dheya.bitrix24.in/rest/crm.deal.list?auth=' + token + '&filter[UF_CRM_5C4ACFAA417C4]=' + email,
                type: 'GET',
                dataType: 'json',
                success: function (d) {
                    if (d.result.length == 0) {
                        //alert("no deals");
                    }
                    for (var i = 0; i < d.result.length; i++) {
                        DealId = d.result[i].ID;
                        GetDealById();
                    }
                },

                error: function () {
                    //  alert("2.Wait...Processing...");
                }
            });
        }

        function getdatabyid() {
            //var id = $('[id*=hfid]').val();
            debugger;
            var id = Id;
            $.ajax({
                url: 'https://dheya.bitrix24.in/rest/crm.lead.get?auth=' + token + '&id=' + id,
                type: 'GET',
                dataType: 'json',
                success: function (data) {
                    var ph = data.result.HAS_PHONE;
                    var em = data.result.HAS_EMAIL;
                    var row = $("[id*=gvLeads] tr:last-child").clone(true);

                    RowCloneView(row, 9);

                    SetValue(row, 0, "BitrixID", data.result.ID, data.result.ID);
                    SetValue(row, 1, "NAME", data.result.NAME, data.result.NAME);
                    SetValue(row, 2, "LAST_NAME", data.result.LAST_NAME, data.result.LAST_NAME);
                    if (ph == 'Y') {
                        SetValue(row, 3, "PHONE", data.result.PHONE[0].VALUE, data.result.PHONE[0].VALUE);
                    }
                    else {
                        SetValue(row, 3, "PHONE", "", "");
                    }
                    if (em == 'Y') {
                        SetValue(row, 4, "EMAIL", data.result.EMAIL[0].VALUE, data.result.EMAIL[0].VALUE);
                    }
                    else {
                        SetValue(row, 4, "EMAIL", "", "");
                    }
                    SetValue(row, 5, "STATUS_ID", data.result.STATUS_ID, data.result.STATUS_ID);
                    //  SetValue(row, 6, "ASSIGNED_BY_ID", d.result.ASSIGNED_BY_ID, d.result.ASSIGNED_BY_ID);
                    SetValue(row, 6, "ReferedBy", data.result.UF_CRM_1548328428067, data.result.UF_CRM_1548328428067);
                    SetValue(row, 7, "DATE_CREATE", data.result.DATE_CREATE, data.result.DATE_CREATE);
                    SetValue(row, 8, "ASSIGNED_BY_ID", data.result.ASSIGNED_BY_ID, data.result.ASSIGNED_BY_ID);
                    $("[id*=gvLeads]").append(row);

                    for (var i = 0; i < 1; i++) {
                        $('[id*=txtID]').val(data.result.ID);
                        $('[id*=txtFName]').val(data.result.NAME);
                        $('[id*=txtLName]').val(data.result.LAST_NAME);
                        $('[id*=txtReferdByEmail]').val(data.result.UF_CRM_1548328428067);

                        if (ph == 'Y') {
                            $('[id*=txtContact]').val(data.result.PHONE[0].VALUE);
                        }
                        else { $('[id*=txtContact]').val(""); }
                        if (em == 'Y') {
                            $('[id*=txtEmail]').val(data.result.EMAIL[0].VALUE);
                        }
                        else { $('[id*=txtEmail]').val(""); }
                        var status_id = data.result.STATUS_ID;
                        var Lead_Type = data.result.UF_CRM_1545482190063;

                        $('[id*=txtStatus]').val(data.result.STATUS_ID);

                        $('[id*=txtDate]').val(data.result.DATE_CREATE);
                        $('[id*=txtLeadType]').val(data.result.UF_CRM_1545482190063);
                        $('[id*=txtLeadSource]').val(data.result.SOURCE_ID);
                        $('[id*=txtASSIGNED_BY_ID]').val(data.result.ASSIGNED_BY_ID);
                        save();
                    }
                },
                error: function () {
                   // alert("3.Error please try again");
                }
            });
        }

        function GetDealById() {
            var dealid = DealId;
            $.ajax({
                url: 'https://dheya.bitrix24.in/rest/crm.deal.get?auth=' + token + '&id=' + dealid,
                type: 'GET',
                dataType: 'json',
                success: function (data) {
                    debugger;
                    var row = $("[id*=gvDeals] tr:last-child").clone(true);

                    RowCloneView(row, 5);

                    SetValue(row, 0, "DealID", data.result.ID, data.result.ID);
                    SetValue(row, 1, "LeadID", data.result.LEAD_ID, data.result.LEAD_ID);
                    SetValue(row, 2, "DealStatus", data.result.STAGE_ID, data.result.STAGE_ID);
                    SetValue(row, 3, "DealReferedBy", data.result.UF_CRM_5C4ACFAA417C4, data.result.UF_CRM_5C4ACFAA417C4);
                    SetValue(row, 4, "RegDate", data.result.DATE_CREATE, data.result.DATE_CREATE);

                    $("[id*=gvDeals]").append(row);

                    for (var i = 0; i < 1; i++) {
                        $('[id*=txtDealId]').val(data.result.ID);
                        $('[id*=txtLeadId]').val(data.result.LEAD_ID);
                        $('[id*=txtDealStatus]').val(data.result.STAGE_ID);
                        $('[id*=txtDealReferedBy]').val(data.result.UF_CRM_5C4ACFAA417C4);
                        $('[id*=txtRegDate]').val(data.result.DATE_CREATE);

                        SaveDeals();
                    }
                },
                error: function () {
                    //alert("3.Error please try again");
                }
            });
        }
        // set value in grid cell
        function SetValue(row, index, name, text, value) {
            //Reference the Cell and set the value.           
            row.find("td").eq(index).html(text);
            //Create and add a Hidden Field to send value to server. 
            var input = $("<input type = 'hidden' />");
            input.prop("name", name);
            input.val(value);
            row.find("td").eq(index).append(input);
        }
        //Get Row Clone
        function RowCloneView(row, count) {
            row.removeAttr("style");
            row.html("");
            row.append("<td></td>");
            for (var i = 0; i <= count; i++) {
                row.append("<td></td>");
            }
            return row;
        }

        function save() {
            var obj = {};
            obj.BitrixId = $('[id*=txtID]').val();
            obj.FName = $('[id*=txtFName]').val();
            obj.LName = $('[id*=txtLName]').val();
            obj.ContactNo = $('[id*=txtContact]').val();
            obj.Email = $('[id*=txtEmail]').val();
            obj.RegDate = $('[id*=txtDate]').val();
            obj.Status = $('[id*=txtStatus]').val();
            obj.ReferedByEmail = $('[id*=txtReferdByEmail]').val();
            obj.LeadType = $('[id*=txtLeadType]').val();
            obj.LeadSource = $('[id*=txtLeadSource]').val();
            obj.AssignedBy = $('[id*=txtASSIGNED_BY_ID]').val();
            $.ajax({
                url: "home1.aspx/BitrixDataInsert",
                contentType: "application/json; charset=utf-8",
                type: 'POST',
                dataType: 'json',
                data: JSON.stringify(obj),
                success: function (data) {
                    ViewCount();
                },
                error: function () {
                    alert("4.Error please try again");
                }
            });
        }

        function SaveDeals() {
            var obj = {};
            obj.DealId = $('[id*=txtDealId]').val();
            obj.LeadId = $('[id*=txtLeadId]').val();
            obj.RegDate = $('[id*=txtRegDate]').val();
            obj.DealStatus = $('[id*=txtDealStatus]').val();
            obj.ReferedByEmail = $('[id*=txtDealReferedBy]').val();
            $.ajax({
                url: "home1.aspx/BitrixDealInsert",
                contentType: "application/json; charset=utf-8",
                type: 'POST',
                dataType: 'json',
                data: JSON.stringify(obj),
                success: function (data) {
                },
                error: function () {
                    alert("4.Error please try again");
                }
            });
        }

        function ViewCount() {
            var obj = {};
            obj.ReferedByEmail = $('[id*=txtReferdByEmail]').val();
            $.ajax({
                url: "home1.aspx/ShowLeadsCount",
                contentType: "application/json; charset=utf-8",
                type: 'POST',
                dataType: 'json',
                data: JSON.stringify(obj),
                success: function (data) {
                    for (var i = 0; i < data.d.length; i++) {
                        var r = (data.d[i].Refered);
                        var p = (data.d[i].progress);
                        var c = (data.d[i].converted);
                        var j = (data.d[i].junk);
                        $('[id*=lb_Referred]').html(r);
                        $('[id*=lb_Progress]').html(p);
                        $('[id*=lb_Converted]').html(c);
                        $('[id*=lb_Junk]').html(j);
                    }
                },
                error: function () {
                    alert("5.Error please try again");
                }
            });
        }
    </script>

    <script type="text/javascript">

      

        function ShowPOPup() {
            $("[id*=pop_Magazine]").show();
        }
        function ClosePopup() {
            $("[id*=pop_Magazine]").hide();
        }
        function ShowPolicyPopup() {
            $("[id*=pop_Policy]").show();
        }
        function ClosePolicyPopup() {
            $("[id*=pop_Policy]").hide();
        }

        function ShowUpdateCDFPopup() {
            $("[id*=modelUpdate]").show();
        }
        function CloseUpdateCDFPopup() {
            $("[id*=modelUpdate]").hide();
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <%-- Small Blocks --%>
        <%--<div class="row top_tiles">--%>
        <div class="row card_text">

            <div class="col-xs-3">
                <%--style="display:none;"--%>
                <%--<div class="x_panel">--%>
                <div class="dashboard_graph x_panel" style="display: none;">
                    <div class="x_title" style="height: 35px; display: none;">
                        <h2>
                            <asp:Label ID="Label2" runat="server" Text="WORK IN PROGRESS" CssClass="awesome"></asp:Label>
                            <asp:Label ID="Label3" runat="server" Visible="false" Text="(Target Completion Date: 31 March 2019)" CssClass="target"></asp:Label>
                        </h2>

                        <div class="clearfix"></div>
                    </div>

                    <div class="animated flipInY col-lg-3 col-md-3 col-sm-6 col-xs-12" style="padding: 8px; display: none;">
                        <div class="tile-stats hover-shadow" style="background-image: url(images/card_background6.jpg); height: 110px; width: 240px;">
                            <div class="count">
                                <%--<span><i class="fa fa-inr"></i></span>--%>
                                <asp:Label ID="earnings" runat="server" Text="INR 0.00" Font-Size="24px" ForeColor="darkgray"></asp:Label>
                                <hr style="border-color: steelblue; margin-top: 7px; margin-bottom: 0px;" />
                                <h4 style="margin-top: 7px; color: darkgrey">My Earnings</h4>
                            </div>
                        </div>
                    </div>
                    <div class="animated flipInY col-lg-3 col-md-3 col-sm-6 col-xs-12" style="padding: 8px; display: none;">
                        <div class="tile-stats hover-shadow" style="background-image: url(images/card_background6.jpg); height: 110px; width: 240px; margin-left: 127px;">
                            <div class="count">
                                <asp:Image ID="img_star1" runat="server" Height="24px" Width="24px" />
                                <asp:Image ID="img_star2" runat="server" Height="24px" Width="24px" />
                                <asp:Image ID="img_star3" runat="server" Height="24px" Width="24px" />
                                <asp:Image ID="img_star4" runat="server" Height="24px" Width="24px" />
                                <asp:Image ID="img_star5" runat="server" Height="24px" Width="24px" />
                                <hr style="border-color: steelblue; margin-top: 7px; margin-bottom: 0px;" />
                                <h4 style="margin-top: 7px; color: darkgrey">My Rating</h4>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
              <div class="animated flipInY col-xs-4" style="margin-top: 30px; margin-left:-350px;">
                    <div class="tile-stats hover-shadow zoom" style="height: 120px; width: 320px; right:10px; background-image: url(images/card_background6.jpg); background-size: 100%;">
                        <div class="icon"><i class="" aria-hidden="true"></i></div>
                        <div class="count">
                            <asp:LinkButton ID="LinkButton2" runat="server" Text="Dheya Launches New Features in CDF Portal" OnClientClick="ShowUpdateCDFPopup();return false;" Font-Size="12px" ForeColor="DarkGray"></asp:LinkButton>
                        </div>
                        <hr style="border-color:steelblue; margin-top: 12px; margin-bottom: 0px;" />
                        <%-- <asp:LinkButton ID="lnk_policy" runat="server" Text="Download" OnClick="hl_policy_click" ToolTip="click here to download" Font-Size="20px" Style="margin-top: 10px; margin-left: 10px; color: darkgrey"></asp:LinkButton>--%>
                        <asp:LinkButton ID="LinkButton3" runat="server" Text="New Features" OnClientClick="ShowUpdateCDFPopup();return false;" ToolTip="click Here to Show Update Portal" Font-Size="20px" Style="margin-top: 10px; margin-left: 10px; color: darkgrey"></asp:LinkButton>
                    </div>
            </div>
            


          

            <div class="animated flipInY col-xs-4" style="margin-top: 30px; margin-left:-254px;">
                    <div class="tile-stats hover-shadow zoom" style="height: 120px; width: 275px; left:118px; background-image: url(images/card_background6.jpg); background-size: 100%;">
                        <div class="icon"><i class="" aria-hidden="true"></i></div>
                        <div class="count">
                            <asp:LinkButton ID="LinkButton1" runat="server" Text="Dheya Announced New Policy for CDF" OnClientClick="ShowPolicyPopup();return false;" Font-Size="12px" ForeColor="DarkGray"></asp:LinkButton>
                        </div>
                        <hr style="border-color:steelblue; margin-top: 12px; margin-bottom: 0px;" />
                        <%-- <asp:LinkButton ID="lnk_policy" runat="server" Text="Download" OnClick="hl_policy_click" ToolTip="click here to download" Font-Size="20px" Style="margin-top: 10px; margin-left: 10px; color: darkgrey"></asp:LinkButton>--%>
                        <asp:LinkButton ID="lnk_policy" runat="server" Text="Show Policy" OnClientClick="ShowPolicyPopup();return false;" ToolTip="click Here to Show Policy" Font-Size="20px" Style="margin-top: 10px; margin-left: 10px; color: darkgrey"></asp:LinkButton>
                    </div>
            </div>


            <div class="col-xs-4" style="margin-top: 30px; margin-left: -150px;">
                <div class="zoom" style="height: 120px;width:0px; border-radius: 4px;">
                    <asp:ImageButton ID="imgbtnMagazine" runat="server" ImageUrl="~/images/Magazine-thumbnail2.png" Height="119px" Width="261px"  OnClientClick="ShowPOPup();return false;" CssClass="curve-corner" />
                </div>
            </div>

            <div class="animated flipInY col-lg-3 col-md-3 col-sm-6 col-xs-12" style="margin-top: 20px; left:185px; padding-left: 30px; margin-bottom: 6px;">
                <div class="count">
                    <asp:ImageButton BorderStyle="Inset" ID="lnk_ref" runat="server" ImageUrl="~/images/Referel-notification.gif" CssClass="img  hover-shadow" OnClick="click_ref" />
                </div>
            </div>

        </div>


    <div class="modal fade" id="myModal" role="dialog" data-backdrop="static" >
    <div class="modal-dialog">         
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h3 class="modal-title" style="text-align:center; color:red">Alert !</h3>
        </div>
        <div class="modal-body" style="text-align:center;">
          <h4 style="text-align:center;">We do not have your Bank details.Please fill up your Bank Details.</h4>           
            <button id="btnbank_details" style=" min-width: 80px; max-width: 80px;" onclick="myFunction()" type="button" class="btn btn-primary" >Ok</button>    
            
          </div>  
      </div>
      </div>
  </div>

            <%-- POP UP for  New Update CDF Portal--%>
        <div id="modelUpdate" class="popover" style="margin-top: 60px; margin-left: 270px; max-width: 925px;">
            <div class="popover-title">
                <asp:Label runat="server" Text="New Policy"></asp:Label>
                <button id="Close" type="button" class="close" onclick="CloseUpdateCDFPopup()" title="Close">&times;</button>
            </div>
            <div class="popover-content">
                <%-- <asp:Image ImageUrl="https://dheya.com/cdf-dashboard/doc/cdf-doc/Magazine-web-page.jpg" runat="server" Height="700" Width="500" />--%>
                <iframe width="900" height="600" src="https://dheya.com/cdf-dashboard/doc/Policy_Document/NewFeature.pdf"></iframe>
                  <%-- <iframe width="900" height="600" src="doc/Policy_Document/NewFeature.pdf"></iframe>--%>
                <%-- src="doc/cdf-doc/Magazine_-_Quarter_1.pdf" --%>
            </div>
            
            
        </div>
        <%--  end POP UP for  New Policy --%>





        <%-- POP UP for  New Policy--%>
        <div id="pop_Policy" class="popover" style="margin-top: 60px; margin-left: 270px; max-width: 925px;">
            <div class="popover-title">
                <asp:Label runat="server" Text="New Policy"></asp:Label>
                <button id="btn_Close" type="button" class="close" onclick="ClosePolicyPopup()" title="Close">&times;</button>
            </div>
            <div class="popover-content">
                <%-- <asp:Image ImageUrl="https://dheya.com/cdf-dashboard/doc/cdf-doc/Magazine-web-page.jpg" runat="server" Height="700" Width="500" />--%>
                <iframe width="900" height="600" src="https://dheya.com/cdf-dashboard/doc/Policy_Document/new_policy.pdf"></iframe>
                <%-- src="doc/cdf-doc/Magazine_-_Quarter_1.pdf" --%>
            </div>
        </div>
        <%--  end POP UP for  New Policy --%>

        <%-- POP UP for  Magazine-thumbnail--%>
        <div id="pop_Magazine" class="popover" style="margin-top: 60px; margin-left: 270px; max-width: 925px;">
            <div class="popover-title">
                <asp:Label runat="server" Text="Dheya Magazine:Quarter 1"></asp:Label>
                <button id="btnClose" type="button" class="close" onclick="ClosePopup()" title="Close">&times;</button>
            </div>
            <div class="popover-content">
                <%-- <asp:Image ImageUrl="https://dheya.com/cdf-dashboard/doc/cdf-doc/Magazine-web-page.jpg" runat="server" Height="700" Width="500" />--%>
                <iframe width="900" height="600" src="https://dheya.com/cdf-dashboard/doc/cdf-doc/Magazine - Quarter 1.pdf"></iframe>
                <%-- src="doc/cdf-doc/Magazine_-_Quarter_1.pdf" --%>
            </div>
        </div>
        <%--  end POP UP for  Magazine-thumbnail--%>


        <%--<div class="row top_tiles">
            <div class="animated flipInY col-lg-6 col-md-6 col-sm-12 col-xs-12">
                <div class="tile-stats hover-shadow" style="background-color: beige;">
                    <div class="icon"><i class="fa fa-inr"></i></div>
                    <div class="count">
                        <asp:Label ID="lbl_wallet" runat="server" Text="INR 0.00"></asp:Label>
                    </div>
                    <div class="tile-stats" style="background-color: currentColor; margin-block-start: 4px; margin-left: 260px;"></div>
                    <h3 style="margin-top: 15px">My Earnings</h3>
                </div>
            </div>
            <div class="animated flipInY col-lg-6 col-md-6 col-sm-12 col-xs-12">
                <div class="tile-stats hover-shadow" style="background-color: beige;">
                    <div class="icon" id="Div1" runat="server"></div>
                    <div class="count">
                        <asp:Image ID="img_star1" runat="server" Height="50px" Width="52px" />
                        <asp:Image ID="img_star2" runat="server" Height="50px" Width="52px" />
                        <asp:Image ID="img_star3" runat="server" Height="50px" Width="52px" />
                        <asp:Image ID="img_star4" runat="server" Height="50px" Width="52px" />
                        <asp:Image ID="img_star5" runat="server" Height="50px" Width="52px" />
                    </div>
                    <div class="tile-stats" style="background-color: currentColor; margin-block-start: 4px; margin-right: 206px;"></div>
                    <h3 style="margin-top: 15px;">My Rating</h3>
                </div>
            </div>
        </div>--%>


         <div class="row card_text" id="card" runat="server">
            <div class="col-md-12 col-sm-10 col-xs-12">
                <%--<div class="x_panel">--%>
                <div class="dashboard_graph x_panel">
                    <div class=" x_title" style="height: 35px;">
                        <h2> Upcoming Sessions                           
                        </h2>
                        &nbsp;&nbsp;&nbsp;
                     
                        <ul class="nav navbar-right panel_toolbox">
                            <%--  <li><a><i class="btn btn-primary btn-block hover-shadow" style="font-family: Poppins; font-style: normal">Refer New Lead</i></a>
                        </li>--%>
                            <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                            </li>
                            <li><a href="javascript:location.reload()"><i class="fa fa-refresh" aria-hidden="true"></i></a>
                            </li>
                            <li><a class="close-link"><i class="fa fa-close"></i></a>
                            </li>
                        </ul>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content" style="height: 130px; margin-top: -13px;">
                        <div id="chart_dv123" style="width: 95%; height: 100%;">
                            <div class="row top_tiles">
                                <a id="stddetail" name='lnkViews1' href="#" data-toggle="modal" data-target="#studentpopup">
                                    <div class="animated flipInY col-lg-3 col-md-3 col-sm-6 col-xs-12" style="padding: 8px;">
                                        <div class="tile-stats hover-shadow" style="height: 110px; background-image: url(images/card_background5.jpg); background-size: 100%;">
                                  
                                            <div class="auto-style1">
                                                <%--<a href="javascript:OpenModal()" style="color: black;" data-toggle="tooltip" title="View List"></a>--%>
                                                   <div class="count">
                                                <asp:Label ID="lblcount" runat="server" Text=""></asp:Label>
                                                    </div>
                                            </div>
                                            <hr style="border-color: steelblue; margin-top: 12px; margin-bottom: 0px;" />  
                                        </div>
                                    </div>
                                </a>                       
                                                         
                                <div class="modal fade" id="studentpopup" role="dialog" data-backdrop="static">
                                    <div class="modal-dialog modal-lg" style="margin-left: 6%; width: 200px">
                                        <div class="widget-bg">
                                            <div class="row">
                                                <div class="col-lg-4">
                                                    <div class="panel panel-success" style="text-align: center;display:table-row; height: 450px">
                                                        <div class="modal-header">
                                                            <button type="button" onclick="window.location.reload()" class="close" data-dismiss="modal" style="font-size: 35px;">
                                                                &times;
                                                            </button>
                                                            <h3 class="modal-title">Student Details</h3>
                                                        </div>   
                                                        <div class="modal-body" style="width:1501px;" >  
                                                         <div style="overflow-x:scroll;">                                    
                                                        <asp:GridView ID="Gridmysession" runat="server" AutoGenerateColumns="False" style="height:200px; overflow:auto"  OnRowDataBound="Gridmysession_RowDataBound"
                                                             CssClass="GridmysessionCss" HeaderStyle-CssClass="header" RowStyle-CssClass="rows" PagerStyle-CssClass="pager" AllowPaging="True" PageSize="5" CellPadding="4"   ForeColor="#333333" GridLines="None"
                                                             OnRowCommand="Gridmysession_RowCommand" >
                                                            <AlternatingRowStyle HorizontalAlign="Center" BackColor="White" ForeColor="#284775" />
                                                            <EditRowStyle BackColor="#999999"/>
                                                            <FooterStyle BackColor="#35578a" Font-Bold="True" ForeColor="White" />
                                                            <HeaderStyle BackColor="#35578a" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" ForeColor="White" />
                                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                            <RowStyle HorizontalAlign="Center" BackColor="#F7F6F3" ForeColor="#333333" />
                                                            <Columns>
                                                              
                                                                 <asp:BoundField DataField="LeadType" HeaderText="Lead Type" SortExpression="LeadType" />

                                                                <asp:BoundField DataField="YourRole" HeaderText="Your Role" SortExpression="YourRole" />

                                                                <asp:BoundField DataField="Session_Id"  HeaderText="Session Id"
                                                                    ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" SortExpression="Session_Id" />

                                                                 <asp:BoundField DataField="ShadowCDF"  HeaderText="Shadow CDF"
                                                                 SortExpression="ShadowCDF" />

                                                                  <asp:BoundField DataField="CDFMob"  HeaderText="CDFMob"  SortExpression="CDFMob" />

                                                                 <asp:BoundField DataField="CDFMail"  HeaderText="CDFMail" SortExpression="CDFMail" />

                                                                 <asp:BoundField DataField="ShadowMob"  HeaderText="ShadowMob" SortExpression="ShadowMob" />

                                                                 <asp:BoundField DataField="ShaowMail"  HeaderText="ShaowMail" SortExpression="ShaowMail" />



                                                                 <asp:BoundField DataField="ConductingCDF" HeaderText="Conducting CDF " SortExpression="ConductingCDF" />                                                               

                                                                <asp:BoundField DataField="CdfID" HeaderText="CdfID" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"
                                                                    SortExpression="CdfID" />

                                                                <asp:BoundField DataField="StudentId" HeaderText="Student Id"
                                                                    SortExpression="StudentId" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />

                                                                 <asp:BoundField DataField="CandName" HeaderText="Candidate Name"
                                                                    SortExpression="CandName" />

                                                                <asp:BoundField DataField="CandGender" HeaderText="Gender"
                                                                    SortExpression="CandGender" />

                                                                <asp:BoundField DataField="CandContact" HeaderText="Contact"
                                                                    SortExpression="CandContact" />

                                                                <asp:BoundField DataField="CandEmail" HeaderText="Email"
                                                                    SortExpression="CandEmail"  ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />

                                                                <asp:BoundField DataField="SesDate" HeaderText="Session Date"
                                                                    SortExpression="SesDate" />

                                                                <asp:BoundField DataField="SesTime" HeaderText="Session Time"
                                                                    SortExpression="SesTime" />

                                                                <asp:BoundField DataField="SesStatus" HeaderText="Session Status"
                                                                    SortExpression="SesStatus" />

                                                                <asp:BoundField DataField="SesVillageId" HeaderText="Session Village Id"
                                                                    SortExpression="SesVillageId" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />

                                                                <asp:BoundField DataField="SesPin" HeaderText="Session Pin"
                                                                    SortExpression="SesPin" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />

                                                                <asp:BoundField DataField="SesAddress"  HeaderText="Session Address"
                                                                    SortExpression="SesAddress" />

                                                                <asp:BoundField DataField="SesCityId" HeaderText="Session City Id"
                                                                    SortExpression="SesCityId" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />

                                                                <asp:BoundField DataField="SesCity" HeaderText="Session City"
                                                                    SortExpression="SesCity" />

                                                                <asp:BoundField DataField="CandId" HeaderText="Candidate Id"
                                                                    SortExpression="CandId" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />                                                                 
                                                                  
                                                                <asp:BoundField DataField="Action" HeaderText="Action"  ItemStyle-ForeColor="Red" SortExpression="Action" />                                                 
                                                                
                                                              <%--  <asp:TemplateField>  
                                                                    <ItemTemplate>
                                                                       <asp:Button Text="Accept" ID="accptBtn" runat="server" ItemStyle-ForeColor="White" ControlStyle-CssClass="btn btn-success" CommandName="Accept"  Enabled="false" />
                                                                    </ItemTemplate>
                                                                    </asp:TemplateField>          --%> 
                                                                                                                
                                                                 <asp:ButtonField  Text="Accept"                                                 
                                                                   HeaderText="Accept" ItemStyle-ForeColor="White"   ControlStyle-CssClass="btn btn-success" CommandName="Accept" /> 
                                                                
                                                                       
                                                                <asp:ButtonField Text="Reject"   HeaderText="Reject" ItemStyle-ForeColor="White"  ControlStyle-CssClass=" btn btn-danger"  CommandName="Reject" />         
                                                                
                                                                  <asp:BoundField DataField="CDF_Acceptance"  HeaderText="CDF_Acceptance"
                                                                    ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" SortExpression="CDF_Acceptance" />

                                                              

                                                                 


                                                                
                                                                
                                                                                                                    
                                                              <%-- <asp:TemplateField>
                                                                   <ItemTemplate>
                                                                        <asp:Button ID="SesAccept" CssClass="btn btn-success"
                                                                        runat="server" Text="Accept" Visible='<%#  (Eval("CdfAcceptance").ToString() == "Accept")%>'
                                                                        CommandArgument='<%# Eval("CdfAcceptance") + ","+Eval("StudId") %>' CommandName="Accept" />
                                                                   </ItemTemplate>
                                                               </asp:TemplateField>--%>                                                                  
                                                            </Columns>
                                                            <PagerStyle BackColor="#2461BF" HorizontalAlign="Center"
                                                                Font-Bold="True" CssClass="pagination-ys" Wrap="True"/>
                                                        </asp:GridView> 
                                                             </div>
                                                        <div>        
                                                    </div>
                                                </div>                                                
                                            </div>
                                                    </div>
                                                </div>
                                        </div>
                                    </div>
                                    </div>
                                   
                                </div>      
                            </div>
                        </div>
                    </div>
                </div>
             </div>

                       
          








        <div class="row card_text">
            <div class="col-md-12 col-sm-10 col-xs-12">
                <%--<div class="x_panel">--%>
                <div class="dashboard_graph x_panel">
                    <div class=" x_title" style="height: 35px;">
                        <h2>My Leads
                            
                        </h2>
                        &nbsp;&nbsp;&nbsp;
                        <u><a href="javascript:location.reload()" style="font-size: small; color: blue;">(if you don't see the leads...click here to refresh.)</a></u>
                        <ul class="nav navbar-right panel_toolbox">
                            <%--  <li><a><i class="btn btn-primary btn-block hover-shadow" style="font-family: Poppins; font-style: normal">Refer New Lead</i></a>
                        </li>--%>
                            <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                            </li>
                            <li><a href="javascript:location.reload()"><i class="fa fa-refresh" aria-hidden="true"></i></a>
                            </li>
                            <li><a class="close-link"><i class="fa fa-close"></i></a>
                            </li>
                        </ul>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content" style="height: 130px; margin-top: -13px;">
                        <div id="chart_div123" style="width: 95%; height: 100%;">
                            <div class="row top_tiles">
                                <div class="animated flipInY col-lg-3 col-md-3 col-sm-6 col-xs-12" style="padding: 8px;">
                                    <div class="tile-stats hover-shadow" style="height: 110px; background-image: url(images/card_background5.jpg); background-size: 100%;">
                                        <div class="icon"><i class="fa fa-user-plus" aria-hidden="true"></i></div>
                                        <div class="count">
                                            <%--<a href="javascript:OpenModal()" style="color: black;" data-toggle="tooltip" title="View List"></a>--%>
                                            <asp:LinkButton ID="lb_Referred" runat="server" OnClick="btn_Referred_Click"></asp:LinkButton>
                                        </div>
                                        <hr style="border-color: steelblue; margin-top: 12px; margin-bottom: 0px;" />
                                        <%--<h4 style="margin-top: 10px; margin-left: 10px; color: darkgrey">Leads Referred</h4>--%>
                                        <asp:LinkButton ID="btn_Referred" runat="server" Text="Leads Referred" OnClick="btn_Referred_Click" ToolTip="click here to get all details about Leads Referred" Style="margin-top: 10px; margin-left: 10px;"></asp:LinkButton>
                                    </div>
                                </div>
                                <%--<div style:"margin-left=1px">
                        </div>--%>

                                <div class="animated flipInY col-lg-3 col-md-3 col-sm-6 col-xs-12" style="padding: 8px;">
                                    <div class="tile-stats hover-shadow" style="background-image: url(images/card_background_progress1.jpg); height: 110px;">
                                        <div class="icon"><i class="fa fa-spinner" aria-hidden="true"></i></div>
                                        <div class="count">
                                            <asp:LinkButton ID="lb_Progress" runat="server" OnClick="btn_Progress_Click"></asp:LinkButton>
                                        </div>
                                        <hr style="border-color: steelblue; margin-top: 12px; margin-bottom: 0px;" />
                                        <%--<h4 style="margin-top: 10px; margin-left: 10px; color: grey">In Progress</h4>--%>
                                        <asp:LinkButton ID="btn_Progress" runat="server" Text="In Progress" OnClick="btn_Progress_Click" ToolTip="click here to get all details about Leads In Progress" Style="margin-top: 10px; margin-left: 10px;"></asp:LinkButton>
                                    </div>
                                </div>

                                <div class="animated flipInY col-lg-3 col-md-3 col-sm-6 col-xs-12" style="padding: 8px;">
                                    <div class="tile-stats hover-shadow" style="background-image: url(images/card_background_converted3.svg); height: 110px;">
                                        <div class="icon"><i class="fa fa-check-square-o" aria-hidden="true"></i></div>
                                        <div class="count">

                                            <asp:LinkButton ID="lb_Converted" runat="server" OnClick="btn_Converted_Click"></asp:LinkButton>
                                        </div>
                                        <hr style="border-color: steelblue; margin-top: 12px; margin-bottom: 0px;" />
                                        <%--<h4 style="margin-top: 10px; margin-left: 10px; color: grey">Leads Converted</h4>--%>
                                        <asp:LinkButton ID="btn_Converted" runat="server" Text="Leads Converted" OnClick="btn_Converted_Click" ToolTip="click here to get all details about Leads Converted" Style="margin-top: 10px; margin-left: 10px;"></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="animated flipInY col-lg-3 col-md-3 col-sm-6 col-xs-12" style="padding: 8px;">
                                    <div class="tile-stats hover-shadow" style="background-color: yellow; background-image: url(images/card_background_junk1.jpg); height: 110px;">
                                        <div class="icon"><i class="fa fa-user-times" aria-hidden="true"></i></div>
                                        <div class="count">

                                            <asp:LinkButton ID="lb_Junk" runat="server" OnClick="btn_Junk_Click"></asp:LinkButton>
                                        </div>
                                        <hr style="border-color: steelblue; margin-top: 12px; margin-bottom: 0px;" />
                                        <%--<h4 style="margin-top: 10px; margin-left: 10px; color: darkgrey">Junk Lead</h4>--%>
                                        <asp:LinkButton ID="btn_Junk" runat="server" Text="Junk Lead" OnClick="btn_Junk_Click" ToolTip="click here to get all details about Junk Lead" Style="margin-top: 10px; margin-left: 10px;"></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <%--</div>--%>
            </div>
        </div>

        <div id="Lead_Details" runat="server" class="row" visible="false" style="height: 352px;">
            <div class="col-md-12 col-sm-10 col-xs-12">
                <%--<div id="Lead_Details" runat="server" visible="false" class="x_panel" style="height: 352px;">--%>
                <div class="dashboard_graph x_panel" style="height: 331px;">
                    <div class=" x_title">
                        <h2>Leads Status</h2>
                        <ul class="nav navbar-right panel_toolbox">
                            <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                            </li>
                            <li><a class="close-link"><i class="fa fa-close"></i></a>
                            </li>
                        </ul>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content">
                        <div class="row" style="padding: 5px">
                            <div>
                                <asp:Label ID="lbl_msg" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                        <div id="chart_div12345" style="width: 95%; height: 100%;">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        ForeColor="#333333" GridLines="None" AllowPaging="True" CssClass="table table-responsive"
                                        Width="100%" PageSize="5" OnPageIndexChanging="GridView2_PageIndexChanging">
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="Id" HeaderText="ID" SortExpression="Id" Visible="false">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FName" HeaderText="First Name" SortExpression="FName">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="LName" HeaderText="Last Name" SortExpression="LName">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ContactNo" HeaderText="Contact No" SortExpression="ContactNo">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="RegDate" HeaderText="Entered Date" SortExpression="RegDate"
                                                DataFormatString="{0:d}">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="LeadStatus" HeaderText="Status" SortExpression="Lead_Status">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>

                                        </Columns>
                                        <RowStyle VerticalAlign="Top" BackColor="#F7F6F3" ForeColor="#333333" />
                                        <EditRowStyle BackColor="#999999" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" VerticalAlign="Top" />
                                        <PagerStyle BackColor="#284775" HorizontalAlign="Center" CssClass="pagination-ys" />
                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:TextBox ID="Leads" runat="server" CssClass="textbox"></asp:TextBox>


        <%--<div class="row top_tiles">
        <div class="animated flipInY col-lg-3 col-md-3 col-sm-6 col-xs-12">
            <div class="tile-stats">
                <div class="icon"><i class="fa fa-user-plus purple"></i></div>
                <div class="count">
                    <asp:Label ID="session_count" runat="server" Text="10"></asp:Label>
                </div>
                <h3 style="margin-top: 15px;">Leads Referred</h3>
            </div>
        </div>
        <div class="animated flipInY col-lg-3 col-md-3 col-sm-6 col-xs-12">
            <div class="tile-stats">
                <div class="icon"><i class="fa fa-user-plus green"></i></div>
                <div class="count">
                    <asp:Label ID="revenue_count" runat="server" Text="2"></asp:Label>
                </div>
                <h3 style="margin-top: 15px;">In Progress</h3>
            </div>
        </div>
        <div class="animated flipInY col-lg-3 col-md-3 col-sm-6 col-xs-12">
            <div class="tile-stats">
                <div class="icon"><i class="fa fa-user-plus yellow"></i></div>
                <div class="count">
                    <asp:Label ID="Label1" runat="server" Text="6"></asp:Label>
                </div>
                <h3 style="margin-top: 15px;">Leads Converted</h3>
            </div>
        </div>
        <div class="animated flipInY col-lg-3 col-md-3 col-sm-6 col-xs-12">
            <div class="tile-stats">
                <div class="icon"><i class="fa fa-user-plus red"></i></div>
                <div class="count">
                    <asp:Label ID="Label2" runat="server" Text="2"></asp:Label>
                </div>
                <h3 style="margin-top: 15px;">Junk Lead</h3>
            </div>
        </div>
    </div>--%>



        <%--<div class="row">
            <div class="col-md-6 col-sm-10 col-xs-12">
                <div class="dashboard_graph x_panel">
                    <div class=" x_title">
                        <h2>Number of Referrals By Months</h2>
                        <ul class="nav navbar-right panel_toolbox">
                            <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                            </li>
                            <li><a class="close-link"><i class="fa fa-close"></i></a>
                            </li>
                        </ul>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content">
                        <div id="chart_div" style="width: 95%; height: 100%;">
                            <asp:Literal ID="lt" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-6 col-sm-10 col-xs-12">
                <div class="x_panel">
                    <div class="x_title">
                        <h2>All CDF's Referrals</h2>
                        <ul class="nav navbar-right panel_toolbox">
                            <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                            </li>

                            <li><a class="close-link"><i class="fa fa-close"></i></a>
                            </li>
                        </ul>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content">
                        <div id="chart_div2" style="width: 95%; height: 100%;">
                            <asp:Literal ID="lt2" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>
            </div>
        </div>--%>

        <%--<div class="row">
            <div class="col-md-8 col-sm-10 col-xs-12">
                <div class="x_panel fixed_height_320">
                    <div class="x_title">
                        <h2>Referral Lead Status</h2>
                        <ul class="nav navbar-right panel_toolbox">
                            <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                            </li>
                            <li class="dropdown">
                                <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-expanded="false"><i class="fa fa-wrench"></i></a>
                                <ul class="dropdown-menu" role="menu">
                                    <li><a href="leads/Previewleads.aspx">View Details</a>
                                    </li>
                                </ul>
                            </li>
                            <li><a class="close-link"><i class="fa fa-close"></i></a>
                            </li>
                        </ul>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content">

                        <div id="div_leadstatus" runat="server" class="" style="text-align: center; margin: 90px 10px 0 10px;">
                        </div>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            ForeColor="#333333" GridLines="None" AllowPaging="True" CssClass="table table-responsive"
                            Width="100%" OnPageIndexChanging="GridView1_PageIndexChanging"
                            PageSize="5">
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <Columns>
                                <asp:BoundField DataField="First Name" HeaderText="First Name" SortExpression="First Name">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Last Name" HeaderText="Last Name" SortExpression="Last Name">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Date" HeaderText="Entered Date" SortExpression="Date"
                                    DataFormatString="{0:d}">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Lead Status" HeaderText="Current Status" SortExpression="Lead Status">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                            </Columns>
                            <RowStyle VerticalAlign="Top" BackColor="#F7F6F3" ForeColor="#333333" />
                            <EditRowStyle BackColor="#999999" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" VerticalAlign="Top" />
                            <PagerStyle BackColor="#284775" HorizontalAlign="Center" CssClass="pagination-ys" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>

                    </div>
                </div>
            </div>--%>

        <div class="col-md-8 col-sm-12 col-xs-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Dheya's Networks</h2>
                    <ul class="nav navbar-right panel_toolbox">
                        <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                        </li>
                        <li><a class="close-link"><i class="fa fa-close"></i></a>
                        </li>
                    </ul>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <br />                         
                    <div id="chart_div1" style="width: 100%; height: 100%;"></div>                                   
                </div>
            </div>
        </div>

        <div class="col-md-4 col-sm-6 col-xs-12">
            <div class="x_panel fixed_height_320">
                <div class="x_title">
                    <h2>Profile Details</h2>
                    <ul class="nav navbar-right panel_toolbox">

                        <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                        </li>
                        <li class="dropdown">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-expanded="false"><i class="fa fa-wrench"></i></a>
                            <ul class="dropdown-menu" role="menu">
                                <li><a href="CDF/updateinfo.aspx">Edit Information</a>
                                </li>
                                <li><a href="CDF/education.aspx">Edit Education</a>
                                </li>
                                <li><a href="CDF/experience.aspx">Edit Experience</a>
                                </li>
                            </ul>
                        </li>
                        <li><a class="close-link"><i class="fa fa-close"></i></a>
                        </li>
                    </ul>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <div class="dashboard-widget-content">
                        <div id="chart11" style="width: 100%; height: 100%;"></div>
                        <h4 style="text-align: center;">
                            <asp:Label ID="profilec" runat="server"></asp:Label>%  Profile Completion</h4>
                    </div>
                </div>
            </div>
        </div>



        <div class="row" style="margin-bottom: 50px;">
            <div class="col-md-4 col-sm-12 col-xs-12">
                <div class="x_panel" style="height: 178px;">
                    <div class="x_title">
                        <h2>Dheya Updates</h2>
                        <ul class="nav navbar-right panel_toolbox">
                            <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                            </li>
                            <li><a class="close-link"><i class="fa fa-close"></i></a>
                            </li>
                        </ul>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content">
                        <asp:TextBox ID="lblDheyaUpdates" runat="server" TextMode="MultiLine" Width="302px" Height="100px" Wrap="true"></asp:TextBox>
                    </div>
                </div>
                <asp:TextBox ID="DheyaUpdates" runat="server" CssClass="textbox"></asp:TextBox>
            </div>



            <%--<div class="col-md-8 col-sm-12 col-xs-12">
                <div class="x_panel">
                    <div class="x_title">
                        <h2>Dheya's Networks</h2>
                        <ul class="nav navbar-right panel_toolbox">
                            <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                            </li>
                            <li><a class="close-link"><i class="fa fa-close"></i></a>
                            </li>
                        </ul>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content">
                        <br />
                        <center>
                                <div id="chart_div1" style="width: 100%; height: 100%;"></div>
                                    </center>
                    </div>
                </div>

                <div class="x_panel">

                    <div class="x_title">
                        <h2>Dheya Updates</h2>
                        <ul class="nav navbar-right panel_toolbox">
                            <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                            </li>
                            <li><a class="close-link"><i class="fa fa-close"></i></a>
                            </li>
                        </ul>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content">
                        <asp:TextBox ID="lblDheyaUpdates" runat="server" TextMode="MultiLine" Width="650px" Height="100px" Wrap="true"></asp:TextBox>
                    </div>

                </div>
            </div>--%>



        </div>



        <!-- Modal Lead Details - Converted-->
        <div class="modal fade" id="myModal_lead_converted" role="dialog">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">
                            &times;</button>
                        <h4 class="modal-title">Lead Converted</h4>
                    </div>
                    <div class="modal-body">
                        <div>
                            <div id="div_leadstatus_converted" runat="server" class="" style="text-align: center; margin-top: 10px;"></div>

                            <div class="row">
                                <asp:GridView ID="gv_lead_converted" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" GridLines="None" AllowPaging="True" CssClass="table table-responsive" EmptyDataText="No Data"
                                    Width="100%" OnPageIndexChanging="gv_lead_converted_PageIndexChanging"
                                    PageSize="5">
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="First Name" HeaderText="First Name" SortExpression="First Name">
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Last Name" HeaderText="Last Name" SortExpression="Last Name">
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Date" HeaderText="Entered Date" SortExpression="Date"
                                            DataFormatString="{0:d}">
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Lead Status" HeaderText="Current Status" SortExpression="Lead Status">
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" Visible="false">
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                    </Columns>
                                    <RowStyle VerticalAlign="Top" BackColor="#F7F6F3" ForeColor="#333333" />
                                    <EditRowStyle BackColor="#999999" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" VerticalAlign="Top" />
                                    <PagerStyle BackColor="#284775" HorizontalAlign="Center" CssClass="pagination-ys" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <div id="Div1" runat="server" class="row" visible="true" style="height: 352px;">
            <div class="col-md-12 col-sm-10 col-xs-12" style="margin-top: -71px; margin-left: 6px; width: 1082px;">
                <div class="dashboard_graph x_panel" style="height: 464px;">
                    <div class="x_title">
                        <h2>News Feed</h2>
                        <ul class="nav navbar-right panel_toolbox">
                            <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                            </li>
                            <li><a class="close-link"><i class="fa fa-close"></i></a>
                            </li>
                        </ul>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content">
                        <br />
                        <ul class="list-unstyled timeline widget">
                            <li>
                                <div class="block">
                                    <div class="block_content">
                                        <h2 class="title">
                                            <asp:Label ID="lbl_title1" runat="server"></asp:Label>
                                        </h2>
                                        <div class="byline">
                                            <span>
                                                <asp:Label ID="lbl_hr1" runat="server"></asp:Label>
                                                days ago</span>
                                        </div>
                                        <p class="excerpt">
                                            <asp:Label ID="lbl_content1" runat="server"></asp:Label>
                                        </p>
                                    </div>
                                </div>
                            </li>
                            <li>
                                <div class="block">
                                    <div class="block_content">
                                        <h2 class="title">
                                            <asp:Label ID="lbl_title2" runat="server"></asp:Label>
                                        </h2>
                                        <div class="byline">
                                            <span>
                                                <asp:Label ID="lbl_hr2" runat="server"></asp:Label>
                                                days ago</span>
                                        </div>
                                        <p class="excerpt">
                                            <asp:Label ID="lbl_content2" runat="server"></asp:Label>
                                        </p>
                                    </div>
                                </div>
                            </li>
                            <li>
                                <div class="block">
                                    <div class="block_content">
                                        <h2 class="title">
                                            <asp:Label ID="lbl_title3" runat="server"></asp:Label>
                                        </h2>
                                        <div class="byline">
                                            <span>
                                                <asp:Label ID="lbl_hr3" runat="server"></asp:Label>
                                                days ago</span>
                                        </div>
                                        <p class="excerpt">
                                            <asp:Label ID="lbl_content3" runat="server"></asp:Label>
                                        </p>
                                    </div>
                                </div>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
            <asp:TextBox ID="NewFeed" runat="server" CssClass="textbox"></asp:TextBox>
        </div>


        <!-- Modal Lead Details - Created-->
        <div class="modal fade" id="myModal_lead_created" role="dialog">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">
                            &times;</button>
                        <h4 class="modal-title">Lead Created</h4>
                    </div>
                    <div class="modal-body">
                        <div>
                            <div id="div_leadstatus_created" runat="server" class="" style="text-align: center; margin-top: 10px;"></div>

                            <div class="row">
                                <asp:GridView ID="gv_lead_created" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" GridLines="None" AllowPaging="True" CssClass="table table-responsive"
                                    Width="100%" OnPageIndexChanging="gv_lead_created_PageIndexChanging"
                                    PageSize="5">
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="First Name" HeaderText="First Name" SortExpression="First Name">
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Last Name" HeaderText="Last Name" SortExpression="Last Name">
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Date" HeaderText="Entered Date" SortExpression="Date"
                                            DataFormatString="{0:d}">
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Lead Status" HeaderText="Current Status" SortExpression="Lead Status">
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" Visible="false">
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                    </Columns>
                                    <RowStyle VerticalAlign="Top" BackColor="#F7F6F3" ForeColor="#333333" />
                                    <EditRowStyle BackColor="#999999" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" VerticalAlign="Top" />
                                    <PagerStyle BackColor="#284775" HorizontalAlign="Center" CssClass="pagination-ys" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Fetch Bitrix Data-->
        <div id="div_bitrix" runat="server" style="display: none;">
            <asp:HiddenField ID="hfid" runat="server" />
            <%--<asp:HiddenField ID="hfRefEmail" runat="server" />--%>
            <input type="hidden" id="hfRefEmail" runat="server" />
            <input type="hidden" id="HiddenField_AccessToken" runat="server" />
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblID" runat="server" Text="ID"> </asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtID" runat="server"> </asp:TextBox><br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblFName" runat="server" Text="First Name"> </asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtFName" runat="server"></asp:TextBox><br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblLName" runat="server" Text="Last Name"> </asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtLName" runat="server"></asp:TextBox><br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="Referd By Email"> </asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtReferdByEmail" runat="server"></asp:TextBox><br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="Contact"> </asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtContact" runat="server"></asp:TextBox><br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label6" runat="server" Text="Email"> </asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox><br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label7" runat="server" Text="Status"> </asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtLeadType" runat="server"></asp:TextBox><br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label8" runat="server" Text="Status"> </asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtLeadSource" runat="server"></asp:TextBox><br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label9" runat="server" Text="Status"> </asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtStatus" runat="server"></asp:TextBox><br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblASSIGNED_BY_ID" runat="server" Text="Assigned by"> </asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtASSIGNED_BY_ID" runat="server"></asp:TextBox><br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblDate" runat="server" Text="Date"> </asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnUpdate" runat="server" Text="SAVE" />
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvLeads" runat="server" ShowHeaderWhenEmpty="true" EnableTheming="false" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField HeaderText="BitrixID" />
                    <asp:BoundField HeaderText="First Name" />
                    <asp:BoundField HeaderText="Last Name" />
                    <asp:BoundField HeaderText="Contact No." />
                    <asp:BoundField HeaderText="Email ID" />
                    <asp:BoundField HeaderText="Status" />
                    <asp:BoundField HeaderText="Refered By" />
                    <asp:BoundField HeaderText="Registration Date" />
                    <asp:BoundField HeaderText="Assigned By" />
                </Columns>
            </asp:GridView>
            <asp:GridView ID="gvBitrix" runat="server" ShowHeaderWhenEmpty="true" EnableTheming="false" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField HeaderText="BitrixID" DataField="BitrixId" />
                    <asp:BoundField HeaderText="First Name" DataField="FName" />
                    <asp:BoundField HeaderText="Last Name" DataField="LName" />
                    <asp:BoundField HeaderText="Contact No." DataField="ContactNo" />
                    <asp:BoundField HeaderText="Email ID" DataField="Email" />
                    <asp:BoundField HeaderText="Lead Type" DataField="LeadType" />
                    <asp:BoundField HeaderText="Lead Source" DataField="LeadSource" />
                    <asp:BoundField HeaderText="Lead Status" DataField="LeadStatus" />
                    <asp:BoundField HeaderText="Refered By" DataField="ReferedByEmail" />
                    <asp:BoundField HeaderText="Registration Date" DataField="RegDate" />
                </Columns>
            </asp:GridView>
        </div>

        <%-- Bitrix Deals --%>
        <div id="div2" runat="server" style="display: none;">
            <table>
                <tr>
                    <td>
                        <asp:TextBox ID="txtDealId" runat="server"> </asp:TextBox><br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtLeadId" runat="server"> </asp:TextBox><br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtDealReferedBy" runat="server"></asp:TextBox><br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtDealStatus" runat="server"></asp:TextBox><br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtRegDate" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtAddedDate" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvDeals" runat="server" ShowHeaderWhenEmpty="true" EnableTheming="false" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField HeaderText="BitrixDealsID" />
                    <asp:BoundField HeaderText="BitrixLeadsID" />
                    <asp:BoundField HeaderText="DealStatus" />
                    <asp:BoundField HeaderText="DealReferedBy" />
                    <asp:BoundField HeaderText="Registration Date" />
                </Columns>
            </asp:GridView>
            <asp:GridView ID="GridView3" runat="server" ShowHeaderWhenEmpty="true" EnableTheming="false" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField HeaderText="BitrixID" DataField="BitrixId" />
                    <asp:BoundField HeaderText="First Name" DataField="FName" />
                    <asp:BoundField HeaderText="Last Name" DataField="LName" />
                    <asp:BoundField HeaderText="Contact No." DataField="ContactNo" />
                    <asp:BoundField HeaderText="Email ID" DataField="Email" />
                    <asp:BoundField HeaderText="Lead Type" DataField="LeadType" />
                    <asp:BoundField HeaderText="Lead Source" DataField="LeadSource" />
                    <asp:BoundField HeaderText="Lead Status" DataField="LeadStatus" />
                    <asp:BoundField HeaderText="Refered By" DataField="ReferedByEmail" />
                    <asp:BoundField HeaderText="Registration Date" DataField="RegDate" />
                </Columns>
            </asp:GridView>
        </div>
        <!-- Fetch Bitrix Data-->

    </form>

    <!-- jQuery -->
    <script src="js/custom.min.js"></script>
    <script type='text/javascript' src='https://www.google.com/jsapi'></script>
    <script type='text/javascript'>
        google.load('visualization', '1', { 'packages': ['geochart'] });
        google.setOnLoadCallback(drawMarkersMap);

        function drawMarkersMap() {
            var data = google.visualization.arrayToDataTable([
               ['State', 'Total Number of CDF'],
              ['West Bengal', 29],
              ['Maharashtra', 226],
              ['Uttar Pradesh', 21],
              ['Madhya Pradesh', 11],
              ['Tamil Nadu', 61],
              ['Rajasthan', 12],
              ['Karnataka', 111],
              ['Gujarat', 50],
              ['Andhra Pradesh', 1],
              ['Orissa', 5],
              ['Telangana', 49],
              ['Chhattisgarh', 1],
              ['Jharkhand', 7],
              ['Bihar', 1],
              ['Punjab', 1],
              ['Haryana', 11],
              ['Delhi', 21],
              ['Goa', 2],
              ['Tripura', 1],
              ['Uttarakhand', 1],
            ]);

            var options = {
                region: 'IN',
                displayMode: 'regions',
                resolution: 'provinces',
                colorAxis: { colors: ['#81aff9', '#4374e0'] },
                backgroundColor: '#ffffff',
            };

            var chart = new google.visualization.GeoChart(document.getElementById('chart_div1'));
            chart.draw(data, options);
        };
    </script>
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>

    <script type="text/javascript">

        google.load("visualization", "1", { packages: ["corechart"] });
        google.setOnLoadCallback(drawChart);
        function drawChart() {
            var options = {
                legend: 'none',
                //pieSliceText: 'label',
                slices: {
                    0: { color: '#3498DB' },
                    1: { color: '#9CC2CB' }
                },
                pieHole: 0.4
            };
            $.ajax({
                type: "POST",
                url: "home1.aspx/GetChartData",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var data = google.visualization.arrayToDataTable(r.d);
                    var chart = new google.visualization.PieChart($("#chart11")[0]);
                    chart.draw(data, options);
                },
                failure: function (r) {
                    alert(r.d);
                },
                error: function (r) {
                    alert(r.d);
                }
            });
        }
    </script>
</asp:Content>

