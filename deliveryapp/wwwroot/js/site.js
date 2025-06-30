let quantity = 1;
let basePrice = 0;
function setBasePrice(price) {
    basePrice = price;
}

function updateDisplay() {
    document.getElementById('quantity').textContent = quantity;
    document.getElementById('totalPrice').textContent = `S/${(basePrice * quantity).toFixed(1)}`;
    document.getElementById('decreaseBtn').disabled = quantity <= 1;
}

function increaseQuantity() {
    if (quantity < 10) {
        quantity++;
        updateDisplay();
        document.getElementById('quantityInput').value = quantity;
    }
}

function decreaseQuantity() {
    if (quantity > 1) {
        quantity--;
        updateDisplay();
        document.getElementById('quantityInput').value = quantity;
    }
}

window.setBasePrice = setBasePrice;