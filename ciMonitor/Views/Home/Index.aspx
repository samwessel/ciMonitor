<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<ciMonitor.ViewModels.BuildOutcomesViewModel>" %>
<html>

<head>
    <title>ciMonitor</title>
    <link href="../../Content/Site.css" rel="stylesheet" type="text/css" />
</head>

<body class="<%= Model.OverallStatus %>">

<ul>
<% foreach (var buildStatus in Model.BuildOutcomes)
{
%>
    <li class="<%= buildStatus.Status %>"><%= buildStatus.Name %></ li>
<%
} %>
</ul>
</body>

</html>

