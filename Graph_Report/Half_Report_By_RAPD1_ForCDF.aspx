<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CDFMaster.master" CodeFile="Half_Report_By_RAPD1_ForCDF.aspx.cs" Inherits="MobileAppReport1_Half_Report_By_RAPD1_ForCDF" %>

<%@ Register TagPrefix="dotnet" Namespace="dotnetCHARTING" Assembly="dotnetCHARTING"%>
<%@ Import Namespace="System.Drawing" %>
<%@ Import Namespace="System.Drawing.Drawing2D" %>
<%@ Import Namespace="dotnetCHARTING.Mapping" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <form id="form1" runat="server">
    <div>       
    
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />
    
        <asp:Chart ID="AbilityBarChart" runat="server" BackColor="Transparent" Palette="Berry"            >
            <Series>
                <asp:Series Name="Series1">
                </asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1" BackColor="Transparent">
                    <Position Height="94" Width="94" X="3" Y="3" />
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
    





    <dotnet:Chart id="Chart" runat="server" BackColor="White" BubbleCenterStack="False" 
            BubbleStackShadeAsOne="False" CalculateEmptyElement="False" CleanupPeriod="50" 
            DataGridSeriesHeader="" PieLabelMode="Outside" SpacingPercentageNested="1" 
            BorderColor="Black" BorderStyle="Solid" BorderWidth="20px">
<JS>
<Buttons>
<Background ShadingEffectMode="Default" Color="Gainsboro"></Background>

<Foreground Color="White"></Foreground>

<Outline Color="123, 123, 123"></Outline>

<IconStrokeLine Color="123, 123, 123"></IconStrokeLine>

<ForegroundHover Color="White"></ForegroundHover>

<OutlineHover Color="80, 80, 80"></OutlineHover>

<IconStrokeLineHover Color="108, 106, 133"></IconStrokeLineHover>
</Buttons>
</JS>

<Box DynamicSize="True" CornerBottomLeft="Cut" CornerBottomRight="Cut" 
            CornerTopRight="Round" DefaultCorner="Cut" Size="639, 479">
<Header>
<Label Alignment="Center" Font="Tahoma, 8.25pt, style=Bold"></Label>
    <Background ShadingEffectMode="Default" Visible="False" />
</Header>

<HeaderLabel Alignment="Center" Font="Tahoma, 8.25pt, style=Bold"></HeaderLabel>

    <HeaderBackground ShadingEffectMode="Default" Visible="False" />

<Background Color="" ShadingEffectMode="Default"></Background>
    <Shadow Depth="1" ExpandBy="2" Visible="False" />
</Box>

<SmartForecast Start="" Unit="None" TimeSpan="00:00:00"></SmartForecast>

<DefaultElement>
<DefaultSubValue Visible="False"></DefaultSubValue>
    <SmartLabel PieLabelMode="Outside">
    </SmartLabel>
</DefaultElement>

<DefaultLegendBox Padding="4" Visible="False">
<HeaderEntry Visible="False"></HeaderEntry>

<Header>
<Label Alignment="Center" Font="Tahoma, 7.5pt, style=Bold"></Label>

<Background ShadingEffectMode="Default"></Background>
</Header>

<HeaderLabel Alignment="Center" Font="Tahoma, 7.5pt, style=Bold"></HeaderLabel>

<HeaderBackground ShadingEffectMode="Default"></HeaderBackground>

<Shadow ExpandBy="2"></Shadow>
</DefaultLegendBox>

<DefaultTitleBox Visible="False">
<Header>
<Label Alignment="Center" Font="Tahoma, 7.5pt, style=Bold"></Label>

<Background ShadingEffectMode="Default"></Background>
</Header>

<HeaderLabel Alignment="Center" Font="Tahoma, 7.5pt, style=Bold"></HeaderLabel>

<HeaderBackground ShadingEffectMode="Default"></HeaderBackground>

<Shadow ExpandBy="2"></Shadow>
</DefaultTitleBox>

        <Background Color="Transparent" />

<ChartArea StartDateOfYear="" CornerTopLeft="Square">
<DefaultElement>
<DefaultSubValue Visible="True">
    <Line Length="4" />
    </DefaultSubValue>

<SmartLabel PieLabelMode="Outside"></SmartLabel>
</DefaultElement>

<YAxis>
<ScaleBreakLine Color="Gray"></ScaleBreakLine>

<TimeScaleLabels MaximumRangeRows="4"></TimeScaleLabels>

