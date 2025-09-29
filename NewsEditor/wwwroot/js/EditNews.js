var Image;

$(document).ready(function () {
    Image = $('#LoadedImage');
});

function ImageLoad(file) {
    const img = file.files[0];

    if (img && img.type.startsWith('image/')) { // Проверка, что это изображение
        const reader = new FileReader();

        reader.onload = function (e) {
            Image.attr('src', e.target.result);
            Image.css('display', 'block'); // Делаем изображение видимым
        }

        reader.readAsDataURL(img); // Читаем файл как URL данных
    }
}

//const FileInput = document.getElementById('NewsImage');
//FileInput.addEventListener('change', ImageLoad(FileInput));