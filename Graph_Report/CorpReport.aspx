<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CorpReport.aspx.cs" Inherits="Corptest_CorpReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="dotnet" Namespace="dotnetCHARTING" Assembly="dotnetCHARTING" %>
<%@ Import Namespace="System.Drawing" %>
<%@ Import Namespace="System.Drawing.Drawing2D" %>
<%@ Import Namespace="dotnetCHARTING.Mapping" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Dheya Career Mentors</title>
    <link href="../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="../images/favicons.ico" type="image/x-icon" />
</head>
<body>
    <form id="form1" runat="server">
    <br />
    <br />
    <div id="div_msg" runat="server" class="alert " style="text-align: center;">
    </div>
    <div id="divchart" runat="server">
        <dotnet:Chart ID="Chart1" runat="server" BackColor="White" BubbleCenterStack="False"
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
            <Box DynamicSize="True" CornerBottomLeft="Cut" CornerBottomRight="Cut" CornerTopRight="Round"
                DefaultCorner="Cut" Size="639, 479">
                <Header>
                    <Label Alignment="Center" Font="Tahoma, 8.25pt, style=Bold">
                    </Label>
                    <Background ShadingEffectMode="Default" Visible="False" />
                </Header>
                <HeaderLabel Alignment="Center" Font="Tahoma, 8.25pt, style=Bold">
                </HeaderLabel>
                <HeaderBackground ShadingEffectMode="Default" Visible="False" />
                <Background Color="" ShadingEffectMode="Default"></Background>
                <Shadow Depth="1" ExpandBy="2" Visible="False" />
            </Box>
            <SmartForecast Start="" Unit="None" TimeSpan="00:00:00"></SmartForecast>
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
                    <Background ShadingEffectMode="Default"></Background>
                </Header>
                <HeaderLabel Alignment="Center" Font="Tahoma, 7.5pt, style=Bold">
                </HeaderLabel>
                <HeaderBackground ShadingEffectMode="Default"></HeaderBackground>
                <Shadow ExpandBy="2"></Shadow>
            </DefaultLegendBox>
            <DefaultTitleBox Visible="False">
                <Header>
                    <Label Alignment="Center" Font="Tahoma, 7.5pt, style=Bold">
                    </Label>
                    <Background ShadingEffectMode="Default"></Background>
                </Header>
                <HeaderLabel Alignment="Center" Font="Tahoma, 7.5pt, style=Bold">
                </HeaderLabel>
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
                    <TimeScaleLabels MaximumRangeRows="4">
                    </TimeScaleLabels>
                    <MinorTimeIntervalAdvanced Start="" Unit="None" TimeSpan="00:00:00"></MinorTimeIntervalAdvanced>
                    <ZeroTick>
                        <Line Length="3"></Line>
                    </ZeroTick>
                    <DefaultTick>
                        <Line Length="3"></Line>
                        <Label Text="%Value">
                        </Label>
                    </DefaultTick>
                    <TimeIntervalAdvanced Start="" Unit="Months" TimeSpan="00:00:00"></TimeIntervalAdvanced>
                    <AlternateGridBackground Color="100, 220, 220, 220"></AlternateGridBackground>
                    <Label Alignment="Center" Font="Tahoma, 9pt, style=Bold">
                    </Label>
                </YAxis>
                <XAxis>
                    <ScaleBreakLine Color="Gray"></ScaleBreakLine>
                    <TimeScaleLabels MaximumRangeRows="4">
                    </TimeScaleLabels>
                    <MinorTimeIntervalAdvanced Start="" Unit="None" TimeSpan="00:00:00"></MinorTimeIntervalAdvanced>
                    <ZeroTick>
                        <Line Length="3"></Line>
                    </ZeroTick>
                    <DefaultTick>
                        <Line Length="3"></Line>
                        <Label Text="%Value">
                        </Label>
                    </DefaultTick>
                    <TimeIntervalAdvanced Start="" Unit="Months" TimeSpan="00:00:00"></TimeIntervalAdvanced>
                    <Label Alignment="Center" Font="Tahoma, 9pt, style=Bold">
                    </Label>
                </XAxis>
                <Shadow ExpandBy="2" Depth="1" Visible="False"></Shadow>
                <TitleBox Position="Left">
                    <Header>
                        <Label Alignment="Center" Font="Tahoma, 7.5pt, style=Bold">
                        </Label>
                        <Background ShadingEffectMode="Default"></Background>
                    </Header>
                    <HeaderLabel Alignment="Center" Font="Tahoma, 7.5pt, style=Bold">
                    </HeaderLabel>
                    <HeaderBackground ShadingEffectMode="Default"></HeaderBackground>
                    <Label LineAlignment="Center">
                    </Label>
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
                        <Label Alignment="Center" Font="Tahoma, 7.5pt, style=Bold">
                        </Label>
                        <Background ShadingEffectMode="Default"></Background>
                    </Header>
                    <HeaderLabel Alignment="Center" Font="Tahoma, 7.5pt, style=Bold">
                    </HeaderLabel>
                    <HeaderBackground ShadingEffectMode="Default"></HeaderBackground>
                    <Line Color="Black"></Line>
                    <Shadow ExpandBy="2"></Shadow>
                </LegendBox>
            </ChartArea>
            <TitleBox Position="Left">
                <Header>
                    <Label Alignment="Center" Font="Tahoma, 7.5pt, style=Bold">
                    </Label>
                    <Background ShadingEffectMode="Default"></Background>
                </Header>
                <HeaderLabel Alignment="Center" Font="Tahoma, 7.5pt, style=Bold">
                </HeaderLabel>
                <HeaderBackground ShadingEffectMode="Default"></HeaderBackground>
                <Label LineAlignment="Center">
                </Label>
                <Shadow ExpandBy="2"></Shadow>
            </TitleBox>
            <DefaultShadow ExpandBy="2" Color="50, 50, 50, 50" Visible="False"></DefaultShadow>
        </dotnet:Chart>
    </div>
    </form>
</body>
</html>
