



function onEvaluateExclude()
{

    debugger;
    var resp = "";
    var excluded = "";

    var actdata = sessionStorage.getItem('excludedPac');
    if (actdata !== null)
    {
		sessionStorage.removeItem('excludedPac');
        var rs = JSON.parse(actdata);
        if (rs.excluded === true) {
            return true;
        }
        else {
            return false;
        }

    }


    $.ajaxSetup({ async: false });


    $.when(onExludePatient()).done(function (response)
    {
        debugger;

        resp = JSON.parse(response.d);
        if (resp.DetalleError === "not excluded") {
            excluded = false;
        }
        else if (resp.DetalleError === "excluded") {
            excluded = true;
        }

        $.ajaxSetup({ async: true });
    });   


    if (actdata === null) {

        var data = {
            nhc: $("input[name*='lblnhc']").val(),
            excluded: excluded
        };

        var excludePatient = JSON.stringify(data);
        sessionStorage.setItem('excludedPac', excludePatient);
    }

    return excluded;
}