<MinorTimeIntervalAdvanced Start="" Unit="None" TimeSpan="00:00:00"></MinorTimeIntervalAdvanced>

<ZeroTick>
<Line Length="3"></Line>
</ZeroTick>

<DefaultTick>
<Line Length="3"></Line>

<Label Text="%Value"></Label>
</DefaultTick>

<TimeIntervalAdvanced Start="" Unit="Months" TimeSpan="00:00:00"></TimeIntervalAdvanced>

<AlternateGridBackground Color="100, 220, 220, 220"></AlternateGridBackground>

<Label Alignment="Center" Font="Tahoma, 9pt, style=Bold"></Label>
</YAxis>

<XAxis>
<ScaleBreakLine Color="Gray"></ScaleBreakLine>

<TimeScaleLabels MaximumRangeRows="4"></TimeScaleLabels>

<MinorTimeIntervalAdvanced Start="" Unit="None" TimeSpan="00:00:00"></MinorTimeIntervalAdvanced>

<ZeroTick>
<Line Length="3"></Line>
</ZeroTick>

<DefaultTick>
<Line Length="3"></Line>

<Label Text="%Value"></Label>
</DefaultTick>

<TimeIntervalAdvanced Start="" Unit="Months" TimeSpan="00:00:00"></TimeIntervalAdvanced>

<Label Alignment="Center" Font="Tahoma, 9pt, style=Bold"></Label>
</XAxis>

<Shadow ExpandBy="2" Depth="1" Visible="False"></Shadow>

<TitleBox Position="Left">
<Header>
<Label Alignment="Center" Font="Tahoma, 7.5pt, style=Bold"></Label>

<Background ShadingEffectMode="Default"></Background>
</Header>

<HeaderLabel Alignment="Center" Font="Tahoma, 7.5pt, style=Bold"></HeaderLabel>

<HeaderBackground ShadingEffectMode="Default"></HeaderBackground>

<Label LineAlignment="Center"></Label>

<Shadow ExpandBy="2"></Shadow>
</TitleBox>

<LegendBox Padding="4" CornerBottomRight="Cut" Orientation="TopRight">
<LabelStyle Font="Trebuchet MS, 8pt"></LabelStyle>

<DefaultEntry>
<LabelStyle Font="Trebuchet MS, 8pt"></LabelStyle>
</DefaultEntry>

<HeaderEntry Name="Name" Value="Value" Visible="False" SortOrder="-1">
    <LabelStyle Font="Arial, 8pt, style=Bold" />
    </HeaderEntry>

<Header>
<Label Alignment="Center" Font="Tahoma, 7.5pt, style=Bold"></Label>

<Background ShadingEffectMode="Default"></Background>
</Header>

<HeaderLabel Alignment="Center" Font="Tahoma, 7.5pt, style=Bold"></HeaderLabel>

<HeaderBackground ShadingEffectMode="Default"></HeaderBackground>

<Line Color="Black"></Line>

<Shadow ExpandBy="2"></Shadow>
</LegendBox>
</ChartArea>

<TitleBox Position="Left">
<Header>
<Label Alignment="Center" Font="Tahoma, 7.5pt, style=Bold"></Label>

<Background ShadingEffectMode="Default"></Background>
</Header>

<HeaderLabel Alignment="Center" Font="Tahoma, 7.5pt, style=Bold"></HeaderLabel>

<HeaderBackground ShadingEffectMode="Default"></HeaderBackground>

<Label LineAlignment="Center"></Label>

<Shadow ExpandBy="2"></Shadow>
</TitleBox>

<DefaultShadow ExpandBy="2" Color="50, 50, 50, 50" Visible="False"></DefaultShadow>
        </dotnet:Chart>

         <dotnet:Chart id="Chart1" runat="server" BackColor="White" BubbleCenterStack="False" 
            BubbleStackShadeAsOne="False" CalculateEmptyElement="False" CleanupPeriod="50" 
            DataGridSeriesHeader="" PieLabelMode="Outside" SpacingPercentageNested="1">
<JS>
<Buttons>
<Background ShadingEffectMode="Default" Color="Gainsboro"></Background>

<Foreground Color="White"></Foreground>

<Outline Color="123, 123, 123"></Outline>

<IconStrokeLine Color="123, 123, 123"></IconStrokeLine>

<ForegroundHover Color="White"></ForegroundHover>

<OutlineHover Color="80, 80, 80"></OutlineHover>

