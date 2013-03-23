<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<ciMonitor.ViewModels.BuildOutcomesViewModel>" %>
<html>

    <head>
        <title>ciMonitor</title>
        <link href="<%= Url.Content("~/Content/Site.css") %>" rel="stylesheet" type="text/css" />
        <script src="<%= Url.Content("~/Scripts/jquery-1.4.1.min.js") %>" type="text/javascript"></script> 
        <script src="<%= Url.Content("~/Scripts/soundmanager2-nodebug-jsmin.js") %>" type="text/javascript"></script>

        <script type="text/javascript">
            var soundIds = ["BuildStarted", "SuccessfulBuild", "FailedBuild", "FixedBuild", "RepeatedlyFailingBuild"];
            var soundPath = '<%= Url.Content("~/Content/Sounds/") %>';
            soundManager.url = '<%= Url.Content("~/Content/SWF/") %>';
            soundManager.debugMode = false;
            soundManager.onready(function () {
                soundIds.map(function (soundId) {
                    soundManager.createSound({
                        id: soundId,
                        url: soundPath + soundId + '.mp3'
                    });
                });
            });

            $(document).ready(function () {
                var lastData = null;
                setInterval(function () {
                    $.ajax({
                        url: 'http://localhost:8080/api/json?format=json',
                        dataType: "json",
                        success: function (data) {
                            $(data.jobs).each(function () {
                                $('#builds ul').append("<li>" + this.name + "</li>");
                            });
                        }
                    });
                }, 3000);
            });   
        </script>
    </head>

    <body>
        <div id="header">
            <img src="http://www.esendex.co.uk/sites/all/themes/Esendex_v3/newheaderimages/esendex-logo.jpg" alt="" />
        </div>
        <div id="builds">
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

                <% foreach (var transition in Model.Transitions) { %>
                    soundManager.play('<%= transition %>');
                <% } %>
            </script>        
        </div>
    </body>
</html>

