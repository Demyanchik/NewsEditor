using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NewsEditor.Models.DB;
using static System.Net.Mime.MediaTypeNames;

using NewsEditor.Controllers;
using System.Runtime.CompilerServices;

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
    public void CreateArticle(string header, IFormFile image, int? userId, string? subHeader, string? text)
    {
        var new_article = new News();

        if (image != null) 
        {
            new_article.Image = HomeController.GetImageBytes(image);
            new_article.ImageFormat = HomeController.GetImageExtension(image);
        }
        new_article.Header = header;
        new_article.SubHeader = subHeader;
        new_article.Text = text;
        new_article.UserId = userId;
        new_article.TimeCreated = DateTime.Now.ToString();

        News.Add(new_article);

        SaveChanges();
    }

    public void UpdateArticle(int id, string header, string hasImage, IFormFile changedImage, string? subHeader, string? text) 
    {
        var article = News.Find(id);
        var hasImg = hasImage != null ? true : false;

        if (hasImg && changedImage != null) 
        {
            article.Image = HomeController.GetImageBytes(changedImage);
            article.ImageFormat = HomeController.GetImageExtension(changedImage);
        }
        if (article.Image != null && !hasImg) 
        {
            article.Image = null;
            article.ImageFormat = null;
        }

        article.Header = header;
        article.SubHeader = subHeader;
        article.Text = text;
        
        SaveChanges();
    }

    public void DeleteArticle(int id) 
    {
        News.Find(id).Deleted = 1;
        SaveChanges();
    }
}