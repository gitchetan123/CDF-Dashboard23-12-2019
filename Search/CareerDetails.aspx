<%@ Page Title="" Language="C#" MasterPageFile="~/CDFMaster.master" AutoEventWireup="true"
    CodeFile="CareerDetails.aspx.cs" Inherits="Search_CareerDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .row {
            padding: 5px;
            margin: 5px;
            padding-left: 10px;
            padding-right: 10px;
        }

        .panel {
            padding-bottom: 10px;
            padding-left: 10px;
            padding-right: 10px;
            padding-top: 10px;
            text-align: left;
            max-width: 900px;
            padding: 5px;
            margin: 0 auto;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
    <script
        src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js">
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('body').bind('cut copy paste', function (e) {
                e.preventDefault();
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="Form1" role="form" runat="server">
        <div class="x_panel">
            <div class="x_title">
                <h2>Search Career Information</h2>
                <ul class="nav navbar-right panel_toolbox">
                    <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                    </li>

                    <li><a class="close-link"><i class="fa fa-close"></i></a>
                    </li>
                </ul>
                <div class="clearfix"></div>
            </div>
            <div class="x_content">
                <div class="row">
                    <div class="col-sm-4">
                    </div>
                    <div class="col-sm-6">
                        <h4>Career :-
                    <asp:Label ID="lblCareerName" runat="server" class="text-primary"></asp:Label></h4>
                    </div>
                    <div class="col-sm-2">
                    </div>
                </div>
                <hr class="divider"></hr>
                <div class="row">
                    <div class="col-sm-2">
                    </div>
                    <div class="col-sm-4">
                        <a href="#Basicinfo">Basic Information</a>
                    </div>
                    <div class="col-sm-4">
                        <a href="#Jobyoudo">Job you Do</a>

                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-2">
                    </div>
                    <div class="col-sm-4">
                        <a href="#Coursetopursue">Courses to Pursue</a>
                    </div>
                    <div class="col-sm-4">
                        <a href="#Careeradv">Career Advancement</a>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-2">
                    </div>
                    <div class="col-sm-4">
                        <a href="#Workenv">Work Environment</a>
                    </div>
                    <div class="col-sm-4">
                        <a href="#Workcontext">Work Context</a>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-2">
                    </div>
                    <div class="col-sm-4">
                        <a href="#Workcommdemands">Work Communication Demands</a>
                    </div>
                    <div class="col-sm-4">
                        <a href="#Menability">Mental Abilities</a>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-2">
                    </div>
                    <div class="col-sm-4">
                        <a href="#Phyability">Physical Abilities</a>
                    </div>
                </div>
                <hr class="divider"></hr>
                <div class="row">
                    <div class="col-sm-offset-2 col-sm-8 ">
                        <table class="table" cellpadding="5px">
                            <tr>
                                <td width="180" align="left" valign="top" class="main2">Sector You Work for:
                                </td>
                                <td width="500" align="left" valign="top" class="main4">
                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("basic_info2") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top" class="main2">Industries You Work for:
                                </td>
                                <td align="left" valign="top" class="main4">
                                    <asp:Label ID="Label7" runat="server" Text='<%# Eval("basic_info7") %>'></asp:Label>
                                    <asp:Label ID="Label8" runat="server" Text='<%# Eval("basic_info8") %>'></asp:Label>
                                    <asp:Label ID="Label9" runat="server" Text='<%# Eval("basic_info9") %>'></asp:Label>
                                    <asp:Label ID="Label10" runat="server" Text='<%# Eval("basic_info10") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top" class="main2">Your professional Interests:
                                </td>
                                <td align="left" valign="top" class="main4">
                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("basic_info6") %>'></asp:Label>
                                    ,
                        <asp:Label ID="Label5" runat="server" Text='<%# Eval("basic_info6") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="main2" valign="top">Career Category :
                                </td>
                                <td align="left" class="main4" valign="top">
                                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("basic_info6") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="main2" valign="top">Future Relevance :
                                </td>
                                <td align="left" class="main4" valign="top">
                                    <asp:Label ID="lbl_career_scope" runat="server" Text='<%# Eval("Career_scope") %>'></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <hr class="divider"></hr>
                <div class="row" id="Basicinfo">
                    <label for="name" class="col-sm-12 control-label ">
                        What's this career all about</label>
                </div>
                <div class="row">
                    <asp:Label ID="Label3" class="col-sm-12 control-label" runat="server" Text='<%# Eval("basic_info3") %>'></asp:Label>
                </div>
                <hr class="divider"></hr>
                <div class="row" id="Jobyoudo">
                    <label for="name" class="col-sm-12 control-label ">
                        The Job you do</label>
                </div>
                <div class="row">
                    <div align="justify">
                        <ul>
                            <asp:Label ID="job_focus1Label" runat="server" class="col-sm-12 control-label" Text='<%# Eval("job_focus1") %>'></asp:Label>
                            <asp:Label ID="job_focus2Label" runat="server" class="col-sm-12 control-label" Text='<%# Eval("job_focus2") %>'></asp:Label>
                            <asp:Label ID="job_focus3Label" runat="server" class="col-sm-12 control-label" Text='<%# Eval("job_focus3") %>'></asp:Label>
                            <asp:Label ID="job_focus4Label" runat="server" class="col-sm-12 control-label" Text='<%# Eval("job_focus4") %>'></asp:Label>
                            <asp:Label ID="job_focus5Label" runat="server" class="col-sm-12 control-label" Text='<%# Eval("job_focus5") %>'></asp:Label>
                            <asp:Label ID="job_focus6Label" runat="server" class="col-sm-12 control-label" Text='<%# Eval("job_focus6") %>'></asp:Label>
                            <asp:Label ID="job_focus7Label" runat="server" class="col-sm-12 control-label" Text='<%# Eval("job_focus7") %>'></asp:Label>
                            <asp:Label ID="job_focus8Label" runat="server" class="col-sm-12 control-label" Text='<%# Eval("job_focus8") %>'></asp:Label>
                        </ul>
                    </div>
                </div>
                <hr class="divider"></hr>
                <div class="row" id="Careeradv">
                    <label for="name" class="col-sm-12 control-label ">
                        Career Advancement Opportunities</label>
                </div>
                <div class="row">
                    <asp:Label ID="career_advancement1Label" class="col-sm-12 control-label" runat="server"
                        Text='<%# Eval("career_advancement1") %>'></asp:Label>
                </div>
                <hr class="divider"></hr>
                <div class="row" id="Coursetopursue">
                    <label for="name" class="col-sm-12 control-label ">
                        Line of education that leads to this career</label>
                </div>
                <div class="row">
                    <div class="col-sm-1">
                    </div>
                    <asp:Label ID="lblListOfCourses" class="col-sm-11 control-label" runat="server" Text='<%# Eval("career_advancement1") %>'></asp:Label>
                </div>
                <hr class="divider"></hr>
                <div class="row" id="Workenv">
                    <label for="name" class="col-sm-12 control-label ">
                        The Work Environment</label>
                </div>
                <div class="row">
                    <div class="col-sm-offset-2 col-sm-8 ">

                        <table class="table" cellpadding="5px">
                            <tr>
                                <td width="300" align="left" valign="top" class="main2">Working in an Office Environment Indoors :
                                </td>
                                <td width="500" align="left" valign="top" class="main4">
                                    <asp:Label ID="work_environment1Label" runat="server" Text='<%# Eval("work_environment1") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top" class="main2">Mobility & travel :
                                </td>
                                <td align="left" valign="top" class="main4">
                                    <asp:Label ID="work_environment2Label" runat="server" Text='<%# Eval("work_environment2") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top" class="main2">Working with Equipment/ Machinery :
                                </td>
                                <td align="left" valign="top" class="main4">
                                    <asp:Label ID="work_environment3Label" runat="server" Text='<%# Eval("work_environment3") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top" class="main2">Working with Computers :
                                </td>
                                <td align="left" valign="top" class="main4">
                                    <asp:Label ID="work_environment4Label" runat="server" Text='<%# Eval("work_environment4") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top" class="main2">High Risk environment :
                                </td>
                                <td align="left" valign="top" class="main4">
                                    <asp:Label ID="work_environment5Label" runat="server" Text='<%# Eval("work_environment5") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top" class="main2">Long Hours :
                                </td>
                                <td align="left" valign="top" class="main4">
                                    <asp:Label ID="work_environment6Label" runat="server" Text='<%# Eval("work_environment6") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top" class="main2">Working at Night :
                                </td>
                                <td align="left" valign="top" class="main4">
                                    <asp:Label ID="work_environment7Label" runat="server" Text='<%# Eval("work_environment7") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="main2" valign="top">Working with High-tech / sophisticated equipment :
                                </td>
                                <td align="left" class="main4" valign="top">
                                    <asp:Label ID="work_environment8Label" runat="server" Text='<%# Eval("work_environment8") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="main2" valign="top">Working closely with people :
                                </td>
                                <td align="left" class="main4" valign="top">
                                    <asp:Label ID="work_environment9Label" runat="server" Text='<%# Eval("work_environment9") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="main2" valign="top">Sitting / standing at one place :
                                </td>
                                <td align="left" class="main4" valign="top">
                                    <asp:Label ID="work_environment10Label" runat="server" Text='<%# Eval("work_environment10") %>'></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <hr class="divider"></hr>
                <div class="row" id="Workcontext">
                    <label for="name" class="col-sm-12 control-label ">
                        The Work Context</label>
                </div>
                <div class="row">
                    <div class="col-sm-offset-2 col-sm-8 ">

                        <table class="table" cellpadding="5px">
                            <tr>
                                <td width="300" align="left" valign="top" class="main2">Result Oriented / Target oriented jobs :
                                </td>
                                <td width="500" align="left" valign="top" class="main4">
                                    <asp:Label ID="work_context1Label" runat="server" Text='<%# Eval("work_context1") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top" class="main2">Short term timeline based projects :
                                </td>
                                <td align="left" valign="top" class="main4">
                                    <asp:Label ID="work_context2Label" runat="server" Text='<%# Eval("work_context2") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top" class="main2">Setting goals for yourself and the team :
                                </td>
                                <td align="left" valign="top" class="main4">
                                    <asp:Label ID="work_context3Label" runat="server" Text='<%# Eval("work_context3") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top" class="main2">High decision making needs :
                                </td>
                                <td align="left" valign="top" class="main4">
                                    <asp:Label ID="work_context4Label" runat="server" Text='<%# Eval("work_context4") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top" class="main2">High problem solving needs :
                                </td>
                                <td align="left" valign="top" class="main4">
                                    <asp:Label ID="work_context5Label" runat="server" Text='<%# Eval("work_context5") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top" class="main2">People to people interaction :
                                </td>
                                <td align="left" valign="top" class="main4">
                                    <asp:Label ID="work_context6Label" runat="server" Text='<%# Eval("work_context6") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top" class="main2">Working in teams/ with people groups :
                                </td>
                                <td align="left" valign="top" class="main4">
                                    <asp:Label ID="work_context7Label" runat="server" Text='<%# Eval("work_context7") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="main2" valign="top">Leading/ managing a team :
                                </td>
                                <td align="left" class="main4" valign="top">
                                    <asp:Label ID="work_context8Label" runat="server" Text='<%# Eval("work_context8") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="main2" valign="top">Individual working environment :
                                </td>
                                <td align="left" class="main4" valign="top">
                                    <asp:Label ID="work_context9Label" runat="server" Text='<%# Eval("work_context9") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="main2" valign="top">Repetitive jobs and routine :
                                </td>
                                <td align="left" class="main4" valign="top">
                                    <asp:Label ID="work_context10Label" runat="server" Text='<%# Eval("work_context10") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="main2" valign="top">Working with higher Quality and Accuracy requirements :
                                </td>
                                <td align="left" class="main4" valign="top">
                                    <asp:Label ID="work_context11Label" runat="server" Text='<%# Eval("work_context11") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="main2" valign="top">Designing processes/ policies :
                                </td>
                                <td align="left" class="main4" valign="top">
                                    <asp:Label ID="work_context12Label" runat="server" Text='<%# Eval("work_context12") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="main2" valign="top">Working with processes :
                                </td>
                                <td align="left" class="main4" valign="top">
                                    <asp:Label ID="work_context13Label" runat="server" Text='<%# Eval("work_context13") %>'></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <hr class="divider"></hr>
                <div class="row" id="Workcommdemands">
                    <label for="name" class="col-sm-12 control-label ">
                        Work Communication Demands</label>
                </div>
                <div class="row">
                    <div class="col-sm-offset-2 col-sm-8 ">

                        <table class="table" cellpadding="5px">
                            <tr>
                                <td width="300" align="left" valign="top" class="main2">Working with Emails :
                                </td>
                                <td width="500" align="left" valign="top" class="main4">
                                    <asp:Label ID="work_comm_demands1Label" runat="server" Text='<%# Eval("work_comm_demands1") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top" class="main2">People to People Interaction :
                                </td>
                                <td align="left" valign="top" class="main4">
                                    <asp:Label ID="work_comm_demands2Label" runat="server" Text='<%# Eval("work_comm_demands2") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top" class="main2">Verbal Communication with people masses :
                                </td>
                                <td align="left" valign="top" class="main4">
                                    <asp:Label ID="work_comm_demands3Label" runat="server" Text='<%# Eval("work_comm_demands3") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top" class="main2">Written communication &amp; Expression :
                                </td>
                                <td align="left" valign="top" class="main4">
                                    <asp:Label ID="work_comm_demands4Label" runat="server" Text='<%# Eval("work_comm_demands4") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top" class="main2">Non Verbal communication / actions / acting :
                                </td>
                                <td align="left" valign="top" class="main4">
                                    <asp:Label ID="work_comm_demands5Label" runat="server" Text='<%# Eval("work_comm_demands5") %>'></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <hr class="divider"></hr>
                <div class="row" id="Menability">
                    <label for="name" class="col-sm-12 control-label ">
                        The Mental Abilities</label>
                </div>
                <div class="row">
                    <div class="col-sm-offset-2 col-sm-8 ">

                        <table class="table" cellpadding="5px">
                            <tr>
                                <td width="300" align="left" valign="top" class="main2">Numerical Ability :
                                </td>
                                <td width="500" align="left" valign="top" class="main4">
                                    <asp:Label ID="mental_abilities1Label" runat="server" Text='<%# Eval("mental_abilities1") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top" class="main2">Verbal Ability :
                                </td>
                                <td align="left" valign="top" class="main4">
                                    <asp:Label ID="mental_abilities2Label" runat="server" Text='<%# Eval("mental_abilities2") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top" class="main2">Written comprehension &amp; Writing Skills :
                                </td>
                                <td align="left" valign="top" class="main4">
                                    <asp:Label ID="mental_abilities3Label" runat="server" Text='<%# Eval("mental_abilities3") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top" class="main2">Written Expression :
                                </td>
                                <td align="left" valign="top" class="main4">
                                    <asp:Label ID="mental_abilities4Label" runat="server" Text='<%# Eval("mental_abilities4") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top" class="main2">Verbal Comprehension :
                                </td>
                                <td align="left" valign="top" class="main4">
                                    <asp:Label ID="mental_abilities5Label" runat="server" Text='<%# Eval("mental_abilities5") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top" class="main2">Verbal Expression :
                                </td>
                                <td align="left" valign="top" class="main4">
                                    <asp:Label ID="mental_abilities6Label" runat="server" Text='<%# Eval("mental_abilities6") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top" class="main2">Logical thinking :
                                </td>
                                <td align="left" valign="top" class="main4">
                                    <asp:Label ID="mental_abilities7Label" runat="server" Text='<%# Eval("mental_abilities7") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="main2" valign="top">Analytical Thinking :
                                </td>
                                <td align="left" class="main4" valign="top">
                                    <asp:Label ID="mental_abilities8Label" runat="server" Text='<%# Eval("mental_abilities8") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="main2" valign="top">Problem Sensitivity and solving :
                                </td>
                                <td align="left" class="main4" valign="top">
                                    <asp:Label ID="mental_abilities9Label" runat="server" Text='<%# Eval("mental_abilities9") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="main2" valign="top">Space relations :
                                </td>
                                <td align="left" class="main4" valign="top">
                                    <asp:Label ID="mental_abilities10Label" runat="server" Text='<%# Eval("mental_abilities10") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="main2" valign="top">Abstract reasoning :
                                </td>
                                <td align="left" class="main4" valign="top">
                                    <asp:Label ID="mental_abilities11Label" runat="server" Text='<%# Eval("mental_abilities11") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="main2" valign="top">Visualization :
                                </td>
                                <td align="left" class="main4" valign="top">
                                    <asp:Label ID="mental_abilities12Label" runat="server" Text='<%# Eval("mental_abilities12") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="main2" valign="top">Creative imagination &amp; originality :
                                </td>
                                <td align="left" class="main4" valign="top">
                                    <asp:Label ID="mental_abilities13Label" runat="server" Text='<%# Eval("mental_abilities13") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="main2" valign="top">Memory &amp; recall :
                                </td>
                                <td align="left" class="main4" valign="top">
                                    <asp:Label ID="mental_abilities14Label" runat="server" Text='<%# Eval("mental_abilities14") %>'></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <hr class="divider"></hr>
                <div class="row" id="Phyability">
                    <label for="name" class="col-sm-12 control-label ">
                        The Physical Abilities</label>
                </div>
                <div class="row">
                    <div class="col-sm-offset-2 col-sm-8 ">

                        <table class="table" cellpadding="5px">
                            <tr>
                                <td width="300" align="left" valign="top" class="main2">Finger &amp; hand swiftness :
                                </td>
                                <td width="500" align="left" valign="top" class="main4">
                                    <asp:Label ID="physical_abilities1Label" runat="server" Text='<%# Eval("physical_abilities1") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top" class="main2">Hearing :
                                </td>
                                <td align="left" valign="top" class="main4">
                                    <asp:Label ID="physical_abilities2Label" runat="server" Text='<%# Eval("physical_abilities2") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top" class="main2">Vision :
                                </td>
                                <td align="left" valign="top" class="main4">
                                    <asp:Label ID="physical_abilities3Label" runat="server" Text='<%# Eval("physical_abilities3") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top" class="main2">Night Vision :
                                </td>
                                <td align="left" valign="top" class="main4">
                                    <asp:Label ID="physical_abilities4Label" runat="server" Text='<%# Eval("physical_abilities4") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top" class="main2">Stamina &amp; Fitness :
                                </td>
                                <td align="left" valign="top" class="main4">
                                    <asp:Label ID="physical_abilities5Label" runat="server" Text='<%# Eval("physical_abilities5") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top" class="main2">Hand steadiness :
                                </td>
                                <td align="left" valign="top" class="main4">
                                    <asp:Label ID="physical_abilities6Label" runat="server" Text='<%# Eval("physical_abilities6") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top" class="main2">Gross body coordination :
                                </td>
                                <td align="left" valign="top" class="main4">
                                    <asp:Label ID="physical_abilities7Label" runat="server" Text='<%# Eval("physical_abilities7") %>'></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>

                <hr class="divider"></hr>

                <div class="row" id="other">
                    <label for="name" class="col-sm-12 control-label ">
                        Other Information</label>
                </div>
                <div class="row">
                    <div class="col-sm-offset-2 col-sm-8 ">

                        <table class="table" cellpadding="5px">
                            <tr>
                                <td width="300" align="left" valign="top" class="main2">Famous Personalities :
                                </td>
                                <td width="500" align="left" valign="top" class="main4">
                                    <asp:Label ID="lbl_FamousPersonalities" runat="server" Text='<%# Eval("famousPersonalities") %>'></asp:Label>
                                    <asp:DataList ID="DataList_famousPersonalities" runat="server">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HyperLink1" Target="_blank" runat="server" NavigateUrl='<%# Eval("famousPersonalities") %>' Text='<%# Eval("famousPersonalities") %>'></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top" class="main2">Useful Links :
                                </td>
                                <td align="left" valign="top" class="main4">
                                    <asp:Label ID="lbl_UsefulLinks" runat="server" Text='<%# Eval("usefulLinks") %>'></asp:Label>
                                    <asp:DataList ID="DataList_usefulLinks" runat="server">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HyperLink2" Target="_blank" runat="server" NavigateUrl='<%# Eval("usefulLinks") %>' Text='<%# Eval("usefulLinks") %>'></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:DataList>

                                    <%-- CssClass="table table-responsive"--%>
                                </td>
                            </tr>

                        </table>




                    </div>
                </div>
                <hr class="divider"></hr>
                <div class="row">
                    <div class="col-sm-5">
                    </div>
                    <div class="col-sm-1">
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="#top">Top</asp:HyperLink>
                    </div>
                </div>
            </div>
        </div>

    </form>
    <!-- Custom Theme Scripts -->
    <script type="text/javascript" src="../js/custom.min.js"></script>
</asp:Content>
