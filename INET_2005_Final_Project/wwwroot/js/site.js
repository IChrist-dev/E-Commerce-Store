// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Function to count the number of product IDs in the cookie
function getCartProductCount() {
    var cookieContent = document.cookie
        .split('; ')
        .find(row => row.startsWith('ProductCart'))
        .split('=')[1];

    var cartItems = cookieContent.split('%2C');

    if (cartItems[0] == "") return 0;
    else return cartItems.length;
}

// Function to update the shopping cart icon number
function updateCartProductCount() {
    var cartCountElement = document.getElementById('cartProductCount');
    
    var productCount = getCartProductCount();
    cartCountElement.textContent = productCount;
}

// Call function chain on page-load
window.onload = function () {
    updateCartProductCount();
}