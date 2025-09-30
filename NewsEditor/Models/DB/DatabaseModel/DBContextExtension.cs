using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NewsEditor.Models.DB;

namespace NewsEditor.Models.DB;

public partial class MyDBContext : DbContext
{
    /// <summary>
    /// Создать новостную статью
    /// </summary>
    /// <param name="header"></param>
    /// <param name="image"></param>
    /// <param name="subHeader"></param>
    /// <param name="text"></param>
    public void CreateArticle(string header, byte[]? image, string? imageFormat, string? subHeader, string? text)
    {
        var new_article = new News();
        new_article.Header = header;
        new_article.Image = image;
        new_article.ImageFormat = imageFormat;
        new_article.SubHeader = subHeader;
        new_article.Text = text;
        new_article.UserId = null;
        new_article.TimeCreated = DateTime.Now.ToString();

        News.Add(new_article);

        SaveChanges();
    }
}