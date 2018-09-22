$(document).ready(function(){
    $("#AirClick").click(function(){
		$("#Hotel").css("display","none");
		$("#Activity").css("display","none");
		$("#Car").css("display","none");
		$("#Air").css("display","block");
	});
	$("#ActivityClick").click(function(){
		$("#Hotel").css("display","none");
		$("#Activity").css("display","block");
		$("#Car").css("display","none");
		$("#Air").css("display","none");
	});
	$("#CarClick").click(function(){
		$("#Hotel").css("display","none");
		$("#Activity").css("display","none");
		$("#Car").css("display","block");
		$("#Air").css("display","none");
	});
	$("#HotelClick").click(function(){
		$("#Hotel").css("display","block");
		$("#Activity").css("display","none");
		$("#Car").css("display","none");
		$("#Air").css("display","none");
	});
});

function bookProduct(Type,id) {
	var url = "http://localhost:49256/api/"+Type;
	var Product ={};
	var xhr = new XMLHttpRequest();
	Product.BookOrSave="book";
	Product.Id=id;
	var json = JSON.stringify(Product);
	xhr.open("PUT",url,true);
	xhr.setRequestHeader('Content-type','application/json; charset=utf-8');
	xhr.send(json);
}
function saveProduct(Type,id) {
	var url = "http://localhost:49256/api/"+Type;
	var Product ={};
	var xhr = new XMLHttpRequest();
	Product.BookOrSave="save";
	Product.Id=id;
	var json = JSON.stringify(Product);
	xhr.open("PUT",url,true);
	xhr.setRequestHeader('Content-type','application/json; charset=utf-8');
	xhr.send(json);
}
function getAllHotelProducts()
{
	var xhttp = new XMLHttpRequest();
	var result = document.getElementById("Hotel");
	result.innerHTML="";
	xhttp.onreadystatechange = function() {
		if (this.readyState == 4 && this.status == 200) {
			productresult = JSON.parse(this.responseText);
			productresult.forEach(currentproduct => {
				var content;
				content=
				"<div class='card'>"+
				"<div class='row card-body'>"+
				"<div class='col small'><h3 class='card-title'>"+ currentproduct.HotelName + "</h3></div>"+
				"<div class='col small' >"+
				"<h3> Address: </h3>" + currentproduct.HotelAddress +
				"</div>"+
				"<div class='col small' >"+"<h3> Available From:</h3>" + currentproduct.AvailableFrom +"</div>"+
				"<div class='col small' >"+"<h3> Available Till:</h3>" + currentproduct.AvailableTill +"</div>"+
				"<div class='col small' >"+"<h3> Contact Number:</h3>" + currentproduct.HotelContactNumber +"</div>"+
				"<div class='col' >"+"<h3> Description:</h3>" + currentproduct.HotelDescription +"</div>"+
				"<div class='col small' >"+"<h3> Amenities:</h3>" + currentproduct.HotelAmenities +"</div>"+
				"<div class='col smaller' >"+"<h3> Rating:</h3>" + currentproduct.HotelRating +"</div>"+
				"<div class='col small' >"+"<h3> Photos:</h3>" + currentproduct.HotelImageURL +"</div>";
				"</div>"+
				"</div>"
				result.innerHTML += content; 
				"</div>"+
				"</div>"
			});
		}
	};
	xhttp.open("GET", "http://localhost:51223/api/hotel", true);
	xhttp.send();
}