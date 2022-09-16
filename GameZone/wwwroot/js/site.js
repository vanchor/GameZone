function ReloadDevelopersList() {
    $.ajax({
        url: '@Url.Action("ReloadDevelopersList", "Company")', type: "GET",
        success: function (data) {
            $("#popUpsList").html(data);
        }
    })
}
