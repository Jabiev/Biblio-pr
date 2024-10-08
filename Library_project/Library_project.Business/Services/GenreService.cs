﻿using Library_project.Business.Exceptions;
using Library_project.Business.Interfaces;
using Library_project.Core.Entities;
using static Library_project.DataAccess.Repositories.DataContext;

namespace Library_project.Business.Services;

public class GenreService : IGenreService
{
    public void Create(string? name)
    {
        if (string.IsNullOrEmpty(name))
            throw new NotFoundException("The value is null or empty");
        if (Genres?.Find(g => g.Name == name.ToUpper()) is not null)
            throw new AlreadyExistException("The Value already exists");
        Genre genre = new(name.ToUpper());
        Genres?.Add(genre);
    }

    public void Delete(int id, BookService bookService)
    {
        Genre? genre = Genres?.Find(g => g.Id == id);
        if (genre is null)
            throw new NotFoundException("The value doesn't exist");
        if (bookService.GetByGenre(id).Count > 0)
            throw new NotRemovedbyContainSomeItemsException("Not removed the value by depending on some other values");
        Genres?.Remove(genre);
    }

    public List<Genre> GetAll()
    {
        return Genres;
    }

    public Genre GetById(int id)
    {
        Genre? genre = Genres?.Find(g => g.Id == id);
        if (genre is null)
            throw new NotFoundException("The value doesn't exist");
        return genre;
    }

    public List<Genre> SearchByName(string? search)
    {
        if (string.IsNullOrEmpty(search))
            throw new NullorEmptyException("The value is null or empty");
        return Genres.FindAll(g => g.Name.Contains(search.ToUpper()));
    }

    public void Update(int id, string? name)
    {
        Genre? genre = Genres?.Find(g => g.Id == id);
        if (genre is null)
            throw new NotFoundException("The value doesn't exist");
        if (string.IsNullOrEmpty(name))
            throw new NullorEmptyException("The value is null or empty");
        if (Genres?.Find(g => g.Name == name) is not null)
            throw new AlreadyExistException($"The object which involving the {name} is already exist");
        genre.Name = name;
    }
}