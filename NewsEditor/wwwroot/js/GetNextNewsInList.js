function onNewsScroll(element)
{
    if (element.scrollTop + element.clientHeight >= element.scrollHeight) {
        $('#GetNextNews').click();
    }
}