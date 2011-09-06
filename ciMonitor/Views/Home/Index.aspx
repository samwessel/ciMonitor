<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<ciMonitor.ViewModels.BuildOutcomesViewModel>" %>
<html>

<head>
    <title>ciMonitor</title>
    <link href="<%= Url.Content("~/Content/Site.css") %>" rel="stylesheet" type="text/css" />
    <script src="<%= Url.Content("~/Scripts/jquery-1.4.1.min.js") %>" type="text/javascript"></script> 

    <script type="text/javascript">
        function refreshBuildStatuses() {
            $.ajax({
                url: '/Home/Builds',
                cache: false,
                success: function (data) {
                    $('#builds').html(data);
                }
            });
        }

        $(document).ready(function() {
            setInterval(refreshBuildStatuses, 3000);
        });   
    </script>
</head>

<body>
    <div id="header">
        <img src="http://www.esendex.co.uk/sites/all/themes/Esendex_v3/newheaderimages/esendex-logo.jpg" />
    </div>
    <div id="builds">
        <% Html.RenderPartial("Builds", Model); %>
    </div>
</body>

</html>

