document.addEventListener("DOMContentLoaded", function () {
    var currentUrl = window.location.pathname;
    var navLinks = document.querySelectorAll(".nav-link");

    navLinks.forEach(function (link) {
        if (link.getAttribute("href") === currentUrl) {
            link.classList.add("active");
        }
    });
});

function onShowToast(message, title = "Thông báo", delay = 3000) {
    showToast(message, title, delay);
}

function showToast(message, title, delay) {
    // Tạo một toast element
    var toastElement = document.createElement('div');
    toastElement.classList.add('toast');
    toastElement.classList.add('fade');
    toastElement.setAttribute('role', 'alert');
    toastElement.setAttribute('aria-live', 'assertive');
    toastElement.setAttribute('aria-atomic', 'true');

    // Tạo nội dung cho toast
    var toastMarkup = `
        <div class="toast-header">
          <strong class="me-auto">${title} </strong>
          <button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>
        </div>
        <div class="toast-body">${message}</div>
      `;
    toastElement.innerHTML = toastMarkup;

    var toastContainer = document.querySelector('.toast-container');
    toastContainer.appendChild(toastElement);

    var toast = new bootstrap.Toast(toastElement);

    toast.show();

    setTimeout(function () {
        toastElement.remove();
    }, delay); 
}