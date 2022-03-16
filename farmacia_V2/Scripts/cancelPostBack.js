var controlId = "";
var ReadFingerPrint = false;
var elemntsToBreak = ["Page", "Selector", "btnRefresh", "btnCancelar", "Selector"];
var computedRole = "";
var _activeElement = "";
var msg = "";

class MyRegExp extends RegExp {
    [Symbol.split](str, limit) {
        var result = RegExp.prototype[Symbol.split].call(this, str, limit);
        return result.map(x => "(" + x + ")");
    }
}

function aContainsB(a, b) {
    return a.indexOf(b) >= 0;
}


function validateEventContainsControlId(Event, controlId) {
    if (aContainsB(Event, controlId)) {
        return true;
    } else {
        return false;
    }
}


debugger;
Sys.Application.add_load(ApplicationLoadHandler);
Sys.Application.add_init(appl_init);



function beforeAsyncPostBack(controlId) {


}

function afterAsyncPostBack(sender, e) {
    debugger;
    //if (sender._postBackSettings.sourceElement.computedRole !== "button") {

    //    if (sender._postBackSettings.panelsToUpdate !== null)
    //    {

    //     //ShowGridViewColumn(7, false);
    //    }
    //}
    // checkStatePatient();

}

function ApplicationLoadHandler(sender, args) {
    debugger;
    Sys.WebForms.PageRequestManager.getInstance().remove_initializeRequest(BeginRequest);
    Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(BeginRequest);

}

function getRowClicked() {
    var rows = document.getElementById('MasterPageHolder_gvCitasPacientes').getElementsByTagName('tbody')[0].getElementsByTagName('tr');
    //var rows = $(this).closest("tr");
    var este = $("td", rows).eq(3)[0].innerText;

}


function getFinalElement(control, controlType) {
    if (controlType === "link") {
        var o = control.match(/\((.*)\)/i);
        var re = new MyRegExp(',');
        var str = o[1];
        var result = str.split(re);
    }
    else {
        result = control;
    }

    return result;
}


