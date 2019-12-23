<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin-master.master" AutoEventWireup="true"
    CodeFile="documents.aspx.cs" Inherits="Admin_documents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .row {
            padding: 10px;
        }

        body {
            font-family: 'Arial Rounded MT';
        }
    </style>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form runat="server">
        <div class="x_panel">
            <div class="x_title">
                <h2>Add Documents</h2>
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
                <div class="container">
                    <div style="padding: 0 20px 0 20px; margin-bottom: 20px">
                        <center>
                <div id="div_msg" runat="server" class="" style="text-align: center; max-width: 95%; margin-top: 10px;">
                </div>
            </center>
                        <div class="row">
                            <div class="col-md-10 col-md-offset-1">
                                <label class="control-label text-primary ">
                                    Parent Directory Name
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddl_parent"
                            Display="Static" ErrorMessage="Please select Directory name" InitialValue="-- Select --">*</asp:RequiredFieldValidator>
                                </label>
                                <asp:DropDownList ID="ddl_parent" CssClass="form-control" runat="server" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddl_parent_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div id="div_parent" class="row" runat="server">
                            <div class="col-md-6 col-md-offset-1">
                                <label class="control-label text-primary ">
                                    New Parent Document Name
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_newparent"
                            Display="Static" ErrorMessage="Please enter Directory name" ValidationGroup="qe" InitialValue="">*</asp:RequiredFieldValidator>
                                </label>
                                <asp:TextBox ID="txt_newparent" ValidationGroup="qe" class="form-control" placeholder="Enter New Parent Directory Name"
                                    runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3 col-md-offset-1" style="margin-top: 25px;">
                                <asp:Button ID="btn_addparent" class="btn btn-primary btn-block" runat="server" Text="Add Parent"
                                    OnClick="btn_addparent_Click" ValidationGroup="qe" />
                            </div>
                        </div>
                        <div id="div_rows" runat="server">
                            <div class="row">
                                <div class="col-md-10 col-md-offset-1">
                                    <label for="name" class="control-label text-primary">
                                        Document Name
                                    </label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_docname"
                                        Display="Static" ErrorMessage="Please enter document name">*</asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txt_docname" class="form-control" placeholder="Enter Document Name"
                                        runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-10 col-md-offset-1">
                                    <label class="control-label text-primary text-primary">
                                        Tooltip of Document (Description of document)
                                    </label> 
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txt_tooltip"
                                        runat="server" ErrorMessage="Please enter Tooltip of document">*</asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txt_tooltip" CssClass="form-control" placeholder="Enter Document Description"
                                        runat="server" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-10 col-md-offset-1">
                                    <label class="control-label text-primary text-primary">
                                        Preview uploaded image (png or jpg only)
                                    </label>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationExpression="(([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif|.jpeg|.bmp|.GIF|.JPG|.JPEG|.PNG|.BMP)$)$"
                                        ControlToValidate="fileUpload2" runat="server" ForeColor="Red" ErrorMessage="Please select a valid png,jpg,jpeg or bmp File."
                                        Display="Dynamic" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="fileUpload2"
                                        Display="Static" ErrorMessage="Please Select file">*</asp:RequiredFieldValidator>
                                    <asp:FileUpload ID="fileUpload2" runat="server" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-10 col-md-offset-1">
                                    <label class="control-label text-primary text-primary">
                                        Upload the File
                                    </label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="fileUpload1"
                                        Display="Static" ErrorMessage="Please Select file">*</asp:RequiredFieldValidator>
                                    <asp:FileUpload ID="fileUpload1" runat="server" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-10 col-md-offset-1">
                                    <label class="control-label text-primary ">
                                        Status
                                    </label>
                                    <asp:DropDownList ID="ddl_status" CssClass="form-control" runat="server">
                                        <asp:ListItem Value="ACTIVE" Text="ACTIVE"></asp:ListItem>
                                        <asp:ListItem Value="DEACTIVE" Text="DEACTIVE"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2 col-md-offset-2">
                                    <asp:Button ID="btnUpload" CssClass="btn btn-primary btn-block" runat="server" Text="Upload"
                                        OnClick="btnUpload_Click" />
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="btn_update" class="btn btn-primary btn-block" runat="server" Text="Update"
                                        OnClick="btn_update_Click" ValidationGroup="a" />
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="btn_delete" class="btn btn-primary btn-block" runat="server" Text="Delete"
                                        ValidationGroup="a" OnClick="btn_delete_Click" />
                                </div>
                                <div class=" col-md-2">
                                    <asp:Button ID="btn_clear" class="btn btn-primary btn-block" runat="server" Text="Clear"
                                        ValidationGroup="clear" OnClick="btn_clear_Click" />
                                </div>
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" meta:resourcekey="ValidationSummary1Resource1" DisplayMode="List" />
                            </div>
                        </div>
                    </div>
                    <div>
                        <asp:GridView ID="grid_doc" CssClass="table" runat="server" AutoGenerateColumns="False"
                            DataKeyNames="id" DataSourceID="SqlDataSource_doc" OnSelectedIndexChanged="grid_doc_SelectedIndexChanged"
                            Width="100%" GridLines="None">
                            <Columns>
                                <asp:CommandField HeaderText="Select" ShowSelectButton="True" />
                                <asp:BoundField DataField="id" HeaderText="id" InsertVisible="False"
                                    ReadOnly="True" SortExpression="id" Visible="False" />
                                <asp:BoundField DataField="doc_name" HeaderText="Document Name"
                                    SortExpression="doc_name" />
                                <asp:BoundField DataField="p_id" HeaderText="p_id" SortExpression="p_id">
                                    <HeaderStyle Font-Size="0pt" />
                                    <ItemStyle Font-Size="0pt" />
                                </asp:BoundField>
                                <asp:BoundField DataField="parent_name" HeaderText="Parent Name "
                                    SortExpression="parent_name" />
                                <asp:BoundField DataField="path" HeaderText="path" SortExpression="path"
                                    Visible="False" />
                                <asp:BoundField DataField="tooltip" HeaderText="ToolTip"
                                    SortExpression="tooltip" />
                                <asp:BoundField DataField="status" HeaderText="Status"
                                    SortExpression="status" />
                            </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource_doc" runat="server" ConnectionString="<%$ ConnectionStrings:DBConnection %>"
                            SelectCommand="SELECT c.id,c.doc_name,c.p_id,c.path,c.tooltip,c.status,p.doc_name as parent_name FROM tblDocDirectory c,tblDocDirectory p  WHERE c.p_id=p.id;
"></asp:SqlDataSource>
                        <asp:HiddenField ID="hf_id" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </form>
    <!-- Custom Theme Scripts -->
    <script type="text/javascript" src="../js/custom.min.js"></script>

    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnUpload.ClientID %>").disabled = true;
            document.getElementById("<%=btnUpload.ClientID %>").value = "Uploading..."
        }
        window.onbeforeunload = DisableButton;
    </script>
    <script type="text/javascript">
        disableSelection(document.body) //disable text selection on entire body of page
    </script>

</asp:Content>
