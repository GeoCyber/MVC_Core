function GetMultipleFormValue(rowcounts) {

    var data = [];

    for (var i = 0; i < $("#TotalUnit").val(); i++) {


        var assidname = "#AssetSubCode" + i;
        var regisidname = "#RegistrationNumber" + i;
        var seriaidname = "#SerialNumber" + i;
        var deparidname = "#Department" + i;
        var locidname = "#Location" + i;
        var unitidname = "#UnitPrice" + i;
        var allocidname = "#AllocationValue" + i;

        var asssubcode = $(assidname).val();
        var registrationno = $(regisidname).val();
        var serialno = $(seriaidname).val();
        var departmnt = $(deparidname).val();
        var location = $(locidname).val();
        var unitprc = $(unitidname).val();
        var allocationval = $(allocidname).val();

        data.push({ "asssubcode": asssubcode, "registrationno": registrationno, "serialno": serialno, "departmnt": departmnt, "location": location, "unitprc": unitprc, "allocationval": allocationval });


    }
    return data;
}



function Submit(id) {

    $("#CreationStatus").val(id);

    if (id == 1) $('input[type=text]').removeAttr('required');

    $("#myform").submit();


}

function validate() {

    var checked = true;
    var AName = $("#AssetName").val();
    var AType = $("#AssetType").val();
    var ACategory = $("#AssetCategory").val();
    var Totalunt = $("#TotalUnit").val();
    var untprice = $("#UnitPrice").val();
    var statDate = $("#DepreciationStartDate").val();
  
    if (AName == "" || AName == undefined) {
        checked = false;
    }
    if (AType == "" || AType == undefined) {
        checked = false;
    }
    if (ACategory == "" || ACategory == undefined) {
        checked = false;
    }
    if (Totalunt == "" || Totalunt == undefined) {
        checked = false;
    }
    if (untprice == "" || untprice == undefined) {
        checked = false;
    }
    if (statDate == "" || statDate == undefined) {
        checked = false;
    }

    return checked;

}


function FormSubmit(event) {
   
    var data = new FormData(document.getElementById("myform"));
    var check = validate();
    if (check) {
        var data1 = GetMultipleFormValue();
        data.append("MultiValuesForm", JSON.stringify(data1));
       
        $.ajax({
            url: "Create",
            type: "POST",
            contentType: false,
            processData: false,
            data: data,
            success: function (res) {

                window.location.href = "/Master_AllModule/Index";
            }
        });

    }
   
    event.preventDefault();

}
