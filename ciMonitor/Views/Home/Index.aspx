<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
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
                        url: 'http://buildsrvr01/api/json?format=json&jsonp=?',
                        dataType: "jsonp",
                        success: function (data) {
                            $('#builds ul').html("");
                            $(data.jobs).each(function () {
                                $('#builds ul').append("<li class=\"" + this.color + "\">" + this.name + "</li>");
                            });
                            $('body').attr('class', overallStatus(data.jobs));
                        }
                    });
                }, 5000);
            });   
        </script>
    </head>

    <body>
        <div id="builds">
            <ul>
                <li>Contacting build server...</li>
            </ul>
        </div>

        <script type="text/javascript">
            
            function overallStatus(jobs) {
                var statuses = {};
                $(jobs).each(function () {
                    if (!statuses[this.color])
                        statuses[this.color] = 0;
                    statuses[this.color]++;
                });

                if (jobs.length > 0 && jobs.length == statuses["blue"])
                    return "blue";
                if (statuses["red"] > 0)
                    return "red";
                if (statuses["unknown"] > 0)
                    return "unknown";
                if (statuses["blue_anime"] > 0)
                    return "blue_anime";
                return "unknown";
            }

        </script>        
    </body>
</html>

