<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<List<ciMonitor.ViewModels.BuildOutcome>>" %>
<html>

<head>
    <title>Build monitor test</title>
    <link href="../../Content/Site.css" rel="stylesheet" type="text/css" />
</head>

<body>

<ul>
<% foreach (var buildStatus in Model)
{
%>
    <li class="<%= buildStatus.Status %>"><%= buildStatus.Name %></ li>
<%
} %>
</ul>
</body>

</html>