<IconStrokeLineHover Color="108, 106, 133"></IconStrokeLineHover>
</Buttons>
</JS>

<Box DynamicSize="True" CornerBottomLeft="Cut" CornerBottomRight="Cut" 
            CornerTopRight="Round" DefaultCorner="Cut" Size="639, 479">
<Header>
<Label Alignment="Center" Font="Tahoma, 8.25pt, style=Bold"></Label>
    <Background ShadingEffectMode="Default" Visible="False" />
</Header>

<HeaderLabel Alignment="Center" Font="Tahoma, 8.25pt, style=Bold"></HeaderLabel>

    <HeaderBackground ShadingEffectMode="Default" Visible="False" />

<Background Color="" ShadingEffectMode="Default"></Background>
    <Shadow Depth="1" ExpandBy="2" Visible="False" />
</Box>

<SmartForecast Start="" Unit="None" TimeSpan="00:00:00"></SmartForecast>

<DefaultElement>
<DefaultSubValue Visible="False"></DefaultSubValue>
    <SmartLabel PieLabelMode="Outside">
    </SmartLabel>
</DefaultElement>

<DefaultLegendBox Padding="4" Visible="False">
<HeaderEntry Visible="False"></HeaderEntry>

<Header>
<Label Alignment="Center" Font="Tahoma, 7.5pt, style=Bold"></Label>

<Background ShadingEffectMode="Default"></Background>
</Header>

<HeaderLabel Alignment="Center" Font="Tahoma, 7.5pt, style=Bold"></HeaderLabel>

<HeaderBackground ShadingEffectMode="Default"></HeaderBackground>

<Shadow ExpandBy="2"></Shadow>
</DefaultLegendBox>

<DefaultTitleBox Visible="False">
<Header>
<Label Alignment="Center" Font="Tahoma, 7.5pt, style=Bold"></Label>

<Background ShadingEffectMode="Default"></Background>
</Header>

<HeaderLabel Alignment="Center" Font="Tahoma, 7.5pt, style=Bold"></HeaderLabel>

<HeaderBackground ShadingEffectMode="Default"></HeaderBackground>

<Shadow ExpandBy="2"></Shadow>
</DefaultTitleBox>

<ChartArea StartDateOfYear="" CornerTopLeft="Square">
<DefaultElement>
<DefaultSubValue Visible="True">
    <Line Length="4" />
    </DefaultSubValue>
</DefaultElement>

<YAxis>
<ScaleBreakLine Color="Gray"></ScaleBreakLine>

<TimeScaleLabels MaximumRangeRows="4"></TimeScaleLabels>

<MinorTimeIntervalAdvanced Start="" Unit="None" TimeSpan="00:00:00"></MinorTimeIntervalAdvanced>

<ZeroTick>
<Line Length="3"></Line>
</ZeroTick>

<DefaultTick>
<Line Length="3"></Line>

<Label Text="%Value"></Label>
</DefaultTick>

<TimeIntervalAdvanced Start="" Unit="Months" TimeSpan="00:00:00"></TimeIntervalAdvanced>

<AlternateGridBackground Color="100, 220, 220, 220"></AlternateGridBackground>

<Label Alignment="Center" Font="Tahoma, 9pt, style=Bold"></Label>
</YAxis>

<XAxis>
<ScaleBreakLine Color="Gray"></ScaleBreakLine>

<TimeScaleLabels MaximumRangeRows="4"></TimeScaleLabels>

<MinorTimeIntervalAdvanced Start="" Unit="None" TimeSpan="00:00:00"></MinorTimeIntervalAdvanced>

<ZeroTick>
<Line Length="3"></Line>
</ZeroTick>

<DefaultTick>
<Line Length="3"></Line>

<Label Text="%Value"></Label>
</DefaultTick>

<TimeIntervalAdvanced Start="" Unit="Months" TimeSpan="00:00:00"></TimeIntervalAdvanced>

<Label Alignment="Center" Font="Tahoma, 9pt, style=Bold"></Label>
</XAxis>

<Shadow ExpandBy="2" Depth="1" Visible="False"></Shadow>

<TitleBox Position="Left">
<Header>
<Label Alignment="Center" Font="Tahoma, 7.5pt, style=Bold"></Label>

<Background ShadingEffectMode="Default"></Background>
</Header>

<HeaderLabel Alignment="Center" Font="Tahoma, 7.5pt, style=Bold"></HeaderLabel>

