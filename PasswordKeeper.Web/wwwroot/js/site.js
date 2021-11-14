// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.


$("#customSwitch1").change(function () {
    if ($(this).is(':checked')) {
        changeClassName('d-none', 'Visible');
    } else {
        changeClassName('Visible', 'd-none');
    }
});

function changeClassName(oldClassName, newClassName){

    var elements = document.getElementsByClassName(oldClassName);

    for (var i = elements.length - 1; i >= 0; --i) {
        elements[i].className = newClassName;
    } 
}


function copythat(sentence){
    navigator.clipboard.writeText(sentence)
}

//$(document).ready(function () {

//    new ClipboardJS('.copy_option');

//});