function getElementDetail(result) {
    var myResult = result[0].match(/\((.*)\)/i);
    var temp_controlId = myResult[1].match(/\'(.*)\'/i);
    var IdControl = temp_controlId[1];

    return IdControl;
}

function breakByElement(control, controlType, elementType) {
    var result = getFinalElement(control, controlType);
    var found = false;
    var posElement;
    var element;
    var elFound;

    if (Object.prototype.toString.call(result) === '[object Array]') {

        for (i = 0; i < result.length; i++) {
            for (x = 0; x < elementType.length; x++) {
                posElement = elementType[x];
                element = posElement;
                elFound = result[i];
                if (validateEventContainsControlId(elFound, element)) {
                    found = true;
                    break;
                }
            }
        }
    }

    else {
        for (x = 0; x < elementType.length; x++) {
            posElement = elementType[x];
            element = posElement;
            if (validateEventContainsControlId(result, element)) {
                found = true;
                break;
            }
        }
    }

    return found;
}


function getActiveElement(prm, element) {
    var control;
    if (element === "link")
        control = prm._postBackSettings.asyncTarget;
    if (element === "button")
        control = prm._postBackSettings.sourceElement.id;

    return control;
}

function getTypeOfElement(element, typeElement) {

    var elementType = "";

    if (typeElement === "image" || typeElement === "INPUT") {
        if (validateEventContainsControlId(element, "btn"))
            elementType = "button";
    }
    if (typeElement === "TABLE") {
        elementType = "link";
    }

    return elementType;
}


function stopPostBackCallCompare(prm1, prm2, args)
{

    if (prm1.get_isInAsyncPostBack() || prm2._postBackSettings.async) {

        if (validateEventContainsControlId(args.get_postBackElement().id, "GV_pacientes")) {
            var actElement = getActiveElement(prm2, _activeElement);
            var element = getFinalElement(actElement, _activeElement);
            controlId = getElementDetail(element);
            ReadFingerPrint = true;
        }

        if (validateEventContainsControlId(args.get_postBackElement().id, "btn_end")
            ||
            validateEventContainsControlId(args.get_postBackElement().id, "btn_init"))
        {

            controlId = args.get_postBackElement().id;
            ReadFingerPrint = true;
        }


        if (ReadFingerPrint === true) {

            $("[id$=controlID]").val(controlId);
            prm1.abortPostBack();
            args.set_cancel(true);
            createModal();
        }

    }
}

function BeginRequest(sender, args) {

    debugger;

    var prm1 = Sys.WebForms.PageRequestManager.getInstance();
    var prm2 = Sys.WebForms.PageRequestManager.getInstance();
    msg = "";


    if (prm2._postBackSettings.sourceElement.type === "text" || prm2._postBackSettings.sourceElement.type === "checkbox" || prm2._postBackSettings.sourceElement.type === "image" || prm2._postBackSettings.sourceElement.type === "submit") {

        if (!validateEventContainsControlId(args.get_postBackElement().id, "btn"))
            return;
    }

    if (document.getElementById(document.activeElement.id).getAttribute('href') !== null)
        type = "TABLE";
    else {
        type = $(args).prev().prevObject[0]._postBackElement.nodeName;

    }

    _activeElement = getTypeOfElement(args.get_postBackElement().id, type);

    if (_activeElement === "link" || _activeElement === "button") {
        var event = getActiveElement(prm2, _activeElement);

        var needBreak = breakByElement(event, _activeElement, elemntsToBreak);
        if (needBreak === true) {
            return;
        }
    }

    if (onEvaluateExclude() === true) {
        return;
    }

    $('[id$="MensajeError"]').css("display", "none");
    $('[id$="MensajeError"]').attr("style", "display:none");

    var data = sessionStorage.getItem('pacstate');
    var rs = JSON.parse(data);


    if (rs === null && validateEventContainsControlId(prm2._postBackSettings.sourceElement.id, "btn_end") === true  )
    {
        msg = "Debe iniciar Atencion";
        getAlertCircuit(msg);
        prm1.abortPostBack();
        args.set_cancel(true);
        return;
    }
        


    if (rs !== null && rs.step === "in" && validateEventContainsControlId(prm2._postBackSettings.sourceElement.id, "btn_end") === true)
    {
        if (rs.nhc !== $('[id$="txt_asi"]')[1].value) {
            getAlertCircuit();
            prm1.abortPostBack();
            args.set_cancel(true);
            return;
        }

        stopPostBackCallCompare(prm1, prm2, args);
    }

    else if (rs !== null &&
                            (validateEventContainsControlId(prm2._postBackSettings.sourceElement.id, "btn_end") === true
                            ||
                             validateEventContainsControlId(prm2._postBackSettings.sourceElement.id, "btn_init") === true ))
    {
        if (rs.nhc !== $('[id$="txt_asi"]')[1].value) {
            getAlertCircuit();
            prm1.abortPostBack();
            args.set_cancel(true);
            return;
        }

        stopPostBackCallCompare(prm1, prm2, args);

    }

    else if (rs === null &&
                            (validateEventContainsControlId(prm2._postBackSettings.sourceElement.id, "btn_end") === true
                            ||
                             validateEventContainsControlId(prm2._postBackSettings.sourceElement.id, "btn_init") === true))
        {
        stopPostBackCallCompare(prm1, prm2, args);

    }


    else if (rs === null && validateEventContainsControlId(prm2._postBackSettings.sourceElement.id, "btn_end") === false
                         && validateEventContainsControlId(prm2._postBackSettings.sourceElement.id, "btn_init") === false) 
    {
        msg = "Debe iniciar Atencion";
        getAlertCircuit(msg);
        prm1.abortPostBack();
        args.set_cancel(true);
        return;
        //stopPostBackCallCompare(prm1, prm2, args);
    }

    else if (rs !== null && validateEventContainsControlId(prm2._postBackSettings.sourceElement.id, "btn_init") === false
                         && validateEventContainsControlId(prm2._postBackSettings.sourceElement.id, "btn_end" ) === false) {
        if (rs.nhc !== $('[id$="txt_asi"]')[1].value) {
            getAlertCircuit(msg);
            prm1.abortPostBack();
            args.set_cancel(true);
            return;
        }
        //stopPostBackCallCompare(prm1, prm2, args);
    }

}

function appl_init(sender, args) {
    debugger;

    loadPatientState();
    var prm = Sys.WebForms.PageRequestManager.getInstance();



    prm.add_beginRequest(function (sender, args) {

        debugger;

        var pgRegMgr = Sys.WebForms.PageRequestManager.getInstance();
        //Event raised when the Async Postback is started.
        //sender.abortPostBack();
        // args.set_cancel(true);

        //Detect whether the request is Async
        var isAsync = sender._postBackSettings.async;

        //Detect Id of the control that caused the postback.
        controlId = sender._postBackSettings.sourceElement.id;


        //Id of the updatepanel that caused the postback
        // var updatePanelId = sender._postBackSettings.panelID.split('|')[0];

        BeginHandler(controlId);
    });

    prm.add_endRequest(EndHandler);
}

function BeginHandler(controlId) {
    debugger;

    beforeAsyncPostBack(controlId);
}

function EndHandler(sender, e) {
    debugger;

    afterAsyncPostBack(sender, e);
}



