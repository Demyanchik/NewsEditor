namespace NewsEditor.Methods
{
    public static class LangPacks
    {
        public static Dictionary<string, string> Russian = new Dictionary<string, string>();
        public static Dictionary<string, string> English = new Dictionary<string, string>();

        public static Dictionary<string, string> GetLanguagePack(int LanguageId = -1) 
        {
            switch (LanguageId) 
            {
                case 1:
                    return Russian;
                case 2:
                    return English;
                default:
                    return Russian;
            }
        }
        static LangPacks() 
        {
            Russian.Add("language_name_RU", "Русский");
            English.Add("language_name_RU", "Russian");

            Russian.Add("language_name_EN", "Английский");
            English.Add("language_name_EN", "English");

            Russian.Add("project_name", "Редактор новостей");
            English.Add("project_name", "NewsEditor");

            Russian.Add("home_link", "Домашняя страница");
            English.Add("home_link", "Home");

            Russian.Add("news_list", "Новости");
            English.Add("news_list", "News");

            Russian.Add("privacy", "Конфиденциальность");
            English.Add("privacy", "Privacy");

            Russian.Add("lang", "Язык:");
            English.Add("lang", "Language:");

            Russian.Add("login_btn", "Войти в систему");
            English.Add("login_btn", "Sign in");

            Russian.Add("FIO", "Демьянчик Илья Сергеевич");
            English.Add("FIO", "Demyanchik I.S.");

            Russian.Add("publication_time", "Время публикации:");
            English.Add("publication_time", "Publication time:");

            Russian.Add("username", "Имя пользователя");
            English.Add("username", "Username");

            Russian.Add("password", "Пароль");
            English.Add("password", "Password");

            Russian.Add("login_stay", "Оставаться в системе");
            English.Add("login_stay", "Stay logged in");

            Russian.Add("username_error", "Такого пользователя не существует.");
            English.Add("username_error", "This user does not exist.");

            Russian.Add("password_error", "Неверный пароль.");
            English.Add("password_error", "Password incorrect.");

            Russian.Add("logout_btn", "Выйти");
            English.Add("logout_btn", "Log out");

            Russian.Add("read_article", "Читать статью");
            English.Add("read_article", "Read article");

            Russian.Add("edit_article", "Редактировать статью");
            English.Add("edit_article", "Edit article");

            Russian.Add("delete_article", "Удалить статью");
            English.Add("delete_article", "Delete article");

            Russian.Add("create_article", "Создать статью");
            English.Add("create_article", "Create an article");

            Russian.Add("all_news_shown", "Показаны все новости.");
            English.Add("all_news_shown", "All news are shown.");

            Russian.Add("rm_art_header", "Удаление статьи");
            English.Add("rm_art_header", "Deleting an article");

            Russian.Add("rm_confirmation", "Вы действительно хотите удалить статью");
            English.Add("rm_confirmation", "Do you really want to delete this article");

            Russian.Add("cancel", "Отмена");
            English.Add("cancel", "Cancel");

            Russian.Add("rm_art_btn", "Удалить статью");
            English.Add("rm_art_btn", "Delete this article");

            Russian.Add("create_art_page_header", "Создание новости:");
            English.Add("create_art_page_header", "Creating an article:");

            Russian.Add("new_art_header", "Заголовок новости:");
            English.Add("new_art_header", "News headline:");

            Russian.Add("new_art_header_undertext", "Введите заголовок");
            English.Add("new_art_header_undertext", "Enter the headline");

            Russian.Add("new_art_no_picture", "Новость будет создана без изображения.");
            English.Add("new_art_no_picture", "The news will be created without an image.");

            Russian.Add("new_art_with_picture", "Для новости будет установлено следующее изображение:");
            English.Add("new_art_with_picture", "The following image will be set for the news:");

            Russian.Add("rm_image_btn", "Удалить изображение");
            English.Add("rm_image_btn", "Delete image");

            Russian.Add("chose_img", "Выбрать изображение");
            English.Add("chose_img", "Select image");

            Russian.Add("new_art_subheader", "Подзаголовок:");
            English.Add("new_art_subheader", "Subtitle:");

            Russian.Add("new_art_subheader_undertext", "Введите подзаголовок");
            English.Add("new_art_subheader_undertext", "Enter subtitle");

            Russian.Add("new_art_text", "Текст статьи:");
            English.Add("new_art_text", "Text of the article:");

            Russian.Add("new_art_submit", "Создать новость");
            English.Add("new_art_submit", "Create news");

            Russian.Add("edit_art_title", "Редактирование новости: ");
            English.Add("edit_art_title", "Editing article: ");

            Russian.Add("edit_art_page_header", "Редактирование новости:");
            English.Add("edit_art_page_header", "Editing news:");

            Russian.Add("edit_art_had_no_picture", "Изображение отсутствует.");
            English.Add("edit_art_had_no_picture", "Image is missing.");

            Russian.Add("edit_art_had_picture", "Для новости установлено следующее изображение:");
            English.Add("edit_art_had_picture", "The following image is set for the news:");

            Russian.Add("edit_art_no_picture", "Новость будет сохранена без изображения.");
            English.Add("edit_art_no_picture", "The news will be saved without an image.");

            Russian.Add("edit_art_with_picture", "Для новости будет установлено следующее изображение:");
            English.Add("edit_art_with_picture", "The following image will be set for the news:");

            Russian.Add("edit_art_save", "Сохранить");
            English.Add("edit_art_save", "Save");

            Russian.Add("privacy_header", "Политика конфиденциальности");
            English.Add("privacy_header", "Privacy Policy");

            Russian.Add("privacy_text", "Используйте эту страницу, чтобы подробно описать политику конфиденциальности вашего сайта.");
            English.Add("privacy_text", "Use this page to detail your site's privacy policy.");

            Russian.Add("home_title", "Домашняя страница");
            English.Add("home_title", "Home Page");

            Russian.Add("new_art_title", "Создание новости");
            English.Add("new_art_title", "Creating an article");

            Russian.Add("login_page", "Вход");
            English.Add("login_page", "Login");

            Russian.Add("edit_art_set_image", "Для новости будет установлено следующее изображение:");
            English.Add("edit_art_set_image", "The following image will be set for the news:");

            Russian.Add("edit_art_set_no_image", "Новость будет сохранена без изображения.");
            English.Add("edit_art_set_no_image", "The news will be saved without an image.");

            Russian.Add("home_no_items", "Нет новостей для отображения.");
            English.Add("home_no_items", "There are no news items to display.");

            Russian.Add("art_author", "Автор:");
            English.Add("art_author", "Author:");

            //Russian.Add("", "");
            //English.Add("", "");
        }
    }
}
