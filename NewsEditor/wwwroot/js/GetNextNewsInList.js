var lastCall = new Date().getTime();
function onNewsScroll(element)
{
    // если докрутили скролл до дна
    console.log((element.scrollTop + element.clientHeight + 1) + ' ' + element.scrollHeight);
    //таймаут 0.2 секунды для скролла
    var timeOut = 200;
    var callTime = new Date().getTime();
    if (element.scrollTop + element.clientHeight + 1 >= element.scrollHeight
        && callTime - lastCall > timeOut) {
        // отправка ajax-формы подгрузки новых записей

        // было - с использованием ajax-формы
        lastCall = callTime;
        $('#GetNextNews').click();

        // думал так, но передумал
        //$.ajax({
        //    url: '/Home/GetNextNews',
        //    method: 'GET',
        //    data: {
        //        lastShownArticleId: $('#last_shown_article_id').val() // Передаем параметр id
        //    },
        //    dataType: 'json',
        //    success: function (response) {

        //    },
        //    error: function (xhr, status, error) {
        //        console.error('Ошибка: ', error);
        //    }
        //});
    }
}
function GetNextNewsResponse()
{
    //console.log($('#NextNews').html());
    var lastId;
    var lastArticle = $('#NextNews').children().last();
    if (lastArticle != undefined) {
        // получаем input с id
        lastId = lastArticle.children().first().val();
    }
    if (lastId != undefined && lastId != '') {
        // меняем значение input-а с id для нового запроса
        $('#last_shown_article_id').val(lastId); 
    }

    console.log('Значение id из последней выборки: ' + lastId);

    // удаляем предыдущие уведомления
    $('#NewsContainer').find('div[type="notification"]').remove();
    // дописываем новые записи
    $('#NewsContainer').append($('#NextNews').html());
}