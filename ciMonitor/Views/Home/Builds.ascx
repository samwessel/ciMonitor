<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ciMonitor.ViewModels.BuildOutcomesViewModel>" %>

<span style="display:none"><%= DateTime.Now %></span>
<ul>
    <% foreach (var buildStatus in Model.BuildOutcomes) { %>
        <li class="<%= buildStatus.Status %>">
            <%= buildStatus.Name %>
            <span class="buildNumber">#<%= buildStatus.BuildNumber %></span>
        </ li>
    <% } %>
</ul>

<script type="text/javascript">
    $(function () {
        $('body').attr('class', '<%= Model.OverallStatus %>');
    });
</script>