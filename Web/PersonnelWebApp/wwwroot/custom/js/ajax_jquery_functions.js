var companyId;
var selectedCompanyId;
var pageNumber;
var totalPages;

$("#selectCompany").on("change", function () {
       selectedCompanyId = $(this).val();

       $("#show-all").val(selectedCompanyId);

       $("#inputCompanyIdAgreement").val(selectedCompanyId);

      /* console.log($("#inputCompanyIdAgreement").val());*/

	/*********************************** */
	let isANumber = Number.isInteger(parseInt(selectedCompanyId));

	if (isANumber) {
		pageNumber = 1;
		companyId = selectedCompanyId;

		getEmployees();
	}
	else {
		alert("Pleace,select a company from the list !");
	}

	$(this).trigger("blur");
});

$(".previous-page").on("click", function () {
	if (pageNumber > 1) {
		pageNumber = pageNumber - 1;
	}

	getEmployees();

	$(this).trigger("blur");
});

$(".next-page").on("click", function () {
	if (pageNumber < totalPages) {
		pageNumber = pageNumber + 1;
	}

	getEmployees();

	$(this).trigger("blur");
});

$(".emp-paging").on("keypress", empPagingPressEnter);

function empPagingPressEnter(event) {
	if (event.which == 13) {

		let index = $(this).val();
		let indexNumber = parseInt(index);
		let indexIsANumber = Number.isInteger(indexNumber);

		let totalPagesNumber = parseInt(totalPages);
		let isANumber = Number.isInteger(totalPagesNumber);

		if (isANumber && totalPagesNumber > 0) {
			if (indexIsANumber && indexNumber > 0 && indexNumber <= totalPagesNumber) {
				pageNumber = index;

				getEmployees();
			}
		}
	}
}

function getEmployees() {
	var token = $('input[name="__RequestVerificationToken"]').val();

	$.ajax({
		type: "POST",
		url: "/Employees/Index",
		dataType: "json",
		contentType: "application/x-www-form-urlencoded; charset=UTF-8",
		data: {
			__RequestVerificationToken: token,
			"companyId": companyId,
			"pageNumber": pageNumber
		},
		success: function (response) {
			setNavigationButtons(response);
			setFormFields(response["itemsCollection"]);
		},
		error: function (response, status, error) {
			alert("Error: " + error + " ;;; status: "  + status + " ;;; response: " + response.responseText);
		}
	});
}

function setNavigationButtons(data) {
	pageNumber = data["pageIndex"];
	totalPages = data["totalPages"];

	var prevDisabled = !data["hasPreviousPage"] ? "disabled" : "";
	var nextDisabled = !data["hasNextPage"] ? "disabled" : "";

	var previousBtnBaseClass = "previous-page btn btn-outline-primary ";
	var nextBtnBaseClass = "next-page btn btn-outline-primary ";

	$("#totalPages").text("/ " + data["totalPages"]);

	$(".previous-page")
		.attr("class",previousBtnBaseClass + prevDisabled);
	$(".next-page")
		.attr("class", nextBtnBaseClass + nextDisabled);
	$(".emp-paging").val(data["pageIndex"]);
}

function setFormFields(items) {
	var empId = items[0]["id"];

       $("#inputEmployeeIdAgreement").val(empId);

       console.log($("#inputEmployeeIdAgreement").val());

	if (empId == 0) {
		$("#isPresent").val("");
	}
	else {
		$("#isPresent").val(items[0]["isPresent"]);
              $("#edit-entity-id").val(empId);
              $("#delete-entity-id").val(empId);
       }

	$("#numberFromTheList").val(items[0]["numberFromTheList"]);

	setPersonFields(items[0]["person"],empId);

	setConnectionFields(items[0]["contactInfo"]);

       $("#cardPassport").val(items[0]["idCardPassport"]["documentName"]);

	$("#cardPassportNumber").val(items[0]["idCardPassport"]["documentNumber"]);

	setContractFields(items[0]["contractInfo"]);
}

function setPersonFields(person,empId) {
	if (empId == 0) {
		$("#fullName").val("");
	}
	else {
		$("#fullName").val(person["fullName"]);
	}
	$("#gender").val(person["genderType"]);
	$("#egn").val(person["egn"]);
	$("#empPhoto").attr("src", person["photoFilePath"]);

	$("#permanentAddress").val(person["permanentAddress"]);
	$("#currentAddress").val(person["currentAddress"]);
}

function setConnectionFields(items) {
	$("#phone-one").val(items["phoneNumberOne"]);
	$("#phone-two").val(items["phoneNumberTwo"]);
	$("#email-one").val(items["e_MailAddress1"]);
	$("#web-site").val(items["website"]);
}

function setContractFields(contract) {
       $("#jobTitle").val(contract["currentJobTitle"]);
       $("#departmentName").val(contract["currentDepartmentName"]);
	$("#contractType").val(contract["contractType"]);
	$("#contractNumber").val(contract["contractNumber"]);
       $("#contractDate").val(contract["contractDate"]);
}
/*************************************************************************/
/*************************************************************************/
