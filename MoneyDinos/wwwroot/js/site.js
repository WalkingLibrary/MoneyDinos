// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function toggleBreakdown(button) {
    var breakdown = button.nextElementSibling;
    var downArrow = button.querySelector('.down-arrow');
    var upArrow = button.querySelector('.up-arrow');

    if (breakdown.style.display === "none") {
        breakdown.style.display = "block";
        downArrow.style.display = "none";
        upArrow.style.display = "inline-block";
    } else {
        breakdown.style.display = "none";
        downArrow.style.display = "inline-block";
        upArrow.style.display = "none";
    }
}
