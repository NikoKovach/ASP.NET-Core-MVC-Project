

/*$("#btnSave").on("click", insertStudent); */

/*$("#btnSave").on("click", postMethod);*/
function LoadData() {
	$("#tblStudent tbody tr").remove();

	$.ajax({
		method: "GET",
		url: "/Students/GetStudent",
		dataType: 'json',
		/*type: 'POST',*/
		/*url: '@Url.Action("getStudent")',*/
		/*data: { id: '' },*/

		success: function (data) {
			$.each(data, function (i, item) {
				var rows = "<tr>"
					+ "<td class='prtoducttd'>" + item.studentID + "</td>"
					+ "<td class='prtoducttd'>" + item.studentName + "</td>"
					+ "<td class='prtoducttd'>" + item.studentAddress + "</td>"
					+ "</tr>";

				$('#tblStudent tbody').append(rows);
			});
		},

		error: function (ex) {
			var r = JSON.parse(response.responseText);
			/*var r = jQuery.parseJSON(response.responseText);*/
			alert("Message: " + r.Message);
			alert("StackTrace: " + r.StackTrace);
			alert("ExceptionType: " + r.ExceptionType);
		}
	});

	return false;
}

function loadDoc() {
	var xhttp = new XMLHttpRequest();
	var url = "/Students/FuckFuck";

	xhttp.onreadystatechange = function () {
		if (this.readyState == 4 && this.status == 200) {
			document.getElementById("jsonAjax").append(this.responseText);
		}
	};

	var student = {
		"studentID": 100,
		"studentName": "fff",
		"studentAddress": "ddddd"
	};

	var number = 5;

	xhttp.open("POST", url, true);
	xhttp.setRequestHeader('Content-type', 'application/json');
	xhttp.send(JSON.stringify(number));
}

function saveStudent() {
	var student = {
		studentID: 1,
		studentName: "fff",
		studentModel: "ddddd"
	};
	var responceAjax = "Empty";

	$.ajax({
		type: "POST",
		url: "/Students/FuckingStudent",
		dataType: "json",
		contentType: "application/x-www-form-urlencoded; charset=UTF-8",
		data: {
			studentID: student.studentID,
			studentName: student.studentName,
			studentModel: student.studentModel
		},
		success: function (response) {
			let data = JSON.stringify(response);
			$("#paragraphAjax").empty().append(data);

			/*const parsaData = [JSON.parse(response)];*/

			document.body.innerHTML = JSON.parse(response);
		},
		error: function (response, status, error) {
			alert(status + " :: " + response.responseText);
		}
	});

	/*$("p").append("<b>" + responceAjax  +"</b>");*/
	return false;
}

function insertStudent() {
	var student = {
		studentID: 5,
		studentName: "Planet",
		studentModel: "Space"
	};

	$.ajax({
		type: "POST",
		url: "/Students/FuckingStudent",
		dataType: "json",
		contentType: "application/x-www-form-urlencoded; charset=UTF-8",
		data: {
			studentID: student.studentID,
			studentName: student.studentName,
			studentModel: student.studentModel
		},
		success: function (response) {
			let newArr = jQuery.map(response, function (val, index) {
				return {
					number: val,
				};
			});

			printResponse(newArr);
		},
		error: function (response, status, error) {
			alert(status + " :: " + response.responseText);
		}
	});
}

function postMethod() {
	var url = "/Students/FuckingStudent";

	var student = {
		"studentID": 100,
		"studentName": "fff",
		"studentModel": "ddddd"
	};

	$.post(
		url,
		student,
		function (data, status) {
			let newArr = jQuery.map(data, function (val, index) {
				return {
					number: val,
				};
			});

			printResponse(newArr);

			document.getElementById("studentId").innerHTML
				= newArr[0]["number"]["studentId"];

			document.getElementById("studentName").innerHTML
				= newArr[0]["number"]["studentName"];

			document.getElementById("studentModel").innerHTML
				= newArr[0]["number"]["studentModel"];
		}
	);
}

function printResponse(data) {
	$.each(data, function (i, item) {
		var rows = "<tr>"
			+ "<td class='prtoducttd'>" + item["number"]["studentId"] + "</td>"
			+ "<td class='prtoducttd'>" + item["number"]["studentName"] + "</td>"
			+ "<td class='prtoducttd'>" + item["number"]["studentModel"] + "</td>"
			+ "</tr>";

		$('#tblStudent tbody').append(rows);
	});
}