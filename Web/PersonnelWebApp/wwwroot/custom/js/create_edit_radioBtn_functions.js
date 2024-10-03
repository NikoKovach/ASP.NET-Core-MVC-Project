
$(".radio-create").on("click", onRadioButtonClick);

$(".radio-edit").on("click", onRadioButtonClick);

function onRadioButtonClick() {
	var radioButtonValue = $(this).val();
	$("#create-edit-emp-btn").text(radioButtonValue);

	var controller = $("#controller-name").val();
	var actionName = "";

	if (radioButtonValue === "Create") {
		actionName = "CreateEmployee";
	}
	else if (radioButtonValue === "Edit") {
		actionName = "EditEmployee";
	}

	var empRoute = '/' + controller+'/' + actionName;

	$("#create-edit-emp-btn").attr("formaction", empRoute);
};

$(function () {
	var currentEmpId = $("#emp-id").val();
	var numberId = parseInt(currentEmpId);
	
	if (numberId > 0) {
		$(".radio-edit").trigger("click");
	}
});


//input.removeAttr( "title" )
//$(".radio - create").attr("checked", false);
//$(".radio - create").prop("checked", false);
/*$(".radio-edit").prop("checked", true);*/
/*$(".radio-edit").attr("checked","");*/
//var radioBtnEditVal = $(".radio-edit").val();