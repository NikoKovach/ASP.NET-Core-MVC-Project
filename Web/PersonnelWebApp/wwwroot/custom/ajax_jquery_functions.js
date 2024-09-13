var companyId;
var selectedCompanyId;
var pageNumber;
var totalPages;

$("#selectCompany").on("change", function () {
	$("#show-all").val($(this).val());
	/*********************************** */
	selectedCompanyId = $(this).val();

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
			setNavigationButtins(response);
			setFormFields(response["itemsCollection"]);
		},
		error: function (response, status, error) {
			alert("Error: " + error + " ;;; status: "  + status + " ;;; response: " + response.responseText);
		}
	});
}

function setNavigationButtins(data) {
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
/*
//let stringDate = contract["contractDate"];
	//if (stringDate== null) {
	//	$("#contractDate").val("");
	//}
	//else {
	//	var tempDate = new Date(contract["contractDate"]);

	//	$("#contractDate")
	//		.val(tempDate.toLocaleDateString('bg-BG', { dateStyle: 'short' }));
	//}

{
    "pageIndex": 1,
    "totalPages": 2,
    "hasPreviousPage": false,
    "hasNextPage": true,
    "itemsCollection": [
        {
            "id": 3,
            "companyId": 10,
            "numberFromTheList": "2",
            "isPresent": true,
            "person": {
                "id": 7,
                "fullName": "Asena Y Koleva",
                "genderId": 7,
                "genderType": "woman",
                "egn": "222222",
                "photoFilePath": "/app-folder/\u0411\u0443\u043B\u0433\u0430\u0440\u0441\u0442\u0440\u043E\u0439-World-\u0410\u0414/Employees/Asena-Y-Koleva/koleva-clock.png",
                "permanentAddress": "Bul, Pl., Pl., Plovdiv, Maritsa, 555",
                "currentAddress": ""
            },
            "contactInfo": {
                "phoneNumberOne": "0888111111",
                "phoneNumberTwo": "0888777555",
                "e_MailAddress1": "A.Koleva@mail.bg",
                "website": "Asena.net"
            },
            "idCardPassport": {
                "documentName": "Driver License",
                "documentNumber": "333222"
            },

           "contractInfo": {
                "jobTitle": "CEO",
                "departmentName": "Managment",
                "contractType": "employment contract",
                "contractNumber": "777999",
                "contractDate": "10.6.2024 \u0433.",
                "agreementsCount": null,
                "lastAnnex": {
                    "id": 5,
                    "jobTitle": "Chief Accountant",
                    "departmentName": "Managment"
                },
                "realJobTitle": "Chief Accountant",
                "realDepartmentName": "Managment"
            },

            "experience": null
        }
    ]
}
*/