<HeaderBackground ShadingEffectMode="Default"></HeaderBackground>

<Label LineAlignment="Center"></Label>

<Shadow ExpandBy="2"></Shadow>
</TitleBox>

<LegendBox Padding="4" CornerBottomRight="Cut" Orientation="TopRight">
<LabelStyle Font="Trebuchet MS, 8pt"></LabelStyle>

<DefaultEntry>
<LabelStyle Font="Trebuchet MS, 8pt"></LabelStyle>
</DefaultEntry>

<HeaderEntry Name="Name" Value="Value" Visible="False" SortOrder="-1">
    <LabelStyle Font="Arial, 8pt, style=Bold" />
    </HeaderEntry>

<Header>
<Label Alignment="Center" Font="Tahoma, 7.5pt, style=Bold"></Label>

<Background ShadingEffectMode="Default"></Background>
</Header>

<HeaderLabel Alignment="Center" Font="Tahoma, 7.5pt, style=Bold"></HeaderLabel>

<HeaderBackground ShadingEffectMode="Default"></HeaderBackground>

<Line Color="Black"></Line>

<Shadow ExpandBy="2"></Shadow>
</LegendBox>
</ChartArea>

<TitleBox Position="Left">
<Header>
<Label Alignment="Center" Font="Tahoma, 7.5pt, style=Bold"></Label>

<Background ShadingEffectMode="Default"></Background>
</Header>

<HeaderLabel Alignment="Center" Font="Tahoma, 7.5pt, style=Bold"></HeaderLabel>

<HeaderBackground ShadingEffectMode="Default"></HeaderBackground>

<Label LineAlignment="Center"></Label>

<Shadow ExpandBy="2"></Shadow>
</TitleBox>

<DefaultShadow ExpandBy="2" Color="50, 50, 50, 50" Visible="False"></DefaultShadow>
        </dotnet:Chart>


 <dotnet:Chart id="Chart2" runat="server" BackColor="White" BubbleCenterStack="False" 
            BubbleStackShadeAsOne="False" CalculateEmptyElement="False" CleanupPeriod="50" 
            DataGridSeriesHeader="" PieLabelMode="Outside" SpacingPercentageNested="1">
<JS>
<Buttons>
<Background ShadingEffectMode="Default" Color="Gainsboro"></Background>

<Foreground Color="White"></Foreground>

<Outline Color="123, 123, 123"></Outline>

<IconStrokeLine Color="123, 123, 123"></IconStrokeLine>

<ForegroundHover Color="White"></ForegroundHover>

<OutlineHover Color="80, 80, 80"></OutlineHover>

<IconStrokeLineHover Color="108, 106, 133"></IconStrokeLineHover>
</Buttons>
</JS>

<Box DynamicSize="True" CornerBottomLeft="Cut" CornerBottomRight="Cut" 
            CornerTopRight="Round" DefaultCorner="Cut" Size="639, 479">
<Header>
<Label Alignment="Center" Font="Tahoma, 8.25pt, style=Bold"></Label>
    <Background ShadingEffectMode="Default" Visible="False" />
</Header>

<HeaderLabel Alignment="Center" Font="Tahoma, 8.25pt, style=Bold"></HeaderLabel>

    <HeaderBackground ShadingEffectMode="Default" Visible="False" />

<Background Color="" ShadingEffectMode="Default"></Background>
    <Shadow Depth="1" ExpandBy="2" Visible="False" />
</Box>

<SmartForecast Start="" Unit="None" TimeSpan="00:00:00"></SmartForecast>

<DefaultElement>
<DefaultSubValue Visible="False"></DefaultSubValue>
    <SmartLabel PieLabelMode="Outside">
    </SmartLabel>
</DefaultElement>

<DefaultLegendBox Padding="4" Visible="False">
<HeaderEntry Visible="False"></HeaderEntry>

<Header>
<Label Alignment="Center" Font="Tahoma, 7.5pt, style=Bold"></Label>

<Background ShadingEffectMode="Default"></Background>
</Header>

<HeaderLabel Alignment="Center" Font="Tahoma, 7.5pt, style=Bold"></HeaderLabel>

<HeaderBackground ShadingEffectMode="Default"></HeaderBackground>

<Shadow ExpandBy="2"></Shadow>
</DefaultLegendBox>

<DefaultTitleBox Visible="False">
<Header>
<Label Alignment="Center" Font="Tahoma, 7.5pt, style=Bold"></Label>

<Background ShadingEffectMode="Default"></Background>
</Header>

