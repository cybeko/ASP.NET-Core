document.addEventListener("DOMContentLoaded", function () {
    const posterInput = document.getElementById("posterInput");
    const posterPreview = document.getElementById("posterPreview");

    if (posterInput && posterPreview) {
        posterInput.addEventListener("change", function (event) {
            const file = event.target.files[0];
            if (file) {
                const reader = new FileReader();
                reader.onload = function (e) {
                    posterPreview.src = e.target.result; 
                };
                reader.readAsDataURL(file);
            }
        });
    }
});
