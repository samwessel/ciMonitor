<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Exception>" %>
<%@ Import Namespace="ciMonitor" %>

<ul>
    <li class="<%= Status.Fail() %>">
        Error contacting server<br />
        <span class="buildNumber"><%= Model.Message%></span>
    </ li>
</ul>

<img src="<%= Url.Content("~/Content/DoubleFacePalm.jpg") %>" alt="Double facepalm" />
<div style="display:none"><%= Model.StackTrace %></div>

<script type="text/javascript">
    $(function () {
        $('body').attr('class', '<%= Status.Unknown() %>');
    });
</script>