<HeaderLabel Alignment="Center" Font="Tahoma, 7.5pt, style=Bold"></HeaderLabel>

<HeaderBackground ShadingEffectMode="Default"></HeaderBackground>

<Shadow ExpandBy="2"></Shadow>
</DefaultTitleBox>

<ChartArea StartDateOfYear="" CornerTopLeft="Square">
<DefaultElement>
<DefaultSubValue Visible="True">
    <Line Length="4" />
    </DefaultSubValue>
</DefaultElement>

<YAxis>
<ScaleBreakLine Color="Gray"></ScaleBreakLine>

<TimeScaleLabels MaximumRangeRows="4"></TimeScaleLabels>

<MinorTimeIntervalAdvanced Start="" Unit="None" TimeSpan="00:00:00"></MinorTimeIntervalAdvanced>

<ZeroTick>
<Line Length="3"></Line>
</ZeroTick>

<DefaultTick>
<Line Length="3"></Line>

<Label Text="%Value"></Label>
</DefaultTick>

<TimeIntervalAdvanced Start="" Unit="Months" TimeSpan="00:00:00"></TimeIntervalAdvanced>

<AlternateGridBackground Color="100, 220, 220, 220"></AlternateGridBackground>

<Label Alignment="Center" Font="Tahoma, 9pt, style=Bold"></Label>
</YAxis>

<XAxis>
<ScaleBreakLine Color="Gray"></ScaleBreakLine>

<TimeScaleLabels MaximumRangeRows="4"></TimeScaleLabels>

<MinorTimeIntervalAdvanced Start="" Unit="None" TimeSpan="00:00:00"></MinorTimeIntervalAdvanced>

<ZeroTick>
<Line Length="3"></Line>
</ZeroTick>

<DefaultTick>
<Line Length="3"></Line>

<Label Text="%Value"></Label>
</DefaultTick>

<TimeIntervalAdvanced Start="" Unit="Months" TimeSpan="00:00:00"></TimeIntervalAdvanced>

<Label Alignment="Center" Font="Tahoma, 9pt, style=Bold"></Label>
</XAxis>

<Shadow ExpandBy="2" Depth="1" Visible="False"></Shadow>

<TitleBox Position="Left">
<Header>
<Label Alignment="Center" Font="Tahoma, 7.5pt, style=Bold"></Label>

<Background ShadingEffectMode="Default"></Background>
</Header>

<HeaderLabel Alignment="Center" Font="Tahoma, 7.5pt, style=Bold"></HeaderLabel>

<HeaderBackground ShadingEffectMode="Default"></HeaderBackground>

<Label LineAlignment="Center"></Label>

<Shadow ExpandBy="2"></Shadow>
</TitleBox>

<LegendBox Padding="4" CornerBottomRight="Cut" Orientation="TopRight">
<LabelStyle Font="Trebuchet MS, 8pt"></LabelStyle>

<DefaultEntry>
<LabelStyle Font="Trebuchet MS, 8pt"></LabelStyle>
</DefaultEntry>

<HeaderEntry Name="Name" Value="Value" Visible="False" SortOrder="-1">
    <LabelStyle Font="Arial, 8pt, style=Bold" />
    </HeaderEntry>

<Header>
<Label Alignment="Center" Font="Tahoma, 7.5pt, style=Bold"></Label>

<Background ShadingEffectMode="Default"></Background>
</Header>

<HeaderLabel Alignment="Center" Font="Tahoma, 7.5pt, style=Bold"></HeaderLabel>

<HeaderBackground ShadingEffectMode="Default"></HeaderBackground>

<Line Color="Black"></Line>

<Shadow ExpandBy="2"></Shadow>
</LegendBox>
</ChartArea>

<TitleBox Position="Left">
<Header>
<Label Alignment="Center" Font="Tahoma, 7.5pt, style=Bold"></Label>

<Background ShadingEffectMode="Default"></Background>
</Header>

<HeaderLabel Alignment="Center" Font="Tahoma, 7.5pt, style=Bold"></HeaderLabel>

<HeaderBackground ShadingEffectMode="Default"></HeaderBackground>

<Label LineAlignment="Center"></Label>

<Shadow ExpandBy="2"></Shadow>
</TitleBox>

<DefaultShadow ExpandBy="2" Color="50, 50, 50, 50" Visible="False"></DefaultShadow>
        </dotnet:Chart>

    </div>
    </form>

</asp:Content>
