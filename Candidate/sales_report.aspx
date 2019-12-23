<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sales_report.aspx.cs" Inherits="simsir_short_report_2" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register TagPrefix="dotnet" Namespace="dotnetCHARTING" Assembly="dotnetCHARTING"%>
<%@ Import Namespace="System.Drawing" %>
<%@ Import Namespace="System.Drawing.Drawing2D" %>
<%@ Import Namespace="dotnetCHARTING.Mapping" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    

        <dotnet:Chart ID="Chart" runat="server" BackColor="White" BorderColor="Black" 
            BorderStyle="Solid" BorderWidth="20px" BubbleCenterStack="False" 
            BubbleStackShadeAsOne="False" CalculateEmptyElement="False" CleanupPeriod="50" 
            DataGridSeriesHeader="" PieLabelMode="Outside" SpacingPercentageNested="1">
            <JS>
                <Buttons>
                    <Background Color="Gainsboro" ShadingEffectMode="Default" />
                    <Foreground Color="White" />
                    <Outline Color="123, 123, 123" />
                    <IconStrokeLine Color="123, 123, 123" />
                    <ForegroundHover Color="White" />
                    <OutlineHover Color="80, 80, 80" />
                    <IconStrokeLineHover Color="108, 106, 133" />
                </Buttons>
            </JS>
            <Box CornerBottomLeft="Cut" CornerBottomRight="Cut" CornerTopRight="Round" 
                DefaultCorner="Cut" DynamicSize="True" Size="639, 479">
                <Header>
                    <Label Alignment="Center" Font="Tahoma, 8.25pt, style=Bold">
                    </Label>
                    <Background ShadingEffectMode="Default" Visible="False" />
                </Header>
                <HeaderLabel Alignment="Center" Font="Tahoma, 8.25pt, style=Bold">
                </HeaderLabel>
                <HeaderBackground ShadingEffectMode="Default" Visible="False" />
                <Background Color="" ShadingEffectMode="Default" />
                <Shadow Depth="1" ExpandBy="2" Visible="False" />
            </Box>
            <SmartForecast Start="" TimeSpan="00:00:00" Unit="None" />
            <DefaultElement>
                <DefaultSubValue Visible="False">
                </DefaultSubValue>
                <SmartLabel PieLabelMode="Outside">
                </SmartLabel>
            </DefaultElement>
            <DefaultLegendBox Padding="4" Visible="False">
                <HeaderEntry Visible="False">
                </HeaderEntry>
                <Header>
                    <Label Alignment="Center" Font="Tahoma, 7.5pt, style=Bold">
                    </Label>
                    <Background ShadingEffectMode="Default" />
                </Header>
                <HeaderLabel Alignment="Center" Font="Tahoma, 7.5pt, style=Bold">
                </HeaderLabel>
                <HeaderBackground ShadingEffectMode="Default" />
                <Shadow ExpandBy="2" />
            </DefaultLegendBox>
            <DefaultTitleBox Visible="False">
                <Header>
                    <Label Alignment="Center" Font="Tahoma, 7.5pt, style=Bold">
                    </Label>
                    <Background ShadingEffectMode="Default" />
                </Header>
                <HeaderLabel Alignment="Center" Font="Tahoma, 7.5pt, style=Bold">
                </HeaderLabel>
                <HeaderBackground ShadingEffectMode="Default" />
                <Shadow ExpandBy="2" />
            </DefaultTitleBox>
            <Background Color="Transparent" />
            <ChartArea CornerTopLeft="Square" StartDateOfYear="">
                <DefaultElement>
                    <DefaultSubValue Visible="True">
                        <Line Length="4" />
                    </DefaultSubValue>
                    <SmartLabel PieLabelMode="Outside">
                    </SmartLabel>
                </DefaultElement>
                <YAxis>
                    <ScaleBreakLine Color="Gray" />
                    <TimeScaleLabels MaximumRangeRows="4">
                    </TimeScaleLabels>
                    <MinorTimeIntervalAdvanced Start="" TimeSpan="00:00:00" Unit="None" />
                    <ZeroTick>
                        <Line Length="3" />
                    </ZeroTick>
                    <DefaultTick>
                        <Line Length="3" />
                        <Label Text="%Value">
                        </Label>
                    </DefaultTick>
                    <TimeIntervalAdvanced Start="" TimeSpan="00:00:00" Unit="Months" />
                    <AlternateGridBackground Color="100, 220, 220, 220" />
                    <Label Alignment="Center" Font="Tahoma, 9pt, style=Bold">
                    </Label>
                </YAxis>
                <XAxis>
                    <ScaleBreakLine Color="Gray" />
                    <TimeScaleLabels MaximumRangeRows="4">
                    </TimeScaleLabels>
                    <MinorTimeIntervalAdvanced Start="" TimeSpan="00:00:00" Unit="None" />
                    <ZeroTick>
                        <Line Length="3" />
                    </ZeroTick>
                    <DefaultTick>
                        <Line Length="3" />
                        <Label Text="%Value">
                        </Label>
                    </DefaultTick>
                    <TimeIntervalAdvanced Start="" TimeSpan="00:00:00" Unit="Months" />
                    <Label Alignment="Center" Font="Tahoma, 9pt, style=Bold">
                    </Label>
                </XAxis>
                <Shadow Depth="1" ExpandBy="2" Visible="False" />
                <TitleBox Position="Left">
                    <Header>
                        <Label Alignment="Center" Font="Tahoma, 7.5pt, style=Bold">
                        </Label>
                        <Background ShadingEffectMode="Default" />
                    </Header>
                    <HeaderLabel Alignment="Center" Font="Tahoma, 7.5pt, style=Bold">
                    </HeaderLabel>
                    <HeaderBackground ShadingEffectMode="Default" />
                    <Label LineAlignment="Center">
                    </Label>
                    <Shadow ExpandBy="2" />
                </TitleBox>
                <LegendBox CornerBottomRight="Cut" Orientation="TopRight" Padding="4">
                    <LabelStyle Font="Trebuchet MS, 8pt" />
                    <DefaultEntry>
                        <LabelStyle Font="Trebuchet MS, 8pt" />
                    </DefaultEntry>
                    <HeaderEntry Name="Name" SortOrder="-1" Value="Value" Visible="False">
                        <LabelStyle Font="Arial, 8pt, style=Bold" />
                    </HeaderEntry>
                    <Header>
                        <Label Alignment="Center" Font="Tahoma, 7.5pt, style=Bold">
                        </Label>
                        <Background ShadingEffectMode="Default" />
                    </Header>
                    <HeaderLabel Alignment="Center" Font="Tahoma, 7.5pt, style=Bold">
                    </HeaderLabel>
                    <HeaderBackground ShadingEffectMode="Default" />
                    <Line Color="Black" />
                    <Shadow ExpandBy="2" />
                </LegendBox>
            </ChartArea>
            <TitleBox Position="Left">
                <Header>
                    <Label Alignment="Center" Font="Tahoma, 7.5pt, style=Bold">
                    </Label>
                    <Background ShadingEffectMode="Default" />
                </Header>
                <HeaderLabel Alignment="Center" Font="Tahoma, 7.5pt, style=Bold">
                </HeaderLabel>
                <HeaderBackground ShadingEffectMode="Default" />
                <Label LineAlignment="Center">
                </Label>
                <Shadow ExpandBy="2" />
            </TitleBox>
            <DefaultShadow Color="50, 50, 50, 50" ExpandBy="2" Visible="False" />
        </dotnet:Chart>

    
        <br />
        <dotnet:Chart ID="Chart1" runat="server" BackColor="White" 
            BubbleCenterStack="False" BubbleStackShadeAsOne="False" 
            CalculateEmptyElement="False" CleanupPeriod="50" DataGridSeriesHeader="" 
            PieLabelMode="Outside" SpacingPercentageNested="1">
            <JS>
                <Buttons>
                    <Background Color="Gainsboro" ShadingEffectMode="Default" />
                    <Foreground Color="White" />
                    <Outline Color="123, 123, 123" />
                    <IconStrokeLine Color="123, 123, 123" />
                    <ForegroundHover Color="White" />
                    <OutlineHover Color="80, 80, 80" />
                    <IconStrokeLineHover Color="108, 106, 133" />
                </Buttons>
            </JS>
            <Box CornerBottomLeft="Cut" CornerBottomRight="Cut" CornerTopRight="Round" 
                DefaultCorner="Cut" DynamicSize="True" Size="639, 479">
                <Header>
                    <Label Alignment="Center" Font="Tahoma, 8.25pt, style=Bold">
                    </Label>
                    <Background ShadingEffectMode="Default" Visible="False" />
                </Header>
                <HeaderLabel Alignment="Center" Font="Tahoma, 8.25pt, style=Bold">
                </HeaderLabel>
                <HeaderBackground ShadingEffectMode="Default" Visible="False" />
                <Background Color="" ShadingEffectMode="Default" />
                <Shadow Depth="1" ExpandBy="2" Visible="False" />
            </Box>
            <SmartForecast Start="" TimeSpan="00:00:00" Unit="None" />
            <DefaultElement>
                <DefaultSubValue Visible="False">
                </DefaultSubValue>
                <SmartLabel PieLabelMode="Outside">
                </SmartLabel>
            </DefaultElement>
            <DefaultLegendBox Padding="4" Visible="False">
                <HeaderEntry Visible="False">
                </HeaderEntry>
                <Header>
                    <Label Alignment="Center" Font="Tahoma, 7.5pt, style=Bold">
                    </Label>
                    <Background ShadingEffectMode="Default" />
                </Header>
                <HeaderLabel Alignment="Center" Font="Tahoma, 7.5pt, style=Bold">
                </HeaderLabel>
                <HeaderBackground ShadingEffectMode="Default" />
                <Shadow ExpandBy="2" />
            </DefaultLegendBox>
            <DefaultTitleBox Visible="False">
                <Header>
                    <Label Alignment="Center" Font="Tahoma, 7.5pt, style=Bold">
                    </Label>
                    <Background ShadingEffectMode="Default" />
                </Header>
                <HeaderLabel Alignment="Center" Font="Tahoma, 7.5pt, style=Bold">
                </HeaderLabel>
                <HeaderBackground ShadingEffectMode="Default" />
                <Shadow ExpandBy="2" />
            </DefaultTitleBox>
            <ChartArea CornerTopLeft="Square" StartDateOfYear="">
                <DefaultElement>
                    <DefaultSubValue Visible="True">
                        <Line Length="4" />
                    </DefaultSubValue>
                </DefaultElement>
                <YAxis>
                    <ScaleBreakLine Color="Gray" />
                    <TimeScaleLabels MaximumRangeRows="4">
                    </TimeScaleLabels>
                    <MinorTimeIntervalAdvanced Start="" TimeSpan="00:00:00" Unit="None" />
                    <ZeroTick>
                        <Line Length="3" />
                    </ZeroTick>
                    <DefaultTick>
                        <Line Length="3" />
                        <Label Text="%Value">
                        </Label>
                    </DefaultTick>
                    <TimeIntervalAdvanced Start="" TimeSpan="00:00:00" Unit="Months" />
                    <AlternateGridBackground Color="100, 220, 220, 220" />
                    <Label Alignment="Center" Font="Tahoma, 9pt, style=Bold">
                    </Label>
                </YAxis>
                <XAxis>
                    <ScaleBreakLine Color="Gray" />
                    <TimeScaleLabels MaximumRangeRows="4">
                    </TimeScaleLabels>
                    <MinorTimeIntervalAdvanced Start="" TimeSpan="00:00:00" Unit="None" />
                    <ZeroTick>
                        <Line Length="3" />
                    </ZeroTick>
                    <DefaultTick>
                        <Line Length="3" />
                        <Label Text="%Value">
                        </Label>
                    </DefaultTick>
                    <TimeIntervalAdvanced Start="" TimeSpan="00:00:00" Unit="Months" />
                    <Label Alignment="Center" Font="Tahoma, 9pt, style=Bold">
                    </Label>
                </XAxis>
                <Shadow Depth="1" ExpandBy="2" Visible="False" />
                <TitleBox Position="Left">
                    <Header>
                        <Label Alignment="Center" Font="Tahoma, 7.5pt, style=Bold">
                        </Label>
                        <Background ShadingEffectMode="Default" />
                    </Header>
                    <HeaderLabel Alignment="Center" Font="Tahoma, 7.5pt, style=Bold">
                    </HeaderLabel>
                    <HeaderBackground ShadingEffectMode="Default" />
                    <Label LineAlignment="Center">
                    </Label>
                    <Shadow ExpandBy="2" />
                </TitleBox>
                <LegendBox CornerBottomRight="Cut" Orientation="TopRight" Padding="4">
                    <LabelStyle Font="Trebuchet MS, 8pt" />
                    <DefaultEntry>
                        <LabelStyle Font="Trebuchet MS, 8pt" />
                    </DefaultEntry>
                    <HeaderEntry Name="Name" SortOrder="-1" Value="Value" Visible="False">
                        <LabelStyle Font="Arial, 8pt, style=Bold" />
                    </HeaderEntry>
                    <Header>
                        <Label Alignment="Center" Font="Tahoma, 7.5pt, style=Bold">
                        </Label>
                        <Background ShadingEffectMode="Default" />
                    </Header>
                    <HeaderLabel Alignment="Center" Font="Tahoma, 7.5pt, style=Bold">
                    </HeaderLabel>
                    <HeaderBackground ShadingEffectMode="Default" />
                    <Line Color="Black" />
                    <Shadow ExpandBy="2" />
                </LegendBox>
            </ChartArea>
            <TitleBox Position="Left">
                <Header>
                    <Label Alignment="Center" Font="Tahoma, 7.5pt, style=Bold">
                    </Label>
                    <Background ShadingEffectMode="Default" />
                </Header>
                <HeaderLabel Alignment="Center" Font="Tahoma, 7.5pt, style=Bold">
                </HeaderLabel>
                <HeaderBackground ShadingEffectMode="Default" />
                <Label LineAlignment="Center">
                </Label>
                <Shadow ExpandBy="2" />
            </TitleBox>
            <DefaultShadow Color="50, 50, 50, 50" ExpandBy="2" Visible="False" />
        </dotnet:Chart>

    
    </div>
    </form>
</body>
</html>
