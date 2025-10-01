var Image;

$(document).ready(function () {
    Image = $('#LoadedImage');
});

function ImageLoad(file) {
    // убираем изображение
    if (file.files.length === 0)
        return;

    //копируем файл картинки в другой input-file если он не null
    $("#ChangeImage")[0].files = $("#NewsImage")[0].files;

    const img = file.files[0];

    if (img && img.type.startsWith('image/')) { // Проверка, что это изображение
        const reader = new FileReader();

        reader.onload = function (e) {
            Image.attr('src', e.target.result);
            $('#ImageView').css('display', ''); // Делаем изображение видимым

            $('#ImageText').html('Для новости будет установлено следующее изображение:');
        }

        reader.readAsDataURL(img); // Читаем файл как URL данных
    }
}

function removeImage() {
    Image.removeAttr('src');
    $('#ImageView').css('display', 'none');
    $('#ImageText').html('Новость будет сохранена без изображения.');

    $('#ChangeImage').val(null);
}

function submitNewsEditing()
{
    if (!$('#ImageView').is(':hidden'))
        $('#HasImage').prop('checked', true);
    else
        $('#HasImage').prop('checked', false);
}

//const FileInput = document.getElementById('NewsImage');
//FileInput.addEventListener('change', ImageLoad(FileInput));