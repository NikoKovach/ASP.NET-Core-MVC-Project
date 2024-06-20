var prevItemIndex = -1;
var prevItemBgColor = "";
var indexMouseOver = -1;

$('#tbody').on('click', 'tr', onRowClick);

function onRowClick()
{
	if (prevItemIndex > -1) {
		$('#tbody tr').eq(prevItemIndex).css("background-color", prevItemBgColor);
	}

	prevItemIndex = $(this).index();
	prevItemBgColor = $(this).css("background-color");

	$(this).changeCurrentRowBgcolor();

	$('input#entityId').val($(this).children().first().text());
};

$('#tbody').on("mouseover",'tr', function () {

	indexMouseOver = $(this).index();
});

$(document).on("keypress", function (event) {
	$('#tbody tr').eq(indexMouseOver).trigger("click");
});

$.fn.changeCurrentRowBgcolor = function () {
	$(this).css("background-color", "yellow");
};

/********************************************************* */
$("#beach").css("background-color", "green");
/*$("#beach").css("border", "red");*/
$("#beach").css("border-radius","10px");
$(".headerDiv").append("<p class=\"headerPar\">Test</p>");
$(".headerPar").css("color","blue");