// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



var SearchInp = document.getElementById("SearchInp");

SearchInp.addEventListener("keyup", function () {

    let xhr = new XMLHttpRequest();

    let url = `https://localhost:44337/Employee/Index?SearchInp=${SearchInp.value}`;

    xhr.open("GET", url, true);

    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            console.log(this.response);
        }
    }

});